using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Timers;

namespace League_Of_Legends_Timer {
    public delegate void buttonPressed(int pButton);
    
    static class LcdHelper {
        public delegate IntPtr GetBitmap();
        public static event buttonPressed buttonPressed;

        private static int _intDevice = 0;
        private static int _intDeviceType = 0;
        private static int _intConnection = 0;

        private static GetBitmap _delGetBitmapFunction;

        /// <summary>
        /// Sets up the initial connection with the keyboard
        /// Also makes the applet pop up to the front
        /// </summary>
        public static void Connect(GetBitmap pDelegate, bool pForceForground) {
            _delGetBitmapFunction = pDelegate;

            if (DMcLgLCD.ERROR_SUCCESS == DMcLgLCD.LcdInit()) {
                _intConnection = DMcLgLCD.LcdConnect(Settings.AppletName, 0, 0);

                if (DMcLgLCD.LGLCD_INVALID_CONNECTION != _intConnection) {
                    _intDevice = DMcLgLCD.LcdOpenByType(_intConnection, DMcLgLCD.LGLCD_DEVICE_QVGA);

                    if (DMcLgLCD.LGLCD_INVALID_DEVICE == _intDevice) {
                        _intDevice = DMcLgLCD.LcdOpenByType(_intConnection, DMcLgLCD.LGLCD_DEVICE_BW);
                        if (DMcLgLCD.LGLCD_INVALID_DEVICE != _intDevice) {
                            _intDeviceType = DMcLgLCD.LGLCD_DEVICE_BW;
                        }
                    } else {
                        _intDeviceType = DMcLgLCD.LGLCD_DEVICE_QVGA;
                    }
                }
                SetBitmap(_delGetBitmapFunction());

                if (pForceForground) {
                    DMcLgLCD.LcdSetAsLCDForegroundApp(_intDevice, DMcLgLCD.LGLCD_FORE_YES);
                }
            }

            //Set callback function for the keyboard buttons - raise the event 
            if (_intDeviceType > 0) {
                DMcLgLCD.LcdSetButtonCallback(delegate(int pDeviceType, int pButton) {
                    if (buttonPressed != null) {
                        buttonPressed(pButton);
                    }
                });
            }
       }

        public static void SetBitmap(IntPtr pBitmap) {
            DMcLgLCD.LcdUpdateBitmap(_intDevice, pBitmap, DMcLgLCD.LGLCD_DEVICE_BW);
        }


        internal static void Disconnect() {
            DMcLgLCD.LcdClose(_intDevice);
            DMcLgLCD.LcdDisconnect(_intConnection);
            DMcLgLCD.LcdDeInit();
        }

        public static void StartDrawing() {
            Timer objTimer = new Timer(100);
            objTimer.Elapsed += new ElapsedEventHandler(
                delegate {
                    SetBitmap(_delGetBitmapFunction());
                }
            );
            objTimer.Start();
        }
    }
}
