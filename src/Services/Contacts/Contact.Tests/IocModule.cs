using Autofac;
using AutoMapper;
using Contact.Application.Contracts.Persistence;
using Contact.Application.Mappings;
using Contact.Infrastructure.Persistence;
using Contact.Infrastructure.Repositories;
using System.Collections.Generic;

namespace Contact.Tests
{
    public class IocModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ContactContext>();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).AsSelf().InstancePerLifetimeScope();

            builder.RegisterType<MappingProfile>().As<Profile>();
            builder.Register(c => new MapperConfiguration(cfg =>
            {
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>().CreateMapper(c.Resolve)).As<IMapper>().InstancePerLifetimeScope();


            base.Load(builder);
        }
    }
}
