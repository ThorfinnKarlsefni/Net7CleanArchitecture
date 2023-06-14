using System;
using Microsoft.Extensions.Configuration;
using static Logistics.Infrastructure.Settings.AppSettings;

namespace Logistics.Infrastructure.Settings
{
    public interface IAppSettings : IJwtSettings
    {

    }

    public class AppSettings : IAppSettings
    {
        private readonly IConfiguration _config;

        public AppSettings(IConfiguration configuration)
        {
            _config = configuration;
        }

        // JWT related stuff
        public string Key => ReadString("Key");
        public string Issuer => ReadString("Issuer");
        public string Audience => ReadString("Audience");
        public double DurationInMinutes => ReadDouble("DurationInMinutes");
        public int RememberMeDurationInHours => ReadInt("RememberMeDurationInHours");

        private string ReadString(string key)
        {
            var settings = _config.GetSection("AppSettings");
            return settings[key];

        }

        private int ReadInt(string key)
        {
            return Convert.ToInt16(ReadString(key));
        }

        private bool ReadBoolean(string key)
        {
            var val = ReadString(key);
            return val != null && bool.Parse(val);
        }

        private long ReadLong(string key)
        {
            return Convert.ToInt64(ReadString(key));
        }

        private double ReadDouble(string key)
        {
            return Convert.ToDouble(ReadString(key));
            // return ReadString(key).con();
        }
    }
}

