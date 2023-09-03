using CustomerService.Entites;
using CustomerService.Services;

namespace CustomerService
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the service program!");
            Console.WriteLine("Please enter the number corresponding to the service you want to use:");
            Console.WriteLine("1. Generate Ticket Report");
            Console.WriteLine("2. Send Email");
            Console.WriteLine("3. Synchronize with External System");
            Console.WriteLine("4. Search Knowledge Base");
            Console.WriteLine("5. Send Notification");
            Console.WriteLine("6. Create Ticket");
            Console.WriteLine("7. Create User");
            Console.WriteLine("8. Exit");

            bool exit = false;
            AnalyticsReportingService reportingService = new();
            CommunicationService communicationService = new();
            IntegrationService integrationService = new();
            KnowledgeBaseService knowledgeBaseService = new();
            NotificationService notificationService = new();
            TicketManagementService ticketManagementService = new();
            UserManagementService userManagementService = new();

            while (!exit)
            {
                string? input = Console.ReadLine();

                switch (input)
                {
                    case "1":
                        Report ticketReport = reportingService.GenerateTicketReport();
                        Console.WriteLine("Ticket Report:");
                        Console.WriteLine($"Report ID: {ticketReport.Id}");
                        Console.WriteLine($"Title: {ticketReport.Title}");
                        Console.WriteLine($"Generated Date: {ticketReport.GeneratedDate}");
                        Console.WriteLine($"Content: {ticketReport.Content}");
                        break;
                    case "2":
                        Console.WriteLine("Enter recipient:");
                        string? recipient = Console.ReadLine();
                        Console.WriteLine("Enter subject:");
                        string? subject = Console.ReadLine();
                        Console.WriteLine("Enter body:");
                        string? body = Console.ReadLine();
                        communicationService.SendEmail(recipient, subject, body);
                        break;
                    case "3":
                        integrationService.SyncWithExternalSystem();
                        break;
                    case "4":
                        Console.WriteLine("Enter search term:");
                        string? searchTerm = Console.ReadLine();
                        List<Article> searchResults = knowledgeBaseService.SearchArticles(searchTerm);
                        Console.WriteLine("Search Results:");
                        foreach (Article article in searchResults)
                        {
                            Console.WriteLine($"Article ID: {article.Id}");
                            Console.WriteLine($"Title: {article.Title}");
                            Console.WriteLine($"Content: {article.Content}");
                        }
                        break;
                    case "5":
                        Console.WriteLine("Enter recipient:");
                        string? notificationRecipient = Console.ReadLine();
                        Console.WriteLine("Enter message:");
                        string? notificationMessage = Console.ReadLine();
                        notificationService.SendNotification(notificationRecipient, notificationMessage);
                        break;
                    case "6":
                        Console.WriteLine("Enter customer ID:");
                        string? customerId = Console.ReadLine();
                        Console.WriteLine("Enter issue:");
                        string? issue = Console.ReadLine();
                        Ticket newTicket = ticketManagementService.CreateTicket(customerId, issue);
                        Console.WriteLine("New Ticket Created:");
                        Console.WriteLine($"Ticket ID: {newTicket.TicketId}");
                        Console.WriteLine($"Customer ID: {newTicket.CustomerId}");
                        Console.WriteLine($"Issue: {newTicket.Issue}");
                        break;
                    case "7":
                        Console.WriteLine("Enter username:");
                        string? username = Console.ReadLine();
                        Console.WriteLine("Enter password:");
                        string? password = Console.ReadLine();
                        User newUser = userManagementService.CreateUser(username, password);
                        Console.WriteLine("New User Created:");
                        Console.WriteLine($"User ID: {newUser.Id}");
                        Console.WriteLine($"Username: {newUser.Username}");
                        Console.WriteLine($"Password: {newUser.Password}");
                        break;
                    case "8":
                        exit = true;
                        Console.WriteLine("Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid input. Please enter a valid number.");
                        break;
                }
            }
        }
    }
}