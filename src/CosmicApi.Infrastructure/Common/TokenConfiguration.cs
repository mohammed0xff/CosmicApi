﻿using Microsoft.IdentityModel.Tokens;

namespace CosmicApi.Infrastructure.Common
{
    public class TokenConfiguration
    {
        public string Secret { get; set; } = null!;
        public string Issuer { get; set; } = null!;
        public string Audience { get; set; } = null!;
        public double DurationInMinutes { get; set; }  
        public string Algorithm { get; set; } = null!;
    }
}