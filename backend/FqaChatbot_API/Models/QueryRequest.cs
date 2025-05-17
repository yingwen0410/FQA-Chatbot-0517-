// 接收前端發送的使用者查詢

namespace FqaChatbot_API.Models
{
    public class QueryRequest
    {
        public required string UserInput { get; set; }
    }
}