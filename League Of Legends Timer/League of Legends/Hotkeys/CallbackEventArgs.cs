using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// Callback event args we get back from the windowshook
    /// </summary>
    public class CallbackEventArgs : EventArgs {
        private IntPtr wParam;
        private IntPtr lParam;

        public CallbackEventArgs(IntPtr wParam, IntPtr lParam) {
            this.wParam = wParam;
            this.lParam = lParam;
        }

        /// <summary>
        /// Gets the wParam value passed to the current hook procedure. (The meaning of this parameter depends on the type of hook associated with the current hook chain.)
        /// </summary>
        /// <value>The W param.</value>
        public IntPtr WParam {
            get { return wParam; }
        }

        /// <summary>
        /// Gets the lParam value passed to the current hook procedure. (The meaning of this parameter depends on the type of hook associated with the current hook chain.)
        /// </summary>
        /// <value>The L param.</value>
        public IntPtr LParam {
            get { return lParam; }
        }
    }
}
