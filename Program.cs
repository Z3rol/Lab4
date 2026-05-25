using MyFrac = (long nom, long denom);

namespace Lab4
{
    class Program
    {
        static void Main()
        {
            Console.Clear();

            bool isRunning = true;
            while (isRunning)
            {
                PrintMainMenu();

                int choice = Functions.GetValidInt("Choice", 0, 2);
                switch (choice)
                {
                    case 1:
                        ExecuteFractionsTask();
                    break;
                    
                    case 2:
                        ExecuteStudentsTask();
                    break;

                    case 0:
                        Console.WriteLine("Closing...");
                        isRunning = false;
                    break;
                }
            }
        }



        // UI
        static void PrintMainMenu()
        {
            Console.WriteLine(" 1. Fractions");
            Console.WriteLine(" 2. Students");
            Console.WriteLine(" 0. Close the app");
        }

        static void PrintFractionsMenu()
        {
            Console.WriteLine("\n1. CalcExpr1");
            Console.WriteLine("2. CalcExpr2");
            Console.WriteLine("3. Plus");
            Console.WriteLine("4. Minus");
            Console.WriteLine("5. Multiply");
            Console.WriteLine("6. Divide");
            Console.WriteLine("7. MyFracToString");
            Console.WriteLine("8. ToStringWithIntPart");
            Console.WriteLine("9. DoubleValue");
            Console.WriteLine("10. Clear fractions");
            Console.WriteLine("11. Clear the console");
            Console.WriteLine("0. Return to the menu");
        }

        static void WaitForKeyPress(string message = "Press any key to continue")
        {
            Console.WriteLine(message);
            Console.ReadKey();
        }



        // Fractions
        static void ExecuteFractionsTask()
        {
            MyFrac f1 = new();
            MyFrac f2 = new();

            bool hasF1 = false;
            bool hasF2 = false;

            bool isRunning = true;

            while (isRunning)
            {
                PrintFractionsMenu();

                int choice = Functions.GetValidInt("Your choice", 0, 11);
                switch (choice)
                {
                    case 1:
                        Console.WriteLine($"\n{Fractions.CalcExpr1(Functions.GetValidInt("Enter n", 1))}");
                    break;

                    case 2:
                        Console.WriteLine($"{Fractions.CalcExpr2(Functions.GetValidInt("Enter n", 2))}");
                    break;

                    case 3:
                        EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                        EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");

                        Console.WriteLine($"\nResult: {Fractions.Plus(f1, f2)}");
                    break;

                    case 4:
                        EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                        EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");

                        Console.WriteLine($"\nResult: {Fractions.Minus(f1, f2)}");
                    break;

                    case 5:
                        EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                        EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");

                        Console.WriteLine($"\nResult: {Fractions.Multiply(f1, f2)}");
                    break;

                    case 6:
                        EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                        EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");

                        Console.WriteLine($"\nResult: {Fractions.Divide(f1, f2)}");
                    break;

                    case 7:
                    {
                        int fracChoice = Functions.GetValidInt("What fraction would u like to use", 1, 2);

                        if (fracChoice == 1)
                        {
                            EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                            Console.WriteLine(Fractions.MyFracToString(f1));
                        }
                        else
                        {
                            EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");
                            Console.WriteLine(Fractions.MyFracToString(f2));
                        }
                    }
                    break;
                    

                    case 8:
                    {
                        int fracChoice = Functions.GetValidInt("What fraction would u like to use", 1, 2);

                        if (fracChoice == 1)
                        {
                            EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                            Console.WriteLine(Fractions.ToStringWithIntPart(f1));
                        }
                        else
                        {
                            EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");
                            Console.WriteLine(Fractions.ToStringWithIntPart(f2));
                        }
                    }
                    break;

                    case 9:
                    {
                        int fracChoice = Functions.GetValidInt("What fraction would u like to use", 1, 2);

                        if (fracChoice == 1)
                        {
                            EnsureFractionIsEntered(ref f1, ref hasF1, "First frac");
                            Console.WriteLine(Fractions.DoubleValue(f1));
                        }
                        else
                        {
                            EnsureFractionIsEntered(ref f2, ref hasF2, "Second frac");
                            Console.WriteLine(Fractions.DoubleValue(f2));
                        }
                    }
                    break;

                    case 10:
                        hasF1 = false;
                        hasF2 = false;
                        Console.WriteLine("Your fractions have been cleared.");
                    break;

                    case 11:
                        Console.Clear();
                    break;

                    case 0:
                        isRunning = false;
                    break;
                }

                WaitForKeyPress();
            }
        }

        static void EnsureFractionIsEntered(ref MyFrac f, ref bool hasF, string message)
        {
            if (hasF)
            {
                return;
            }

            f = GetValidFrac(message);
            hasF = true;
        }

        static MyFrac GetValidFrac(string message)
        {
            Console.WriteLine($"\n{message}");
            
            int nom = Functions.GetValidInt("Enter nom");
            int denom = Functions.GetValidInt("Enter denom", bannedNumbers: 0);

            return (nom, denom);
        }



        // Students
        static void ExecuteStudentsTask()
        {
            List<Student> students = StudentDatabase.ReadStudentsFromFile("input.txt");

            if (students.Count == 0)
            {
                Console.WriteLine("This file has no students data");
                return;
            }
            
            PrintStudents(students);
            WaitForKeyPress("Press enter to sort");

            List<Student> filteredStudents = StudentDatabase.FilterStudents(students);

            if (filteredStudents.Count == 0)
            {
                Console.WriteLine("There are no students that are younger than 18 and have atleast one exam unpassed");
                return;
            }

            PrintStudents(filteredStudents);

        }

        public static void PrintStudents(List<Student> students)
        {
            if (students.Count == 0) return;

            string divider = new string('-', 109);

            Console.WriteLine("\n--- STUDENTS ---");
            Console.WriteLine(divider);
            
            Console.WriteLine($"| {"Last Name",-15} | {"First Name",-15} | {"Middle Name",-15} | {"Sex",-3} | {"Birth Date",-10} | {"Math",-4} | {"Phys",-4} | {"IT",-4} | {"Scholarship",11} |");
            
            Console.WriteLine(divider);

            foreach (var student in students)
            {
                string mathStr = student.MathGrade == -1 ? "-" : student.MathGrade.ToString();
                string physStr = student.PhysicsGrade == -1 ? "-" : student.PhysicsGrade.ToString();
                string itStr = student.ItGrade == -1 ? "-" : student.ItGrade.ToString();

                Console.WriteLine($"| {student.LastName,-15} | {student.Name,-15} | {student.MiddleName,-15} | {student.Sex,-3} | {student.BirthDate,-10:dd.MM.yyyy} | {mathStr,-4} | {physStr,-4} | {itStr,-4} | {student.Scholarship,11} |");
            }

            Console.WriteLine(divider);
            Console.WriteLine($"Total Students: {students.Count}\n");
        }
    }
}