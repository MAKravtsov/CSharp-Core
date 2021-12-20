using System.Threading.Tasks;

namespace TFA.Sms.Services.Interfaces
{
    public interface ISmsService
    {
        Task SendAsync(string number, string message);
    }
}