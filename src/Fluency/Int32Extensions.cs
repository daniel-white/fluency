using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluency
{
    public static class Int32Extensions
    {
        public static TimeSpanModel Minutes(this int minutes)
        {
            return new TimeSpanModel(minutes, UnitOfTime.Minute);
        }

        public static TimeSpanModel Seconds(this int seconds)
        {
            return new TimeSpanModel(seconds, UnitOfTime.Second);
        }

        public static TimeSpanModel Hours(this int hours)
        {
            return new TimeSpanModel(hours, UnitOfTime.Hours);
        }

        public static TimeSpanModel Ago(this TimeSpanModel step)
        {
            return step.NegateLastStep();
        }

        public static TimeSpanModel Days(this int hours)
        {
            return new TimeSpanModel(hours, UnitOfTime.Days);
        }

        public static TimeSpanModel Milliseconds(this int hours)
        {
            return new TimeSpanModel(hours, UnitOfTime.Millisecond);
        }
    }
}
