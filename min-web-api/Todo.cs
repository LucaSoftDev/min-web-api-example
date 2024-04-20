using System.ComponentModel.DataAnnotations;

namespace min_web_api
{
    public class Todo
    {
        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public bool IsCompleted { get; set; }
        [MaxLength(100)]
        public string? Secret { get; set; }
    }
}
