using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("posts")]
    public class Post : BaseAuditableEntity
    {
        [Column("post_name")]
        public string Name { get; set; }

        [Column("content")]
        public string Content { get; set; }

        [Column("author_id")]
        public Guid? AuthorId { get; set; }
        public User? Author { get; set; }
        public List<Comment>? Comments { get; set; }
    }
}
