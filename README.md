# installments-calc
This is solving non-trival problem of calculating equal repayment for daily accrued simple or compound interest. The problem is non-trival because of different duration periods between the repayments and requires solving exponential equation.

## Parameters
* capital - represents the initial lended capital
* dailyInterest - represents the daily interest, e.g. 0.01 for 1%
* repayentDates - represents dates when repayments are made
* isDailyCompound - represents whether interest is simple (false) or coumpound (true)

## Sample Usage
```
var repayments = new List<DateTime> { new DateTime(2016, 11, 30), new DateTime(2016, 12, 24), new DateTime(2017, 1, 31) };
var repaymentAmount = Calculator.Calculate(200, 0.01, repayments, false));
```
