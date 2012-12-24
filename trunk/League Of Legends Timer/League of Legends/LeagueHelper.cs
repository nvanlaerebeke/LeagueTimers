using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LgLCD;

namespace League_Of_Legends_Timer {
    class LeagueHelper {
        /// <summary>
        /// Object that keeps track of all league of legends timers
        /// </summary>
        LeagueTimers _objLeagueTimers;

        #region Public methods
        /// <summary>
        /// Constructor
        /// Will register all hostkeys and start the timers
        /// </summary>
        public LeagueHelper() {
            //Initilize the timers
            _objLeagueTimers = new LeagueTimers();

            //Listen to the LCD display button events 
            LcdHelper.buttonPressed += new buttonPressed(LcdHelper_buttonPressed);

            //Register all the hotkeys
            List<HotKey> lstKeys = new List<HotKey>();
            lstKeys.Add(new HotKey(Keys.F1, ModifierKeys.Shift | ModifierKeys.Control | ModifierKeys.Alt));
            lstKeys.Add(new HotKey(Keys.F2, ModifierKeys.Shift | ModifierKeys.Control | ModifierKeys.Alt));
            lstKeys.Add(new HotKey(Keys.F3, ModifierKeys.Shift | ModifierKeys.Control | ModifierKeys.Alt));
            lstKeys.Add(new HotKey(Keys.F4, ModifierKeys.Shift | ModifierKeys.Control | ModifierKeys.Alt));
            lstKeys.Add(new HotKey(Keys.F5, ModifierKeys.Shift | ModifierKeys.Control | ModifierKeys.Alt));
            lstKeys.Add(new HotKey(Keys.F6, ModifierKeys.Shift | ModifierKeys.Control | ModifierKeys.Alt));
            LeagueKeyListener.Listen(lstKeys);

            //start listening to events
            LeagueKeyListener.keyPressed += new keyPressed(LeagueKeyListener_keyPressed);
        }

        /// <summary>
        /// Function that returns the bitmap that will be displayed on the LCD panel
        /// </summary>
        /// <returns></returns>
        public IntPtr GetBitmap() {
            return BitmapCreator.Create(_objLeagueTimers.ToString());
        }

        #endregion

        #region Eventhanlders

        /// <summary>
        /// Event handler for the hotkeys
        /// </summary>
        /// <param name="pHotKey">Key that was pressed</param>
        private void LeagueKeyListener_keyPressed(HotKey pHotKey) {
            try {
                switch (pHotKey.Key) {
                    case Keys.F1:
                        _objLeagueTimers.Reset(LeagueType.RedOur);
                        break;
                    case Keys.F2:
                        _objLeagueTimers.Reset(LeagueType.RedTheir);
                        break;
                    case Keys.F3:
                        _objLeagueTimers.Reset(LeagueType.Dragon);
                        break;
                    case Keys.F4:
                        _objLeagueTimers.Reset(LeagueType.BlueOur);
                        break;
                    case Keys.F5:
                        _objLeagueTimers.Reset(LeagueType.BlueTheir);
                        break;
                    case Keys.F6:
                        _objLeagueTimers.Reset(LeagueType.Baron);
                        break;
                }
            } catch {
                Console.WriteLine("Failed to reset");
            }
        }

        /// <summary>
        /// Event handler that handles the LCD panel button presses
        /// </summary>
        /// <param name="pButton">Button identifier</param>
        private void LcdHelper_buttonPressed(LCDButton pButton) {
            try {
                switch (pButton) {
                    case LCDButton.BUTTON_1:
                        _objLeagueTimers.Start(LeagueType.All);
                        break;
                    case LCDButton.BUTTON_2:
                        _objLeagueTimers.Stop(LeagueType.All);
                        break;
                    case LCDButton.BUTTON_3:
                        _objLeagueTimers.Reset(LeagueType.All);
                        break;
                    case LCDButton.BUTTON_4:
                        break;
                }
            } catch {
                Console.WriteLine("Unable to handle button press");
            }
        }       
        #endregion
      
    }
}