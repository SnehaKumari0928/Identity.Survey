namespace Identity.Entities
{
    public class Role
    {
        public int RoleId {  get; set; }
        public string Name {  get; set; }
        public string Description { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<RolePermission> RolePermissions { get; set; }

    }
}
