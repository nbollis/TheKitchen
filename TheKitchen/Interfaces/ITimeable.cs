using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Org.BouncyCastle.Asn1.Mozilla;

namespace TheKitchen
{
    public interface ITimeable
    {
        public Stopwatch StopWatch { get; set; }
        public List<TimeSpan> TimeToPerform { get; set; }
        public TimeSpan AverageTimeToPerform { get; set; }

        /// <summary>
        /// Starts the stopwatch on this step
        /// </summary>
        public virtual void Start()
        {
            if (StopWatch == null)
                StopWatch = Stopwatch.StartNew();
            else
                StopWatch.Start();
        }

        /// <summary>
        /// Ends the stopwatch for this step and records the time it took
        /// </summary>
        /// <exception cref="ArgumentException"></exception>
        public virtual void Stop()
        {
            if (StopWatch.IsRunning)
            {
                StopWatch.Stop();
                TimeToPerform.Add(StopWatch.Elapsed);
                CalculateAverageTimeToPerform();
            }
            else
            {
                throw new ArgumentException("Step not started");
            }
        }

        /// <summary>
        /// Calculates the average amount of time this step took
        /// </summary>
        public virtual void CalculateAverageTimeToPerform()
        {
            double averageTimeInMinutes = TimeToPerform.Select(p => p.TotalMilliseconds).Average();
            AverageTimeToPerform = TimeSpan.FromMilliseconds(averageTimeInMinutes);
        }
    }
}
