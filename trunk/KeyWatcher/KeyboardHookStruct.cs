using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KeyWatcher {
    /// <summary>
    /// Contains information returned by the WindowsHook
    /// </summary>
    public struct KeyboardHookStruct {
        public int vkCode;
        public int scanCode;
        public int flags;
        private int time;
        private int dwExtraInfo;
    }
}
