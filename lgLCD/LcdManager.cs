using System;
using System.Windows.Forms;

namespace LgLCD {
    /// <summary>
    /// Class that manages the initialization and closing of the LCD display connection
    /// </summary>
    public static class LcdManager {
        /// <summary>
        /// Initialization function for the LCD connection
        /// </summary>
        /// <param name="pAppletName">Display Name</param>
        /// <param name="pForceToFront">Make the applet show up right away</param>
        /// <param name="pDelegate">Function that will provide the bitmap to be displayed, function must return an IntPtr to the bitmap</param>
        public static void Init(string pAppletName, bool pForceToFront, LcdHelper.GetBitmap pDelegate) {
            //Set up the initial connection & start drawing on the display
            LcdHelper.Connect(pAppletName, pForceToFront, pDelegate);

            //make sure to close the connection when the app closes
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
        }

        /// <summary>
        /// Disconnects the application from the LCD
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void Application_ApplicationExit(object sender, EventArgs e) {
            LcdHelper.Disconnect();
        }
    }
}