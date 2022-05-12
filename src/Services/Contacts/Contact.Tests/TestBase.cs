using Autofac;
using AutoMapper;
using Contact.Application.Contracts.Persistence;

namespace Contact.Tests
{
    public class TestBase
    {
        private Autofac.IContainer _autoFacContainer;

        protected Autofac.IContainer AutoFacContainer
        {
            get
            {
                if (_autoFacContainer == null)
                {
                    var builder = new ContainerBuilder();
                    builder.RegisterModule(new IocModule());
                    var container = builder.Build();
                    _autoFacContainer = container;
                }

                return _autoFacContainer;
            }
        }

        protected IUnitOfWork UnitOfWork
        {
            get
            {
                return AutoFacContainer.Resolve<IUnitOfWork>();
            }
        }

        protected IMapper Mapper
        {
            get
            {
                return AutoFacContainer.Resolve<IMapper>();
            }
        }
    }
}
