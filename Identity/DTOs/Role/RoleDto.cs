namespace Identity.DTOs.Role
{
    public class RoleDto
    {
        public int RoleId {  get; set; }
        public string Name {  get; set; }
        public List<string> Permissions { get; set; }
    }
}
