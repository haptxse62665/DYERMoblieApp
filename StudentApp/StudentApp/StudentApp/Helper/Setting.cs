using System;
using System.Collections.Generic;
using System.Text;

// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace StudentApp.Helpers
{
  /// <summary>
  /// This is the Settings static class that can be used in your Core solution or in any
  /// of your client applications. All settings are laid out the same exact way with getters
  /// and setters. 
  /// </summary>
  public static class Settings
{
    private static ISettings AppSettings
    {
        get
        {
            return CrossSettings.Current;
        }
    }

    #region Setting Constants

        private const string SettingsKey = "settings_key";
        private static readonly string SettingsDefault = string.Empty;

        private const string UsernameKey = "Username_key";
        private static readonly string UsernameKeyDefault = string.Empty;

        private const string TokenKey = "Token_key";
        private static readonly string TokenDefault = string.Empty;

        private const string IdKey = "Id_key";
        private static readonly string IdDefault = string.Empty;

        private const string NewPhoneNumberKey = "NewPhoneNumber_key";
        private static readonly string NewPhoneNumberDefault = string.Empty;

        private const string ContactNumberKey = "ContactNumber_key";
        private static readonly string ContactNumberDefault = string.Empty;

        private const string HostIDKey = "HostID_key";
        private static readonly string HostIDDefault = string.Empty;

        private const string StudentIDKey = "StudentID_key";
        private static readonly string StudentIDDefault = string.Empty;

        private const string EmailKey = "Email_key";
        private static readonly string EmailDefault = string.Empty;

        private const string FullNameKey = "FullName_key";
        private static readonly string FullnameDefault = string.Empty;

        private const string URLKey = "URL_key";
        private static readonly string URLDefault = string.Empty;

        private const string ArrivalKey = "Arrival_key";
        private static readonly string ArrivalDefault = string.Empty;

        private const string FacultyKey = "Faculty_key";
        private static readonly string FacultyDefault = string.Empty;
        #endregion


        public static string GeneralSettings
    {
        get
        {
            return AppSettings.GetValueOrDefault(SettingsKey, SettingsDefault);
        }
        set
        {
            AppSettings.AddOrUpdateValue(SettingsKey, value);
        }
    }

        public static string UsernameSettings //UsernameSettings là từ khóa xuất hiện khi ta sử dụng trong PCL
        {
            get
            {
                return AppSettings.GetValueOrDefault(UsernameKey, UsernameKeyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UsernameKey, value);
            }
        }

        public static string TokenSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(TokenKey, TokenDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(TokenKey, value);
            }
        }

        public static string IdSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(IdKey, IdDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(IdKey, value);
            }
        }

        public static string NewPhoneNumberSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(NewPhoneNumberKey, NewPhoneNumberDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(NewPhoneNumberKey, value);
            }
        }

        public static string ContactNumberSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(ContactNumberKey, ContactNumberDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ContactNumberKey, value);
            }
        }
        public static string HostIDSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(HostIDKey, HostIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(HostIDKey, value);
            }
        }

        public static string StudentIDSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(StudentIDKey, StudentIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(StudentIDKey, value);
            }
        }
        public static string EmailSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(EmailKey, EmailDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(EmailKey, value);
            }
        }
        public static string FullNameSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(FullNameKey, FullnameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FullNameKey, value);
            }
        }

        public static string URLSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(URLKey, URLDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(URLKey, value);
            }
        }
        public static string ArrivalSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(ArrivalKey, ArrivalDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(ArrivalKey, value);
            }
        }
        public static string FacultySettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacultyKey, FacultyDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacultyKey, value);
            }
        }
    }
}
