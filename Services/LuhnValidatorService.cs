// Services/LuhnValidatorService.cs

using System;

namespace ValidateCreditCardAPI.Services
{
    public class LuhnValidatorService
    {
        /// <summary>
        /// Validates the provided credit card number using the Luhn algorithm.
        /// </summary>
        /// <param name="cardNumber">The credit card number to validate.</param>
        /// <returns>True if the card number is valid, false otherwise.</returns>
        public bool Validate(string cardNumber)
        {
            if (string.IsNullOrWhiteSpace(cardNumber))
            {
                throw new ArgumentException("Card number cannot be null or empty", nameof(cardNumber));
            }

            int sum = 0;
            bool alternate = false;

            for (int i = cardNumber.Length - 1; i >= 0; i--)
            {
                if (!char.IsDigit(cardNumber[i]))
                {
                    throw new ArgumentException("Card number must contain only digits", nameof(cardNumber));
                }

                int digit = cardNumber[i] - '0';

                if (alternate)
                {
                    digit *= 2;
                    if (digit > 9) digit -= 9;
                }

                sum += digit;
                alternate = !alternate;
            }

            return (sum % 10 == 0);
        }
    }
}
