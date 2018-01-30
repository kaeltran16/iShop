namespace iShop.Common.Helpers
{
    public class JwtTokenSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Authority { get; set; }
        public int TokenLifeTime { get; set; }
        public int RefreshTokenLifeTime { get; set; }
    }
}
