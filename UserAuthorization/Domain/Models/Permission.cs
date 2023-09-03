using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models;

[Table("permission")]
public class Permission
{
    [Column("permission_id")]
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [JsonPropertyName("permission_id")]
    public int PermissionId { get; set; }

    [Column("permission_name")]
    [JsonPropertyName("permission_name")]
    public string? PermissionName { get; set; }

    [JsonIgnore]
    public virtual ICollection<RolePermission>? RolePermissions { get; set; }

}
