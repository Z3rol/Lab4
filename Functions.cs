namespace Lab4
{
    public class Functions
    {
        // Input validation
        public static int GetValidInt(string message, int min = int.MinValue, int max = int.MaxValue, params int[] bannedNumbers)
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

                Console.Write($"{prompt}");

                if (!int.TryParse(Console.ReadLine(), out int num))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number");
                    continue;
                }

                if (num < min || num > max)
                {
                    Console.WriteLine("Number is out of range");
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
    }
}