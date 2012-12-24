using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace League_Of_Legends_Timer {
    public static class KeyboardKeyState {
        // Virtual key codes.
        // http://www.codeproject.com/KB/system/keyboard.aspx
        #region Win32 API

        // The GetKeyState function retrieves the status of the specified virtual key.
        // The status specifies whether the key is up, down, or toggled (on, off—alternating each time the key is pressed).
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
        private static extern short GetKeyState(Keys key);

        #endregion

        /// <summary>
        /// Determines whether [is shift key down].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is shift key down]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsShiftKeyDown() {
            return IsModifierKeyDown(Keys.ShiftKey, Keys.Shift);
        }

        /// <summary>
        /// Determines whether [is alt key down].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is alt key down]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsAltKeyDown() {
            return IsModifierKeyDown(Keys.Menu, Keys.Alt);
        }

        /// <summary>
        /// Determines whether [is control key down].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is control key down]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsControlKeyDown() {
            return IsModifierKeyDown(Keys.ControlKey, Keys.Control);
        }

        /// <summary>
        /// Determines whether [is caps lock on].
        /// </summary>
        /// <returns>
        ///     <c>true</c> if [is caps lock on]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCapsLockOn() {
            return GetKeyState(Keys.CapsLock) != 0;
        }

        private static bool IsModifierKeyDown(Keys virtualKey, Keys key) {
            Keys none = Keys.None;
            if (GetKeyState(virtualKey) < 0) {
                none |= key;
            }
            return (none & key) == key;
        }
    }
}
