using System.Collections.Generic;
using KeyWatcher;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// Delegate used for the keyPressed events
    /// </summary>
    /// <param name="pHotKey">Key that was pressed</param>
    public delegate void keyPressed(HotKey pHotKey);

    /// <summary>
    /// Class that will listen to all incomming key hits and raises the keyPressed event if needed
    /// </summary>
    public class LeagueKeyListener {
        /// <summary>
        /// List of keys the class is listening to
        /// </summary>
        private static List<HotKey> _lstKeys;

        /// <summary>
        /// Event raised when a key that it's listening to is pressed
        /// </summary>
        public static event keyPressed keyPressed;

        /// <summary>
        /// Keyboard events 
        /// </summary>
        private static KeyboardEvents objEvents;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pKeys">List of keys that need to be listen to</param>
        public static void Listen(List<HotKey> pKeys) {
            _lstKeys = pKeys;

            //Listen to all keyboard hits
            objEvents = new KeyboardEvents(true);
            objEvents.KeyPress += new KeyPressed(objEvents_KeyPress);
        }

        /// <summary>
        /// Will raise the keyPressed event if the key that was press is one that it's listening for
        /// </summary>
        /// <param name="pKey">Key that was pressed</param>
        /// <param name="pModifier">Mofidiers that were pressed</param>
        private static void objEvents_KeyPress(System.Windows.Forms.Keys pKey, ModifierKeys pModifier) {
            //make it a hotkey first
            HotKey objHotkey = new HotKey(pKey, pModifier);

            //Check if anyone is listening and if the key is in our to watch list
            if(_lstKeys.Contains(objHotkey) && keyPressed != null) {
                keyPressed(objHotkey);
            }
        }
    }
}
