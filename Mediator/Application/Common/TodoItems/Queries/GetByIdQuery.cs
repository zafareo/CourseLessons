using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.TodoItems.Queries
{
    public class GetByIdQuery : IRequest<Employee>
    {
        public GetByIdQuery(int id)
        {
           Id = id;
        }
        public int Id { get; set; }
    }
}
