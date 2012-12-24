﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace League_Of_Legends_Timer {
    /// <summary>
    /// The enumeration of possible modifiers.
    /// </summary>
    [Flags]
    public enum ModifierKeys : uint {
        None = 0,
        Alt = 1,
        Control = 2,
        Shift = 4,
        Win = 8
    }
}
