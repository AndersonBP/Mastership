using System;
namespace Mastership.Domain.DTO {
    public class EmailSettingsDTO {
        public string Server { get; set; }
        public int Port { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
