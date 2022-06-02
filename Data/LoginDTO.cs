using System.ComponentModel.DataAnnotations;

namespace Data
{
    public class LoginDTO
    {
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}

