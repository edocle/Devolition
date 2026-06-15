using edocle.external;
using UnityEngine;

namespace edocle.sample
{
    [CreateAssetMenu(fileName = "Sample_GameSaveHandler_b", menuName = "edocore/Sample/Data/Sample_GameSaveHandler_b")]
    public class Sample_GameSaveHandler_b : GameSaveDataHandler<Sample_GameSave_b>
    {
        public int SampleInt
        {
            get => _data._sampleInt;
            set => _data._sampleInt = value;
        }

        public string SampleString
        {
            get => _data._sampleString;
            set => _data._sampleString = value;
        }
    }

    [System.Serializable]
    public class Sample_GameSave_b : GameSaveData
    {
        public int _sampleInt;
        public string _sampleString;
    }
}