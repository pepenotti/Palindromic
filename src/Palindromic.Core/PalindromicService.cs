namespace Palindromic.Core
{
	public class PalindromicService : IPalindromicService
	{
		/// <summary>
		/// Get the longest palindromic number between two numbers
		/// </summary>
		/// <param name="inputA"></param>
		/// <param name="inputB"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public int GetLongestBetween(int inputA, int inputB)
		{
			int highest = Math.Max(inputA, inputB) - 1;
			int lowest = Math.Min(inputA, inputB);
			int? lowerHalfCandidate = null;

			for (int i = highest; i >= lowest; i--)
			{
				if (IsPalindromic(i))
				{
					return i;
				}

				if (!lowerHalfCandidate.HasValue && IsPalindromic(i))
				{
					lowerHalfCandidate = i;
				}
			}

			return lowerHalfCandidate.HasValue
			? lowerHalfCandidate.Value
			: throw new InvalidOperationException("No palindromic number found");
		}

		/// <summary>
		/// Check if a number is palindromic
		/// </summary>
		/// <param name="input"></param>
		/// <returns></returns>
		public bool IsPalindromic(int input)
		{
			if(input % 10 == 0)
			{
				return false;
			}

			var inputString = input.ToString();
			var reversedString = new string(inputString.Reverse().ToArray());
			return inputString == reversedString;
		}

		public int GetCloserLowerPalindromic(int inputA, int inputB)
		{
			var originalHighest = inputA > inputB ? inputA : inputB;
			var lowest = inputA < inputB ? inputA : inputB;
			var highest = originalHighest - 1;

			if (highest % 10 == 0)
			{
				highest -= 1;
			}

			if (highest <= lowest)
			{
				return IsPalindromic(lowest) ? lowest : throw new InvalidOperationException("No palindromic number found");
			}

			var highestString = highest.ToString();
			var lowestString = lowest.ToString();

			//Mirror the highestString first half to get the closest palindrome
			var firstHalf = highestString.Substring(0, highestString.Length / 2);
			var secondHalf = highestString.Substring(highestString.Length / 2);
			var middleChar = highestString.Length % 2 == 0 ? "" : highestString[highestString.Length / 2].ToString();
			var middleCharValue = string.IsNullOrEmpty(middleChar) ? -1 : int.Parse(middleChar);
			var palindrome = "";

			if (!string.IsNullOrEmpty(middleChar))
			{
				secondHalf = secondHalf.Remove(0, 1);
			}

			var stopAdjusting = false;

			//Construct the palindrome comparing the first half with the second half making sure the resulting number is not higher or lower than the original bounds
			for (var i = 0; i < firstHalf.Length; i++)
			{
				var firstChar = firstHalf[firstHalf.Length - 1 - i];
				var secondChar = secondHalf[i];
				var firstCharInt = int.Parse(firstChar.ToString());
				var secondCharInt = int.Parse(secondChar.ToString());

				if (stopAdjusting)
				{
					palindrome = firstChar + palindrome;
					continue;
				}

				if (firstCharInt < secondCharInt)
				{
					palindrome = firstChar + palindrome;
				}
				else if (firstCharInt > secondCharInt)
				{
					if (middleCharValue > 0)
					{
						middleCharValue--;
						middleChar = middleCharValue.ToString();
						stopAdjusting = true;
					}
					else
					{
						while (firstCharInt > secondCharInt)
						{
							firstCharInt--;
						}
					}

					palindrome = firstCharInt + palindrome;
				}
				else
				{
					palindrome = firstChar + palindrome;
				}
			}
			palindrome = palindrome + middleChar + new string(palindrome.Reverse().ToArray());
			var palindromeNumber = int.Parse(palindrome);

			return palindromeNumber < originalHighest && palindromeNumber > lowest ? palindromeNumber : throw new InvalidOperationException("No palindromic number found");
		}
	}
}
