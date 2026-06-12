using edocore.external.services;
using UnityEngine;

namespace edocle.external
{
    public abstract class GameSaveDataHandler<T> : GameSaveDataHandler where T : GameSaveData
    {
        [SerializeField] protected string _id;
        [SerializeField] protected T _data;

        public void TryLoad()
        {
            _service.TryLoadGameData(_id, success =>
            {
                if (success)
                {
                    // Data successfully loaded, can be used
                }
                else
                {
                    // No data to load, need to generate it
                    _data = System.Activator.CreateInstance<T>();
                    TrySave();
                }
            }, ref _data);
        }

        public void TrySave()
        {
            _service.TrySaveGameData(_id, success =>
            {
                if (success)
                {
                    // Data successfully saved
                }
                else
                {
                    // Failed to save data
                }
            }, ref _data);
        }
    }

    public abstract class GameSaveDataHandler : SaveDataHandler
    {
        protected IService_GameDataSave _service;

        public void Init(IService_GameDataSave service)
        {
            _service = service;
        }

        public void Terminate()
        {
            _service = null;
        }
    }

    public abstract class GameSaveData : SaveData
    {

    }
}