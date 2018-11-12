using System;
using System.Collections.Generic;
using System.Text;

// Helpers/Settings.cs
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace DYCApp.Helpers
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

        private const string FacultyIDKey = "FacultyID_key";
        private static readonly string FacultyIDDefault = string.Empty;

        private const string tmpFacultyIDKey = "tmpFacultyID_key";
        private static readonly string tmpFacultyIDDefault = string.Empty;

        private const string DYCIDKey = "DYCID_key";
        private static readonly string DYCIDDefault = string.Empty;

        private const string EmailKey = "Email_key";
        private static readonly string EmailDefault = string.Empty;

        private const string FullNameKey = "FullName_key";
        private static readonly string FullnameDefault = string.Empty;

        private const string URLKey = "URL_key";
        private static readonly string URLDefault = string.Empty;

        private const string RoleNameKey = "RoleName_key";
        private static readonly string RoleNameDefault = string.Empty;

        private const string FacultyNameKey = "FacultyName_key";
        private static readonly string FacultyNameDefault = string.Empty;

        private const string UserIDKey = "UserID_key";
        private static readonly string UserIDDefault = string.Empty;
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
        public static string FacultyIDSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacultyIDKey, FacultyIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacultyIDKey, value);
            }
        }
        public static string tmpFacultyIDSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(tmpFacultyIDKey, tmpFacultyIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(tmpFacultyIDKey, value);
            }
        }

        public static string DYCIDSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(DYCIDKey, DYCIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(DYCIDKey, value);
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
        public static string RoleNameSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(RoleNameKey, RoleNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(RoleNameKey, value);
            }
        }

        public static string FacultyNameSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(FacultyNameKey, FacultyNameDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(FacultyNameKey, value);
            }
        }
        public static string UserIDSettings
        {
            get
            {
                return AppSettings.GetValueOrDefault(UserIDKey, UserIDDefault);
            }
            set
            {
                AppSettings.AddOrUpdateValue(UserIDKey, value);
            }
        }
    }
}
