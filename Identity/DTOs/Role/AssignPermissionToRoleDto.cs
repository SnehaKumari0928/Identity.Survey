namespace Identity.DTOs.Role
{
    public class AssignPermissionToRoleDto
    {
        public int RoleId {  get; set; }
       public List<int> PermissionId {  get; set; }
    }
}
