using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loanproject
{
    class Loan
    {
        private double p; //amount borrowed principal
        private double r; //rate
        private int n; //number of months
        private double o; //owed balance
        private double a; 
        public double RemainingBalance
        {
            get { return Math.Round(o, 2); }
        }
        public double AmountBorrowed
        {
            get { return Math.Round(p, 2); }
        }
        public double InterestRate
        {
            get { return Math.Round(a, 2); }
        }
            public double NumberOfMonths
        {
            get { return n; }

        }
    
    

        public Loan(double amountBorrowed, double interestRate = 3.0, int numberOfMonths = 360)
        {
            p = amountBorrowed;
            o = amountBorrowed;
            n = numberOfMonths;
            r = interestRate / 100 / 12;
            a = interestRate;
        }
        public double CalculateMonthlyPayment()
        {
            double expTerm = Math.Pow(1 + r, n);
            double numerator = p * r * expTerm;
            double denominator = expTerm - 1;
            return numerator / denominator;
        }
        public double InterestPortion()
        {
            return r * o;
        }
        public double PrincipalPortion(double extra = 0.0)
        {
            return CalculateMonthlyPayment() - InterestPortion() + extra;
 
        }
        public void MakeMonthlyPayment(double extra = 0.0)
        {
            double principal = PrincipalPortion();
            o -= principal + extra;
        }

        public double[,] AmortizationTable(double extra = 0.0)
        {
            double[,] at = new double[n + 1, 4];

            at[0, 0] = 0.0;
            at[0, 1] = 0.0;
            at[0, 2] = 0.0;
            at[0, 3] = AmountBorrowed;
        for (int m = 1; m < at.GetLength(0) && RemainingBalance > 0; m++)
            {
                at[m, 0] = CalculateMonthlyPayment() + extra;
                at[m, 1] = InterestPortion();
                at[m, 2] = PrincipalPortion();
                MakeMonthlyPayment(extra);
                at[m, 3] = RemainingBalance;

            }

            return at;
        }
        public override string ToString()
        {
            return $"{p:C2} @{a:N2}% with {o:C2} remaining";
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            Loan myLoan = new Loan(200000, 4.25, 360);
            double monthlyPayment = myLoan.CalculateMonthlyPayment();
            Console.WriteLine(myLoan);
            Console.WriteLine($"Monthly Payment: {monthlyPayment:C2}");
            Console.WriteLine($"Monthly Payment: {monthlyPayment:C2}");
            Console.WriteLine($"First Payment: {myLoan.InterestPortion():C2} interest");
            Console.WriteLine($"  and {myLoan.PrincipalPortion(0):C2} portion");

            double[,] at = myLoan.AmortizationTable();
            for (int r = 0; r < 5; r++)
            {
                string col0 = $"{at[r, 0],10:C2}";
                string col1 = $"{at[r, 1],10:C2}";
                string col2 = $"{at[r, 2],10:C2}";
                string col3 = $"{at[r, 3],10:C2}";
                Console.WriteLine($"{col0} {col1} {col2} {col3}");
            }

            Console.WriteLine("...");

            for(int r = at.GetLength(0) - 5; r < at.GetLength(0); r++)
            {
                string col0 = $"{at[r, 0],10:C2}";
                string col1 = $"{at[r, 1],10:C2}";
                string col2 = $"{at[r, 2],10:C2}";
                string col3 = $"{at[r, 3],10:C2}";
                Console.WriteLine($"{col0} {col1} {col2} {col3}");
            }
        }
    }
}

  
