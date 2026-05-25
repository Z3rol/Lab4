using MyFrac = (long nom, long denom);

namespace Lab4
{
    public class Fractions
    {
        // Main tasks
        public static MyFrac CalcExpr1(int n)
        {
            MyFrac sum = (0, 1);

            for (int i = 1; i <= n; i++)
            {
                sum = Plus(sum, (1, (long) i * (i + 1)));
            }

            return sum;
        }

        public static MyFrac CalcExpr2(int n)
        {
            MyFrac sum = (1, 1);

            for (int i = 2; i <= n; i++)
            {
                sum = Multiply(Minus((1, 1), (1, i * i)), sum);
            }

            return sum;
        }



        // Arithmetic operations
        public static MyFrac Plus(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom + f1.denom * f2.nom, f1.denom * f2.denom));
        }

        public static MyFrac Minus(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom - f1.denom * f2.nom, f1.denom * f2.denom));
        }

        public static MyFrac Multiply(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.nom, f1.denom * f2.denom));
        }

        public static MyFrac Divide(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom, f1.denom * f2.nom));
        }



        // String formatting
        public static string MyFracToString(MyFrac f)
        {
            return $"{f.nom} / {f.denom}";
        }

        public static string ToStringWithIntPart(MyFrac f)
        {
            f = Normalize(f);

            string sign = "";

            if (f.nom < 0)
            {
                sign = "-";
                f.nom = Math.Abs(f.nom);
            }

            (long num, long rem) = Math.DivRem(f.nom, f.denom);

            if (num == 0)
            {
                return $"{sign}{rem}/{f.denom}";
            }
            else if (rem == 0)
            {
                return $"{sign}{num}";
            }
            else
                return $"{sign}({num} + {rem}/{f.denom})";
        }

        public static double DoubleValue(MyFrac f)
        {
            return (double) f.nom / f.denom;
        }

        

        // Core methods
        public static MyFrac Normalize(MyFrac t)
        {
            if (t.denom == 0)
            {
                throw new DivideByZeroException("Cannot divide by zero");
            }
            // Find greatest common divisor
            long divisor = EuclidMethod(t.nom, t.denom);

            // Simplify fraction
            t.nom /= divisor;
            t.denom /= divisor;

            // Handle negative denom
            if (t.denom < 0)
            {
                t.nom *= -1;
                t.denom *= -1;
            }

            return t;
        }

        // Find greatest common divisor
        private static long EuclidMethod(long nom, long denom)
        {
            nom = Math.Abs(nom);
            denom = Math.Abs(denom);

            while (denom > 0)
            {
                long temp = denom;
                denom = nom % denom;
                nom = temp;
            }

            return nom;
        }
    }
}