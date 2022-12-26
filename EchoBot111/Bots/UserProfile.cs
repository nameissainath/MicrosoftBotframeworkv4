// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.
//
// Generated with Bot Builder V4 SDK Template for Visual Studio EchoBot v4.18.1

using System.Collections.Generic;

namespace EchoBot111.Bots
{
    public class UserProfile
    {
        public string Name { get; set; }

        public int? Age { get; set; }

        public string Date { get; set; }


        // The list of companies the user wants to review.
        public List<string> CompaniesToReview { get; set; } = new List<string>();
    }
}