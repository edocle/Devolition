using System;
using UnityEngine;

namespace edocore.external.services
{
    public class Service_DataSave_Actor_Json : IServiceActor, IService_SystemDataSave_Actor, IService_GameDataSave_Actor
    {
        public void Init(Action<bool> callback)
        {
            if (!System.IO.Directory.Exists(_folderPath))
                System.IO.Directory.CreateDirectory(_folderPath);

            callback?.Invoke(true);
        }

        readonly string _folderPath = System.IO.Path.Combine(Application.persistentDataPath, "DataSave");

        #region system

        readonly string _systemFileName = "System.json";

        string SystemFilePath => System.IO.Path.Combine(_folderPath, _systemFileName);

        public void TryLoadSystem<T>(Action<bool> callback, ref T data)
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
        readonly string _gameFileExtension = ".json";

        string GetGameNameFile( string id )
        { return id + _gameFileExtension; }

        public void TryLoadGame<D>(string slot, string id, Action<bool> callback, ref D data)
        {
            string gameSaveFolder = System.IO.Path.Combine(_folderPath, _gameFolderPrefix + slot);
            string fileName = GetGameNameFile(id);
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

        public void TrySaveGame<D>(string slot, string id, Action<bool> callback, ref D data)
        {
            string gameSaveFolder = System.IO.Path.Combine(_folderPath, _gameFolderPrefix + slot);

            if (!System.IO.Directory.Exists(gameSaveFolder))
                System.IO.Directory.CreateDirectory(gameSaveFolder);

            string fileName = GetGameNameFile(id);
            string filePath = System.IO.Path.Combine(gameSaveFolder, fileName);
            string json = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, json);

            callback?.Invoke(true);
        }

        #endregion game
    }
}