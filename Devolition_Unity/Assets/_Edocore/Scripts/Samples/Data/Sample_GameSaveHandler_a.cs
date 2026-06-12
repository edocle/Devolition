using edocle.external;
using UnityEngine;

[CreateAssetMenu(fileName = "Sample_GameSaveHandler_a", menuName = "edocore/Sample/Data/Sample_GameSaveHandler_a")]
public class Sample_GameSaveHandler_a : GameSaveDataHandler<Sample_GameSave_a>
{

}

[System.Serializable]
public class Sample_GameSave_a : GameSaveData
{
    public int _exampleInt;
    public string _exampleString;
}