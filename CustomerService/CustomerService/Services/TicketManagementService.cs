using CustomerService.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Services
{
    public class TicketManagementService
    {
        private List<Ticket> tickets;

        public TicketManagementService()
        {
            tickets = new List<Ticket>();
        }

        public Ticket CreateTicket(string customerId, string issue)
        {
            string ticketId = GenerateUniqueId();

            Ticket newTicket = new Ticket
            {
                TicketId = ticketId,
                CustomerId = customerId,
                Issue = issue
            };

            // Perform any additional logic (e.g., validation, calculations, etc.) before adding the ticket
            // ...

            tickets.Add(newTicket);

            return newTicket;
        }

        public Ticket GetTicketById(string ticketId)
        {
            Ticket? ticket = tickets.FirstOrDefault(t => t.TicketId == ticketId);
            return ticket;
        }

        public void UpdateTicket(Ticket ticket)
        {
            // Perform any additional logic (e.g., validation, authorization, etc.) before updating the ticket
            // ...

            Ticket? existingTicket = tickets.FirstOrDefault(t => t.TicketId == ticket.TicketId);
            if (existingTicket != null)
            {
                existingTicket.CustomerId = ticket.CustomerId;
                existingTicket.Issue = ticket.Issue;
                // Update other ticket properties as needed
            }
        }

        private string GenerateUniqueId()
        {
            string ticketId = Guid.NewGuid().ToString();
            return ticketId;
        }

    }
}
