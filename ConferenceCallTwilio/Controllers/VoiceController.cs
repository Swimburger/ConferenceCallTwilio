using System.Web.Mvc;
using Twilio.AspNet.Mvc;
using Twilio.TwiML;
using Twilio.TwiML.Voice;

namespace ConferenceCallTwilio.Controllers
{
    public class VoiceController : TwilioController
    {
        private const string Moderator = "+1234567890";

        [HttpPost]
        public ActionResult Index(string from)
        {
            var response = new VoiceResponse();
            var dial = new Dial();

            // If the caller is our MODERATOR, then start the conference when they
            // join and end the conference when they leave
            if (from == Moderator)
            {
                dial.Conference("My conference",
                                startConferenceOnEnter: true,
                                endConferenceOnExit: true);
            }
            else
            {
                // Otherwise have the caller join as a regular participant
                dial.Conference("My conference",
                                startConferenceOnEnter: false);
            }

            response.Append(dial);

            return TwiML(response);
        }
    }
}