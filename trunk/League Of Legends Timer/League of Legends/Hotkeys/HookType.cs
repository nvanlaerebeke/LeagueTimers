using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// List with different hooks
    /// see: http://msdn.microsoft.com/en-us/library/windows/desktop/ms644959(v=vs.85).aspx
    /// </summary>
    public enum HookType {
        CALLWNDPROC = 4,
        CALLWNDPROCRET = 12,
        CBT = 5, DEBUG = 9,
        FOREGROUNDIDLE = 11,
        GETMESSAGE = 3,
        JOURNALPLAYBACK = 1,
        JOURNALRECORD = 0,
        KEYBOARD = 2,
        KEYBOARD_LL = 13,
        MOUSE = 7,
        MOUSE_LL = 14,
        MSGFILTER = -1,
        SHELL = 10,
        SYSMSGFILTER = 6
    }
}
