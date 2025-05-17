using System.Collections.Generic;

namespace FqaChatbot_API.Data
{
    public static class SynonymData
    {
        public static readonly Dictionary<string, List<string>> SynonymDict = new()
        {
            // 英文同義詞
            ["ai"] = new List<string> { "artificial intelligence", "人工智能", "ai系統", "ai技術" },
            ["nlp"] = new List<string> { "natural language processing", "自然語言處理", "語義分析", "文本處理" },
            ["ml"] = new List<string> { "machine learning", "機器學習", "機器學習算法", "ml算法" },
            ["dl"] = new List<string> { "deep learning", "深度學習", "深度神經網絡", "深度網路" },
            ["cnn"] = new List<string> { "convolutional neural network", "卷積神經網路", "卷積網路" },
            ["rnn"] = new List<string> { "recurrent neural network", "遞歸神經網路", "循環神經網路" },
            ["gan"] = new List<string> { "generative adversarial network", "生成對抗網路", "對抗網路" },
            ["api"] = new List<string> { "application programming interface", "應用程式介面", "api介面" },
            ["restful"] = new List<string> { "rest", "表徵性狀態轉移" },
            ["docker"] = new List<string> { "containerization", "容器化技術" },
            ["attention"] = new List<string> { "attention mechanism", "注意力機制" },
            ["nosql"] = new List<string> { "not only sql", "非關聯式資料庫" },
            ["transfer learning"] = new List<string> { "遷移學習" },
            ["api gateway"] = new List<string> { "api網關" },
            ["edge computing"] = new List<string> { "邊緣計算" },
            ["data engineering"] = new List<string> { "資料工程" },
            ["ci"] = new List<string> { "continuous integration", "持續整合" },
            ["cd"] = new List<string> { "continuous deployment", "持續部署" },
            ["oop"] = new List<string> { "object-oriented programming", "物件導向程式設計" },

            // 中文同義詞
            ["什麼是"] = new List<string> { "何謂", "請問" },
            ["有哪些"] = new List<string> { "包含哪些", "包括什麼" },
            ["如何"] = new List<string> { "怎樣", "怎麼做" },
            ["學習"] = new List<string> { "學會", "研習" },
            ["應用"] = new List<string> { "用途", "使用" },
            ["原理"] = new List<string> { "機制", "運作方式" },
            ["作用"] = new List<string> { "功能", "用途" },
            ["優勢"] = new List<string> { "好處", "益處" },
            ["重要性"] = new List<string> { "關鍵性", "必要性" },
            ["基本概念"] = new List<string> { "核心概念", "主要概念" },
            ["主要"] = new List<string> { "主要地", "著重於" },
            ["關注"] = new List<string> { "著重", "著眼於" },
            ["領域"] = new List<string> { "範疇", "方面" },
            ["技術"] = new List<string> { "技能", "方法" },
            ["流程"] = new List<string> { "步驟", "過程" },
            ["常見"] = new List<string> { "一般", "普遍" },
            ["模型"] = new List<string> { "演算法", "網路" },
            ["資料庫"] = new List<string> { "資料儲存", "數據庫" },
            ["系統"] = new List<string> { "平台", "機制" },
            ["開發"] = new List<string> { "研發", "創建" },
            ["部署"] = new List<string> { "佈署", "上線" },
            ["維護"] = new List<string> { "保養", "維持" },
            ["評估指標"] = new List<string> { "衡量標準", "評測方法" },
            ["優勢"] = new List<string> { "好處", "益處" },
            ["作用"] = new List<string> { "功能", "用途" },
            ["區別"] = new List<string> { "不同之處", "差異" },
            ["角色"] = new List<string> { "職責", "作用" },
            ["潛力"] = new List<string> { "可能性", "發展空間" },
            ["基本概念"] = new List<string> { "核心概念", "主要概念" },
            ["主要關注"] = new List<string> { "主要著重", "主要考量" },
            ["選擇"] = new List<string> { "挑選", "選取" },
            ["框架"] = new List<string> { "架構", "平台" },
            ["認為"] = new List<string> { "覺得", "看法", "想法" }
        };
    }
}