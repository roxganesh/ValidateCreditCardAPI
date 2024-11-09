using ValidateCreditCardAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ValidateCreditCardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly ILuhnValidator _luhnValidator;
        private readonly ILogger<CreditCardController> _logger;

        public CreditCardController(ILuhnValidator luhnValidator, ILogger<CreditCardController> logger)
        {
            _luhnValidator = luhnValidator;
            _logger = logger;
        }

        /// <summary>
        /// Validates a credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">The credit card number as a string.</param>
        /// <returns>Boolean indicating validity.</returns>
        [HttpGet("validate")]
        public IActionResult Validate([FromQuery] string cardNumber)
        {
            try
            {
                if (string.IsNullOrEmpty(cardNumber))
                {
                    _logger.LogWarning("Validation attempt with empty card number.");
                    return BadRequest("Card number cannot be empty.");
                }

                bool isValid = _luhnValidator.IsValid(cardNumber);
                return Ok(isValid);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during card validation.");
                return StatusCode(500, "An error occurred while processing your request.");
            }
        }
    }
}
