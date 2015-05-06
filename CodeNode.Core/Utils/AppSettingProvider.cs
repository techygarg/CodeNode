using System;
using System.Configuration;
using System.Globalization;

namespace CodeNode.Core.Utils
{
    public static class AppSettingProvider
    {
        #region Variables
        private const string KeyNotFound = "Configuration key {0} is not found.";
        private const string KeyInvalidvalue = "Configuration key {0} has invalid value.";
        #endregion

        #region Public Methods

        public static T GetValue<T>(string key)
        {
            return GetConfigSetting<T>(key);
        }

        public static T GetValue<T>(string key, bool shouldNotEmpty)
        {
            return GetConfigSetting<T>(key, shouldNotEmpty);
        }

        #endregion

        #region Private Methods

        private static T GetConfigSetting<T>(string configKey)
        {
            return GetConfigSetting<T>(configKey, false);
        }

        private static T GetConfigSetting<T>(string configKey, bool shouldNotEmpty)
        {
            var configValue = ConfigurationManager.AppSettings[configKey];

            if (configValue == null)
            {
                throw new ConfigurationErrorsException();
            }

            if (shouldNotEmpty && string.IsNullOrWhiteSpace(configValue))
            {
                throw new ConfigurationErrorsException(string.Format(KeyNotFound, configValue));
            }

            T result;
            try
            {
                result = (T)Convert.ChangeType(configValue, typeof(T), CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException(string.Format(KeyInvalidvalue, configKey), ex);
            }

            return result;
        }

        #endregion
    }
}