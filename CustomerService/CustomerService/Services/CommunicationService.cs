using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Chat.V2.Service.Channel;

namespace CustomerService.Services
{
    public class CommunicationService
    {
        public void SendEmail(string recipient, string subject, string body)
        {
            // Logic to send an email to the recipient
            // Replace this with your actual email sending logic
            Console.WriteLine($"Email sent to: {recipient}");
            Console.WriteLine($"Subject: {subject}");
            Console.WriteLine($"Body: {body}");
        }


        public void SendChatMessage(string sender, string recipient, string message)
        {
            // Replace this with your actual chat message sending logic

            // Simulate sending a chat message
            Console.WriteLine($"Chat message sent from: {sender} to: {recipient}");
            Console.WriteLine($"Message: {message}");
        }


    }
}
