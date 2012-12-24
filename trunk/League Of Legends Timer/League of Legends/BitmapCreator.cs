using System;
using System.Collections.Generic;
using System.Drawing;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// Class responsable for creating the Bitmap that will be displayed on the LCD
    /// </summary>
    static class BitmapCreator {
        /// <summary>
        /// Bitmap that is currently displayed
        /// </summary>
        private static Bitmap _btmCurrent;

        /// <summary>
        /// Initialized(creates) the bitmap
        /// </summary>
        /// <param name="pColorLCD">Color LCD or not</param>
        private static void init(bool pColorLCD) {
            //dimentions are differnt for b&w and color LCD's
            if (pColorLCD) {
                _btmCurrent = new Bitmap(320, 240);
            } else {
                _btmCurrent = new Bitmap(160, 43);
            }

            //create an empty Bitmap
            Graphics g = Graphics.FromImage(_btmCurrent);
            g.Clear(Color.White);
            g.Dispose();
        }

        /// <summary>
        /// Creates the Bitmap that needs to be displayed
        /// </summary>
        /// <param name="pText">Text to display, each element contains 1 line. Maximum 4 lines  will  fit on a b&w  display</param>
        /// <param name="pColorLCD">Color display or not</param>
        /// <returns></returns>
        public static IntPtr Create(Dictionary<string, int> pText, bool pColorLCD = false) {
            //Initialize the bitmap first before it is modified
            if (_btmCurrent == null) { init(pColorLCD); }
        
            //make sure only 1 thread at a time can modify the bitmap
            lock (_btmCurrent) {
                try {
                    Graphics g = Graphics.FromImage(_btmCurrent);
                    g.Clear(Color.White);
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

                    int intMargin = 0;
                    //ToDp: calculate the text height and set a more correct margin - don't know the result for color LCD displays
                    foreach (KeyValuePair<string, int> kvpLine in pText) {
                        g.DrawString(kvpLine.Key, new Font(Settings.Font, kvpLine.Value, FontStyle.Regular), SystemBrushes.WindowText, 0, intMargin);
                        if (kvpLine.Value > 10) {
                            intMargin += 20;
                        } else {
                            intMargin += 10;
                        }
                    }
                } catch { } 

                //return the new bitmap(as intpr)
                return _btmCurrent.GetHbitmap();
            }
        }
    }
}
