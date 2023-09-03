using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.TodoItems.Commands
{
    public class UpdateCommand : IRequest<Employee>
    {
        public UpdateCommand(int id, string? name, string? email)
        {
            Id = id;
            Name = name;
            Email = email;
        }

        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
