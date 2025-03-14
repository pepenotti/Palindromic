using System.Text;

namespace Palindromic.Core
{
	public class PalindromicService : IPalindromicService
	{
		/// <summary>
		/// Get the longest palindromic number between two numbers
		/// This one is the brute force approach
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

		/// <summary>
		/// This method gets the closest lower palindromic number between two numbers
		/// It was intended to be a more optimized version of the GetLongestBetween method
		/// But i couldn't make it work properly with the time i
		/// </summary>
		/// <param name="inputA"></param>
		/// <param name="inputB"></param>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException"></exception>
		public int GetCloserLowerPalindromic(int inputA, int inputB)
		{
			var highest = Math.Max(inputA, inputB) - 1;
			var lowest = Math.Min(inputA, inputB) + 1;
			var highestStr = highest.ToString();

			if(highestStr.Length == 1)
			{
				return highest;
			}

			var highestStrLenght = highestStr.Length;
			var highestStrHalfLenght = highestStrLenght / 2;

			var middle = highestStrLenght % 2 == 0 ? -1 : int.Parse(highestStr[highestStrHalfLenght].ToString());
			var leftHalf = int.Parse(highestStr.Substring(0, highestStrHalfLenght));
			var rightHalf = middle == -1 // If no middle
				? highestStr.Substring(highestStrHalfLenght)
				: highestStr.Substring(highestStrHalfLenght + 1);

			var reversedRightHalf = int.Parse(string.Concat(rightHalf.Reverse()));

			if (leftHalf > reversedRightHalf)
			{
				AdjustMiddleOrLeftHalf(ref middle, ref leftHalf);
			}

			var palindrome = BuildPalindrome(leftHalf, middle, rightHalf);

			if (palindrome > highest)
			{
				AdjustMiddleOrLeftHalf(ref middle, ref leftHalf);

				palindrome = BuildPalindrome(leftHalf, middle, rightHalf);
			}

			return palindrome >= lowest
				? palindrome
				: throw new InvalidOperationException("No palindromic number found");
		}

		private static void AdjustMiddleOrLeftHalf(ref int middle, ref int leftHalf)
		{
			// If number lenght is uneven check if we have to decrease the middle and the left half
			if (middle != -1)
			{
				middle = middle == 0 ? 9 : middle - 1;

				if (middle == 9)
				{
					leftHalf = leftHalf - 1;
				}
			}
			else
			{
				leftHalf = leftHalf - 1;
			}
		}

		private int BuildPalindrome(int leftHalf, int middle, string rightHalf)
		{
			var builder = new StringBuilder();
			builder.Append(leftHalf);

			if (middle != -1)
				builder.Append(middle);

			//If the left side has now less numbers than the right side
			//We need to add a 9 to the middle
			if (leftHalf.ToString().Length < rightHalf.ToString().Length)
				builder.Append("9");

			builder.Append(string.Concat(leftHalf.ToString().Reverse()));
			return int.Parse(builder.ToString());
		}
	}
}
