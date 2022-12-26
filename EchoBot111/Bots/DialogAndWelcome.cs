// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace EchoBot111.Bots
{
    public class DialogAndWelcomeBot<T> : EchoBot<T> where T : Dialog
    {
        public DialogAndWelcomeBot(ConversationState conversationState, UserState userState, T dialog, ILogger<EchoBot<T>> logger)
            : base(conversationState, userState, dialog, logger)
        {
        }



        protected override async Task OnMembersAddedAsync(IList<ChannelAccount> membersAdded, ITurnContext<IConversationUpdateActivity> turnContext, CancellationToken cancellationToken)
        {
            var welcomeText = "Hello and welcome!";
            foreach (var member in membersAdded)
            {
                if (member.Id != turnContext.Activity.Recipient.Id)
                {
                    await turnContext.SendActivityAsync(MessageFactory.Text(welcomeText, welcomeText), cancellationToken);
                }
            }
        }



        //protected override async Task OnMembersAddedAsync(
        //    IList<ChannelAccount> membersAdded,
        //    ITurnContext<IConversationUpdateActivity> turnContext,
        //    CancellationToken cancellationToken)
        //{
        //    foreach (var member in membersAdded)
        //    {
        //        // Greet anyone that was not the target (recipient) of this message.
        //        // To learn more about Adaptive Cards, see https://aka.ms/msbot-adaptivecards for more details.
        //        if (member.Id != turnContext.Activity.Recipient.Id)
        //        {
        //            var reply = MessageFactory.Text($"Welcome to Complex Dialog Bot {member.Name}. " +
        //                "This bot provides a complex conversation, with multiple dialogs. " +
        //                "Type anything to get started.");
        //            await turnContext.SendActivityAsync(reply, cancellationToken);
        //        }
        //    }
        //}
    }
}