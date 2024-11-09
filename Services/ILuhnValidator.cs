using System.Threading.Tasks;

namespace ValidateCreditCardAPI.Services
{
    public interface ILuhnValidator
    {
        bool IsValid(string cardNumber);
    }
}
