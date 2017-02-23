namespace Project.Common.InternalContracts.Authentication
{
    public class UserIdentityModel
    {
        public int Id { get; set; }
        public string Login { set; get; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Lang { get; set; }
        public string AuthenticationType { get; set; }
    }
}