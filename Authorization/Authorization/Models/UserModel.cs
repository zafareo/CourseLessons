using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Authorization.Models
{
    public class UserModel
    {
        [Column("user_id")]
        [JsonPropertyName("user_id")]
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]   
        public int Id { get; set; }

        [Column("user_name")]
        [JsonPropertyName("user_name")]
        public string Username { get; set; }

        [Column("user_password")]
        [JsonPropertyName("user_password")]
        public string Password { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
