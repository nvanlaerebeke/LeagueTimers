using System;
using System.Windows.Forms;
using LgLCD.Net;

namespace League_Of_Legends_Timer
{
    
    static class Program
    {
        /// <summary>
        /// League Helper
        /// Handles all actions and functionallity related to league of legends
        /// </summary>
        private static LeagueHelper _objLeagueHelper;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //This object will manage all league of legends related actions
            _objLeagueHelper = new LeagueHelper();

            //Initialize the lcd display, also the callback function that will supply the bitmap to display
            LcdManager.Init(Settings.AppletName, Settings.ForceAppletToFront, _objLeagueHelper.GetBitmap);

            //run the application
            Application.Run();
        }
    }
}
