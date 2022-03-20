using System;
using System.Collections.Generic;
using System.Linq;

namespace CombinationSolver.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            new Program().Run();
        }

        private void Run()
        {
            var solutions = AllPossibleCombinations()
                .Where(OneDigitRightButWrongPlace(1, 4, 7))
                .Where(OneDigitRightAndInPlace(1, 8, 9))
                .Where(TwoCorrectInWrongPlace(9, 6, 4))
                .Where(AllWrong(5, 2, 3))
                .Where(OneDigitRightButWrongPlace(2, 8, 6))
                .ToList();

            System.Console.WriteLine("Solutions:");
            solutions.ForEach(x => System.Console.WriteLine($"{x[0]}{x[1]}{x[2]}"));
            System.Console.ReadLine();
        }

        private Func<int[], bool> OneDigitRightButWrongPlace(params int[] constraint) => solution =>
        {
            var oneRight = IsRight(constraint, solution, howMany: 1);
            var inWrongPlace = InWrongPlace(constraint, solution);
            return oneRight && inWrongPlace;
        };

        private Func<int[], bool> OneDigitRightAndInPlace(params int[] constraint) => solution =>
        {
            var oneRight = IsRight(constraint, solution, howMany: 1);
            var inRightPlace = InRightPlace(constraint, solution, howMany: 1);
            return oneRight && inRightPlace;
        };

        private Func<int[], bool> TwoCorrectInWrongPlace(params int[] constraint) => solution =>
        {
            var twoCorrect = IsRight(constraint, solution, howMany: 2);
            var inWrongPlace = InWrongPlace(constraint, solution);
            return twoCorrect && inWrongPlace;
        };

        private Func<int[], bool> AllWrong(params int[] constraint) => solution =>
        {
            return new[]
            {
                !solution.Contains(constraint[0]),
                !solution.Contains(constraint[1]),
                !solution.Contains(constraint[2])
            }.Count(x => x) == 3;
        };

        private bool InWrongPlace(int[] constraint, int[] solution)
        {
            return new[]
            {
                solution[0] != constraint[0],
                solution[1] != constraint[1],
                solution[2] != constraint[2]
            }.Count(x => x) == 3;
        }

        private bool IsRight(int[] constraint, int[] solution, int howMany)
        {
            return new[]
            {
                solution.Contains(constraint[0]),
                solution.Contains(constraint[1]),
                solution.Contains(constraint[2])
            }.Count(x => x) == howMany;
        }

        private bool InRightPlace(int[] constraint, int[] solution, int howMany)
        {
            return new[]
            {
                solution[0] == constraint[0],
                solution[1] == constraint[1],
                solution[2] == constraint[2]
            }.Count(x => x) == howMany;
        }

        private IEnumerable<int[]> AllPossibleCombinations()
        {
            for (var a = 1; a < 10; a++)
            {
                for (var b = 1; b < 10; b++)
                {
                    for (var c = 1; c < 10; c++)
                    {
                        if (a != b && a != c && b != c)
                        {
                            yield return new[] { a, b, c };
                        }
                    }
                }
            }
        }
    }
}