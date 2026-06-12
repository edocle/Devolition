using edocle.external;
using UnityEngine;

[CreateAssetMenu(fileName = "Sample_SystemSaveHandler", menuName = "edocore/Sample/Data/Sample_SystemSaveHandler")]
public class Sample_SystemSaveHandler : SystemSaveDataHandler<Sample_SystemSave>
{
    
}

public class Sample_SystemSave : SystemSaveData
{
    public int _exampleInt;
    public string _exampleString;
}