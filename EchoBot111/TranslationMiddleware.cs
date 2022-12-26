
using EchoBot111.Services;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot111
{
    public class TranslationMiddleware : IMiddleware
    {
        private readonly string _apiKey;
        private readonly string _toLanguage;
        public  readonly string finals;
        string subscriptionKey = "------------------------------";
        string endpointlanguagetransultor = "https://api.cognitive.microsofttranslator.com/";
        private static readonly string location = "eastus";

        public TranslationMiddleware(string apiKey, string toLanguage)
        {
            _apiKey = apiKey;
            _toLanguage = toLanguage;
        }

        public async Task OnTurnAsync(ITurnContext turnContext, NextDelegate next, CancellationToken cancellationToken = default)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {

                string userUtterance = turnContext.Activity.Text.ToString();
                string targetLanguage = "hi";
                string sourceLanguage = "en";

                //    // Input and output languages are defined as parameters.
               
                string route = "/translate?api-version=3.0&from=en&to=" + targetLanguage;
                string textToTranslate = userUtterance;
                object[] body = new object[] { new { Text = textToTranslate } };
                var requestBody = JsonConvert.SerializeObject(body);

                var client = new HttpClient();
                using (var request = new HttpRequestMessage())
                {
                    // Build the request.
                    request.Method = HttpMethod.Post;
                    request.RequestUri = new Uri(endpointlanguagetransultor + route);
                    request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                    request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
                    // location required if you're using a multi-service or regional (not global) resource.
                    request.Headers.Add("Ocp-Apim-Subscription-Region", location);

                    // Send the request and get response.
                    HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
                    // Read response as a string.
                    string result = await response.Content.ReadAsStringAsync();
                    Activity activity1 = new Activity
                    {
                        Type = ActivityTypes.Message,
                        Text = result,
                        Conversation = new ConversationAccount
                        {
                            Id = "12345"
                        }
                    };


                    //await turnContext.SendActivityAsync(activity1, CancellationToken.None);

                    // Replace the original text with the translated text
                    turnContext.Activity.Text = activity1.ToString() ;
                }

                


            }

            await next(cancellationToken);
        }
    }
}
