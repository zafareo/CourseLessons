﻿
using System.Text.Json.Serialization;

namespace Domain.Models.Token
{
    public class Token
    {
        //[JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

       // [JsonPropertyName("access_token")]
        public string RefreshToken { get; set; }
    }
}
