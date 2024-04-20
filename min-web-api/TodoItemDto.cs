namespace min_web_api
{
    public class TodoItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCompleted { get; set; }

        public TodoItemDto() {}

        public TodoItemDto(Todo todoItem)
        {
            Id = todoItem.Id;
            Name = todoItem.Name;
            IsCompleted = todoItem.IsCompleted;
        }
    }
}
