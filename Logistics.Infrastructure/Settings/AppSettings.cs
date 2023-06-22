using System;
using Microsoft.Extensions.Configuration;

namespace Logistics.Infrastructure.Settings {
    public interface IAppSettings : IJwtSettings {

    }

    public class AppSettings : IAppSettings {
        public readonly IConfiguration _configuration;

        public AppSettings(IConfiguration configuration) {
            _configuration = configuration;
        }

        //jwt
        public string Key => _configuration["AppSettings:Key"] ?? string.Empty;

        public string Issuer => _configuration["Appsettings:Issuer"] ?? string.Empty;

        public string Audience => _configuration["Appsettings:Audience"] ?? string.Empty;

        public double DurationInMinutes {
            get {
                double.TryParse(_configuration["Appsettings:DurationInMinutes"], out double result);
                return result;
            }
        }

        public int RefreshTokenDurationInHours {
            get {
                int.TryParse(_configuration["Appsettings:RefreshTokenDurationInHours"], out int result);
                return result;
            }
        }
    }
}

