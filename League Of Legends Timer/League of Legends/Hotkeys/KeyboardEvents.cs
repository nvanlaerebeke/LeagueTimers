using System;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// Delegate used for when keyboard keys are pressed
    /// </summary>
    /// <param name="pKey"></param>
    /// <param name="pModifier"></param>
    public delegate void KeyPressed(Keys pKey, ModifierKeys pModifier);

    /// <summary>
    /// Provides an independent high level interface for the keyboard events.
    /// </summary>
    public class KeyboardEvents : IDisposable {
        /// <summary>
        /// Occurs when a key is pressed.
        /// </summary>
        public event KeyPressed KeyPress;

        private WindowsHook keyboardHook;

        // The WM_KEYDOWN message is posted to the window with the keyboard focus when a nonsystem key is pressed.
        // A nonsystem key is a key that is pressed when the ALT key is not pressed.
        private const int WM_KEYDOWN = 0x0100;

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardEvents"/> class.
        /// </summary>
        /// <param name="ignoresApplicationFocus">if set to <c>true</c> the keyboard events will raise regardless to the application focus.</param>
        public KeyboardEvents(bool ignoresApplicationFocus) {
            keyboardHook = new WindowsHook(HookType.KEYBOARD_LL, ignoresApplicationFocus);
            keyboardHook.Callback += new CallbackHandler(keyboardHook_Callback);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyboardEvents"/> class.
        /// </summary>
        public KeyboardEvents()
            : this(false) {
        }

        #region Win32 API
        /// <summary>
        /// Translates a character to the corresponding virtual-key code and shift state for the current keyboard 
        /// see: http://msdn.microsoft.com/en-us/library/ms646329(VS.85).aspx
        /// </summary>
        /// <param name="ch"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern short VkKeyScan(char ch);

        #endregion

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            keyboardHook.Dispose();
        }

        #endregion

        #region Private Core Methods
        /// <summary>
        /// Handles the key press event that was caught
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void keyboardHook_Callback(object sender, CallbackEventArgs e) {
            //Get the modifiers(ctrl/shift/alt)
            ModifierKeys objModifiers = 0;
            if (KeyboardKeyState.IsAltKeyDown()) { objModifiers += 1; }
            if (KeyboardKeyState.IsControlKeyDown()) { objModifiers += 2; }
            if (KeyboardKeyState.IsShiftKeyDown()) { objModifiers += 4; }

            // Copy the data into KeyboardHookStruct
            KeyboardHookStruct keyboardHookStruct = (KeyboardHookStruct)Marshal.PtrToStructure(e.LParam, typeof(KeyboardHookStruct));

            // Cast the virtual-key codes into the enumeration of Keys
            Keys keyData = (Keys)keyboardHookStruct.vkCode;

            if (KeyPress != null && ((int)e.WParam.ToInt32()) == WM_KEYDOWN) {
                KeyPress(keyData, objModifiers);
            }

            //mark the key hit as 'handled'\
            keyboardHook.Handled = true;
        }

        #endregion
    }
   
}
