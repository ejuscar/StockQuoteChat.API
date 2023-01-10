namespace StockQuoteChat.Infrastructure.Repositories
{
    public class GenericRepository
    {
        protected readonly ChatDbContext _context;

        public GenericRepository(ChatDbContext context)
        {
            _context = context;
        }
    }
}
