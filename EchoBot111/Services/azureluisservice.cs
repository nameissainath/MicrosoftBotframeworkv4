using Azure;
using Azure.AI.Language.Conversations;
using Azure.Core;
using DocumentFormat.OpenXml.Office2021.DocumentTasks;
using Newtonsoft.Json;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace EchoBot111.Services
{
    public class azureluisservice :iazureservice
    {



        public async Task<azureluismodel> exceute(string textuer)
        {
            var endpoint = "https://sainathluisresource.cognitiveservices.azure.com";
            var key = "7f298fb04aed4cebb04174d2f15fce64";
            var projectName = "sainath";
            var deploymentName = "0.";

            AzureKeyCredential credential = new AzureKeyCredential(key);

           


            Uri endpointurl = new Uri(endpoint);


            var client = new ConversationAnalysisClient(endpointurl, credential);


            /////Once you have created a client, you can call synchronous or asynchronous methods.
            ///

            //Uri endpoint = new Uri("https://myaccount.cognitiveservices.azure.com");
            //AzureKeyCredential credential = new AzureKeyCredential("{api-key}");

            //ConversationAnalysisClient client = new ConversationAnalysisClient(endpoint, credential);


            //string projectName = "Menu";
            //string deploymentName = "production";

            //var data = new
            //{
            //    analysisInput = new
            //    {
            //        conversationItem = new
            //        {
            //            text = "Send an email to Carol about tomorrow's demo",
            //            id = "1",
            //            participantId = "1",
            //        }
            //    },
            //    parameters = new
            //    {
            //        projectName,
            //        deploymentName,

            //        // Use Utf16CodeUnit for strings in .NET.
            //        stringIndexType = "Utf16CodeUnit",
            //    },
            //    kind = "Conversation",
            //};

            //Response response = client.AnalyzeConversation(RequestContent.Create(data));

            //using JsonDocument result = JsonDocument.Parse(response.ContentStream);
            //JsonElement conversationalTaskResult = result.RootElement;
            //JsonElement conversationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");

            //Console.WriteLine($"Top intent: {conversationPrediction.GetProperty("topIntent").GetString()}");

            //Console.WriteLine("Intents:");
            //foreach (JsonElement intent in conversationPrediction.GetProperty("intents").EnumerateArray())
            //{
            //    Console.WriteLine($"Category: {intent.GetProperty("category").GetString()}");
            //    Console.WriteLine($"Confidence: {intent.GetProperty("confidenceScore").GetSingle()}");
            //    Console.WriteLine();
            //}

            //Console.WriteLine("Entities:");
            //foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
            //{
            //    Console.WriteLine($"Category: {entity.GetProperty("category").GetString()}");
            //    Console.WriteLine($"Text: {entity.GetProperty("text").GetString()}");
            //    Console.WriteLine($"Offset: {entity.GetProperty("offset").GetInt32()}");
            //    Console.WriteLine($"Length: {entity.GetProperty("length").GetInt32()}");
            //    Console.WriteLine($"Confidence: {entity.GetProperty("confidenceScore").GetSingle()}");
            //    Console.WriteLine();

            //    if (entity.TryGetProperty("resolutions", out JsonElement resolutions))
            //    {
            //        foreach (JsonElement resolution in resolutions.EnumerateArray())
            //        {
            //            if (resolution.GetProperty("resolutionKind").GetString() == "DateTimeResolution")
            //            {
            //                Console.WriteLine($"Datetime Sub Kind: {resolution.GetProperty("dateTimeSubKind").GetString()}");
            //                Console.WriteLine($"Timex: {resolution.GetProperty("timex").GetString()}");
            //                Console.WriteLine($"Value: {resolution.GetProperty("value").GetString()}");
            //                Console.WriteLine();
            //            }
            //        }
            //    }
            //}


            var data = new
            {
                kind = "Conversation",
                analysisInput = new
                {
                    conversationItem = new
                    {
                        text = textuer,
                        id = "1",
                        participantId = "1",
                    }
                },
                parameters = new
                {
                    projectName,
                    deploymentName,

                    //// Use Utf16CodeUnit for strings in .NET.
                    //stringIndexType = "Utf16CodeUnit",
                }


            };

            Response response = client.AnalyzeConversation(RequestContent.Create(data));


            

            //using JsonDocument result = JsonDocument.Parse(response.ContentStream);
            //JsonElement conversationalTaskResult = result.RootElement;
            //JsonElement conversationPrediction = conversationalTaskResult.GetProperty("result").GetProperty("prediction");

            //Console.WriteLine($"Top intent: {conversationPrediction.GetProperty("topIntent").GetString()}");

            //Console.WriteLine("Intents:");
            //foreach (JsonElement intent in conversationPrediction.GetProperty("intents").EnumerateArray())
            //{
            //    Console.WriteLine($"Category: {intent.GetProperty("category").GetString()}");
            //    Console.WriteLine($"Confidence: {intent.GetProperty("confidenceScore").GetSingle()}");
            //    Console.WriteLine();
            //}

            //Console.WriteLine("Entities:");
            //foreach (JsonElement entity in conversationPrediction.GetProperty("entities").EnumerateArray())
            //{
            //    Console.WriteLine($"Category: {entity.GetProperty("category").GetString()}");
            //    Console.WriteLine($"Text: {entity.GetProperty("text").GetString()}");
            //    Console.WriteLine($"Offset: {entity.GetProperty("offset").GetInt32()}");
            //    Console.WriteLine($"Length: {entity.GetProperty("length").GetInt32()}");
            //    Console.WriteLine($"Confidence: {entity.GetProperty("confidenceScore").GetSingle()}");
            //    Console.WriteLine();

            //    if (entity.TryGetProperty("resolutions", out JsonElement resolutions))
            //    {
            //        foreach (JsonElement resolution in resolutions.EnumerateArray())
            //        {
            //            if (resolution.GetProperty("resolutionKind").GetString() == "DateTimeResolution")
            //            {
            //                Console.WriteLine($"Datetime Sub Kind: {resolution.GetProperty("dateTimeSubKind").GetString()}");
            //                Console.WriteLine($"Timex: {resolution.GetProperty("timex").GetString()}");
            //                Console.WriteLine($"Value: {resolution.GetProperty("value").GetString()}");
            //                Console.WriteLine();
            //            }
            //        }
            //    }
            //}
            if (response.Status==200)
            {

                var content = response.Content.ToString();
                var resultmodel = JsonConvert.DeserializeObject<azureluismodel>(content);

                return resultmodel; 
            }

            return null;
        }
    }
}
