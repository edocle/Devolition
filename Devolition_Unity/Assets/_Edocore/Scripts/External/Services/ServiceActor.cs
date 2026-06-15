using System;

namespace edocle.external.services
{
    public interface IServiceActor
    {
        void Init(Action<bool> callback);
    }
}