

namespace Microsoft.Bot.Sample.LuisBot
{
    using System;
    using System.Configuration;
    using System.Threading.Tasks;
    using Microsoft.Bot.Connector;
    using Microsoft.Bot.Builder.Dialogs;
    using Microsoft.Bot.Builder.Luis;
    using Microsoft.Bot.Builder.Luis.Models;
    using LuisBot;
    using Microsoft.Bot.Builder.FormFlow;
    using System.Collections.Generic;
    using System.Linq;
    using Microsoft.Cognitive.LUIS.ActionBinding.Bot;
    using System.Reflection;
    // For more information about this template visit http://aka.ms/azurebots-csharp-luis
    [Serializable]
    public class BasicLuisDialog : LuisActionDialog<object>
    {
        public BasicLuisDialog() : base(
            new Assembly[] { typeof(SoftwareInstallAction).Assembly },
            (action, context) =>
            {
                // Here you can implement a callback to hydrate action contexts as per request

                // For example:
                // If your action is related with a 'Booking' intent, then you could do something like:
                // BookingSystem.Hydrate(action) - hydrate action context already stored within some repository
                // (ex. using a booking ref that you can get from the context somehow)

                // To simply showcase the idea, here we are setting the checkin/checkout dates for 1 night
                // when the user starts a contextual intent related with the 'FindHotelsAction'

                // So if you simply write 'Change location to Madrid' the main action will have required parameters already set up
                // and, as in this case the context is an IDialogContext, you can get the user information for any purpose
               
            }, new LuisService(new LuisModelAttribute(
            ConfigurationManager.AppSettings["LuisAppId"], 
            ConfigurationManager.AppSettings["LuisAPIKey"], 
            domain: ConfigurationManager.AppSettings["LuisAPIHostName"])))
        {
        }
        /*
        [LuisIntent("None")]
        public async Task NoneIntent(IDialogContext context, LuisResult result)
        {
            await context.PostAsync("This is none intent");
            await this.ShowLuisResult(context, result);
        }

        // Go to https://luis.ai and create a new intent, then train/publish your luis app.
        // Finally replace "Gretting" with the name of your newly created intent in the following handler
        [LuisIntent("Greeting")]
        public async Task GreetingIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Cancel")]
        public async Task CancelIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }

        [LuisIntent("Help")]
        public async Task HelpIntent(IDialogContext context, LuisResult result)
        {
            await this.ShowLuisResult(context, result);
        }
        */

        [LuisIntent("Software Installation")]
        public async Task IntentActionResultHandlerAsync(IDialogContext context, object actionResult)
        {
            // we know these actions return a string for their related intents,
            // although you could have individual handlers for each intent
            var message = context.MakeMessage();

            message.Text = actionResult != null ? actionResult.ToString() : "Cannot resolve your query";

            await context.PostAsync(message);
        }

        private async Task ShowLuisResult(IDialogContext context, LuisResult result) 
        {
            await context.PostAsync($"You have reached {result.Intents[0].Intent}. You said: {result.Query}");
            context.Wait(MessageReceived);
        }
    }
}