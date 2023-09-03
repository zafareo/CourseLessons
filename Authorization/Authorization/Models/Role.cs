using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Authorization.Models
{
    [Table("roles")]
    public class Role
    {
        [Column("role_id")]
        [JsonPropertyName("role_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("role_name")]
        [JsonRequired]
        public string Name { get; set; }

        public ICollection<RolePermission> RolePermissions { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
    }
}
