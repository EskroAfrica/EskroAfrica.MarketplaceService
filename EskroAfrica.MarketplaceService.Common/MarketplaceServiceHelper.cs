using Serilog;
using Serilog.Events;
using System.Text;

namespace EskroAfrica.MarketplaceService.Common
{
    public class MarketplaceServiceHelper
    {
        public static List<T> Paginate<T>(IQueryable<T> items, int pageNumber, int pageSize) where T : class
        {
            if(pageNumber < 1) pageNumber = 1;
            if(pageSize < 1) pageSize = 10;

            return items.Skip((pageNumber-1) * pageSize).Take(pageSize).ToList();
        }

        public static string GenerateString(int length)
        {
            var alphabets = "abcdefghijklmnopqrstuvwxyz";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                sb.Append(alphabets[new Random().Next(0, alphabets.Length)]);
            }

            return sb.ToString();
        }
    }
}
