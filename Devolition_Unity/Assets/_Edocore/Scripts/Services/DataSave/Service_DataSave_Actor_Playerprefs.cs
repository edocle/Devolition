
using System;
using UnityEngine;

namespace edocore.services
{
    public class Service_DataSave_Actor_Playerprefs : IService_Datasave_Actor
    {
        public void Init(Action callback)
        {
            callback?.Invoke();
        }



        #region system

        readonly string _systemSaveKey = "SystemSave";

        public void TryRecoverSystemSave<T>(Action<bool> callback, ref T data)
        {
            string content = PlayerPrefs.GetString(_systemSaveKey);
            if (string.IsNullOrEmpty(content))
            {
                callback?.Invoke(false);
                return;
            }

            JsonUtility.FromJsonOverwrite(content, data);
            callback?.Invoke(true);
        }

        public void TrySaveSystem<T>(Action<bool> callback, ref T data)
        {
            string content = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(_systemSaveKey, content);
            PlayerPrefs.Save();
            callback?.Invoke(true);
        }

        #endregion system

        #region game

        readonly string _gameSaveKeyPrefix = "GameSave";

        string GetGameSaveKey<T>(string id, T data)
        { return _gameSaveKeyPrefix + "_" + id + "_" + typeof(T).Name; }

        public void TryLoadGame<T>(string id, Action<bool> callback, ref T data)
        {
            string key = GetGameSaveKey(id, data);
            string content = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(content))
            {
                callback?.Invoke(false);
                return;
            }

            JsonUtility.FromJsonOverwrite(content, data);
            callback?.Invoke(true);
        }

        public void TrySaveGame<T>(string id, Action<bool> callback, ref T data)
        {
            string key = GetGameSaveKey(id, data);
            string content = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, content);
            PlayerPrefs.Save();
            callback?.Invoke(true);
        }

        #endregion game
    }
}