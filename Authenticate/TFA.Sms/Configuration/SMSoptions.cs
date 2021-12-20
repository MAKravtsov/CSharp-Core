namespace TFA.Sms.Configuration
{
    public class SMSoptions
    {
        public static string SMSAccountIdentification { get; set; }
        public static string SMSAccountPassword { get; set; }
        public static string SMSAccountFrom { get; set; }

        public static void Configure(string smsAccountIdentification,
            string smsAccountPassword,
            string smsAccountFrom)
        {
            SMSAccountIdentification = smsAccountIdentification;
            SMSAccountPassword = smsAccountPassword;
            SMSAccountFrom = smsAccountFrom;
        }
    }
}
