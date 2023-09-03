using Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.IdentityEntities
{
    [Table("permissions")]
    public class Permission : BaseAuditableEntity
    {
        [Column("permission_name")]
        public string Name { get; set; }
        public virtual List<Role>? Roles { get; set; }
    }
}
