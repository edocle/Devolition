
using System;
using UnityEngine;

namespace edocore.services
{
    public class Service_DataSave_Actor_Json : IService_Datasave_Actor
    {
        public void Init(Action callback)
        {
            if (!System.IO.Directory.Exists(_folderPath))
                System.IO.Directory.CreateDirectory(_folderPath);

            callback?.Invoke();
        }

        readonly string _folderPath = System.IO.Path.Combine(Application.persistentDataPath, "DataSave");

        #region system

        readonly string _systemFileName = "System.json";

        string SystemFilePath => System.IO.Path.Combine(_folderPath, _systemFileName);

        public void TryRecoverSystemSave<T>(Action<bool> callback, ref T data)
        {
            string filePath = SystemFilePath;

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, data);
                callback?.Invoke(true);
                return;
            }

            callback?.Invoke(false);
        }

        public void TrySaveSystem<T>(Action<bool> callback, ref T data)
        {
            string filePath = SystemFilePath;
            string json = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, json);

            callback?.Invoke(true);
        }

        #endregion system

        #region game

        readonly string _gameFolderPrefix = "Game_";

        string GetGameNameFile<T>( T data )
        { return typeof(T).Name + ".json"; }

        public void TryLoadGame<T>(string id, Action<bool> callback, ref T data)
        {
            string gameSaveFolder = System.IO.Path.Combine(_folderPath, _gameFolderPrefix + id);
            string fileName = GetGameNameFile(data);
            string filePath = System.IO.Path.Combine(gameSaveFolder, fileName);

            if (System.IO.File.Exists(filePath))
            {
                string json = System.IO.File.ReadAllText(filePath);
                JsonUtility.FromJsonOverwrite(json, data);
                callback?.Invoke(true);
                return;
            }

            callback?.Invoke(false);
        }

        public void TrySaveGame<T>(string id, Action<bool> callback, ref T data)
        {
            string gameSaveFolder = System.IO.Path.Combine(_folderPath, _gameFolderPrefix + id);

            if (!System.IO.Directory.Exists(gameSaveFolder))
                System.IO.Directory.CreateDirectory(gameSaveFolder);

            string fileName = GetGameNameFile(data);
            string filePath = System.IO.Path.Combine(gameSaveFolder, fileName);
            string json = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, json);

            callback?.Invoke(true);
        }

        #endregion game
    }
}