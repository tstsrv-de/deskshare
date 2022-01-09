using System;

namespace DeskShare.Helper
{
    public class jwtToken
    {
        public DateTime expiration { get; set; }
        public string user { get; set; }
        public string token { get; set; }
    }
}