using MyFrac = (long nom, long denom);



namespace Lab4
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            MyFrac f1 = new();
            MyFrac f2 = new();

            bool hasF1 = false;
            bool hasF2 = false;

            bool isRunning = true;

            while (isRunning)
            {
                Console.WriteLine("1. CalcExpr1");
                Console.WriteLine("2. CalcExpr2");
                Console.WriteLine("3. Plus");
                Console.WriteLine("4. Minus");
                Console.WriteLine("5. Multiply");
                Console.WriteLine("6. Divide");
                Console.WriteLine("7. MyFracToString");
                Console.WriteLine("8. ToStringWithIntPart");
                Console.WriteLine("9. DoubleValue");
                Console.WriteLine("0. Close the program");

                int choice = GetValidInt("Your choice", 0, 9);

                switch (choice)
                {
                    case 1:
                        Console.WriteLine(CalcExpr1(GetValidInt("Enter n", 1)));
                    break;

                    case 2:
                        Console.WriteLine(CalcExpr2(GetValidInt("Enter n", 2)));
                    break;

                    case 3:
                        EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                        EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");
                    break;



                    case 0:
                        isRunning = false;
                    break;
                }
            }
        }




        static void EnsureFractionIsEntered(ref MyFrac f, ref bool hasF, string message)
        {
            if (hasF)
            {
                return;
            }

            f = GetValidFrac(message);
        }


        // Input validation
        static int GetValidInt(string message, int min = int.MinValue, int max = int.MaxValue, params int[] bannedNumbers)
        {
            while (true)
            {
                string prompt = (min == int.MinValue, max == int.MaxValue) switch
                {
                    (true, true)   => $"{message}: ",
                    (true, false)  => $"{message} (up to {max}): ",
                    (false, true)  => $"{message} ({min}+): ",
                    (false, false) => $"{message} ({min}-{max}): "
                };

                Console.Write($"\n{prompt}");

                if (!int.TryParse(Console.ReadLine(), out int num))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number");
                    continue;
                }

                if (num < min || num > max)
                {
                    Console.WriteLine($"Invalid input. Please enter a number in range ({min}-{max})");
                    continue;
                }

                if (bannedNumbers.Contains(num))
                {
                    Console.WriteLine($"Number {num} is not allowed for current task. Pleaase enter another number");
                    continue;
                }

                return num;
            }
        }

        static MyFrac GetValidFrac(string message)
        {
            Console.WriteLine($"\n{message}");
            
            int nom = GetValidInt("Enter nom");
            int denom = GetValidInt("Enter denom", bannedNumbers: 0);

            return (nom, denom);
        }



        // Main tasks
        static MyFrac CalcExpr1(int n)
        {
            MyFrac sum = (0, 1);

            for (int i = 1; i <= n; i++)
            {
                sum = Plus(sum, (1, (long) i * (i + 1)));
            }

            return sum;
        }

        static MyFrac CalcExpr2(int n)
        {
            MyFrac sum = (1, 1);

            for (int i = 2; i <= n; i++)
            {
                sum = Multiply(Minus((1, 1), (1, i * i)), sum);
            }

            return sum;
        }



        // Arithmetic operations
        static MyFrac Plus(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom + f1.denom * f2.nom, f1.denom * f2.denom));
        }

        static MyFrac Minus(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom - f1.denom * f2.nom, f1.denom * f2.denom));
        }

        static MyFrac Multiply(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.nom, f1.denom * f2.denom));
        }

        static MyFrac Divide(MyFrac f1, MyFrac f2)
        {
            return Normalize((f1.nom * f2.denom, f1.denom * f2.nom));
        }



        // String formatting
        static string MyFracToString(MyFrac f)
        {
            return new string($"{f.nom} / {f.denom}");
        }

        static string ToStringWithIntPart(MyFrac f)
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

        static double DoubleValue(MyFrac f)
        {
            return (double) f.nom / f.denom;
        }

        

        // Core methods
        static MyFrac Normalize(MyFrac t)
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
        static long EuclidMethod(long nom, long denom)
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