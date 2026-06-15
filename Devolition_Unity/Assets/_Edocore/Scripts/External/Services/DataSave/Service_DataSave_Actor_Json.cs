using System;
using UnityEngine;

namespace edocle.external.services
{
    public class Service_DataSave_Actor_Json : Service_DataSave_Actor
    {
        public override void Init(Action<bool> callback)
        {
            if (!System.IO.Directory.Exists(_folderPath))
                System.IO.Directory.CreateDirectory(_folderPath);

            callback?.Invoke(true);
        }

#if UNITY_EDITOR
        /// <summary>
        /// Get path inside unity project for more visibility & simpler tests
        /// </summary>
        readonly string _folderPath = System.IO.Path.Combine(Application.dataPath, "_Edocore", "DataSave\\");   
#else
        readonly string _folderPath = System.IO.Path.Combine(Application.persistentDataPath, "DataSave\\");
#endif

        #region system

        readonly string _systemFileName = "System.json";

        string SystemFilePath => System.IO.Path.Combine(GetSystemFolderPath(), _systemFileName);

        public override void TryLoadSystem<T>(Action<bool> callback, ref T data)
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

        public override void TrySaveSystem<T>(Action<bool> callback, ref T data)
        {
            string filePath = SystemFilePath;
            string json = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, json);

            callback?.Invoke(true);
        }

        string GetSystemFolderPath()
        {
            if (!System.IO.Directory.Exists(_folderPath))
                System.IO.Directory.CreateDirectory(_folderPath);

            return _folderPath;
        }

        #endregion system

        #region game

        readonly string _gameFolderPrefix = "Game_";
        readonly string _gameFileExtension = ".json";

        string GetGameNameFile( string id )
        { return id + _gameFileExtension; }

        public override void TryLoadGame<D>(string slot, string id, Action<bool> callback, ref D data)
        {
            string gameSaveFolder = GetGameFolderPath(slot);

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

        public override void TrySaveGame<D>(string slot, string id, Action<bool> callback, ref D data)
        {
            string gameSaveFolder = GetGameFolderPath(slot);

            string fileName = GetGameNameFile(id);
            string filePath = System.IO.Path.Combine(gameSaveFolder, fileName);
            string json = JsonUtility.ToJson(data, true);
            System.IO.File.WriteAllText(filePath, json);

            callback?.Invoke(true);
        }

        #endregion game

        string GetGameFolderPath(string slot)
        {
            string gameSaveFolder = System.IO.Path.Combine(_folderPath, _gameFolderPrefix + slot);
            if (!System.IO.Directory.Exists(gameSaveFolder))
                System.IO.Directory.CreateDirectory(gameSaveFolder);

            return gameSaveFolder;
        }
    }
}