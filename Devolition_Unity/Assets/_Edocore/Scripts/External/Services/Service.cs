using System;

namespace edocore.external.services
{
    public interface IService
    {
        void Init<T>(T actor, Action<bool> callback) where T : IServiceActor;

        void Terminate();
    }

    public abstract class Service : IService
    {
        protected InternalContext _context;

        protected Service(InternalContext context)
        {
            _context = context;
        }

        public abstract void Init<T>(T actor, Action<bool> callback) where T : IServiceActor;

        public abstract void Terminate();
    }
}