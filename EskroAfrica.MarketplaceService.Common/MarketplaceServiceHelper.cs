namespace EskroAfrica.MarketplaceService.Common
{
    public class MarketplaceServiceHelper
    {
        public static List<T> Paginate<T>(IQueryable<T> items, int pageNumber, int pageSize) where T : class
        {
            if(pageNumber < 1) pageNumber = 1;
            if(pageSize < 1) pageSize = 10;

            return items.Skip(pageNumber * pageSize).Take(pageSize).ToList();
        }
    }
}
