using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using QuickGraph.Unit.Logging;

namespace QuickGraph.Unit.Core
{
    public class UnitContainer : 
        Container, 
        IServiceContainer,
        IUnitServices
    {
        private IServiceContainer services = new ServiceContainer();
        private ILoggerService loggerService = new LoggerService();

        public UnitContainer()
        {
            this.AddService(typeof(ILoggerService),
                this.loggerService
                );
            Assert.ServiceProvider = this;
        }

        public ILoggerService GetLoggerService()
        {
            return this.loggerService;
        }

        #region IServiceContainer Members
        public void AddService(Type serviceType, object serviceInstance)
        {
            this.services.AddService(serviceType, serviceInstance);
        }
        public void AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
        {
            this.services.AddService(serviceType, callback, promote);
        }
        public void AddService(Type serviceType, ServiceCreatorCallback callback)
        {
            this.services.AddService(serviceType, callback);
        }
        public void AddService(Type serviceType, object serviceInstance, bool promote)
        {
            this.services.AddService(serviceType, serviceInstance, promote);
        }
        public void RemoveService(Type serviceType, bool promote)
        {
            this.services.RemoveService(serviceType, promote);
        }
        public void RemoveService(Type serviceType)
        {
            this.services.RemoveService(serviceType);
        }
        #endregion

        #region IServiceProvider Members
        protected override object GetService(Type serviceType)
        {
            object service = this.services.GetService(serviceType);
            if (service != null)
                return service;
            return base.GetService(serviceType);
        }
        object IServiceProvider.GetService(Type serviceType)
        {
            return this.GetService(serviceType);
        }
        #endregion
    }
}
