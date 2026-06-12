using edocore.external.services;
using System.Collections.Generic;

namespace edocle.external
{
    public abstract class SystemSaveDataHandler<T> : SystemSaveDataHandler where T: SystemSaveData
    {
        protected T _data;

        public void TryLoad()
        {
            _service.TryLoadSystemData(success =>
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
            _service.TrySaveSystemData(success =>
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

        public bool HasGameslots =>_data._gameSlotIds.Count > 0;
    }

    public abstract class SystemSaveDataHandler : SaveDataHandler
    {
        protected IService_SystemDataSave _service;

        public void Init(IService_SystemDataSave service)
        {
            _service = service;
        }

        public void Terminate()
        {
            _service = null;
        }
    }

    public abstract class SystemSaveData : SaveData
    {
        protected SystemSaveData()
        {
            _gameSlotIds = new List<string>();
            _currentGameSlotIndex = 0;
        }

        public List<string> _gameSlotIds;
        public int _currentGameSlotIndex;
    }
}