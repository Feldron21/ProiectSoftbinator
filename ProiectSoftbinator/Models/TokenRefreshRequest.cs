﻿namespace ProiectSoftbinator.Models
{
    public class TokenRefreshRequest
    {
        public string ExpiredToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
