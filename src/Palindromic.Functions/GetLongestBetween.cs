using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Palindromic.Core;

namespace Palindromic.Functions
{
	public class GetLongestBetween
	{
		private readonly ILogger<GetLongestBetween> _logger;
		private readonly IPalindromicService _palindromicService;

		public GetLongestBetween(ILogger<GetLongestBetween> logger)
		{
			_logger = logger;
			_palindromicService = new PalindromicService();
		}

		[Function("LongestBetween")]
		public IActionResult Run([HttpTrigger(
					AuthorizationLevel.Anonymous,
					"get",
					Route = "longestbetween/{inputA}/{inputB}")] HttpRequest req,
			int inputA,
			int inputB)
		{
			_logger.LogInformation("C# HTTP trigger function processed a request.");

			try
			{
				var result = _palindromicService.GetCloserLowerPalindromic(inputA, inputB);
				_logger.LogTrace(_logger.IsEnabled(LogLevel.Trace) ? $"The highest palindrome between {inputA} and {inputB} is {result}" : string.Empty);
				return new OkObjectResult(result);
			}
			catch (Exception ex)
			{
				return new BadRequestObjectResult(ex.Message);
			}
		}
	}
}
