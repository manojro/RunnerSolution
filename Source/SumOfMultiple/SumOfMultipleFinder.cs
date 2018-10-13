using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SumOfMultiple
{
    public enum Method
    {
       MATH_FORMULA = 1,
       FORLOOP = 2,
       LINQ = 3,
       HASHSET = 4
    }
    public class SumOfMultipleFinder
    {
        /// <summary>
        /// Calculate the SOM of 3 and 5 within specified limit
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public long CalculateSOM(int limit, int method)
        {
            long som = 0;
            switch (method)
            {
                case (int)Method.LINQ:
                    som = FindSumUsingLINQ(limit);
                    break;
                case (int)Method.HASHSET:
                    som = FindSumUsingHashSet(limit);
                    break;
                case (int)Method.MATH_FORMULA:
                    som = FindSumUsingFormula(limit);
                    break;
                case (int)Method.FORLOOP:
                    som = FindSumUsingForLoop(limit);
                    break;
                default:
                    som = -1;
                    break;
            }
            return som;
        }

        /// <summary>
        /// Calculate sum using for loop
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        long FindSumUsingForLoop(int limit)
        {
            long sum = 0;
            for (int i = 1; i < limit; i++)
            {
                if (i % 3 == 0 || i % 5 == 0)
                {
                    sum += i;
                }
            }
            return sum;
        }

        /// <summary>
        /// Calculate sum using LINQ
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        long FindSumUsingLINQ(int limit)
        {
            return Enumerable.Range(0, limit)
                        .Where(n => n % 3 == 0 || n % 5 == 0).Sum();
        }

        /// <summary>
        /// Calculate sum using math formula
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        long FindSumUsingFormula(int limit)
        {
            // This is based on the mathematical formula (1+2+3....+N) = N*(N+1)/2
            //  SUMBELOW(3, 1000) = 3 + 6 + 9 + ... + 999 = 3 * (1 + 2 + 3 +...+ 333) 
            // = 3 * 333 * (1 + 333) / 2 
            // = 3 * ((1000 - 1) / 3) * (1 + (1000 - 1) / 3) / 2

            var totalSum = 0;
            var num3 = (limit - 1) / 3;
            var num5 = (limit - 1) / 5;
            var num15 = (limit - 1) / 15;

            var sumofNum3 = (3 * num3 * (num3 + 1)) / 2;
            var sumofNum5 = (5 * num5 * (num5 + 1)) / 2;

            //Common in both 3 and 5 so subtract this once
            var sumofNum15 = (15 * num15 * (num15 + 1)) / 2;

            totalSum = (sumofNum3 + sumofNum5)-sumofNum15;
            return totalSum;

        }
        /// <summary>
        /// calculate sum using HashSet
        /// </summary>
        /// <param name="limit"></param>
        /// <returns></returns>
        long FindSumUsingHashSet(int limit)
        {
            ISet<int> numbers = new HashSet<int>();
            for (int i = 3; i < limit; i += 3)
            {
                numbers.Add(i);
            }
            for (int j = 5; j < limit; j += 5)
            {
                numbers.Add(j);
            }
            //This will ensure no duplicates are used for sum calculation
            return numbers.Sum(); 
        }
    }
}
