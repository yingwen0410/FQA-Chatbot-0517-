// 負責處理使用者提出的查詢請求。

using FqaChatbot_API.Models;
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
        private readonly List<FqaItem> _fqaData;
        private readonly string _fqaDataPath = "fqa-data.json"; 

        
        private static readonly Lazy<List<FqaItem>> _lazyFqaData = new Lazy<List<FqaItem>>(() =>
        {
            var path = Path.Combine(AppContext.BaseDirectory, "fqa-data.json");
            try
            {
                var jsonString = File.ReadAllText(path); // 讀取 JSON 檔案
                return JsonSerializer.Deserialize<List<FqaItem>>(jsonString) ?? new List<FqaItem>();
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"警告：找不到 {path} 檔案。");
                return new List<FqaItem>();
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"警告：解析 {path} 檔案時發生錯誤：{ex.Message}");
                return new List<FqaItem>();
            }
        });

        // 公開靜態屬性以訪問 Lazy 加載的 FQA 資料確保 fqaData 只在第一次被訪問時才載入
        private static List<FqaItem> FqaData => _lazyFqaData.Value;

        public FqaController()
        {
            // 在建構子中不再需要載入資料，因為 Lazy<T> 會處理
        }

        [HttpPost("query")]
        public IActionResult Query([FromBody] QueryRequest request)
        {
            if (string.IsNullOrEmpty(request?.UserInput))
            {
                return BadRequest("請輸入您的問題。");
            }

            string userInput = request.UserInput.Trim(); // 去除使用者輸入的首尾空白

            // 更有效率的關鍵字比對：搜尋問題和答案中是否包含使用者輸入的任何單詞
            var matchingFqa = FqaData.FirstOrDefault(fqa =>
                userInput.Split(new char[] { ' ', ',', '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries)
                         .Any(keyword => fqa.Question.Contains(keyword, StringComparison.OrdinalIgnoreCase) ||
                                         fqa.Answer.Contains(keyword, StringComparison.OrdinalIgnoreCase)));

            if (matchingFqa != null)
            {
                return Ok(new QueryResponse { Answer = matchingFqa.Answer });
            }
            else
            {
                return Ok(new QueryResponse { Answer = "找不到答案，請聯絡客服" });
            }
        }
    }
}