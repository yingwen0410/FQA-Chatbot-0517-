using FqaChatbot_API.Models;
using FqaChatbot_API.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace FqaChatbot_API.Controllers
{
    [ApiController]
    [Route("api/fqa")]
    public class FqaController : ControllerBase
    {
        // 同義詞字典（使用者輸入 → 標準關鍵詞列表）
        private static readonly Dictionary<string, List<string>> SynonymDict = new()
        {
            ["何謂"] = new List<string> { "什麼是", "請問" },
            ["ai"] = new List<string> { "人工智慧", "人工智能" },
            ["ml"] = new List<string> { "機器學習" },
            ["dl"] = new List<string> { "深度學習" },
            ["nlp"] = new List<string> { "自然語言處理" }
        };

        // 停用詞列表
        private static readonly HashSet<string> StopWords = new()
        {
            "嗎", "呢", "的", "了", "啊"
        };

        // 延遲載入 FAQ 資料
        private static readonly Lazy<List<FqaItem>> _lazyFqaData = new(() =>
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "fqa-data.json");
            try
            {
                var jsonString = System.IO.File.ReadAllText(path);
                var data = JsonSerializer.Deserialize<List<FqaItem>>(jsonString) ?? new List<FqaItem>();
                Console.WriteLine($"已成功載入 {data.Count} 筆 FAQ 資料");
                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"錯誤載入 FAQ 資料: {ex.Message}");
                return new List<FqaItem>();
            }
        });

        private static List<FqaItem> FqaData => _lazyFqaData.Value;

        // 預先提取所有關鍵詞並排序（長詞優先）
        private static readonly List<string> AllKeywords = FqaData
            .SelectMany(f => f.Keywords ?? new List<string>())
            .Distinct()
            .OrderByDescending(k => k.Length)
            .ToList();

        public FqaController()
        {
            Console.WriteLine($"可用關鍵詞數量: {AllKeywords.Count}");
        }

        [HttpPost("query")]
        public IActionResult Query([FromBody] QueryRequest request)
        {
            if (string.IsNullOrEmpty(request?.UserInput))
                return BadRequest("請輸入您的問題。");

            if (request.UserInput.Length > 100)
                return BadRequest("問題過長，請限制在100字以內。");

            var userInput = request.UserInput.Trim();
            Console.WriteLine($"原始輸入: {userInput}");

            // 強化分詞邏輯
            var keywords = TokenizeWithKeywords(userInput);
            Console.WriteLine($"初步分詞: {string.Join(", ", keywords)}");

            // 擴展同義詞
            var expandedKeywords = ExpandKeywords(keywords);
            Console.WriteLine($"擴展關鍵詞: {string.Join(", ", expandedKeywords)}");

            // 計算匹配分數
            var scoredFaqs = FqaData.Select(fqa =>
            {
                int score = 0;

                // 完全匹配直接最高分
                if (fqa.Question.Equals(userInput, StringComparison.OrdinalIgnoreCase))
                    return new { Fqa = fqa, Score = int.MaxValue };

                // 關鍵字匹配邏輯
                foreach (var keyword in expandedKeywords)
                {
                    if (fqa.Question.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        score += 10;
                    if (fqa.Keywords?.Contains(keyword) == true)
                        score += 8;
                    if (fqa.Answer.Contains(keyword, StringComparison.OrdinalIgnoreCase))
                        score += 3;
                }

                return new { Fqa = fqa, Score = score };
            })
            .Where(x => x.Score > 0)
            .OrderByDescending(x => x.Score)
            .ToList();


            // 輸出除錯資訊
            Console.WriteLine("候選答案分數:");
            foreach (var item in scoredFaqs.Take(5))
            {
                Console.WriteLine($" - {item.Fqa.Question} : {item.Score}");
            }

            // 動態門檻（至少 5 分）
            var bestMatch = scoredFaqs.FirstOrDefault(x => x.Score >= 30);

            return bestMatch != null 
                ? Ok(new QueryResponse { Answer = bestMatch.Fqa.Answer }) 
                : Ok(new QueryResponse { Answer = "不好意思，這部分我不清楚" });
        }

        // 強化分詞方法（優先匹配關鍵詞詞組）
        private List<string> TokenizeWithKeywords(string input)
        {
            var text = input.ToLower();
            var tokens = new List<string>();

            // 優先匹配關鍵詞詞組
            foreach (var keyword in AllKeywords)
            {
                if (text.Contains(keyword))
                {
                    tokens.Add(keyword);
                    text = text.Replace(keyword, " "); // 用空格取代已匹配詞組
                }
            }

            // 處理剩餘文字（拆單字並過濾）
            var remainingChars = text
                .Where(c => !char.IsWhiteSpace(c) && !StopWords.Contains(c.ToString()))
                .Select(c => c.ToString());

            tokens.AddRange(remainingChars);
            return tokens.Distinct().ToList();
        }

        // 擴展同義詞
        private List<string> ExpandKeywords(IEnumerable<string> keywords)
        {
            var expanded = new List<string>();
            foreach (var keyword in keywords)
            {
                expanded.Add(keyword);
                if (SynonymDict.TryGetValue(keyword, out var synonyms))
                {
                    expanded.AddRange(synonyms);
                }
            }
            return expanded.Distinct().ToList();
        }
    }
}
