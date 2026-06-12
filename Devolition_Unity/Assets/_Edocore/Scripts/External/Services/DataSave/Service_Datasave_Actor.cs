using System;

namespace edocore.external.services
{
    /// <summary>
    /// Actor service to register & recover all saved data in generated files
    /// Handles system data
    /// </summary>
    public interface IService_SystemDataSave_Actor
    {
        /// <summary>
        /// Tries to load system data
        /// System save contains all datas saved in order for the system to work
        /// Used to "load" the system, or to generate it if it does not exist yet
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="callback">Callback to indicate success or failure</param>
        /// <param name="data">The data to be recovered</param>
        void TryLoadSystem<T>(Action<bool> callback, ref T data);

        /// <summary>
        /// Tries to update registered system data
        /// </summary>
        /// <typeparam name="T">data type</typeparam>
        /// <param name="callback">Callback to indicate success or failure</param>
        /// <param name="data">The data to be saved</param>
        void TrySaveSystem<T>(Action<bool> callback, ref T data);
    }

    /// <summary>
    /// Actor service to register & recover all saved data in generated files
    /// Handles multiple game saves data
    /// </summary>
    public interface IService_GameDataSave_Actor
    {
        /// <summary>
        /// Tries to load game data
        /// Can have multiple sets of game data, identified by their id
        /// Each set can have multiple data types saved (ex: player data, world data...)
        /// /!\ @todo cannot save multiple datas of the same type for now, may need to update that
        /// ex: if yo uwant to save more than one player, put all player datas into a playersHubData and save the hub
        /// </summary>
        /// <typeparam name="D">data type</typeparam>
        /// <param name="slot">slot of data set</param>
        /// <param name="id">id of data</param>
        /// <param name="callback">Callback to indicate success or failure</param>
        /// <param name="data">The data to be loaded</param>
        void TryLoadGame<D>(string slot, string id, Action<bool> callback, ref D data);

        /// <summary>
        /// Tries to save game data
        /// Can have multiple sets of game data, identified by their id
        /// Each set can have multiple data types saved (ex: player data, world data...)
        /// /!\ @todo cannot save multiple datas of the same type for now, may need to update that
        /// ex: if yo uwant to save more than one player, put all player datas into a playersHubData and save the hub
        /// </summary>
        /// <typeparam name="D">data type</typeparam>
        /// <param name="slot">slot of data set</param>
        /// <param name="id">id of data</param>
        /// <param name="callback">Callback to indicate success or failure</param>
        /// <param name="data">The data to be saved</param>
        void TrySaveGame<D>(string slot, string id, Action<bool> callback, ref D data);
    }
}