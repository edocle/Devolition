
using edocle.external;

namespace edocore
{
    public class ContextsHandler
    {
        Router _router;

        /// <summary>
        /// Has access to every features edocore has to offer
        /// </summary>
        private InternalContext _internalContext;

        /// <summary>
        /// Has access to limited features edocore has, safe to be used by external code
        /// </summary>
        private ExternalContext _externalContext;

        public ContextsHandler(Router router)
        {
            _router = router;
        }

        public void Terminate()
        {
            _router = null;
            _internalContext = null;
            _externalContext = null;
        }

        #region Calls
        public InternalContext InternalContext
        {
            get
            {
                if (_internalContext == null)
                {
                    _internalContext = new InternalContext (
                        _router.ServicesHandler,
                        _router.Parameters);
                }
                return _internalContext;
            }
        }

        public ExternalContext ExternalContext
        {
            get
            {
                if (_externalContext == null)
                {
                    _externalContext = new ExternalContext(_router.ServicesHandler);
                }
                return _externalContext;
            }
        }

        #endregion Calls
    }

    public class ExternalContext : IContext
    {
        public ExternalContext(ServicesHandler servicesHandler)
        {
            ServicesHandler = servicesHandler;
        }

        public ServicesHandler ServicesHandler { get; private set; }
    }

    public class InternalContext : IContext
    {
        public InternalContext(ServicesHandler servicesHandler, RouterParameters routerParameters)
        {
            ServicesHandler = servicesHandler;
            RouterParameters = routerParameters;
        }

        public RouterParameters RouterParameters { get; private set; }
        public ServicesHandler ServicesHandler { get; private set; }
    }

    public interface IContext
    {

    }
}