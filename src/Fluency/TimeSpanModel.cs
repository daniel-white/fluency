using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fluency
{
    public struct TimeSpanModel
    {
        private readonly List<TimeSpanStep> _steps;

        internal TimeSpanModel(int amount, UnitOfTime unitOfTime)
        {
            _steps = new List<TimeSpanStep>();
            AddStep(amount, unitOfTime);
        }

        internal TimeSpanModel AddStep(double amount, UnitOfTime unitOfTime)
        {
            _steps.Add(new TimeSpanStep(amount, unitOfTime));
            return this;
        }

        internal TimeSpanModel NegateLastStep()
        {
            _steps.LastOrDefault().Negate();
            return this;
        }

        public static implicit operator TimeSpan(TimeSpanModel model)
        {
            return model._steps.Aggregate(TimeSpan.Zero, (current, step) => current + step);
        }
    }

    internal class TimeSpanStep
    {
        public TimeSpanStep(double amount, UnitOfTime unitOfTime)
        {
            _amount = amount;
            _unitOfTime = unitOfTime;
        }

        private double _amount;
        public double Amount
        {
            get { return _amount; }
        }

        private UnitOfTime _unitOfTime;
        public UnitOfTime UnitOfTime
        {
            get { return _unitOfTime; }
        }

        public void Negate()
        {
            _amount *= -1;
        }

        public static implicit operator TimeSpan(TimeSpanStep model)
        {
            return UnitOfTimeConverters.Convert(model);
        }
    }

    internal static class UnitOfTimeConverters
    {
        private static readonly IDictionary<UnitOfTime, Func<double, TimeSpan>> Methods = new Dictionary<UnitOfTime, Func<double, TimeSpan>>();
 
        static UnitOfTimeConverters()
        {
            Methods.Add(UnitOfTime.Millisecond, TimeSpan.FromMilliseconds);
            Methods.Add(UnitOfTime.Second, TimeSpan.FromSeconds);
            Methods.Add(UnitOfTime.Minute, TimeSpan.FromMinutes);
            Methods.Add(UnitOfTime.Hours, TimeSpan.FromHours);
            Methods.Add(UnitOfTime.Days, TimeSpan.FromDays);
        }

        public static TimeSpan Convert(TimeSpanStep step)
        {
            return Methods[step.UnitOfTime](step.Amount);
        }
    }
}
