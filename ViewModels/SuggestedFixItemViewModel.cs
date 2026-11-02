using System.Windows.Input;
using IWSEndpointCompanion.Models;

namespace WPFUI.ViewModels;

/// <summary>
/// Presentation wrapper for a suggested fix action card on the Home dashboard.
/// Wraps an ActionDefinition from the Phase 1 domain models.
/// </summary>
public class SuggestedFixItemViewModel : ViewModelBase
{
    public SuggestedFixItemViewModel(
        ActionDefinition action,
        string iconResourceKey,
        ICommand? fixCommand = null,
        ICommand? viewCommand = null)
    {
        Action = action;
        IconResourceKey = iconResourceKey;
        FixCommand = fixCommand;
        ViewCommand = viewCommand;
    }

    public ActionDefinition Action { get; }
    public string IconResourceKey { get; }

    public string Name => Action.Name;
    public string Description => Action.Description;
    public string Category => Action.Category;
    public string? WarningMessage => Action.WarningMessage;
    public bool RequiresConfirmation => Action.RequiresConfirmation;

    public string EstimatedDurationText => Action.EstimatedDuration.HasValue
        ? $"~{Math.Round(Action.EstimatedDuration.Value.TotalMinutes)} mins"
        : string.Empty;

    public bool HasEstimatedDuration => Action.EstimatedDuration.HasValue;

    public ICommand? FixCommand { get; }
    public ICommand? ViewCommand { get; }
}
