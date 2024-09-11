using System.ComponentModel;

namespace Microservices.Security.Domain.Enums;

public enum LoginProviders
{
    [Description("Internal")]
    Internal,
    [Description("MicrosoftAccount")]
    MicrosoftAccount,
    [Description("Google")]
    Google,
    [Description("Facebook")]
    Facebook
}
