using System.Threading.Tasks;
using TFA.Sms.Configuration;
using TFA.Sms.Services.Interfaces;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace TFA.Sms.Services.Implementations
{
    public class SmsService : ISmsService
    {
        public Task SendAsync(string number, string message)
        {
            // Plug in your SMS service here to send a text message.
            // Your Account SID from twilio.com/console
            var accountSid = SMSoptions.SMSAccountIdentification;
            // Your Auth Token from twilio.com/console
            var authToken = SMSoptions.SMSAccountPassword;

            TwilioClient.Init(accountSid, authToken);

            return MessageResource.CreateAsync(
              to: new PhoneNumber(number),
              from: new PhoneNumber(SMSoptions.SMSAccountFrom),
              body: message);
        }
    }
}
