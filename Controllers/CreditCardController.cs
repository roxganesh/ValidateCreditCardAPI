// Controllers/CreditCardController.cs

using Microsoft.AspNetCore.Mvc;
using ValidateCreditCardAPI.Services;

namespace ValidateCreditCardAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CreditCardController : ControllerBase
    {
        private readonly LuhnValidatorService _luhnValidator;

        public CreditCardController(LuhnValidatorService luhnValidator)
        {
            _luhnValidator = luhnValidator;
        }

        /// <summary>
        /// Validates a credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">The credit card number to validate.</param>
        /// <returns>True if the card number is valid, false otherwise.</returns>
        [HttpGet("validate")]
        public IActionResult ValidateCardNumber([FromQuery] string cardNumber)
        {
            try
            {
                bool isValid = _luhnValidator.Validate(cardNumber);
                return Ok(isValid);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "An error occurred while validating the card number." });
            }
        }
    }
}
