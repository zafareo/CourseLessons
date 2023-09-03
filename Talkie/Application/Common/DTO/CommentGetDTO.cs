using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.DTO
{
    public class CommentGetDTO
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public Guid? AuthorId { get; set; }
        public Guid? PostId { get; set; }
    }
}
