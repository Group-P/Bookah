using Bookah.Data;
using Bookah.Data.Interfaces;
using System.Web.Mvc;
using Unity;
using Unity.Mvc5;

namespace Bookah.Web
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            container.RegisterType<IUserRepo, UserRepo>();
            container.RegisterType<IBookRepo, BookRepo>();
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}