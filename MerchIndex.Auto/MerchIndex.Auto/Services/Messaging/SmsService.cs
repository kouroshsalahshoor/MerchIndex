using Nexmo.Api.Messaging;
using Nexmo.Api.Request;

namespace MerchIndex.Auto.Services.Messaging
{
    //https://developer.vonage.com/en/api/sms
    public class SmsService
    {
        public IConfiguration Configuration { get; set; }
        public SmsService(IConfiguration config)
        {
            Configuration = config;
        }

        public SendSmsResponse Send(string to, string from, string text)
        {
            var apiKey = "a6c66419";
            var apiSecret = "nYxJYK63MoiwKuOn";
            // var apiKey = Configuration["VONAGE_API_KEY"];
            // var apiSecret = Configuration["VONAGE_API_SECRET"];
            var creds = Credentials.FromApiKeyAndSecret(apiKey, apiSecret);
            var client = new SmsClient(creds);
            var request = new SendSmsRequest
            {
                To = to,
                From = from,
                Text = text
            };
            return client.SendAnSms(request);
        }
    }
}
