using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Authorization.Models
{
    [Table("permission")]
    public class Permission
    {
        [Column("permission_id")]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonPropertyName("permission_id")]
        public int PermissionId { get; set; }

        [Column("permission_name")]
        public string? PermissionName { get; set; }

        public ICollection<RolePermission>? RolePermissions { get; set; }
        
    }
}
