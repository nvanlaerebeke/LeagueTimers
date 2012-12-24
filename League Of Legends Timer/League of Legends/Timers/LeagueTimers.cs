using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace League_Of_Legends_Timer {
    class LeagueTimers {
        /// <summary>
        /// Timer for the blue buff 
        /// </summary>
        private LeagueTimer _tmrBlueOur;

        /// <summary>
        /// Timer for the enemy blue buff
        /// </summary>
        private LeagueTimer _tmrBlueTheir;

        /// <summary>
        /// Timer for the red buff
        /// </summary>
        private LeagueTimer _tmrRedOur;

        /// <summary>
        /// Timer for the enamy red buff
        /// </summary>
        private LeagueTimer _tmrRedTheir;

        /// <summary>
        /// Timer for the Dragon
        /// </summary>
        private LeagueTimer _tmrDragon;

        /// <summary>
        /// Timer for the Baron
        /// </summary>
        private LeagueTimer _tmrBaron;

        /// <summary>
        /// Constructor - Creates all timers
        /// </summary>
        public LeagueTimers() {
            _tmrBlueOur = new LeagueTimer(new TimeSpan(0, 5, 0));
            _tmrBlueTheir = new LeagueTimer(new TimeSpan(0, 5, 0));

            _tmrRedOur = new LeagueTimer(new TimeSpan(0, 5, 0));
            _tmrRedTheir = new LeagueTimer(new TimeSpan(0, 5, 0));

            _tmrDragon = new LeagueTimer(new TimeSpan(0, 7, 0));
            _tmrBaron = new LeagueTimer(new TimeSpan(0, 7, 0));
        }

        /// <summary>
        /// Starts the timer(s)
        /// </summary>
        /// <param name="pType">Timer to start</param>
        public void Start(LeagueType pType) {
            if (pType == LeagueType.All || pType == LeagueType.BlueOur) {
                _tmrBlueOur.Start();
            }

            if (pType == LeagueType.All || pType == LeagueType.BlueTheir) {
            _tmrBlueTheir.Start();
            }

            if (pType == LeagueType.All || pType == LeagueType.RedOur) {
                _tmrRedOur.Start();
            }

            if (pType == LeagueType.All || pType == LeagueType.RedTheir) {
                _tmrRedTheir.Start();
            }

            if (pType == LeagueType.All || pType == LeagueType.Dragon) {
                _tmrDragon.Start();
            }
            
            if (pType == LeagueType.All || pType == LeagueType.Baron) {
                _tmrBaron.Start();
            }
        }

        /// <summary>
        /// Stops the timer(s)
        /// </summary>
        /// <param name="pType">Timer to stop</param>
        public void Stop(LeagueType pType) {
            if (pType == LeagueType.All || pType == LeagueType.BlueOur) {
                _tmrBlueOur.Stop();
            }

            if (pType == LeagueType.All || pType == LeagueType.BlueTheir) {
                _tmrBlueTheir.Stop();
            }

            if (pType == LeagueType.All || pType == LeagueType.RedOur) {
                _tmrRedOur.Stop();
            }

            if (pType == LeagueType.All || pType == LeagueType.RedTheir) {
                _tmrRedTheir.Stop();
            }

            if (pType == LeagueType.All || pType == LeagueType.Dragon) {
                _tmrDragon.Stop();
            }

            if (pType == LeagueType.All || pType == LeagueType.Baron) {
                _tmrBaron.Stop();
            }
        }

        /// <summary>
        /// Rests the timer(s)
        /// </summary>
        /// <param name="pType">Timer to reset</param>
        public void Reset(LeagueType pType) {
            Stop(pType);
            Start(pType);
        }

        /// <summary>
        /// ToString function
        /// </summary>
        /// <returns>Dictionary with <text, font size></returns>
        public new Dictionary<string, int> ToString() {
            Dictionary<string, int> dicText = new Dictionary<string, int>();

            //red buf
            string strRed = "Red Buff    " + _tmrRedOur.DueTime.ToString(@"mm\:ss\:ff") + "     " + _tmrRedTheir.DueTime.ToString(@"mm\:ss\:ff");

            //blue buff
            string strBlue = "Blue Buff    " + _tmrBlueOur.DueTime.ToString(@"mm\:ss\:ff") + "     " + _tmrBlueTheir.DueTime.ToString(@"mm\:ss\:ff");

            //dragon
            string strDragon = "Dragon     " + _tmrDragon.DueTime.ToString(@"mm\:ss\:ff");

            //baron
            string strBaron = "Baron       " + _tmrBaron.DueTime.ToString(@"mm\:ss\:ff");

            dicText.Add(strRed, 7);
            dicText.Add(strBlue, 7);
            dicText.Add(strDragon, 8);
            dicText.Add(strBaron, 8);

            return dicText;
        }
    }
}