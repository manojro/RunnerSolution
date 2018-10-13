using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace SequenceAnalysis
{
    /// <summary>
    /// Different methods to find sequence
    /// </summary>
    public enum SeqMethod
    {
        LINQ = 1,
        ARRAY = 2,
    }
    public class SequenceAnalyzer
    {
        /// <summary>
        /// Find uppercase words in a string
        /// </summary>
        /// <param name="inputString"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public string FindUpperCaseWords(string inputString, int method)
        {
            string result = "";
            switch (method)
            {
                case (int)SeqMethod.ARRAY:
                    result = DetermineSeqUsingArray(inputString);
                    break;
                case (int)SeqMethod.LINQ:
                    result = DetermineSeqUsingLINQ(inputString);
                    break;
                default:
                    result = string.Empty;
                    break;
            }
            return result;
        }
        /// <summary>
        /// Determine sequence using array
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string DetermineSeqUsingArray(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var temp = new StringBuilder();
            string upperCase = string.Empty;
            for (int i = 0; i < input.Length; i++)
            {
                int startIndex = i;
                //Add the letter to the temp string before checking for uppercase
                if (input[i] != ' ')
                {
                    temp.Append(input[i]);
                }
                else
                {
                    //IF a word is in uppercase then add it to a uppercase string
                    if (IsUpper(temp.ToString()))
                    {
                        upperCase = upperCase + temp;
                    }
                    temp.Clear();
                }
            }
            if (IsUpper(temp.ToString()))
            {
                upperCase = upperCase + temp;
                temp.Clear();
            }
            var finalOutPut = ToAlphabeticalOrder(upperCase);

            return (finalOutPut.ToString().Length > 0) ? finalOutPut.ToString() : "No Sequence Found";
        }

        /// <summary>
        /// To order all characters in the word alphabetically
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        static string ToAlphabeticalOrder(string input)
        {
            char[] characters = input.ToArray();
            Array.Sort(characters);
            return new string(characters);
        }
        /// <summary>
        /// Check if a word in a string is an uppercase word
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsUpper(string value)
        {
            for (int i = 0; i < value.Length; i++)
            {
                if (char.IsLower(value[i]))
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Determine sequence using LINQ
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string DetermineSeqUsingLINQ(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            var intermResult = input.Split(' ').Where(x => x == x.ToUpper());
            string output = new string(string.Join("", intermResult).OrderBy(c => c).Where(c => c >= 'A' && c <= 'Z').ToArray());
            return (!String.IsNullOrEmpty(output)) ? output : "No Sequence Found";
        }
    }
}
