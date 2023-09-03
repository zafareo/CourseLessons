using Domain.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IdentityEntities
{
    [Table("roles")]
    public class Role : BaseAuditableEntity
    {
        [Column("role_name")]
        public string Name { get; set; }
        public virtual List<Permission>? Permissions { get; set; }
        public List<User>? Users { get; set; }
    }
}
