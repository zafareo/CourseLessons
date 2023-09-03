using Domain.Models.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.TodoItems.Queries
{
    public class GetAllQuery : IRequest<Employee>
    {
    }
}
