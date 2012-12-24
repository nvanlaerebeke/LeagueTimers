using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace League_Of_Legends_Timer
{
    static class LcdManager {
        public static void Init(League_Of_Legends_Timer.LcdHelper.GetBitmap pDelegate) {
            LcdHelper.Connect(pDelegate, Settings.ForceAppletToFront);
            Application.ApplicationExit += new EventHandler(Application_ApplicationExit);
            LcdHelper.StartDrawing();
        }

        static void Application_ApplicationExit(object sender, EventArgs e) {
            LcdHelper.Disconnect();
        }
    }
}