using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Bot.Builder.FormFlow;

namespace LuisBot
{
    public class SoftwareQuery
    {
        [Prompt("Please enter your {&}")]
        [Optional]
        public string software { get; set; }

        [Prompt("Enter version")]
        [Optional]
        public string version { get; set; }
    }
}