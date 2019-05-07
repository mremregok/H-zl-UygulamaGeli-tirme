using Autofac;
using System;
using System.Collections.Generic;
using System.Text;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Business.Abstract;
using Business.Concrete;

namespace Business
{
    public static class IOCUtil
    {
        public static IContainer Container { get; set; }

        static IOCUtil()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<BolumDal>().As<IBolumDal>();
            builder.RegisterType<BolumManager>().As<IBolumService>();

            Container = builder.Build();
        }
    }
}
