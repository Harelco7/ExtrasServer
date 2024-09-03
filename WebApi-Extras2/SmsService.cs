using Microsoft.Extensions.Configuration;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

public class SmsService
{
    private readonly string _accountSid;
    private readonly string _authToken;
    private readonly string _twilioNumber;

    public SmsService(IConfiguration configuration)
    {
        // Access configuration values correctly using the section key
        _accountSid = configuration["Twilio:AccountSID"];
        _authToken = configuration["Twilio:AuthToken"];
        _twilioNumber = configuration["Twilio:PhoneNumber"];

        // Initialize the Twilio client with these values
        TwilioClient.Init(_accountSid, _authToken);
    }

    public void SendSms(string to, string message)
    {
        // Send an SMS message using the Twilio API
        var messageOptions = new CreateMessageOptions(
            new PhoneNumber(to))
        {
            From = new PhoneNumber(_twilioNumber),
            Body = message
        };

        var messageResult = MessageResource.Create(messageOptions);
        Console.WriteLine(messageResult.Body); // Optionally log the message body or SID
    }
}
