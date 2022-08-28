using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheKitchen
{
    public class ProcedureStep : ITimeable
    {
        public Stopwatch StopWatch { get; set; }
        public string Procedure { get; set; }
        public int StepNumber { get; set; }
        public List<TimeSpan> TimeToPerform { get; set; }
        public TimeSpan AverageTimeToPerform { get; set; }

        public ProcedureStep(string procedure, int stepNumber)
        {
            TimeToPerform = new List<TimeSpan>();
            AverageTimeToPerform = new();
            StopWatch = new();

            Procedure = procedure;
            StepNumber = stepNumber;
        }

        public ProcedureStep()
        {

        }

        public override string ToString()
        {
            return Procedure;
        }

    }
}
