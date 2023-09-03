using Domain.Common;
using Domain.IdentityEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("users")]
    public class User : BaseAuditableEntity
    {
        [Column("user_name")]
        public string Name { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("picture")]
        public string Picture { get; set; }
        public List<Post>? Posts { get; set; }

        [JsonIgnore]
        public List<Comment>? Comments { get; set; }

        [JsonIgnore]
        public List<Role>? Roles { get; set; }
        
    }
}
