using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using XuatThuy.Controls;
using XuatThuy.PLCs;
using XuatThuy.ViewModel;
using XuatThuy.Utils;

namespace XuatThuy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            Debug.WriteLine("App======================");
            //AppDomain currentDomain = AppDomain.CurrentDomain;
            //currentDomain.UnhandledException += new UnhandledExceptionEventHandler(MyHandler);
        }

        static void MyHandler(object sender, UnhandledExceptionEventArgs args)
        {
            Exception e = (Exception)args.ExceptionObject;
            LogUtils.PrintLog("MyHandler caught : " + e.Message);
            LogUtils.PrintLog("UnhandledException StackTrace : " + e.StackTrace);
            LogUtils.PrintLog("Runtime terminating: " +  args.IsTerminating.ToString());

            LogUtils.PrintDebug("MyHandler caught : " + e.Message);
            LogUtils.PrintDebug("UnhandledException StackTrace : " + e.StackTrace);
            LogUtils.PrintDebug("Runtime terminating: " + args.IsTerminating.ToString());
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //Debug.WriteLine("Application_Startup======");
            //SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");

            TaskScheduler.UnobservedTaskException += (s, e) =>
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
        }

        private void LogUnhandledException(Exception exception, string source)
        {
            Debug.WriteLine("LogUnhandledException ===================");
            string message = $"Unhandled exception ({source})";
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception ex)
            {
                LogUtils.PrintLog("Exception in LogUnhandledException. Message: " + ex.Message);
            }
            finally
            {
                LogUtils.PrintLog(message + ". Message: " + exception.Message);
            }
        }
    }
}
