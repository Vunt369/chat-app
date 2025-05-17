namespace ChatApp.Core.Dtos
{
    public class UserDTO
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
    }

    public class UserVM : UserDTO
    {
        public string Avartar { get; set; }
    }
}