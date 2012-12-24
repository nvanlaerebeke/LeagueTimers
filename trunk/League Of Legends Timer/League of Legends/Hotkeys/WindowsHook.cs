using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// Represents the method that will handle the Callback event.
    /// </summary>
    public delegate void CallbackHandler(object sender, CallbackEventArgs e);

    /// <summary>
    /// Defines a hook procedure object to monitor the system for certain types of events.
    /// </summary>
    [ComVisibleAttribute(false), System.Security.SuppressUnmanagedCodeSecurity()]
    public class WindowsHook : IDisposable {
        private const int HC_ACTION = 0;

        private bool handled = false;
        private bool ignoresApplicationFocus = false;

        private IntPtr hookId = IntPtr.Zero;
        private delegate int InternalCallbackHandler(int code, IntPtr wParam, IntPtr lParam);
        // Keep a reference so that the GC won't wipe it!
        private InternalCallbackHandler internalCallback;

        /// <summary>
        /// Occurs when a certain system events fire.
        /// </summary>
        public event CallbackHandler Callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsHook"/> class.
        /// </summary>
        /// <param name="hookType">Type of the hook.</param>
        /// <param name="ignoresApplicationFocus">if set to <c>true</c> the event will ignore the application focus.</param>
        public WindowsHook(HookType hookType, bool ignoresApplicationFocus) {
            this.ignoresApplicationFocus = ignoresApplicationFocus;
            internalCallback = new InternalCallbackHandler(InternalCallback);
            using (Process process = Process.GetCurrentProcess())
            using (ProcessModule module = process.MainModule) {
                hookId = SetWindowsHookEx(hookType, internalCallback, GetModuleHandle(module.ModuleName), 0);
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsHook"/> class.
        /// </summary>
        /// <param name="hookType">Type of the hook.</param>
        public WindowsHook(HookType hookType)
            : this(hookType, false) {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the application handled the hook notification.
        /// </summary>
        /// <value><c>true</c> if handled by the application, other applications will not continue to receive hook notifications; otherwise, <c>false</c>.</value>
        public bool Handled {
            get { return handled; }
            set { handled = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to call the event regardless to the application focus.
        /// </summary>
        /// <value><c>true</c> if the event should ignore the application focus; otherwise, <c>false</c>.</value>
        public bool IgnoresApplicationFocus {
            get { return ignoresApplicationFocus; }
            set { ignoresApplicationFocus = value; }
        }

        #region IDisposable Members

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose() {
            UnhookWindowsHookEx(hookId);
        }

        #endregion

        /// <summary>
        /// Raises the <see cref="E:Callback"/> event.
        /// </summary>
        /// <param name="e">The <see cref="ShortcutVisualizer.Native.CallbackEventArgs"/> instance containing the event data.</param>
        protected virtual void OnCallback(CallbackEventArgs e) {
            CallbackHandler handler = Callback;
            if (handler != null) {
                handler(this, e);
            }
        }

        #region Win32 API

        // Retrieves a module handle for the specified module.
        // The module must have been loaded by the calling process.
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);

        // The SetWindowsHookEx function installs an application-defined hook procedure into a hook chain.
        // You would install a hook procedure to monitor the system for certain types of events.
        // These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(HookType idHook, InternalCallbackHandler lpfn, IntPtr hMod, uint dwThreadId);

        // The UnhookWindowsHookEx function removes a hook procedure installed in a hook chain by the SetWindowsHookEx function.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        // The CallNextHookEx function passes the hook information to the next hook procedure in the current hook chain.
        // A hook procedure can call this function either before or after processing the hook information.
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        // The GetFocus function retrieves the handle to the window that has the keyboard focus,
        // if the window is attached to the calling thread's message queue.
        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        private static extern IntPtr GetFocus();

        #endregion

        private int InternalCallback(int code, IntPtr wParam, IntPtr lParam) {
            bool shouldCallbackFire = ignoresApplicationFocus == true || GetFocus() != IntPtr.Zero;
            if (code >= 0 && shouldCallbackFire) {
                OnCallback(new CallbackEventArgs(wParam, lParam));
            }
            // Calling the CallNextHookEx function to chain to the next hook procedure is optional, but it is highly recommended;
            //    otherwise, other applications that have installed hooks will not receive hook notifications and may behave incorrectly as a result.
            // You should call CallNextHookEx unless you absolutely need to prevent the notification from being seen by other applications.
            return CallNextHookEx(hookId, code, wParam, lParam);
        }
    }

}
