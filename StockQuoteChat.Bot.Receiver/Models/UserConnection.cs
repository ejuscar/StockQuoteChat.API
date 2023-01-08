namespace StockQuoteChat.Bot.Receiver
{
    public class UserConnection
    {

        public UserConnection(string user, string room)
        {
            User = user;
            Room = room;
        }

        public string User { get; set; }
        public string Room { get; set; }
    }
}
