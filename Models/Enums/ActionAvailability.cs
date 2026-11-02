namespace IWSEndpointCompanion.Models.Enums;

/// <summary>
/// Execution availability state of an action on the endpoint device.
/// </summary>
public enum ActionAvailability
{
    Available = 0,
    Unavailable = 1,
    PrerequisitesNotMet = 2,
    Restricted = 3,
    Disabled = 4
}
