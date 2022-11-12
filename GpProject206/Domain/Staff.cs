namespace GpProject206.Domain
{
    public class Staff : AMongoEntity
    { 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}