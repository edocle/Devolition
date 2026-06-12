using System.Collections.Generic;
using UnityEngine;

namespace edocle.external
{
    [CreateAssetMenu(fileName = "RouterParameters", menuName = "edocore/RouterParameters")]
    public class RouterParameters : ScriptableObject
    {
        #region Data saves

        [Header("Data saves")]
        [SerializeField] private SystemSaveDataHandler _systemSaveDataHandler;
        [SerializeField] private List<GameSaveDataHandler> _gameSaveDataHandlers;

        public SystemSaveDataHandler SystemSaveDataHandler => _systemSaveDataHandler;
        public List<GameSaveDataHandler> GameSaveDataHandlers => _gameSaveDataHandlers;

        #endregion Data saves
    }
}