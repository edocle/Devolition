using System;

namespace edocore.external.services
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

    public class Service_DataSave<T> : Service, IService_SystemDataSave, IService_GameDataSave where T : IServiceActor, IService_SystemDataSave_Actor, IService_GameDataSave_Actor
    {
        private T _actor;
        private string _currentSlot;

        public Service_DataSave(InternalContext context) : base(context)
        {
        }

        public override void Init<A>(A actor, Action<bool> callback)    
        {
            if (actor is T tActor)
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
            var systemData = _context.RouterParameters.SystemSaveDataHandler;
            systemData.Init(this);

            var gameDatas = _context.RouterParameters.GameSaveDataHandlers;
            foreach(var handler in gameDatas)
            {
                handler.Init(this);
            }
        }

        public override void Terminate()
        {
            _context = null;
            _actor = default;
            _currentSlot = null;
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
            _actor.TryLoadGame(_currentSlot, id, callback, ref data);
        }

        public void TrySaveGameData<D>(string id, Action<bool> callback, ref D data)
        {
            _actor.TrySaveGame(_currentSlot, id, callback, ref data);
        }
        #endregion games
    }
}