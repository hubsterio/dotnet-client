namespace Hubster.Auth
{
    public class TokenResponse<TToken> : Response where TToken : class
    {
        public TToken Token { get; internal set; }
    }
}
