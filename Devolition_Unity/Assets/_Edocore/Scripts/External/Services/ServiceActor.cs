using System;

namespace edocore.external.services
{
    public interface IServiceActor
    {
        void Init(Action<bool> callback);
    }
}