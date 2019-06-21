using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependencyInjectionDemo.Services;

namespace DependancyInjectionDemo
{
    public class AutofactModules : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlDataManager>().As<IDataManager>();
        }
    }
}