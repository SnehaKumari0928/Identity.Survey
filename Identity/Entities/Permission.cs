namespace Identity.Entities
{
    public class Permission
    {
        public int PermissionId {  get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<RolePermission> RolePermissions { get; set; }
        public List<UserPermission> UserPermissions { get; set; }
    }
}
