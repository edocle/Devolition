
using edocle.core;
using edocle.external.services;

namespace edocle.external
{
    public class RouterAccess
    {
        Router _router;

        public RouterAccess(Router router)
        {
            _router = router;
        }

        #region Calls

        #region Services

        public T GetService<T>() where T : Service
        {
            return _router.ServicesHandler.Get<T>();
        }

        #endregion Services

        public void Terminate()
        {
            _router.Kill();
            _router = null;
        }

        #endregion Calls
    }
}