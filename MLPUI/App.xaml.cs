using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace MLPUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
           
            AppDomain.CurrentDomain.AssemblyResolve += Resolver;

        }

        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {

            if (args.Name.StartsWith("sxlib"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string binPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin\\sxlib", assemblyName);

                return File.Exists(binPath) ? Assembly.LoadFile(binPath) : null;
            }

            return null;
        }
    }
}
