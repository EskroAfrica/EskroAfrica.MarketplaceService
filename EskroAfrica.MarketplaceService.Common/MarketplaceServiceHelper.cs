using EskroAfrica.MarketplaceService.Common.DTOs.Requests;
using EskroAfrica.MarketplaceService.Common.Enums;
using EskroAfrica.MarketplaceService.Common.Models;
using Serilog;
using Serilog.Events;
using System.Diagnostics;
using System.Text;

namespace EskroAfrica.MarketplaceService.Common
{
    public class MarketplaceServiceHelper
    {
        public static Stopwatch Stopwatch = new Stopwatch();

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

        public static void StartTimer()
        {
            Stopwatch.Start();
        }

        public static void StopTimer()
        {
            Stopwatch.Stop();
        }

        public static NotificationRequest CreateNotificationRequest(string subject, string body, NotificationType type, List<string> recipients)
        {
            return new NotificationRequest
            {
                Type = type,
                Recipients = recipients,
                Subject = subject,
                Body = body
            };
        }
    }
}
