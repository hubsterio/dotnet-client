using System;

namespace Hubster.Auth
{
    public class Token
    {
        public TokenKind Kind { get; set; }
        public string AccessToken { get; set; }        
        public string TokenType { get; set; }
        public DateTimeOffset Expires { get; set; }

        public bool HasExpired()
        {
            // shorten the expire date by 1 minute
            return DateTimeOffset.UtcNow > Expires.AddMinutes(-1);
        }
    }
}
