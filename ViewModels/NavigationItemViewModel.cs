using System.Windows.Input;

namespace WPFUI.ViewModels;

/// <summary>
/// Represents a navigation item in the left collapsible navigation sidebar.
/// </summary>
public class NavigationItemViewModel : ViewModelBase
{
    private bool _isSelected;
    private int? _badgeCount;

    public NavigationItemViewModel(
        string id,
        string title,
        string iconResourceKey,
        bool isSelected = false,
        int? badgeCount = null,
        ICommand? command = null)
    {
        Id = id;
        Title = title;
        IconResourceKey = iconResourceKey;
        _isSelected = isSelected;
        _badgeCount = badgeCount;
        Command = command;
    }

    public string Id { get; }
    public string Title { get; }
    public string IconResourceKey { get; }

    public bool IsSelected
    {
        get => _isSelected;
        set => SetProperty(ref _isSelected, value);
    }

    public int? BadgeCount
    {
        get => _badgeCount;
        set => SetProperty(ref _badgeCount, value);
    }

    public bool HasBadge => BadgeCount.HasValue && BadgeCount.Value > 0;

    public ICommand? Command { get; set; }
}
