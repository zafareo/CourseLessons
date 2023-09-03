using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomerService.Entites
{
    public class Ticket
    {
        public string TicketId { get; set; }
        public string CustomerId { get; set; }
        public string Issue { get; set; }
    }
}
