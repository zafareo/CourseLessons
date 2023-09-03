using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Authorization.Models
{
    [Table("role_permissions")]
    public class RolePermission
    {
        [Column("role_permission_id")]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RolePermissionId { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }
        public Role? Role { get; set; }

        [Column("permission_id")]
        public int PermissionId { get; set; }
        public Permission Permission { get; set; }

    }
}
