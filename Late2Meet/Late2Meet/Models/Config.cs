using System;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace Late2Meet.Models
{
    /// <summary>
    /// This is the Settings static class that can be used in your Core solution or in any
    /// of your client applications. All settings are laid out the same exact way with getters
    /// and setters. 
    /// </summary>
    public static class Config
    {
        private static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        internal static string AddValueAsString(decimal addValue)
        {
            throw new NotImplementedException();
        }

        #region Setting Constants

        public const string AddValueKey = "AddValueKey";
        private static readonly decimal AddValueDefault = decimal.One;

        public static decimal AddValue
        {
            get
            {
                return AppSettings.GetValueOrDefault(AddValueKey, AddValueDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(AddValueKey, value);
            }
        }

        public static string ValueAsString(decimal input)
        {
            return (string) new DecimalConverter().Convert(input, null, null, null);
        }

        public static decimal ValueAsDecimal(string input)
        {
            return (decimal) new DecimalConverter().ConvertBack(input, null, null, null);
        }
        #endregion
    }
}