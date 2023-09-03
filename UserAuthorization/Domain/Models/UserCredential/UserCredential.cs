using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Models.UserCredential;

public class UserCredential
{
    [JsonPropertyName("user_name")]
    public string UserName { get; set; }

    [PasswordPropertyText]
    public string Password { get; set; }
}

