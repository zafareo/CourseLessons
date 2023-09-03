using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.TodoItems.Commands
{
    public class DeleteCommand : IRequest<Employee>
    {
        public DeleteCommand(int id)
        {
            Id = id;
        }

        public int Id { get; set; }
    }
}
