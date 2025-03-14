using Palindromic.Core;

if (args.Length < 2 || !int.TryParse(args[0], out var inputA) || !int.TryParse(args[1], out var inputB))
{
	Console.WriteLine("Please provide two valid numbers as arguments.");
	return;
}

var palindromicService = new PalindromicService();

try
{
	var result = palindromicService.GetCloserLowerPalindromic(inputA, inputB);
	Console.WriteLine($"The highest palindrome between {inputA} and {inputB} is {result}");
}
catch (InvalidOperationException ex)
{
	Console.WriteLine(ex.Message);
}
catch (Exception ex)
{
	Console.WriteLine($"An error occurred: {ex.Message}");
}
