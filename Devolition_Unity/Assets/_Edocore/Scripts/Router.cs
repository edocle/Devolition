
using edocle.external;

namespace edocore
{
    /// <summary>
    /// edocore router is the one way to route all edocore features
    /// </summary>
    public class Router
    {
        RouterParameters _parameters;
        public RouterParameters Parameters => _parameters;

        #region Lifecycle

        public Router(RouterParameters parameters)
        {
            _parameters = parameters;
            Init();
        }

        public void Kill()
        {
            Terminate();
        }

        void Init()
        {
            InitServices();
            InitContexts();
        }

        void Terminate()
        {
            TerminateServices();
            TerminateContexts();
        }

        #endregion Lifecycle

        #region Contexts

        public ContextsHandler ContextsHandler { get; private set; }
        void InitContexts()
        {
            ContextsHandler = new ContextsHandler(this);
        }

        void TerminateContexts()
        {
            ContextsHandler?.Terminate();
            ContextsHandler = null;
        }

        #endregion Contexts

        #region Services

        public ServicesHandler ServicesHandler { get; private set; }

        void InitServices()
        {
            ServicesHandler = new ServicesHandler(this);
        }

        void TerminateServices()
        {
            ServicesHandler?.Terminate();
            ServicesHandler = null;
        }

        #endregion Services
    }
}