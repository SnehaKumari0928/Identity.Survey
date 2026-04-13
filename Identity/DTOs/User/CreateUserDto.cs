namespace Identity.DTOs.User
{
    public class CreateUserDto
    {
        public string Email {  get; set; }
        public string Password { get; set; }

        public string Name {  get; set; }
        public string Phone { get; set; }
    }
}
