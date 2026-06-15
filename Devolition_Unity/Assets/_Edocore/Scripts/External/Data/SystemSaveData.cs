using edocle.external.services;
using System.Collections.Generic;
using UnityEngine;

namespace edocle.external
{

    public abstract class SystemSaveDataHandler<T> : SystemSaveDataHandler where T: SystemSaveData
    {
        [SerializeField] protected T _data;

        public override void Init(IService_SystemDataSave_Actor service)
        {
            base.Init(service);
            TryLoad();
        }

        public override void TryLoad()
        {
            _serviceActor.TryLoadSystem(success =>
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

        public override void TrySave()
        {
            _serviceActor.TrySaveSystem(success =>
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

        #region Slots

        /// <summary>
        /// Generates a new game slot with the given slotId and optional index.
        /// Sets newly generated slot as current.
        /// @todo Add a way to remove a slot and its data
        /// </summary>
        /// <param name="slotId">id of slot</param>
        /// <param name="index">if by default, generates a new slot at the end of the list</param>
        public override void GenerateNewGameSlot(string slotId, int index = -1)
        {
            if (index < 0)
            {
                index = _data._gameSlotIds.Count;
            }

            CurrentGameSlotIndex = index;
            CurrentGameSlotId = slotId;
        }

        public override bool HasGameSlots => _data._gameSlotIds != null && _data._gameSlotIds.Count > 0;

        public int CurrentGameSlotIndex
        {
            get => _data._currentGameSlotIndex;
            private set => _data._currentGameSlotIndex = value;
        }

        public string CurrentGameSlotId
        {
            get
            {
                int index = _data._currentGameSlotIndex;
                if (index >= 0 && index < _data._gameSlotIds.Count)
                    return _data._gameSlotIds[index];
                else
                    return null;
            }

            private set
            {
                int index = CurrentGameSlotIndex;
                int slotsIdCount = _data._gameSlotIds.Count;

                while (index >= _data._gameSlotIds.Count)
                    _data._gameSlotIds.Add("");

                _data._gameSlotIds[index] = value;
            }
        }
        #endregion Slots
    }

    public abstract class SystemSaveDataHandler : SaveDataHandler
    {
        protected IService_SystemDataSave_Actor _serviceActor;

        public virtual void Init(IService_SystemDataSave_Actor service)
        {
            _serviceActor = service;
        }

        public void Terminate()
        {
            _serviceActor = null;
        }

        public abstract void TryLoad();
        public abstract void TrySave();

        public abstract void GenerateNewGameSlot(string slotId, int index = -1);

        public abstract bool HasGameSlots { get; }
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