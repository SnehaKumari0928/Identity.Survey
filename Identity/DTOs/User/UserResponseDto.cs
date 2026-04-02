using Identity.Entities;

namespace Identity.DTOs.User
{
    public class UserResponseDto
    {
        public int UserId {  get; set; }
        public string Email {  get; set; }
        public string Phone {  get; set; }
        public string Name {  get; set; }
        public bool IsActive {  get; set; }

        public List<string> Roles { get; set; }
        public List<string> Permissions { get; set; }
    }
}
