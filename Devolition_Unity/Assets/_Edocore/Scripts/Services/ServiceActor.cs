using System;

namespace edocore.services
{
    public interface IServiceActor
    {
        void Init(Action callback);
    }
}