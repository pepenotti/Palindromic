namespace Palindromic.Core.Tests
{
	public class PalindromicServiceTests
	{
		private readonly IPalindromicService palindromicService;

		public PalindromicServiceTests()
		{
			palindromicService = new PalindromicService();
		}


		[Theory]
		[InlineData(121)]
		[InlineData(1221)]
		[InlineData(12321)]
		[InlineData(123321)]
		public void IsPalindromic_WhenInputIsPalindrome_ReturnsTrue(int input)
		{
			// Act
			var result = palindromicService.IsPalindromic(input);
			
			// Assert
			Assert.True(result);
		}

		[Theory]
		[InlineData(123)]
		public void IsPalindromic_WhenInputIsNotPalindrome_ReturnsFalse(int input)
		{
			// Act
			var result = palindromicService.IsPalindromic(input);
			
			// Assert
			Assert.False(result);
		}

		[Theory]
		[InlineData(122, 130)]
		[InlineData(130, 122)]
		[InlineData(121, 121)] // Equal
		public void GetLongestBetween_WhenNoPalindromeFound_ThrowsInvalidOperationException(int inputA, int inputB)
		{
			// Act
			Action act = () => palindromicService.GetLongestBetween(inputA, inputB);
			
			// Assert
			Assert.Throws<InvalidOperationException>(act);
		}
		
		[Theory]
		[InlineData(120, 130, 121)] // Lower first next to highest palindrome
		[InlineData(130, 120, 121)] // Lower first next to highest palindrome [Inverted]
		[InlineData(110, 120, 111)] // Highest is next to the highest palindrome
		[InlineData(119, 123, 121)] // Palindrome is in the middle
		[InlineData(119, 122, 121)] // Palidrome is in higher half
		[InlineData(10000, 0, 9999)] // Test zeroes
		[InlineData(10001, 0, 9999)] // Test zeroes
		[InlineData(10010, 0, 10001)] // Test zeroes
		[InlineData(10100, 0, 10001)] // Test zeroes
		[InlineData(11000, 0, 10901)] // Test zeroes
		[InlineData(21000, 0, 20902)] // Test zeroes
		[InlineData(20002, 0, 19991)] // Test zeroes
		[InlineData(int.MinValue, int.MaxValue, 0)] // Test bounds
		public void GetLongestBetween_WhenPalindromeFound_ReturnsPalindrome(int inputA, int inputB, int expected)
		{
			// Act
			var result = palindromicService.GetLongestBetween(inputA, inputB);
			
			// Assert
			Assert.True(result == expected);
		}
	}
}