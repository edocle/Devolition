using edocle.core;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace edocle.external.services
{
    /// <summary>
    /// Interface useful to get external code access to system data service manipulation
    /// </summary>
    public interface IService_SystemDataSave
    {
        void TryLoadSystemData<T>(Action<bool> callback, ref T data);

        void TrySaveSystemData<T>(Action<bool> callback, ref T data);
    }

    /// <summary>
    /// Interface useful to get external code access to game data service manipulation
    /// </summary>
    public interface IService_GameDataSave
    {
        void TryLoadGameData<D>(string id, Action<bool> callback, ref D data);

        void TrySaveGameData<D>(string id, Action<bool> callback, ref D data);
    }

    public class Service_DataSave : Service<Service_DataSave_Actor>, IService_SystemDataSave, IService_GameDataSave
    {
        Service_DataSave_Actor _actor;

        SystemSaveDataHandler _systemDataHandler = null;
        List<GameSaveDataHandler> _gameDataHandlers = null;
        string _currentSlot;

        public Service_DataSave(InternalContext context) : base(context)
        {
        }

        public override void Init<A>(A actor, Action<bool> callback)    
        {
            if (actor is Service_DataSave_Actor tActor)
            {
                _actor = tActor;
                InitDatas();
                callback?.Invoke(true);
            }
            else
            {
                callback?.Invoke(false);
            }
        }

        void InitDatas()
        {
            _systemDataHandler = _context.SystemSaveDataHandler;
            _systemDataHandler.Init(_actor);

            _gameDataHandlers = _context.GameSaveDataHandlers;
            foreach(var handler in _gameDataHandlers)
                handler.Init(this);
        }

        public override void Terminate()
        {
            _systemDataHandler.Terminate();
            foreach (var handler in _gameDataHandlers)
                handler.Terminate();

            _context = null;
            _actor = default;
            _currentSlot = null;
        }

        #region Calls

        public D GetData<D>() where D : SaveDataHandler
        {
            if (typeof(SystemSaveDataHandler).IsAssignableFrom(typeof(D)))
            {
                return _systemDataHandler as D;
            }
            return _gameDataHandlers.Find(handler => handler is D) as D;
        }

        public void GenerateNewGameSlot(string slotId, int index = -1, bool forceLoad = true)
        {
            _systemDataHandler.GenerateNewGameSlot(slotId, index);
            _currentSlot = slotId;

            if (forceLoad)
                foreach (var handler in _gameDataHandlers)
                    handler.TryLoad();
        }

        public void LoadGameSlot(string slotId)
        {
            _currentSlot = slotId;
            foreach (var handler in _gameDataHandlers)
                    handler.TryLoad();
        }

        #region system

        public void TryLoadSystemData<D>(Action<bool> callback, ref D data)
        {
            _actor.TryLoadSystem(callback, ref data);
        }

        public void TrySaveSystemData<D>(Action<bool> callback, ref D data)
        {
            _actor.TrySaveSystem(callback, ref data);
        }

        #endregion system

        #region games

        public void TryLoadGameData<D>(string id, Action<bool> callback, ref D data)
        {
            if (string.IsNullOrEmpty(_currentSlot))
            {
                callback(false);
                return;
            }

            _actor.TryLoadGame(_currentSlot, id, callback, ref data);
        }

        public void TrySaveGameData<D>(string id, Action<bool> callback, ref D data)
        {
            if (string.IsNullOrEmpty(_currentSlot))
            {
                callback(false);
                return;
            }

            _actor.TrySaveGame(_currentSlot, id, callback, ref data);
        }

        #endregion games

        #endregion Calls
    }
}