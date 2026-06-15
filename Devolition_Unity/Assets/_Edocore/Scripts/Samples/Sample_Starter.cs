using edocle.external;
using edocle.external.services;
using UnityEngine;

namespace edocle.sample
{
    public class Sample_Starter : Starter
    {
        RouterAccess _access;

        Service_DataSave _dataSaveService;

        protected override void StartGame(RouterAccess access)
        {
            _access = access;
            InitDataSave();
        }


        #region Data save
        void InitDataSave()
        {
            // Get data save service
            _dataSaveService = _access.GetService<Service_DataSave>();
            _dataSaveService.Init(new Service_DataSave_Actor_Json(), (success) =>
            {
                if (success)
                {
                    GetSystemDataSave();
                    GetGameDataSaves();
                }
                else
                {
                    // Failed to initialize data save service
                }
            });
        }

        #region System

        void GetSystemDataSave()
        {
            Sample_SystemSaveHandler systemDataHandler = _dataSaveService.GetData<Sample_SystemSaveHandler>();
            Debug.Log($"System sample> SampleInt>{systemDataHandler.SampleInt}, SampleString>{systemDataHandler.SampleString}");
            systemDataHandler.TryLoad();

            systemDataHandler.SampleInt++;
            systemDataHandler.TrySave();
        }

        #endregion System

        #region Game

        void GetGameDataSaves()
        {
            Sample_GameSaveHandler_a gameDataHandlerA = _dataSaveService.GetData<Sample_GameSaveHandler_a>();
            Debug.Log($"Game sample A> SampleInt>{gameDataHandlerA.SampleInt}, SampleString>{gameDataHandlerA.SampleString}");


            Sample_GameSaveHandler_b gameDataHandlerB = _dataSaveService.GetData<Sample_GameSaveHandler_b>();
            Debug.Log($"Game sample B> SampleInt>{gameDataHandlerB.SampleInt}, SampleString>{gameDataHandlerB.SampleString}");

            Sample_SystemSaveHandler systemDataHandler = _dataSaveService.GetData<Sample_SystemSaveHandler>();

            if (!systemDataHandler.HasGameSlots || string.IsNullOrEmpty(systemDataHandler.CurrentGameSlotId))
                _dataSaveService.GenerateNewGameSlot("Sample_001");
            else
                _dataSaveService.LoadGameSlot(systemDataHandler.CurrentGameSlotId);


            gameDataHandlerA.SampleString = "test1";
            Debug.Log($"Game sample A> SampleString>{gameDataHandlerA.SampleString}");
            gameDataHandlerA.TrySave();

            gameDataHandlerB.SampleString = "test2";

            gameDataHandlerA.SampleString = "test3";
            Debug.Log($"Game sample A> SampleString>{gameDataHandlerA.SampleString}");
            gameDataHandlerA.TryLoad();

            Debug.Log($"Game sample A> SampleString>{gameDataHandlerA.SampleString} (should return to previous state)");
        }

        #endregion Game

        #endregion Data Save
    }
}