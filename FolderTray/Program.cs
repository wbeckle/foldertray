using System.Threading;

namespace FolderTray
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            bool createdNewInstance = false;
            var mutex = new System.Threading.Mutex(true, Application.ProductName, out createdNewInstance);
            if (!createdNewInstance) { return; }
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            Application.Run(new frmMain());
        }
    }
}