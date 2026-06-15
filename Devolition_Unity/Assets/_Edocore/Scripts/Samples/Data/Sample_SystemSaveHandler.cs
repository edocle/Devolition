using edocle.external;
using UnityEngine;

namespace edocle.sample
{
    [CreateAssetMenu(fileName = "Sample_SystemSaveHandler", menuName = "edocore/Sample/Data/Sample_SystemSaveHandler")]
    public class Sample_SystemSaveHandler : SystemSaveDataHandler<Sample_SystemSave>
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
    public class Sample_SystemSave : SystemSaveData
    {
        public int _sampleInt;
        public string _sampleString;
    }
}