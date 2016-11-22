using System;
using System.Collections.Generic;
using System.Linq;

namespace InstallmentCalculator
{
    public static class Calculator
    {
        public static double Calculate(double capital, double dailyInterest, List<DateTime> repaymentDates, bool isDailyCompound)
        {
            if (repaymentDates.Count < 2)
                throw new ArgumentException("At least 2 repaymet dates needed.", "repaymentDates");
            if (repaymentDates.Any(x => x.TimeOfDay != new TimeSpan()))
                throw new ArgumentException("Repayments should only have the date part.", "repaymentDates");
            if (capital <= 0)
                throw new ArgumentException("Capital must be greater than 0.", "capital");
            if (dailyInterest <= 0)
                throw new ArgumentException("Daily interest must be greater than 0.", "dailyInterest");

            var periodicInterests = new List<double>();

            for (int i = 1; i < repaymentDates.Count; i++)
            {
                var interval = DaysBetween(repaymentDates[i - 1], repaymentDates[i]);
                if (isDailyCompound)
                    periodicInterests.Add(Math.Pow(1 + dailyInterest, interval) - 1);
                else
                    periodicInterests.Add(dailyInterest * interval);
            }

            double numerator = 1;
            foreach (double p in periodicInterests)
                numerator *= 1 + p;

            numerator *= capital;

            var periodsCount = periodicInterests.Count();
            double denominator = periodsCount;
            for (int i = periodsCount; i > 0; i--)
            {
                double temp = 1;
                for (int j = i; j < periodsCount; j++)
                    temp *= periodicInterests[j] + 1;

                denominator += (i - 1) * periodicInterests[i - 1] * temp;
            }

            return numerator / denominator;
        }

        private static int DaysBetween(DateTime d1, DateTime d2)
        {
            TimeSpan span = d2.Subtract(d1);
            return (int)span.TotalDays;
        }
    }
}
