using System.Globalization;
using UnityEngine;
namespace _GameData.Scripts.Managers
{
    public struct SaveKey
    {
        public string Key;

        public static SaveKey operator +(SaveKey a, string b)
            => new SaveKey()
            {
                Key = a.Key + b
            };

        public static SaveKey operator +(SaveKey a, int b)
            => new SaveKey()
            {
                Key = a.Key + b.ToString()
            };

        public static SaveKey Parse(string value)
        {
            return new SaveKey()
            {
                Key = value
            };
        }

        public static SaveKey Parse(int value)
        {
            return new SaveKey()
            {
                Key = value.ToString()
            };
        }

        public static SaveKey Parse(float value)
        {
            return new SaveKey()
            {
                Key = value.ToString(CultureInfo.InvariantCulture)
            };
        }

        public static SaveKey Parse(float value, CultureInfo cultureInfo)
        {
            return new SaveKey()
            {
                Key = value.ToString(cultureInfo)
            };
        }
    }

    public static class SaveManager
    {
        #region Keys

        public static readonly SaveKey levelndex = new SaveKey()
        {
            Key = nameof(levelndex)
        };

        public static readonly SaveKey soundToggleStatus = new SaveKey()
        {
            Key = nameof(soundToggleStatus)
        };

        public static readonly SaveKey vibrationToggleStatus = new SaveKey()
        {
            Key = nameof(vibrationToggleStatus)
        };

        public static readonly SaveKey isFirstRunOccured = new SaveKey()
        {
            Key = nameof(isFirstRunOccured)
        };


        #endregion

        #region Setters and Getters
        #region Bool
        public static void SetBool(SaveKey key, bool value)
        {
            PlayerPrefs.SetInt(key.Key, value ? 1 : 0);
        }

        public static bool GetBool(SaveKey key, bool defaultValue = false)
        {
            return PlayerPrefs.GetInt(key.Key, defaultValue ? 1 : 0) > 0;
        }

        public static bool TryGetBool(SaveKey key, out bool value)
        {
            value = false;
            if (!HasKey(key))
            {
                return false;
            }

            value = GetBool(key);
            return true;
        }
        #endregion
        #region Int
        public static void SetInt(SaveKey key, int value)
        {
            PlayerPrefs.SetInt(key.Key, value);
        }

        public static int GetInt(SaveKey key, int defaultValue = 0)
        {
            return PlayerPrefs.GetInt(key.Key, defaultValue);
        }
        public static bool TryGetInt(SaveKey key, out int value)
        {
            value = 0;
            if (!HasKey(key))
            {
                return false;
            }

            value = GetInt(key);
            return true;
        }
        #endregion
        #region Float
        public static void SetFloat(SaveKey key, float value)
        {
            PlayerPrefs.SetFloat(key.Key, value);
        }

        public static float GetFloat(SaveKey key, float defaultValue = 0f)
        {
            return PlayerPrefs.GetFloat(key.Key, defaultValue);
        }
        public static bool TryGetFloat(SaveKey key, out float value)
        {
            value = 0f;
            if (!HasKey(key))
            {
                return false;
            }

            value = GetFloat(key);
            return true;
        }
        #endregion
        #region String
        public static void SetString(SaveKey key, string value)
        {
            PlayerPrefs.SetString(key.Key, value);
        }

        public static string GetString(SaveKey key, string defaultValue = "")
        {
            return PlayerPrefs.GetString(key.Key, defaultValue);
        }

        public static bool TryGetString(SaveKey key, out string value)
        {
            value = "";
            if (!HasKey(key))
            {
                return false;
            }

            value = GetString(key);
            return true;
        }
        #endregion
        #endregion

        public static bool HasKey(SaveKey key)
        {
            return PlayerPrefs.HasKey(key.Key);
        }

        public static void Save()
        {
            PlayerPrefs.Save();
        }

        public static void OnDestroy()
        {
            Save();
        }
    }
}