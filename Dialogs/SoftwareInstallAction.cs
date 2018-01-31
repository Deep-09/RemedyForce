namespace Microsoft.Bot.Sample.LuisBot
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.Cognitive.LUIS.ActionBinding;

    [Serializable]
    [LuisActionBinding("Software Installation", FriendlyName = "Software Installation Service Request")]
    public class SoftwareInstallAction : BaseLuisAction
    {
        [Required(ErrorMessage = "Please provide a software")]
        [LuisActionBindingParam(CustomType ="software", Order = 1)]
        public string Software { get; set; }

        [Required(ErrorMessage = "Please provide a version")]
        [LuisActionBindingParam(CustomType = "version", Order = 2)]
        public string Version { get; set; }

        public override Task<object> FulfillAsync()
        {
            return Task.FromResult((object)$"I will install {Software} with version {Version} shortly...");
        }
    }
}