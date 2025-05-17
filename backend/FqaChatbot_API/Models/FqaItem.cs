// FAQ 的問題和答案
namespace FqaChatbot_API.Models
{
    public class FqaItem
    {
        public required string Question { get; set; }
        public  required string Answer { get; set; }
        public required List<string> Keywords { get; set; }
    }
}