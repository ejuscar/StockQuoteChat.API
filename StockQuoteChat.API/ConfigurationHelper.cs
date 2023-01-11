namespace StockQuoteChat.API
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Config;

        public static void Initialize(IConfiguration config)
        {
            Config = config;
        }
    }
}
