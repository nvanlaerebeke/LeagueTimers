using System;
using System.Windows.Forms;

namespace League_Of_Legends_Timer {

    /// <summary>
    /// HotKey class that contains the key with the modifier
    /// </summary>
    public struct HotKey : IEquatable<HotKey> {
        /// <summary>
        /// Modifier
        /// </summary>
        private ModifierKeys _objModifier;

        /// <summary>
        /// Key
        /// </summary>
        private Keys _objKeys;

        /// <summary>
        /// Constuructor
        /// </summary>
        /// <param name="pKeys"></param>
        /// <param name="pModifier"></param>
        public HotKey(Keys pKeys, ModifierKeys pModifier) {
            _objKeys = pKeys;
            _objModifier = pModifier;
        }

        /// <summary>
        /// Key
        /// </summary>
        public Keys Key { get { return _objKeys; } }

        /// <summary>
        /// Modifier
        /// </summary>
        public ModifierKeys Modifier {
            get { return _objModifier; }
        }

        /// <summary>
        /// IEquqtable interface method
        /// Used to compare 1 HotKey to another
        /// </summary>
        /// <param name="pHotKey">HotKey to compare to</param>
        /// <returns>true if equal</returns>
        public bool Equals(HotKey pHotKey) {
            if (pHotKey._objKeys == _objKeys && pHotKey._objModifier == _objModifier) {
                return true;
            }
            return false;
        }
    }
}
