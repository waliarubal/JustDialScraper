using Nancy.TinyIoc;
using System;

namespace JustDialScraper.Common
{
    public sealed class ServiceLocator: IDisposable
    {
        static ServiceLocator _instance;
        static object _syncRoot;

        static ServiceLocator()
        {
            _syncRoot = new object();
        }

        private ServiceLocator()
        {
            Container = new TinyIoCContainer();
        }

        #region properties

        public static ServiceLocator Instance
        {
            get
            {
                lock (_syncRoot)
                {
                    if (_instance == null)
                        _instance = new ServiceLocator();

                    return _instance;
                }
            }
        }

        internal TinyIoCContainer Container { get; }

        #endregion

        public bool Unregister<T>()
        {
            return Container.Unregister<T>();
        }

        public void RegisterSingleton<TInterface, T>() where TInterface : class where T : class, TInterface
        {
            Container.Register<TInterface, T>().AsSingleton();
        }

        public void Register<T>() where T : class
        {
            Container.Register<T>();
        }

        public T Resolve<T>() where T : class
        {
            return Container.Resolve<T>();
        }

        public void Dispose()
        {
            Container.Dispose();
        }
    }
}
