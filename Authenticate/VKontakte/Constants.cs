namespace VKontakte
{
    public class Constants
    {
        public const string Issuer = "VK";
        public const string CallBackPath = "/signin-vk";
        private const string _endpoint = "https://oauth.vk.com";
        public static string AuthorizationEndpoint = $"{_endpoint}/authorize";
        public static string TokenEndpoint = $"{_endpoint}/access_token";
    }
}
