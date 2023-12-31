﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models
{
    [Table("user_role")]
    public class UserRole
    {
        [Column("user_role_id")]
        [JsonPropertyName("user_role_id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserRoleId { get; set; }

        //[ForeignKey("user_id")]
        [Column("user_id")]
        [JsonPropertyName("user_id")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual User? User { get; set; }


      // [ForeignKey("role_id")]
        [Column("role_id")]
        [JsonPropertyName("role_id")]
        public int RoleId { get; set; }
        [JsonIgnore]
        public virtual Role? Role { get; set; }
    }
}
