using System.ComponentModel.DataAnnotations;

namespace CrudAPI.Domain
{
    public class Member
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Point { get; set; } = 0;
    }
}