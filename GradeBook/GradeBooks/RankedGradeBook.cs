using System;
using System.Linq;
using GradeBook.Enums;

namespace GradeBook.GradeBooks
{
    public class RankedGradeBook : BaseGradeBook
    {
        public RankedGradeBook(string name) : base(name)
        {
            Type = GradeBookType.Ranked;
        }

        public override char GetLetterGrade(double averageGrade)
        {
            if (Students.Count < 5)
                throw new InvalidOperationException("Do średniej oceny w systemie rankingowym musi być co najmniej 5 studentów.");

            var threshold = (int)Math.Ceiling(Students.Count * 0.2);
            var grades = Students.OrderByDescending(s => s.AverageGrade).Select(s => s.AverageGrade).ToList();

            // Count how many students have a higher average than the provided grade
            var higherCount = grades.Count(g => g > averageGrade);

            if (higherCount < threshold)
                return 'A';
            else if (higherCount < threshold * 2)
                return 'B';
            else if (higherCount < threshold * 3)
                return 'C';
            else if (higherCount < threshold * 4)
                return 'D';
            else
                return 'F';
        }
    }
}

