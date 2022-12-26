// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using System;

namespace EchoBot111.Bots
{
    public class ConversationFlow
    {
        // Identifies the last question asked.
        public enum Question
        {
            Name,
            Age,
            Date,
            None, // Our last action did not involve a question.
        }

        // The last question asked.
        public Question LastQuestionAsked { get; set; } = Question.None;

        public bool nameconv;




        // Property to store the timestamp of the last activity
        public DateTime LastActiveTimestamp { get; set; }

        // Other properties for storing user information or conversation state
        public string UserName { get; set; }
        public int CurrentStep { get; set; }



    }
}
