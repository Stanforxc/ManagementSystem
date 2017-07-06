using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition.Primitives;
using System.Reflection;
using Autofac;

namespace Infrastructure.Resolver
{
    public static class ComponentLoader
    {
        public static void LoadContainer(ContainerBuilder container,string path,string pattern)
        {
            var dirCat = new DirectoryCatalog(path, pattern);
            var imporDef = BuildImportDefinition();
            try
            {
                using(var aggregateCatalog = new AggregateCatalog())
                {
                    aggregateCatalog.Catalogs.Add(dirCat);
                    using(var compositionContainer = new CompositionContainer(aggregateCatalog))
                    {
                        IEnumerable<Export> exports = compositionContainer.GetExports(imporDef);
                        IEnumerable<IComponent> modules =
                            exports.Select(export => export.Value as IComponent).Where(m => m != null);
                        var registerComponent = new RegisterComponent(container);
                        foreach(IComponent module in modules)
                        {
                            module.SetUp(registerComponent);
                        }
                    }
                }
                
            }catch(ReflectionTypeLoadException typeLoadExepetion)
            {
                var builder = new StringBuilder();
                foreach(Exception loaderException in typeLoadExepetion.LoaderExceptions)
                {
                    builder.AppendFormat("{0}\n", loaderException.Message);
                }
                throw new TypeLoadException(builder.ToString(), typeLoadExepetion);
            }
        }

        private static ImportDefinition BuildImportDefinition()
        {
            return new ImportDefinition(
                def => true, typeof(IComponent).FullName, ImportCardinality.ZeroOrMore, false, false);
        }
    }

    internal class RegisterComponent : IRegisterComponent
    {
        private readonly ContainerBuilder _container;
        public RegisterComponent(ContainerBuilder container)
        {
            this._container = container;
        }

        public void RegisterType<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            if (withInterception)
            {

            }
            else
            {
                this._container.RegisterType<TTo>().As<TFrom>();
            }
        }

        public void RegisterTypeWithControlledLifeTime<TFrom, TTo>(bool withInterception = false) where TTo : TFrom
        {
            this._container.RegisterType<TTo>().As<TFrom>().InstancePerRequest();
        }
    }
}
