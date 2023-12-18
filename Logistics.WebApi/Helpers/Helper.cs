using System;
using System.Text.RegularExpressions;

namespace Logistics.WebApi.Helpers
{
    public static class Helper
    {
        public static bool IsValidPhoneNumber(string? phoneNumber)
        {
            string pattern = @"^1[3456789]\d{9}$";
            if (string.IsNullOrEmpty(phoneNumber))
                return true;
            return Regex.IsMatch(phoneNumber, pattern);
        }
    }
}

