namespace ProiectSoftbinator.Models
{
    public class UserLoginResponse
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}
