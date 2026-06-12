using edocore.external.services;
using System;
using System.Collections.Generic;

namespace edocore
{

    /// <summary>
    ///  Contains all edocore services
    ///  Used to access or generate them
    /// </summary>
    public class ServicesHandler
    {
        Router _router;
        public ServicesHandler(Router router)
        {
            _router = router;
        }

        HashSet<Service> _services = new HashSet<Service>();

        public T Get<T>() where T : Service
        {
            // Return right type of service if already generated
            foreach (var service in _services)
            {
                if (service is T tService)
                    return tService;
            }

            // Generate service
            var newService = GenerateService<T>();
            return newService;
        }

        T GenerateService<T>() where T : Service
        {
            var newService = (T)Activator.CreateInstance(typeof(T), _router.ContextsHandler.InternalContext);
            _services.Add(newService);
            return newService;
        }

        public void Terminate()
        {
            foreach (var item in _services)
                item.Terminate();

            _services.Clear();
        }
    }
}