using JetBrains.Annotations;

namespace LearningPlatformApi.Settings;

[UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
public class EmailSettings
{
    public required string SmtpFrom { get; set; }

    public required string SmtpHost { get; set; }

    public int SmtpPort { get; set; }

    public required string SmtpUsername { get; set; }

    public required string SmtpPassword { get; set; }

    public required string SendConfirmationUrl { get; set; }
}