namespace Identity.Entities
{
    public class User
    {
        public int UserId {  get; set; }
        public string Email {  get; set; }
        public string HashedPassword {  get; set; }
        public bool IsActive {  get; set; }
        public bool IsDeleted {  get; set; }

        public UserProfile Profile { get; set; }

        public List<UserRole> UserRoles { get; set; }
        public List<UserPermission> UserPermissions { get; set; }

        public List<RefreshToken> RefreshTokens { get; set; }

    }
}
