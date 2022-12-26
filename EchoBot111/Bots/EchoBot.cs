// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using AdaptiveCards;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using System.Linq;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Net.Http;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Xml.Linq;
using System.IO.Pipelines;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Bot.Builder.Dialogs;
using System.Text.RegularExpressions;
using Microsoft.Azure.Documents.Client;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Cosmos;
using System.Text;
using Microsoft.Bot.Connector;
using Activity = Microsoft.Bot.Schema.Activity;

namespace EchoBot111.Bots
{


    //public class EchoBot : ActivityHandler

    public class EchoBot<T> : ActivityHandler where T : Dialog

    {

        /// <summary>
        /// ///////////////////Language transuation
        /// </summary>
        string subscriptionKey = "-----------------------";
        string endpointlanguagetransultor = "https://api.cognitive.microsofttranslator.com/";
        private static readonly string location = "eastus";

        private readonly DocumentClient _client;
        private readonly string _databaseId;
        private readonly string _collectionId;


        private readonly BotState _userState;
        private readonly BotState _conversationState;
        protected readonly Dialog Dialog;
        protected readonly ILogger Logger;

        //public EchoBot(ConversationState conversationState, UserState userState)
        //{
        //    _conversationState = conversationState;
        //    _userState = userState;
        //}


        /// <summary>
        /// ////////Cosmodb 
        /// </summary>
        string endpoint = "https://sainathdatabase.documents.azure.com:443/";
        string key = "-----------------------------------------------";
        string databaseId = "sainath";
        string containerId = "sainathtable";



     
        public EchoBot(ConversationState conversationState, UserState userState, T dialog, ILogger<EchoBot<T>> logger)
        {
            //IConfiguration configuration,
            //string endpoint = configuration["CosmosDb:Endpoint"];
            //string key = configuration["CosmosDb:Key"];
            //_databaseId = configuration["CosmosDb:DatabaseId"];
            //_collectionId = configuration["CosmosDb:CollectionId"];
            //_client = new DocumentClient(new Uri(endpoint), key);

            //string endpoint = Environment.GetEnvironmentVariable("https://sainathdatabase.documents.azure.com:443/");
            //string key = Environment.GetEnvironmentVariable("---------");
            //_databaseId = Environment.GetEnvironmentVariable("sainath");
            //_collectionId = Environment.GetEnvironmentVariable("sainathtable");
            //_client = new DocumentClient(new Uri(endpoint), key);


            _conversationState = conversationState;
            _userState = userState;
            Dialog = dialog;
            Logger = logger;
        }
        public async Task StoreUtteranceAsync(Utterance utterance)
        {
            await _client.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_databaseId, _collectionId),
                utterance);
        }

        public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {


            ///////Language transulation 

            //if (turnContext.Activity.Type != "conversationUpdate")
            //{
            //    string userUtterance = turnContext.Activity.Text.ToString();
            //    string targetLanguage = "hi";
            //    string sourceLanguage = "en";


            //    // Input and output languages are defined as parameters.
            //    //string route = "/translate?api-version=3.0&from=en&to=fr&to=zu";
            //    string route = "/translate?api-version=3.0&from=en&to="+targetLanguage;
            //    string textToTranslate = userUtterance;
            //    object[] body = new object[] { new { Text = textToTranslate } };
            //    var requestBody = JsonConvert.SerializeObject(body);

            //    using (var client = new HttpClient())
            //    using (var request = new HttpRequestMessage())
            //    {
            //        // Build the request.
            //        request.Method = HttpMethod.Post;
            //        request.RequestUri = new Uri(endpointlanguagetransultor + route);
            //        request.Content = new StringContent(requestBody, Encoding.UTF8, "application/json");
            //        request.Headers.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            //        // location required if you're using a multi-service or regional (not global) resource.
            //        request.Headers.Add("Ocp-Apim-Subscription-Region", location);

            //        // Send the request and get response.
            //        HttpResponseMessage response = await client.SendAsync(request).ConfigureAwait(false);
            //        // Read response as a string.
            //        string result = await response.Content.ReadAsStringAsync();

            //        Activity activity1 = new Activity
            //        {
            //            Type = ActivityTypes.Message,
            //            Text = result,
            //            Conversation = new ConversationAccount
            //            {
            //                Id = "12345"
            //            }
            //        };

            //        await turnContext.SendActivityAsync(activity1, CancellationToken.None);
            //    }
            //}







            //CosmosClient client = new CosmosClient(endpoint, key);

            //Microsoft.Azure.Cosmos.Database database = client.GetDatabase(databaseId);
            //Container container = database.GetContainer(containerId);

            //dynamic item = new
            //{
            //    id = Guid.NewGuid(),
            //    name = "sai1",
            //    age = 30,
            //    address = "123 Main Street"
            //};

            //ItemResponse<dynamic> response = await container.CreateItemAsync<dynamic>(item);



            ///// time stamp for session end


            //// Get the conversation state for the current turn
            //var conversationStateAccessor = _conversationState.CreateProperty<ConversationFlow>(nameof(ConversationFlow));
            //var conversationData = await conversationStateAccessor.GetAsync(turnContext, () => new ConversationFlow());

            //// Update the conversation data with the current timestamp
            //conversationData.LastActiveTimestamp = DateTime.UtcNow;


            //// Check if the bot has been inactive for more than 1 minute
            //if (conversationData.LastActiveTimestamp.AddSeconds(10) < DateTime.UtcNow)
            //{
            //    // Send a goodbye message and end the conversation
            //    await turnContext.SendActivityAsync("It looks like you haven't been active for a while. Goodbye!");



            //}



            var activity = turnContext.Activity;

            if(string.IsNullOrWhiteSpace(activity.Text))
            {
                activity.Text = JsonConvert.SerializeObject(activity.Value);
            }
            


            await base.OnTurnAsync(turnContext, cancellationToken);

            // Save any state changes that might have occurred during the turn.
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
            await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        }

        protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        {
            Logger.LogInformation("Running dialog with Message Activity.");

            // Run the Dialog with the new message Activity.
            await Dialog.RunAsync(turnContext, _conversationState.CreateProperty<DialogState>(nameof(DialogState)), cancellationToken);
        }






        //public override async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default)
        //{
        //    // Check if the user sent a message
        //    if (turnContext.Activity.Type == ActivityTypes.Message)
        //    {
        //        // Check if the user requested a file download
        //        string messageText = turnContext.Activity.Text.ToLowerInvariant();
        //        if (messageText.Contains("download"))
        //        {
        //            // Get the file URL
        //            string fileUrl = "https://testazure11111.blob.core.windows.net/test/SainathReddyDasireddy-HCL.pdf";

        //            // Create the attachment
        //            Attachment attachment = new Attachment
        //            {
        //                ContentUrl = fileUrl,
        //                ContentType = "application/pdf",
        //                Name = "sample.pdf"
        //            };




        //            // Send the attachment to the user
        //            var message = MessageFactory.Text("Here is the file you requested:");
        //            message.Attachments = new List<Attachment> { attachment };
        //            await turnContext.SendActivityAsync(message);
        //        }
        //    }
        //}




        ///////////////////This code is handling carousal and cards looping in botframework v4 


        //protected override async Task OnMessageActivityAsync(ITurnContext<IMessageActivity> turnContext, CancellationToken cancellationToken)
        //{
        //using states

        //var conversationStateAccessors = _conversationState.CreateProperty<ConversationFlow>(nameof(ConversationFlow));
        //var flow = await conversationStateAccessors.GetAsync(turnContext, () => new ConversationFlow(), cancellationToken);

        //var userStateAccessors = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
        //var profile = await userStateAccessors.GetAsync(turnContext, () => new UserProfile(), cancellationToken);


        //// Check the current message text to see if the user is asking for their name.
        //if (turnContext.Activity.Text.Equals("what is my name?", StringComparison.OrdinalIgnoreCase))
        //{
        //    // Get the user's name from the user state.
        //    var userName = profile.Name;

        //    // If the user's name is not stored in the user state, ask the user for their name.
        //    if (string.IsNullOrEmpty(userName))
        //    {
        //        await turnContext.SendActivityAsync("I don't know your name. What is your name?");

        //        // Set the flag to ask for the user's name.
        //        flow.nameconv = true;
        //    }
        //    else
        //    {
        //        // If the user's name is stored in the user state, tell the user their name.
        //        await turnContext.SendActivityAsync($"Your name is {userName}.");
        //    }
        //}
        //else if (flow.nameconv)
        //{
        //    // If the flag to ask for the user's name is set, store the user's name in the user state.
        //    profile.Name = turnContext.Activity.Text;

        //    // Clear the flag to ask for the user's name.
        //    flow.nameconv = false;

        //    await turnContext.SendActivityAsync("Thank you. I will remember your name.");
        //}
        //else
        //{
        //    // If the bot is not currently asking for the user's name, do something else.
        //    await turnContext.SendActivityAsync("I'm not sure what you want. Can you please rephrase your request?");
        //}

        //// Save the updated conversation and user state.
        //await _conversationState.SaveChangesAsync(turnContext, false, cancellationToken);
        //await _userState.SaveChangesAsync(turnContext, false, cancellationToken);



        ///////////////////////////////////////////////////////////////////////////////////







        //var replyText = $"Echo: {turnContext.Activity.Text}";
        //await turnContext.SendActivityAsync(MessageFactory.Text(replyText, replyText), cancellationToken);


        // Create a list of HeroCard objects to display in the carousel


        //List<HeroCard> cards = new List<HeroCard>();

        //cards.Add(new HeroCard()
        //{
        //    Title = "Card 1",
        //    Subtitle = "This is the first card",
        //    Text = "Some more information about card 1",
        //    Images = new List<CardImage>() { new CardImage("https://s.yimg.com/fz/api/res/1.2/UZnC3LT_Z5u.jbxV1TVI5w--~C/YXBwaWQ9c3JjaGRkO2ZpPWZpdDtoPTI0MDtxPTgwO3c9MzMy/https://s.yimg.com/zb/imgv1/d89c76c4-a90b-327b-91c2-28405b5674f5/t_500x300") },
        //    Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://example.com/card1") }
        //});

        //cards.Add(new HeroCard()
        //{
        //    Title = "Card 2",
        //    Subtitle = "This is the second card",
        //    Text = "Some more information about card 2",
        //    Images = new List<CardImage>() { new CardImage("https://s.yimg.com/fz/api/res/1.2/UZnC3LT_Z5u.jbxV1TVI5w--~C/YXBwaWQ9c3JjaGRkO2ZpPWZpdDtoPTI0MDtxPTgwO3c9MzMy/https://s.yimg.com/zb/imgv1/d89c76c4-a90b-327b-91c2-28405b5674f5/t_500x300") },
        //    Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://example.com/card2") }
        //});


        //cards.Add(new HeroCard()
        //{
        //    Title = "Card 1",
        //    Subtitle = "This is the first card",
        //    Text = "Some more information about card 1",
        //    Images = new List<CardImage>() { new CardImage("https://s.yimg.com/fz/api/res/1.2/UZnC3LT_Z5u.jbxV1TVI5w--~C/YXBwaWQ9c3JjaGRkO2ZpPWZpdDtoPTI0MDtxPTgwO3c9MzMy/https://s.yimg.com/zb/imgv1/d89c76c4-a90b-327b-91c2-28405b5674f5/t_500x300") },
        //    Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://example.com/card1") }
        //});


        //cards.Add(new HeroCard()
        //{
        //    Title = "Card 1",
        //    Subtitle = "This is the first card",
        //    Text = "Some more information about card 1",
        //    Images = new List<CardImage>() { new CardImage("https://s.yimg.com/fz/api/res/1.2/UZnC3LT_Z5u.jbxV1TVI5w--~C/YXBwaWQ9c3JjaGRkO2ZpPWZpdDtoPTI0MDtxPTgwO3c9MzMy/https://s.yimg.com/zb/imgv1/d89c76c4-a90b-327b-91c2-28405b5674f5/t_500x300") },
        //    Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: "https://example.com/card1") }
        //});


        //// Create a carousel of cards
        //Activity reply = (Activity) MessageFactory.Carousel(cards.Select(card => new Attachment { Content = card, ContentType = HeroCard.ContentType }));

        //// Send the carousel to the user
        //await turnContext.SendActivityAsync(reply, cancellationToken);



        // Create an empty list of HeroCard objects
        //List<HeroCard> cards = new List<HeroCard>();

        //// Use a loop to create and add cards to the list
        //for (int i = 1; i <= 10; i++)
        //{
        //    cards.Add(new HeroCard()
        //    {
        //        Title = $"Card {i}",
        //        Subtitle = $"This is card {i}",
        //        Text = $"Some more information about card {i}",
        //        Images = new List<CardImage>() { new CardImage($"https://example.com/image{i}.jpg") },
        //        Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: $"https://example.com/card{i}") }
        //    });
        //}

        //// Create a carousel of cards
        //Activity reply = (Activity)MessageFactory.Carousel(cards.Select(card => new Attachment { Content = card, ContentType = HeroCard.ContentType }));

        //// Send the carousel to the user
        //await turnContext.SendActivityAsync(reply, cancellationToken);




        ////////////////////////////////////////////////////////////////////////////////////////////////////
        ///



        //// Create arrays or lists for the titles, subtitles, and other content for each card
        //string[] titles = { "Card 1", "Card 2", "Card 3" };
        //string[] subtitles = { "This is the first card", "This is the second card", "This is the third card" };
        //string[] texts = { "Some more information about card 1", "Some more information about card 2", "Some more information about card 3" };
        //string[] images = { "https://example.com/image1.jpg", "https://example.com/image2.jpg", "https://example.com/image3.jpg" };
        //string[] buttonValues = { "https://example.com/card1", "https://example.com/card2", "https://example.com/card3" };

        //// Create an empty list of HeroCard objects
        //List<HeroCard> cards = new List<HeroCard>();

        //// Use a loop to create and add cards to the list
        //for (int i = 0; i < titles.Length; i++)
        //{
        //    cards.Add(new HeroCard()
        //    {
        //        Title = titles[i],
        //        Subtitle = subtitles[i],
        //        Text = texts[i],
        //        Images = new List<CardImage>() { new CardImage(images[i]) },
        //        Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: buttonValues[i]) }
        //    });
        //}

        //// Create a carousel of cards
        //Activity reply = (Activity)MessageFactory.Carousel(cards.Select(card => new Attachment { Content = card, ContentType = HeroCard.ContentType }));

        //// Send the carousel to the user
        //await turnContext.SendActivityAsync(reply, cancellationToken);






        ///////////////////////////////////////////////////////////////////////////////////////////////////






        //  example of how you could retrieve data from a SQL database and create a carousel of cards in Bot Framework v4 using C#:

        //// Connect to the SQL database and retrieve the data
        //string connectionString = "YOUR_CONNECTION_STRING";
        //string queryString = "SELECT * FROM Products";

        //using (SqlConnection connection = new SqlConnection(connectionString))
        //{
        //    SqlCommand command = new SqlCommand(queryString, connection);
        //    connection.Open();

        //    // Use a data reader to retrieve the data from the database
        //    SqlDataReader reader = command.ExecuteReader();

        //    // Create an empty list of HeroCard objects
        //    List<HeroCard> cards = new List<HeroCard>();

        //    // Use a loop to create and add cards to the list
        //    while (reader.Read())
        //    {
        //        string productName = reader["ProductName"].ToString();
        //        string productDescription = reader["ProductDescription"].ToString();
        //        decimal productPrice = (decimal)reader["ProductPrice"];

        //        cards.Add(new HeroCard()
        //        {
        //            Title = productName,
        //            Subtitle = $"Price: {productPrice:C}",
        //            Text = productDescription,
        //            Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: $"https://example.com/products/{productName}") }
        //        });
        //    }

        //    // Create a carousel of cards
        //    Activity reply = (Activity)MessageFactory.Carousel(cards.Select(card => new Attachment { Content = card, ContentType = HeroCard.ContentType }));

        //    // Send the carousel to the user
        //    await turnContext.SendActivityAsync(reply, cancellationToken);
        //}

        ///////////////////////////////////////////////////////////////////////////////////////////////////
        ///




        // Define the JSON for the Adaptive Card
        //            string json = @"

        //{
        //    ""type"": ""AdaptiveCard"",
        //    ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
        //    ""version"": ""1.3"",
        //    ""body"": [
        //        {
        //            ""type"": ""TextBlock"",
        //            ""text"": ""sai"",
        //            ""wrap"": true
        //        },
        //        {
        //            ""type"": ""Image"",
        //            ""url"": ""https://s.yimg.com/fz/api/res/1.2/UZnC3LT_Z5u.jbxV1TVI5w--~C/YXBwaWQ9c3JjaGRkO2ZpPWZpdDtoPTI0MDtxPTgwO3c9MzMy/https://s.yimg.com/zb/imgv1/d89c76c4-a90b-327b-91c2-28405b5674f5/t_500x300""
        //        }
        //    ]
        //}
        //";

        //            // Create an AdaptiveCard object from the JSON
        //            AdaptiveCard card = AdaptiveCard.FromJson(json).Card;

        //            // Create an Attachment object for the Adaptive Card
        //            Attachment attachment = new Attachment()
        //            {
        //                ContentType = AdaptiveCard.ContentType,
        //                Content = card
        //            };

        //            // Create an Activity with the Attachment
        //          Activity activity = (Activity)MessageFactory.Attachment(attachment);

        //            // Send the Adaptive Card to the user
        //            await turnContext.SendActivityAsync(activity, cancellationToken);



        ///////////////////////////////////////////////////////////////////////////////////////////////////


        // Define the JSON for the Adaptive Card
        //            string json = @"
        //{
        //  ""type"": ""AdaptiveCard"",
        //  ""$schema"": ""http://adaptivecards.io/schemas/adaptive-card.json"",
        //  ""version"": ""1.3"",
        //  ""body"": [],
        //  ""actions"": []
        //}";

        //            // Create an AdaptiveCard object from the JSON
        //            AdaptiveCard card = AdaptiveCard.FromJson(json).Card;

        //            // Create a list of values to display in the card
        //            List<string> values = new List<string>() { "value1", "value2", "value3" };

        //            // Use a loop to create and add elements to the card's body array
        //            foreach (string value in values)
        //            {
        //                card.Body.Add(new AdaptiveTextBlock()
        //                {
        //                    Text = value,
        //                    Wrap = true
        //                });


        //            }

        //            // Create an Attachment object for the Adaptive Card
        //            Attachment attachment = new Attachment()
        //            {
        //                ContentType = AdaptiveCard.ContentType,
        //                Content = card
        //            };

        //            // Create an Activity with the Attachment
        //            Activity activity = (Activity)MessageFactory.Attachment(attachment);

        //            // Send the Adaptive Card to the user
        //            await turnContext.SendActivityAsync(activity, cancellationToken);


        ///////////////////////////////////////////////////////////////////////////////////////////////////







        // Create a new HttpClient object
        // var client = new HttpClient();




        // // Create a new HttpRequestMessage object
        // var request = new HttpRequestMessage(HttpMethod.Get, "https://pro-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

        // // Set the "Content-Type" header on the request
        //// request.Headers.Add("Content-Type", "application/json");
        // request.Headers.Add("X-CMC_PRO_API_KEY", "--------------------------------");
        // // Send the request and get the response
        // var response = await client.SendAsync(request);

        // // Read the response as a string
        // var responseString = await response.Content.ReadAsStringAsync();

        // JObject jsonObject = JObject.Parse(responseString);


        ////////////////////////////////////////////////////////////////////////////////


        // // Extract the data array from the JObject
        // JArray dataArray = (JArray)jsonObject["data"];

        // Iterate over the data array and print the name and symbol of each object
        //foreach (JObject dataObject in dataArray)
        //{
        //    string name = (string)dataObject["name"];
        //    string symbol = (string)dataObject["symbol"];


        //    await turnContext.SendActivityAsync($"{name} ({symbol})");
        //}

        // only   top2

        //int count = 0;
        //foreach (JObject dataObject in dataArray)
        //{
        //    if (count >= 2)
        //    {
        //        break;
        //    }

        //    string name = (string)dataObject["name"];
        //    string symbol = (string)dataObject["symbol"];
        //    await turnContext.SendActivityAsync($"{name} ({symbol})");

        //    count++;
        //}



        //int count = 0;
        //foreach (JObject dataObject in dataArray)
        //{

        //    if (count >= 2)
        //    {
        //        break;
        //    }

        //    string name = (string)dataObject["name"];
        //     string symbol = (string)dataObject["symbol"];
        //    // Get the tags array and convert it to a string
        //    JArray tagsArray = (JArray)dataObject["tags"];
        //    string tagsString = string.Join(", ", tagsArray.Select(t => (string)t));

        //    // Get the quote USD object and extract the price
        //    JObject quoteUsdObject = (JObject)dataObject["quote"]["USD"];
        //    decimal price = (decimal)quoteUsdObject["price"];

        //    // Construct the final string
        //    string result = $"Tags: {tagsString}, Price: {price}, {name} , {symbol}";
        //    Console.WriteLine(result);

        //    await turnContext.SendActivityAsync(result);

        //    count++;
        //}



        ///////////////////////////////////////////////////////////////////////////////////////////


        // Replace YOUR_API_KEY with your Etherscan API key
        //string apiKey = "--------------------------------------";



        //// Replace CONTRACT_ADDRESS with the contract address of the token you are interested in
        //string contractAddress = "0x95ad61b0a150d79219dcf64e1e6cc01f0b64c4ce";

        //// Construct the API endpoint URL
        //string apiUrl = $"https://api.etherscan.io/api?module=account&action=tokentx&contractaddress={contractAddress}&sort=desc&apikey={apiKey}";

        //// Create an HttpClient to make the request
        //HttpClient client = new HttpClient();








        //// Make the HTTP GET request to the API endpoint
        //HttpResponseMessage response = await client.GetAsync(apiUrl);

        //// Read the response as a string
        //string responseString = await response.Content.ReadAsStringAsync();

        //// Parse the JSON response
        //JObject jsonResponse = JObject.Parse(responseString);

        // Extract the top 5 transactions from the response
        //JArray topTransactions = (JArray)jsonResponse["result"].Take(5);

        //// Iterate over the top transactions and print their details
        //foreach (JObject transaction in topTransactions)
        //{
        //    // Extract the transaction hash, value, and block number
        //    string txHash = (string)transaction["hash"];
        //    string value = (string)transaction["value"];
        //    string blockNumber = (string)transaction["blockNumber"];

        //}


        /////////////////////////////////////////////////////////////////////////////
        ///
        //top1  as the transcations will be more number 



        // JObject firstTransaction = (JObject)jsonResponse["result"][0];

        // // Extract the transaction hash, value, and block number
        // string blockNumber = (string)firstTransaction["blockNumber"];
        // string from = (string)firstTransaction["from"];
        // string to = (string)firstTransaction["to"];
        // string tokenName = (string)firstTransaction["tokenName"];
        // string tokenSymbol = (string)firstTransaction["tokenSymbol"];
        // string gasPrice = (string)firstTransaction["gasPrice"];
        // string confirmations = (string)firstTransaction["confirmations"];

        // string txHash = (string)firstTransaction["hash"];
        // string value = (string)firstTransaction["value"];


        // // Print the transaction details
        // Console.WriteLine($"Transaction: {txHash}");


        // string result = $"block: {blockNumber}, from: {from},to: {to}," +
        //     $" {tokenName} , {tokenSymbol}";


        //await turnContext.SendActivityAsync(result);


        ////////////////////////////////////////////////////////////////////////////////////



        // Extract the total number of transactions from the response
        // int transactionCount = (int)jsonResponse["result"].Count();

        /////////////////////////////////////////////////////////////////////////////////////////////////////


        //string[] titles = { "Card 1", "Card 2", "Card 3" };
        //// create a new AdaptiveCard instance
        //AdaptiveCard card = new AdaptiveCard();

        //// create a new AdaptiveColumnSet and add it to the card
        //AdaptiveColumnSet columnSet = new AdaptiveColumnSet();
        //card.Body.Add(columnSet);

        //// create an AdaptiveColumn for each item in the list and add it to the column set
        //foreach (var item in titles)
        //{
        //    AdaptiveColumn column = new AdaptiveColumn();
        //    column.Items.Add(new AdaptiveTextBlock() { Text = item });
        //    columnSet.Columns.Add(column);
        //}

        //// create an attachment with the AdaptiveCard as its content
        //Attachment attachment = new Attachment()
        //{
        //    ContentType = AdaptiveCard.ContentType,
        //    Content = card
        //};

        //// send the attachment as a response to the user
        //await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment));

        ////////////////////////////////////////////////////////////////////////////////

        //string[] titles = { "Card 1", "Card 2", "Card 3" };


        //List<string> listData = new List<string>() { "item1", "item2", "item3" };

        //foreach (string item in listData)
        //{
        //    await turnContext.SendActivityAsync(item);
        //}


        //// Define the list of values to loop through
        //List<string> values = new List<string> { "value1", "value2", "value3" };

        //// Define the Adaptive Card
        //AdaptiveCard card = new AdaptiveCard();

        //// Loop through the values and add an Input.Text element to the card for each one
        //foreach (string value in values)
        //{
        //    // Create a new Input.Text element
        //    //AdaptiveTextInput input = new AdaptiveTextInput()
        //    //{
        //    //    Placeholder = value,
        //    //    Id = value,
        //    //    Label = value
        //    //};


        //    AdaptiveTextBlock input = new AdaptiveTextBlock()
        //    {
        //        Text = value,

        //    };


        //    // Add the Input.Text element to the body of the card
        //    card.Body.Add(input);
        //}
        //// create an attachment with the AdaptiveCard as its content
        //Attachment attachment = new Attachment()
        //{
        //    ContentType = AdaptiveCard.ContentType,
        //    Content = card
        //};

        //// send the attachment as a response to the user
        //await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment));


        //////////////////////////////////////////////////////////////////////////////


        //            var card = new AdaptiveCard();

        //            // Create a list of choices
        //            List<AdaptiveChoice> choices = new List<AdaptiveChoice>()
        //{
        //    new AdaptiveChoice() { Title = "Choice 1", Value = "1" },
        //    new AdaptiveChoice() { Title = "Choice 2", Value = "2" },
        //    new AdaptiveChoice() { Title = "Choice 3", Value = "3" }
        //};

        //            // Add the list of choices to the AdaptiveCard
        //            card.Body.Add(new AdaptiveChoiceSetInput()
        //            {
        //                Placeholder="select",
        //                Id = "choiceSet",
        //                Choices = choices,
        //                IsMultiSelect = false
        //            });

        //            // Add the AdaptiveCard to the message
        //            var attachment = new Attachment()
        //            {
        //                ContentType = AdaptiveCard.ContentType,
        //                Content = card
        //            };

        //            var message = MessageFactory.Attachment(attachment);
        //            await turnContext.SendActivityAsync(message);

        ///////////////////////////////////////////////////////////////////////////////
        //// Create a list of values
        //List<string> values = new List<string>() { "1", "2", "3" };

        //// Create a list of titles
        //List<string> titles = new List<string>() { "Choice 1", "Choice 2", "Choice 3" };
        //var card = new AdaptiveCard();
        //// Create a list of choices
        //List<AdaptiveChoice> choices = new List<AdaptiveChoice>();





        //// Iterate through the values and titles lists and create a new Choice object for each item
        //for (int i = 0; i < values.Count; i++)
        //{
        //    choices.Add(new AdaptiveChoice() { Title = titles[i], Value = values[i] });
        //}

        //// Add the list of choices to the AdaptiveCard
        //card.Body.Add(new AdaptiveChoiceSetInput()
        //{
        //    Placeholder = "select",
        //    Id = "choiceSet",
        //    Choices = choices,
        //    IsMultiSelect = false
        //});

        //card.Actions.Add(new AdaptiveSubmitAction()
        //{
        //    Title = "Submit"
        //});

        //// Add the AdaptiveCard to the message
        //var attachment = new Attachment()
        //{
        //    ContentType = AdaptiveCard.ContentType,
        //    Content = card
        //};

        //var message = MessageFactory.Attachment(attachment);
        //await turnContext.SendActivityAsync(message);

        /////////////////////////////////////////////////////////////////////////////////////////

        // To fetch values from an SQL database and store them in a list in C#, you can use the SqlCommand class and the SqlDataReader class.


        // Create a connection to the database
        //string connectionString = "Server=yourserver;Database=yourdatabase;Trusted_Connection=True;";
        //            using (SqlConnection connection = new SqlConnection(connectionString))
        //            {
        //                connection.Open();

        //                // Create a command to select values from the database
        //                string queryString = "SELECT * FROM YourTable";
        //                SqlCommand command = new SqlCommand(queryString, connection);

        //                // Execute the command and create a reader to read the results
        //                SqlDataReader reader = command.ExecuteReader();

        //                // Create a list to store the values
        //                List<string> values = new List<string>();

        //                // Read the results and add the values to the list
        //                while (reader.Read())
        //                {
        //                    values.Add(reader["YourColumn"].ToString());
        //                }

        //                // Close the reader and connection
        //                reader.Close();
        //                connection.Close();
        //            }

        //            //List<string> values = new List<string>() { "value1", "value2", "value3" };

        //            // Clear the list
        //            //values.Clear();

        //            // The list is now empty



        ///////////////////////////////////////////////////////////////////////////////////////////

        //private async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        //{
        //    // Check if the activity is a message
        //    if (turnContext.Activity.Type == ActivityTypes.Message)
        //    {
        //        // Get the QnA Maker response
        //        var response = await _qnaMaker.GetAnswersAsync(turnContext);
        //        if (response != null && response.Length > 0)
        //        {
        //            // Get the first answer
        //            var answer = response[0];

        //            // Check if the answer has attachments
        //            if (answer.Attachments.Any())
        //            {
        //                // Iterate through the attachments
        //                foreach (var attachment in answer.Attachments)
        //                {
        //                    // Check if the attachment is an image
        //                    if (attachment.ContentType.StartsWith("image/"))
        //                    {
        //                        // Send the image as an attachment
        //                        await turnContext.SendActivityAsync(MessageFactory.Attachment(attachment));
        //                    }
        //                    else
        //                    {
        //                        // Send the text as a message
        //                        await turnContext.SendActivityAsync(answer.Answer);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                // Send the text as a message
        //                await turnContext.SendActivityAsync(answer.Answer);
        //            }
        //        }
        //    }
        //}
        /////////////////////////////////////////////////////////////////////////////





        //  }



        //protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        //{
        //    var welcomeText = "Hello and welcome!";
        //    foreach (var member in membersAdded)
        //    {
        //        if (member.Id != turnContext.Activity.Recipient.Id)
        //        {
        //            await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
        //        }
        //    }
        }











    


}