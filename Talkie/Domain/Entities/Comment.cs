using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("comments")]
    public class Comment : BaseAuditableEntity
    {
        [Column("text")]
        public string Text { get; set; }

        [Column("author_id")]
        public Guid? AuthorId { get; set; }
        public User? Author { get; set; }

        [Column("post_id")]
        public Guid? PostId { get; set; }
        public Post? Post { get; set; }
    }
}
