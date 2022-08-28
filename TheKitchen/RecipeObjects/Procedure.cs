using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class Procedure : ITimeable
    {
        public Stopwatch StopWatch { get; set; }
        public List<TimeSpan> TimeToPerform { get; set; }
        public TimeSpan AverageTimeToPerform { get; set; }
        public List<ProcedureStep> Procedures { get; set; }
        public int ZeroBasedCurrentStep { get; set; }

        public Procedure()
        {
            Procedures = new();
            StopWatch = new();
            TimeToPerform = new();
        }

        /// <summary>
        /// Begins the stopwatch on the overall procedure and the first step
        /// </summary>
        public void StartProcedure()
        {
            ((ITimeable)this).Start();
            ZeroBasedCurrentStep = 0;
            ((ITimeable)Procedures[ZeroBasedCurrentStep]).Start();
        }

        /// <summary>
        /// Records the time of the previous step and starts timing the next step
        /// </summary>
        public void MoveToNextStep()
        {
            ((ITimeable)Procedures[ZeroBasedCurrentStep]).Stop();
            ZeroBasedCurrentStep++;

            // if last step was just finished
            if (ZeroBasedCurrentStep == Procedures.Count)
            {
                StopWatch.Stop();
                TimeToPerform.Add(StopWatch.Elapsed);
                ZeroBasedCurrentStep = 0;
                ((ITimeable)this).CalculateAverageTimeToPerform();
            }
            else
            {
                ((ITimeable)Procedures[ZeroBasedCurrentStep]).Start();
            }
        }

    }
}
