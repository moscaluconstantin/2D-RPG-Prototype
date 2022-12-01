using Assets._2D_RPG_Prototype.Code.Infrastructure.Services;
using System;
using System.Collections.Generic;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure
{
    public static class ServiceProvider
    {
        private static Dictionary<Type, IService> _services;

        static ServiceProvider() =>
            _services = new();

        public static void AddService<T>(IService service) where T : IService
        {
            if (_services.ContainsKey(typeof(T)))
            {
                _services[typeof(T)] = service;
                return;
            }

            _services.Add(typeof(T), service);
        }

        public static T GetService<T>() where T : IService
        {
            if (_services.TryGetValue(typeof(T), out var service))
                return (T)service;

            return default;
        }
    }
}
