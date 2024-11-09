// Services/LuhnValidatorService.cs

using System;
using System.Linq;
using ValidateCreditCardAPI.Services;
namespace ValidateCreditCardAPI.Services
{
    public class LuhnValidatorService : ILuhnValidator
    {
        /// <summary>
        /// Validates the provided credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">The credit card number to validate.</param>
        /// <returns>True if the card number is valid, false otherwise.</returns>
        public bool IsValid(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber) || cardNumber.Any(c => !char.IsDigit(c)))
                return false;

            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                int n = int.Parse(cardNumber[i].ToString());

                if (alternate)
                {
                    n *= 2;
                    if (n > 9) n -= 9;
                }

                sum += n;
                alternate = !alternate;
            }

            return sum % 10 == 0;
        }
    }
}