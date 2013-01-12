using System;
using System.Timers;


namespace League_Of_Legends_Timer {    
    /// <summary>
    /// Custom Timer class
    /// </summary>
    public class LeagueTimer : IDisposable {
        /// <summary>
        /// Actual timer
        /// </summary>
        private Timer _timer;

        /// <summary>
        /// Period between start and stop
        /// </summary>
        private TimeSpan _objPeriod;

        /// <summary>
        /// DateTime when the next timer end is
        /// </summary>
        private DateTime _next;

        /// <summary>
        /// Boolean that keeps track if the timer is started or not
        /// </summary>
        private bool _blnStarted;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pPeriod">Timespan between start and end</param>
        public LeagueTimer(TimeSpan pPeriod) {
            _timer = new Timer(pPeriod.TotalMilliseconds);
            _objPeriod = pPeriod;
            _timer.Elapsed += new ElapsedEventHandler(_timer_Elapsed);
            
        }

        /// <summary>
        /// Starts the timer
        /// </summary>
        public void Start() {
            _timer.Start();
            _blnStarted = true;
            _next = DateTime.Now.Add(_objPeriod);
        }

        /// <summary>
        /// Stops the timer
        /// </summary>
        public void Stop() {
            _blnStarted = false;
            _next = DateTime.Now;
            _timer.Stop();
        }

        /// <summary>
        /// Returns the timespan between now and the timer end 
        /// </summary>
        public TimeSpan DueTime {
            get {
                if (_blnStarted) {
                    return _next - DateTime.Now;
                } else {
                    return new TimeSpan();
                }
            }
        }

        /// <summary>
        /// Eventhandler for when the timer ends
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _timer_Elapsed(object sender, ElapsedEventArgs e) {
            Stop();
        }


        /// <summary>
        /// Cleanup
        /// </summary>
        public void Dispose() {
            _timer.Dispose();
        }
    }
}
