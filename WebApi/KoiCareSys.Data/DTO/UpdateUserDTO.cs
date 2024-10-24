using System.ComponentModel.DataAnnotations;

namespace KoiCareSys.Data.DTO
{
    public class UpdateUserDTO
    {
        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
    }
}
