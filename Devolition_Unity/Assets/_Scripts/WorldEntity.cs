using UnityEngine;

public abstract class WorldEntity: MonoBehaviour
{
    private Transform _transform;

    public void Awake()
    {
        _transform = transform;
        Debug.Log($"WorldEntity initialized with transform: {_transform}");
    }

    public abstract void Init();
}