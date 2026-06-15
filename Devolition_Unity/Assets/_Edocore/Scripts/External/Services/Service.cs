using edocle.core;
using System;

namespace edocle.external.services
{
    public interface IService
    {
        void Init<A>(A actor, Action<bool> callback) where A : IServiceActor;

        void Terminate();
    }

    public abstract class Service<A> : Service where A : IServiceActor
    {
        protected Service(InternalContext context) : base(context)
        { }
    }

    public abstract class Service : IService
    {
        protected InternalContext _context;

        protected Service(InternalContext context)
        {
            _context = context;
        }

        public abstract void Init<A>(A actor, Action<bool> callback) where A : IServiceActor;

        public abstract void Terminate();
    }
}