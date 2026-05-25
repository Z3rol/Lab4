using System.Globalization;

namespace Lab4
{
    public class StudentDatabase
    {
        public static List<Student> ReadStudentsFromFile(string filePath)
        {
            List<Student> students = new();

            if (!File.Exists(filePath))
            {
                Console.WriteLine($"\nError: File '{filePath}' does not exist");
                return students;
            }

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;

                string[] parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 9)
                {
                    Console.WriteLine($"[Warning]: Skipping current line due to invalid parameters number: {line}");
                    continue;
                }

                Student student = new();

                student.LastName = parts[0];
                student.Name = parts[1];
                student.MiddleName = parts[2];
                student.Sex = char.Parse(parts[3]);
                student.BirthDate = DateTime.ParseExact(parts[4], "dd.MM.yyyy", CultureInfo.InvariantCulture);
                student.MathGrade = int.TryParse(parts[5], out int mathGrade) ? mathGrade : -1;
                student.PhysicsGrade = int.TryParse(parts[6], out int physicsGrade) ? physicsGrade : -1;
                student.ItGrade = int.TryParse(parts[7], out int itGrade) ? itGrade : -1;
                student.Scholarship = int.Parse(parts[8]);

                students.Add(student);
            }

            return students;
        }

        public static List<Student> FilterStudents(List<Student> students)
        {
            List<Student> filteredStudents = new();

            foreach (var student in students)
            {
                int age = DateTime.Now.Year - student.BirthDate.Year;

                if (DateTime.Now.Month < student.BirthDate.Month ||
                    (DateTime.Now.Month == student.BirthDate.Month && DateTime.Now.Day < student.BirthDate.Day))
                {
                    age -= 1;
                }

                if (age >= 18) continue;

                if (student.MathGrade < 3 || student.PhysicsGrade < 3 || student.ItGrade < 3)
                {
                    filteredStudents.Add(student);
                }
            }

            return filteredStudents;
        }
    }
}