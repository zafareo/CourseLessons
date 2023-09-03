using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Services
{
    public class NotificationService
    {
        public void SendNotification(string recipient, string message)
        {
            // Logic to send a notification to the recipient
            // Replace this with your actual notification sending logic
            Console.WriteLine($"Notification sent to: {recipient}");
            Console.WriteLine($"Message: {message}");
        }
    }
}
