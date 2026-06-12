using System;
using UnityEngine;

namespace edocore.external.services
{
    public class Service_DataSave_Actor_Playerprefs : IServiceActor, IService_SystemDataSave_Actor, IService_GameDataSave_Actor
    {
        public void Init(Action<bool> callback)
        {
            callback?.Invoke(true);
        }

        #region system

        readonly string _systemSaveKey = "SystemSave";

        public void TryLoadSystem<T>(Action<bool> callback, ref T data)
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

        string GetGameSaveKey<D>(string slot, string id, D data)
        { return _gameSaveKeyPrefix + "_" + slot + "_" + id; }

        public void TryLoadGame<D>(string slot, string id, Action<bool> callback, ref D data)
        {
            string key = GetGameSaveKey(slot, id, data);
            string content = PlayerPrefs.GetString(key);
            if (string.IsNullOrEmpty(content))
            {
                callback?.Invoke(false);
                return;
            }

            JsonUtility.FromJsonOverwrite(content, data);
            callback?.Invoke(true);
        }

        public void TrySaveGame<D>(string slot, string id, Action<bool> callback, ref D data)
        {
            string key = GetGameSaveKey(slot, id, data);
            string content = JsonUtility.ToJson(data);
            PlayerPrefs.SetString(key, content);
            PlayerPrefs.Save();
            callback?.Invoke(true);
        }

        #endregion game
    }
}