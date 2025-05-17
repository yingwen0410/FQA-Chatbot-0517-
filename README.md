# FAQ Chatbot 小幫手
FAQ Chatbot 小幫手是一套可快速部署於網站或內部網路的常見問題自動回覆系統。
本系統以 ASP.NET Core Web API 為後端，前端採用 HTML、CSS（Bootstrap）、JavaScript/jQuery，
支援自訂 FAQ JSON 檔案，具備關鍵字與同義詞比對功能，協助自動回覆常見問題，減輕人工客服負擔。


## 產品功能

| 功能模組         | 說明                          |
|------------------|-------------------------------|
| 前端互動介面     | 簡潔對話框，支援輸入問題、顯示回應        |
| FAQ 問答比對     | 以 C# 關鍵字搜尋最適合的 FAQ 答案            |
| FAQ 資料管理     | 以 JSON 檔案儲存 FAQ 問題與答案，方便直接編輯與維護                 |
| 後端 API 服務     | 提供 RESTful API 查詢介面（ASP.NET Core），方便前後端分離                 |
| Swagger 文件     | 內建 Swagger API 文件，方便測試與前後端協作                 |
| RWD 響應式設計     | 採用 Bootstrap，支援不同裝置瀏覽                 |


## 技術架構
| 技術項目	         | 使用技術與工具                 |
|------------------|-------------------------------|
| 前端技術          | 	HTML、CSS（Bootstrap）、JavaScript、jQuery |
| 聊天邏輯處理	      | C# 關鍵字與同義詞比對演算法 |
| 資料管理	      | JSON 檔案 |
| 後端 API	      | ASP.NET Core Web API |
| 開發工具	      | Visual Studio Code、.NET 7/8/9、Git |
| 文件與測試	      | Swagger |


## 適用場景
．公司內部 IT / HR 常見問題查詢

．官網訪客 FAQ 自動回覆

．教學平台常見疑問輔助工具

．客服系統 FAQ 前端自動篩選




## 產品優勢
．快速導入：僅需編輯 FAQ JSON 檔案即可上線

．彈性維護：FAQ 內容可直接修改 JSON 檔案

．簡潔 UI：對話介面直覺，支援時間戳與自動捲動

．易於擴充：API 與前端皆可依需求調整

．開發友善：內建 Swagger API 文件，方便測試與協作

## 快速啟動
1.安裝 .NET 7/8/9 SDK

2.下載專案並進入資料夾

3.執行：

bash
dotnet run

4.瀏覽 http://localhost:xxxx/swagger 測試 API
或直接開啟 http://localhost:xxxx/ 使用前端聊天介面

本系統適合用於 FAQ 自動化、知識管理、網站常見問題回覆等場景。
