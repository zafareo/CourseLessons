using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Authorization.Models
{
    public class UserRole
    {
        [Column("user_role_id")]
        [JsonPropertyName("user_role_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("user_id")]
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        public UserModel User { get; set; }

        [Column("role_id")]
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}