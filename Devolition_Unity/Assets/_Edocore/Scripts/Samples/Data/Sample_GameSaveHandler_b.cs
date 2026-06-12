using edocle.external;
using UnityEngine;

[CreateAssetMenu(fileName = "Sample_GameSaveHandler_b", menuName = "edocore/Sample/Data/Sample_GameSaveHandler_b")]
public class Sample_GameSaveHandler_b : GameSaveDataHandler<Sample_GameSave_b>
{

}

public class Sample_GameSave_b : GameSaveData
{
    public int _exampleInt;
    public string _exampleString;
}