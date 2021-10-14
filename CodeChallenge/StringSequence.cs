using System;
using System.Linq;
using System.Text;

namespace CodeChallenge
{
    public class StringSequence
    {
        string _inputString;

        public StringSequence(string inputString)
        {
            _inputString = inputString;
        }

        /// <summary>
        /// Process input string and return the longest increasing subsequence (increased by any number) present in that string 
        /// </summary>
        /// <returns>String which contains longest increasing subsequence </returns>
        public string GetLongestIncreasingSequence()
        {
            string[] splittedInput = _inputString.Split(' ');

            //Remove non integer inputs and prepare integer array
            int[] intArray = splittedInput.Select(str => {
                int value;
                bool success = int.TryParse(str, out value);
                return new { value, success };
            })
                  .Where(pair => pair.success)
                  .Select(pair => pair.value).ToArray();

            var result = GetMaxIndexAndLength(intArray);

            // Calculate startIndex in the sequence using maximumArrayIndex & maximumLength
            int sequenceStartIndex = (result.Item2 + 1 - result.Item1);
            int sequenceLength = result.Item1;

            StringBuilder output = new StringBuilder();
            // Loop through intArray to prepare the result
            for (int i = sequenceStartIndex; i < (sequenceStartIndex + sequenceLength); i++)
            {
                output.AppendFormat("{0}{1}", intArray[i].ToString(), ' ');
            }

            //Remove last space
            return output.Remove(output.Length - 1, 1).ToString();
        }

        /// <summary>
        /// Process input array and return maximum ArrayIndex and maximum Length for the Sequence within array 
        /// </summary>
        /// <param name="numberSequence">Number Sequence in an integer array</param>
        /// <returns>maximum ArrayIndex and maximum Length</returns>
        private Tuple<int, int> GetMaxIndexAndLength(int[] numberSequence)
        {
            int maximumLength = 1;
            int maximumArrayIndex = 0;

            int arrayLength = numberSequence.Length;

            // Loop through numberSequence to find the sequence
            for (int i = 0; i < arrayLength; i++)
            {
                int length = GetMaxLength(numberSequence, i);
                if (length > maximumLength)
                {
                    maximumLength = length;
                    maximumArrayIndex = i;
                }
            }

            return new Tuple<int, int>(maximumLength, maximumArrayIndex);
        }

        /// <summary>
        /// Recursive function to capture maximum length of the longest increasing subsequence
        /// </summary>
        /// <param name="numberArray">Number Sequence in an integer array</param>
        /// <param name="currentIndex">Current Index</param>
        /// <returns>Maximum length</returns>
        private int GetMaxLength(int[] numberArray, int currentIndex)
        {
            // Subsequence ends at first index
            if (currentIndex == 0)
                return 1;

            int maxLength = 1;
            int previousIndex = currentIndex - 1;

            // Compare adjacent values and call recursive function if there is an increment 
            if (numberArray[previousIndex] < numberArray[currentIndex])
            {
                // Recursive call. Add plus one to count the current item
                int result = (1 + (GetMaxLength(numberArray, previousIndex)));
                if (result > maxLength)
                {
                    maxLength = result;
                }
            }

            return maxLength;
        }
    }
}
