using edocle.core;
using UnityEngine;

namespace edocle.external
{
    public abstract class Starter : MonoBehaviour
    {
        // Links
        [SerializeField] private RouterParameters _routerParameters;

        void Awake()
        {
            Router router = new Router(_routerParameters);
            StartGame(new RouterAccess(router));
        }

        abstract protected void StartGame(RouterAccess access);
    }
}