using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("role_permission")]
    public class RolePermission
    {
        [Column("role_permission_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePermissionId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
        public virtual Role? Role { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }
        public Permission? Permission { get; set; }
    }
}
