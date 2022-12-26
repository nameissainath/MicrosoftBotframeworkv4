// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Data.SqlClient;

using System.IO;
using System.Linq;

using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using EchoBot111.Bots;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Connector.Authentication;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using AdaptiveCards.Templating;
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
using Activity = Microsoft.Bot.Schema.Activity;
using System.Data.Common;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Azure.Cosmos;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Text;
using Azure.Core;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.InkML;
using DocumentFormat.OpenXml.Drawing.Charts;
using static Antlr4.Runtime.Atn.SemanticContext;
using Microsoft.AspNetCore.Http;
using EchoBot111.Services;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Extensions.Configuration;

using Azure;
using Azure.AI.Language.QuestionAnswering;
using System;


namespace EchoBot111.Bots
{
    public class MainDialog : ComponentDialog
    {


        private readonly HttpClient _httpClient;
        private readonly string _languageStudioApiKey;
        private readonly string _languageStudioModelId;

        Uri endpointqna = new Uri("https://sainathluisresource.cognitiveservices.azure.com/");
        AzureKeyCredential credentialqna = new AzureKeyCredential("-------------");
        string projectName = "qna";
        string deploymentName = "production";


        private readonly iazureservice _iazureservice;
        string connectionStringblob = "DefaultEndpointsProtocol=https;AccountName=sainathteststorage;AccountKey=----------------;EndpointSuffix=core.windows.net";

        private readonly UserState _userState;
        public string connectionString = "Server=tcp:sainathreddy.database.windows.net,1433;Initial Catalog=sainathreddy;Persist Security Info=False;User ID=saiadmin;Password=-----;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        public string queryString = "SELECT ingredient_name FROM ingredients";
        string endpoint = "https://sainathdatabase.documents.azure.com:443/";
        string key = "---------------";
        string databaseId = "sainath";
        string containerId = "sainathtable";
        private List<string> alldcglist = new List<string>();
        //public MainDialog(IConfiguration configuration,UserState userState,iazureservice azureservuce, HttpClient httpClient, string languageStudioApiKey, string languageStudioModelId)

        public MainDialog( UserState userState, iazureservice azureservuce, HttpClient httpClient)

        : base(nameof(MainDialog))
        {

            _httpClient = httpClient;
            //_languageStudioApiKey = languageStudioApiKey;
            //_languageStudioModelId = languageStudioModelId;

            //_languageStudioApiKey = configuration["LANGUAGE_STUDIO_API_KEY"];
            //_languageStudioModelId = configuration["LANGUAGE_STUDIO_MODEL_ID"];


           // _iazureservice = azureservuce;
            _userState = userState;

           AddDialog(new TopLevelDialog());

            AddDialog(new testdialog());

            AddDialog(new WaterfallDialog(nameof(WaterfallDialog), new WaterfallStep[]
            {

                /////QNA sample test latest version
                ///
                AskQuestionStepAsync,
                DisplayAnswerStepAsync

                /////Luis Sample test latest version
                ///
                //firstluis,
                //secondluis

                //InitialStepAsync,
                //FinalStepAsync,

                ////CosmoDB storage and Blob stoarge file stoarge test display with SAS sample test

                //start1,
                //start2
                // start3



                /////////////////adaptive card values insertion ofvalues from sql to dropdown sample test

                // InitialStepAsync1,
                //FinalStepAsync1,
                //FinalStepAsync2,
                //FinalStepAsync3
               // InitialStepAsynctestcolumn

                //////////////////Dialog value store sample test
                ///
                //GetAgeAsync,
                //GetNameAsync,
                //GetTicketsAsync,
                //ConfirmAsync,
                //FinalStepAsyncdata


                ////Dailog to get all values sample test
                //GetInputsAsync,
                //FinalStepAsyncforssingleline
            }));
            
            AddDialog(new ChoicePrompt(nameof(ChoicePrompt)));
            AddDialog(new ConfirmPrompt(nameof(ConfirmPrompt)));
         AddDialog(new TextPrompt(nameof(TextPrompt)));


            AddDialog(new NumberPrompt<int>("agePrompt", ValidateAge));

            // Add the name prompt
            AddDialog(new TextPrompt("namePrompt"));

            // Add the tickets prompt
            AddDialog(new NumberPrompt<int>("ticketsPrompt", ValidateTickets));

            // Add the confirm prompt
            AddDialog(new ConfirmPrompt("confirmPrompt"));

            // Add the inputs prompt
            AddDialog(new TextPrompt("inputsPrompt", ValidateInputs));


            InitialDialogId = nameof(WaterfallDialog);





            
           

        }

        private async Task<DialogTurnResult> AskQuestionStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Prompt the user to enter a question
            return await stepContext.PromptAsync(nameof(TextPrompt), new PromptOptions { Prompt = MessageFactory.Text("Please enter your question:") }, cancellationToken);
        }



        private async Task<DialogTurnResult> DisplayAnswerStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            //string question = "When can I expect the refund for my online order that was cancelled?";
            string question = stepContext.Context.Activity.Text;
            QuestionAnsweringClient client = new QuestionAnsweringClient(endpointqna, credentialqna);
            QuestionAnsweringProject project = new QuestionAnsweringProject(projectName, deploymentName);

            Azure.Response<AnswersResult> response = client.GetAnswers(question, project);

           var qnafinalanswer=  response.Value.Answers[0].Answer.ToString();
            await stepContext.Context.SendActivityAsync(
                  MessageFactory.Text(qnafinalanswer));


            return await stepContext.EndDialogAsync();
        }


        //private async Task<DialogTurnResult> DisplayAnswerStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        //{
        //    // Send the question to the custom question answering model in Language Studio and receive the answer
        //    string question = (string)stepContext.Result;
        //    string answer = await GetAnswerFromLanguageStudioAsync(question);

        //    // Display the answer to the user
        //    await stepContext.Context.SendActivityAsync(MessageFactory.Text(answer), cancellationToken);

        //    // End the dialog
        //    return await stepContext.EndDialogAsync(cancellationToken);
        //}

        //private async Task<string> GetAnswerFromLanguageStudioAsync(string question)
        //{
        //    // Set up the request to the Language Studio API
        //    var request = new HttpRequestMessage(HttpMethod.Post, "https://api.languagestudio.com/v1/answer");
        //    request.Headers.Add("Authorization", $"Bearer {_languageStudioApiKey}");
        //    request.Content = new StringContent($"{{\"modelId\":\"{_languageStudioModelId}\",\"question\":\"{question}\"}}", Encoding.UTF8, "application/json");

        //    // Send the request and receive the response
        //    var response = await _httpClient.SendAsync(request);
        //    response.EnsureSuccessStatusCode();
        //    var responseContent = await response.Content.ReadAsStringAsync();

        //    // Extract the answer from the response
        //    var responseJson = JObject.Parse(responseContent);
        //    return (string)responseJson["answer"];
        //}







        private async Task<DialogTurnResult> firstluis(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var text = stepContext.Context.Activity.Text;

            var response = await _iazureservice.exceute(text);
            var topintent = response.result.prediction.topintent;

            var score = response.result.prediction.intents[0].confidencescore;

            switch(topintent.ToUpper())
            {
                case "FLIGHT":
                    await stepContext.Context.SendActivityAsync(
                   MessageFactory.Text($"Your hello flight"), cancellationToken);

                    // await secondluis1(stepContext);
                    break;
                case "NONE":
                    await stepContext.Context.SendActivityAsync(
                   MessageFactory.Text($"Your hello none"), cancellationToken);
                    break;
                case "GREETING":
                     await stepContext.Context.SendActivityAsync(
                   MessageFactory.Text($"Your hello greeting"), cancellationToken);
                    break;
                default:
                    break;
            }




            return await stepContext.NextAsync();
        }

        //private async Task secondluis1(WaterfallStepContext stepContext)
        //{


        //     await stepContext.Context.SendActivitiesAsync("hello");
        //}



        private async Task<DialogTurnResult> secondluis(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

           
            return await  stepContext.EndDialogAsync();
        }

        private static Task<bool> ValidateInputs(PromptValidatorContext<string> promptContext, CancellationToken cancellationToken)
        {
            // Split the input into age, name, tickets, and confirmation
            var inputs = promptContext.Recognized.Value.Split(',');
            if (inputs.Length != 4)
            {
                return Task.FromResult(false);
            }

            // Validate that the age is an integer greater than 0
            if (!int.TryParse(inputs[0], out int age) || age <= 0)
            {
                return Task.FromResult(false);
            }

            // Validate that the name is not empty
            if (string.IsNullOrEmpty(inputs[1]))
            {
                return Task.FromResult(false);
            }

            // Validate that the tickets is an integer greater than 0
            if (!int.TryParse(inputs[2], out int tickets) || tickets <= 0)
            {
                return Task.FromResult(false);
            }

            // Validate that the confirmation is either "yes" or "no"
            if (!inputs[3].Equals("yes", StringComparison.InvariantCultureIgnoreCase) && !inputs[3].Equals("no", StringComparison.InvariantCultureIgnoreCase))
            {
                return Task.FromResult(false);
            }

            // If all input is valid, return true
            return Task.FromResult(true);
        }



        private async Task<DialogTurnResult> FinalStepAsyncforssingleline(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the inputs the user provided
            var inputs = stepContext.Result.ToString();

            // Split the inputs into age, name, tickets, and confirmation
            var inputsArray = inputs.Split(',');
            var age = int.Parse(inputsArray[0]);
            var name = inputsArray[1];
            var tickets = int.Parse(inputsArray[2]);
            //var confirm = inputsArray[3].Equals("yes", StringComparison.InvariantCultureIgnoreCase);

            // Process the user's order
            //await ProcessOrderAsync(age, name, tickets, confirm, cancellationToken);
            await stepContext.Context.SendActivityAsync(
              MessageFactory.Text($"Your age is {age}, your name is {name}, and you want to purchase {tickets} tickets."),
              cancellationToken);
            // End the dialog
            return await stepContext.EndDialogAsync(cancellationToken);
        }




        private async Task<DialogTurnResult> GetInputsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Prompt the user for their age, name, tickets, and confirmation
            return await stepContext.PromptAsync(
                "inputsPrompt",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("Please enter your age, name, number of tickets, and confirmation (yes/no) separated by commas."),
                    RetryPrompt = MessageFactory.Text("Please enter valid input in the format 'age, name, tickets, confirmation'."),
                },
                cancellationToken);
        }




        //public void InsertIntoTable(string jsonString)
        //{
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        connection.Open();

        //        string query = "INSERT INTO TableName (JsonColumn) VALUES (@jsonValue)";

        //        using (SqlCommand command = new SqlCommand(query, connection))
        //        {
        //            command.Parameters.AddWithValue("@jsonValue", jsonString);
        //            command.ExecuteNonQuery();
        //        }
        //    }
        //}

        private static Task<bool> ValidateAge(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            // Validate that the user's age is greater than 0
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0);
        }

        private static Task<bool> ValidateTickets(PromptValidatorContext<int> promptContext, CancellationToken cancellationToken)
        {
            // Validate that the user is purchasing at least 1 ticket
            return Task.FromResult(promptContext.Recognized.Succeeded && promptContext.Recognized.Value > 0);
        }
        private async Task<DialogTurnResult> GetAgeAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Prompt the user for their age
            return await stepContext.PromptAsync(
                "agePrompt",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your age?"),
                    RetryPrompt = MessageFactory.Text("Please enter a valid age."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> GetNameAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the age the user provided
            var age = (int)stepContext.Result;
            stepContext.Values["age"] = age;

            // Prompt the user for their name
            return await stepContext.PromptAsync(
                "namePrompt",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("What is your name?"),
                    RetryPrompt = MessageFactory.Text("Please enter a valid name."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> GetTicketsAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the name the user provided
            var name = (string)stepContext.Result;
            stepContext.Values["name"] = name;
            // Prompt the user for the number of tickets they want to purchase
            return await stepContext.PromptAsync(
                "ticketsPrompt",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text("How many tickets do you want to purchase?"),
                    RetryPrompt = MessageFactory.Text("Please enter a valid number of tickets."),
                },
                cancellationToken);
        }

        private async Task<DialogTurnResult> ConfirmAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the number of tickets the user provided
            var tickets = (int)stepContext.Result;
            stepContext.Values["tickets"] = tickets;


           
           

            // Prompt the user to confirm their order
            return await stepContext.PromptAsync(
                "confirmPrompt",
                new PromptOptions
                {
                    Prompt = MessageFactory.Text($"Do you want to purchase {tickets} tickets?"),
                    RetryPrompt = MessageFactory.Text("Please confirm your order by entering 'yes' or 'no'."),
                },
                cancellationToken);
        }



        private async Task<DialogTurnResult> FinalStepAsyncdata(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // Get the age, name, tickets, and confirmation values from the previous steps
            var age = (int)stepContext.Values["age"];
            var name = (string)stepContext.Values["name"];
            var tickets = (int)stepContext.Values["tickets"];
            var confirm = (bool)stepContext.Result;

            // Show the user the information they provided
            await stepContext.Context.SendActivityAsync(
                MessageFactory.Text($"Your age is {age}, your name is {name}, and you want to purchase {tickets} tickets. Your order is confirmed: {confirm}."),
                cancellationToken);

            // Process the user's order
           // await ProcessOrderAsync(age, name, tickets, confirm, cancellationToken);

            // End the dialog
            return await stepContext.EndDialogAsync(cancellationToken);
        }




        //private async Task ProcessOrderAsync(int age, string name, int tickets, bool confirm, CancellationToken cancellationToken)
        //{
        //    if (confirm)
        //    {
        //        // Save the order to the database
        //        await SaveOrderAsync(age, name, tickets, cancellationToken);

        //        // Send a confirmation email to the user
        //        await SendConfirmationEmailAsync(age, name, tickets, cancellationToken);
        //    }
        //    else
        //    {
        //        // Send a message to the user indicating that their order was not confirmed
        //        await stepContext.Context.SendActivityAsync(
        //            MessageFactory.Text("Your order was not confirmed."),
        //            cancellationToken);
        //    }
        //}































        private async Task<DialogTurnResult> start1(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
           


            //////cosmodb connection
            //CosmosClient client = new CosmosClient(endpoint, key);

            //Microsoft.Azure.Cosmos.Database database = client.GetDatabase(databaseId);
            //Container container = database.GetContainer(containerId);

            //dynamic item = new
            //{
            //    id = Guid.NewGuid(),
            //    name = stepContext.Context.Activity.Text.ToString(),
            //    age = 30,
            //    address = "123 Main Street"
            //};

            //ItemResponse<dynamic> response = await container.CreateItemAsync<dynamic>(item);
            //var jsonObject = new
            //{
            //    name = "John",
            //    age = 30,
            //    address = "123 Main Street"
            //};

            //// Convert the object to a JSON string
            //string jsonString = JsonConvert.SerializeObject(jsonObject);

            //// Insert the JSON string into the table
            //InsertIntoTable(jsonString);

            //await stepContext.Context.SendActivityAsync("It");
            //return await stepContext.NextAsync();




            // Create a CloudStorageAccount object from the connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionStringblob);

            // Create a BlobClient object
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Get a reference to the container
            CloudBlobContainer container = blobClient.GetContainerReference("sai");

            // Set the permissions so the blobs are public
            BlobContainerPermissions permissions = new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            };
            //await container.SetPermissionsAsync(permissions);

            // Get a reference to the file
            CloudBlockBlob blob = container.GetBlockBlobReference("test.txt");

            // Get a reference to the image
            CloudBlockBlob imageBlob = container.GetBlockBlobReference("dhoni.jpg");


            // Get a reference to the Excel file
            CloudBlockBlob excelBlob = container.GetBlockBlobReference("test.xlsx");


            // Create a SAS token for the file
            string sasToken = blob.GetSharedAccessSignature(new SharedAccessBlobPolicy
            {
                Permissions = SharedAccessBlobPermissions.Read,
                SharedAccessExpiryTime = DateTime.UtcNow.AddSeconds(20)
            });



            ////You can now use the SAS token to authenticate requests to the file
            //   string fileUrl = blob.Uri + sasToken;
            //string fileUrlimage = imageBlob.Uri + sasToken;
            //// Download the file
            //using (MemoryStream stream = new MemoryStream())
            //{
            //    //attaiv cards

            //    await imageBlob.DownloadToStreamAsync(stream);

            //    // Send the image to the user as an attachment
            //    Attachment imageAttachment = new Attachment
            //    {
            //        ContentType = "image/jpeg",
            //        ContentUrl = imageBlob.Uri.AbsoluteUri,
            //        Name = "myimage.jpg"
            //    };
            //    Activity reply = (Activity)MessageFactory.Attachment(imageAttachment);
            //    await stepContext.Context.SendActivityAsync(reply);


            //    ///link
            //    //await stepContext.Context.SendActivityAsync($"Here is the file: {fileUrl}");


            //    //////txt file
            //    // await blob.DownloadToStreamAsync(stream);

            //    // Display the file in the bot
            //    // Convert the byte array to a string
            //    //string fileData = Encoding.UTF8.GetString(stream.ToArray());

            //    //// Send the file data to the user
            //    //await stepContext.Context.SendActivityAsync($"Here is the file data: {fileData}");

            //    //await stepContext.Context.SendActivityAsync($"Here is the file: {stream.ToArray()}");
            //}

            













            return await stepContext.NextAsync();
        }
        private async Task<DialogTurnResult> start2(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.EndDialogAsync();
        }
        private async Task<DialogTurnResult> InitialStepAsynctestcolumn(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {

            string pathm = @"Cards\tempdatatable.json";
            var filereadm = File.ReadAllText(pathm);

            var itemm = (JObject)JsonConvert.DeserializeObject(filereadm);
            var classdata = itemm.ToString();

            AdaptiveCardTemplate template = new AdaptiveCardTemplate(filereadm);

            var cardjson2 = template.Expand(classdata);

            JObject jsonObject = JObject.Parse(cardjson2);


         

            // Extract the value of the "type" property
            string type = (string)jsonObject["type"];

            // Extract the value of the "version" property
            string version = (string)jsonObject["version"];

            // Extract the value of the "body" property
            JArray bodyArray = (JArray)jsonObject["body"];


            //using (SqlConnection connection = new SqlConnection(connectionString))
            //{
            //    //connection.Open();

            //    using (SqlCommand command = new SqlCommand(queryString, connection))
            //    {
            //        using (SqlDataReader reader = command.ExecuteReader())
            //        {
            //            // Build a list of key-value pairs to insert into the JSON object
            //            List<JProperty> properties = new List<JProperty>();

            //            while (reader.Read())
            //            {
            //                // Create a key-value pair for each row of data
            //                JProperty property = new JProperty((string)reader["ingredient_name"]);
            //                properties.Add(property);
            //            }

            //            // Update the values of the key-value pairs in the JSON object
            //            foreach (JProperty property in properties)
            //            {
            //                jsonObject[property.Name] = property.Value;
            //            }
            //        }
            //    }
            //}





















            // Iterate over the items in the "body" array
            //foreach (JObject item in bodyArray.Children<JObject>())
            //{
            //    // Extract the values of the "type" and "columns" properties of the item
            //    string itemType = (string)item["type"];
            //    JArray columnsArray = (JArray)item["columns"];

            //    // Iterate over the items in the "columns" array
            //    foreach (JObject column in columnsArray.Children<JObject>())
            //    {
            //        // Extract the value of the "type" property of the column
            //        string columnType = (string)column["type"];

            //        // Extract the value of the "items" property of the column
            //        JArray itemsArray = (JArray)column["items"];

            //        // Iterate over the items in the "items" array
            //        foreach (JObject item2 in itemsArray.Children<JObject>())
            //        {
            //            // Extract the values of the "type" and "text" properties of the item
            //            string item2Type = (string)item2["type"];
            //            string text = (string)item2["text"];
            //        }
            //    }
            //}












            return await stepContext.NextAsync();


















            // AdaptiveCard card = AdaptiveCard.FromJson(cardjson2).Card;


            //var cardAttachment = new Attachment
            //{
            //    ContentType = "application/vnd.microsoft.card.adaptive",
            //    Content = JObject.Parse(cardjson2)
            //};

            //// Create the prompt options with the card attachment
            //var promptOptions = new PromptOptions
            //{
            //    Prompt = new Activity
            //    {
            //        Attachments = new List<Attachment>() { cardAttachment },
            //        Type = ActivityTypes.Message,
            //    }
            //};

            //// Prompt the user for input
            //return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions, cancellationToken);




            //while (reader.Read())
            //{
            //    // Create a TextBlock element for each row of data
            //    AdaptiveTextBlock textBlock = new AdaptiveTextBlock
            //    {
            //        Text = $"{reader["ProductName"]}: {reader["Quantity"]}"
            //    };
            //    elements.Add(textBlock);
            //}











        }

        private async Task<DialogTurnResult> InitialStepAsync1(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            List<string> datalist = new List<string>();

            try
            {
                alldcglist.Clear();
                string connectionString = "Server=tcp:sainathreddy.database.windows.net,1433;Initial Catalog=sainathreddy;Persist Security Info=False;User ID=saiadmin;Password=------;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                string queryString = "SELECT ingredient_name FROM ingredients";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    connection.Open();

                    // Use a data reader to retrieve the data from the database
                    SqlDataReader reader = command.ExecuteReader();



                    // Use a loop to create and add cards to the list
                    while (reader.Read())
                    {
                        alldcglist.Add(reader["ingredient_name"].ToString());
                    }
                }



                    }
            catch
            {

            }
            return await stepContext.NextAsync();  
        }

        private async Task<DialogTurnResult> FinalStepAsync1(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            dcginfo users = new dcginfo();
            dcgcode a = new dcgcode();

            List<dcgcode> aa1 = new List<dcgcode>();

            var datatest= new List<string> { };

            for (int i=0; i <alldcglist.Count(); i++)
            {
                string s = alldcglist[i];
                string padd = s.PadRight(10);

                aa1.Add(new dcgcode
                {
                    choice = alldcglist[i],
                    value = alldcglist[i]
                });
            }

            users.allitems = aa1;
            var json = JsonConvert.SerializeObject(users);
            string path = @"Cards\tempdata.json";

            string outpath = Newtonsoft.Json.JsonConvert.SerializeObject(json,Newtonsoft.Json.Formatting.Indented);
            

            File.WriteAllText(path, outpath);




            return await stepContext.NextAsync();


        }


        private async Task<DialogTurnResult> FinalStepAsync2(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            string path = @"Cards\tempdata.json";
            var cardpath = Path.Combine(".", path);
            string fileread = File.ReadAllText(cardpath);
            var item= Newtonsoft.Json.JsonConvert.DeserializeObject(fileread);

            var classdata = item.ToString();

            dynamic jsonobj = Newtonsoft.Json.JsonConvert.DeserializeObject(fileread);

            string pathm = @"Cards\tempdatacard.json";
            var filereadm = File.ReadAllText(pathm);

            var itemm = (JObject)JsonConvert.DeserializeObject(filereadm);


            AdaptiveCardTemplate template = new AdaptiveCardTemplate(filereadm);

            var cardjson2 = template.Expand(classdata);

            AdaptiveCard card = AdaptiveCard.FromJson(cardjson2).Card;

            //// Create an Attachment object for the Adaptive Card
            //Attachment attachment = new Attachment()
            //{
            //    ContentType = AdaptiveCard.ContentType,
            //    Content = card
            //};

            ////// Create an Activity with the Attachment
            //Activity activity = (Activity)MessageFactory.Attachment(attachment);

            //// Send the Adaptive Card to the user
            //await stepContext.Context.SendActivityAsync(activity, cancellationToken);


            // Create the attachment for the card
            var cardAttachment = new Attachment
            {
                ContentType = "application/vnd.microsoft.card.adaptive",
                Content = JObject.Parse(cardjson2)
            };

            // Create the prompt options with the card attachment
            var promptOptions = new PromptOptions
            {
                Prompt = new Activity
                {
                    Attachments = new List<Attachment>() { cardAttachment },
                    Type = ActivityTypes.Message,
                }
            };

            // Prompt the user for input
            return await stepContext.PromptAsync(nameof(TextPrompt), promptOptions,cancellationToken);


            //return await stepContext.NextAsync(cancellationToken);






            //var opts = new PromptOptions
            //{
            //    Prompt = new Microsoft.Bot.Schema.Activity
            //    {
            //        Attachments = new List<Attachment>() { attachment },
            //        Type = ActivityTypes.Message,
            //    }
            //};

            // Create an Activity with the Attachment
            //Activity activity = (Activity)MessageFactory.Attachment(attachment);

            // Send the Adaptive Card to the user
            // await stepContext.Context.SendActivityAsync(activity,cancellationToken);





            // return await stepContext.PromptAsync(nameof(TextPrompt), activity, cancellationToken);




            // send the activity with the card attachment
            //await  stepContext.Context.SendActivityAsync(attachment.ToString());
            // return await stepContext.NextAsync();


        }


        private async Task<DialogTurnResult> FinalStepAsync3(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // throw new NotImplementedException();


            var value = stepContext.Result.ToString();

            

                var se = JObject.Parse((string)stepContext.Result)["action"].ToString();


                var se1 = JObject.Parse((string)stepContext.Result)["textdata"].ToString();

                var se2 = JObject.Parse((string)stepContext.Result)["dropdowndata"].ToString();
           

            //catch
            //{

            //stepContext.ActiveDialog.State["stepIndex"] = (int)stepContext.ActiveDialog.State["stepIndex"] - 2;
            //return await stepContext.NextAsync(null, cancellationToken);

                    
            //        }
            return await stepContext.EndDialogAsync();
        }


        private async Task<DialogTurnResult> InitialStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            // return await stepContext.BeginDialogAsync(nameof(TopLevelDialog), null, cancellationToken);

            //  example of how you could retrieve data from a SQL database and create a carousel of cards in Bot Framework v4 using C#:

            //// Connect to the SQL database and retrieve the data
            string connectionString = "Server=tcp:sainathreddy.database.windows.net,1433;Initial Catalog=sainathreddy;Persist Security Info=False;User ID=saiadmin;Password=-----;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
            string queryString = "SELECT * FROM ingredients";

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
            //        string productName = reader["ingredient_name"].ToString();
            //        //string productDescription = reader["ingredient_price"].ToString();
            //        int productPrice = (int)reader["ingredient_price"];

            //    cards.Add(new HeroCard()
            //    {
            //        Title = productName,
            //        Subtitle = $"Price: {productPrice:C}",
            //        Text = productName,
            //        Buttons = new List<CardAction>() { new CardAction(ActionTypes.OpenUrl, "Learn More", value: $"https://example.com/products/{productName}") }
            //    });
            //}

            //    // Create a carousel of cards
            //    Activity reply = (Activity)MessageFactory.Carousel(cards.Select(card => new Attachment { Content = card, ContentType = HeroCard.ContentType }));

            //    // Send the carousel to the user
            //    await stepContext.Context.SendActivityAsync(reply, cancellationToken);

            ////////////////////////////////////////////////////////////////////////////////

           
            //var message = stepContext.Context.Activity.CreateReply();
            //message.Attachments.Add(attachment);
            //await stepContext.Context.SendActivityAsync(message);

            return await stepContext.EndDialogAsync(null, cancellationToken);
            // return await stepContext.BeginDialogAsync(nameof(testdialog), null, cancellationToken);

        }

            private async Task<DialogTurnResult> FinalStepAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            var userInfo = (UserProfile)stepContext.Result;

            string status = "You are signed up to review "
                + (userInfo.CompaniesToReview.Count is 0 ? "no companies" : string.Join(" and ", userInfo.CompaniesToReview))
                + ".";

            await stepContext.Context.SendActivityAsync(status);

            var accessor = _userState.CreateProperty<UserProfile>(nameof(UserProfile));
            await accessor.SetAsync(stepContext.Context, userInfo, cancellationToken);

            return await stepContext.EndDialogAsync(null, cancellationToken);
        }
    }
}
