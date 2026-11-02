using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using Microsoft.Extensions.Logging;
using WPFUI.Services.Theme;

namespace WPFUI.ViewModels;

/// <summary>
/// Root ViewModel for MainWindow, managing shell navigation, header state, and theme switching.
/// </summary>
public class MainViewModel : ViewModelBase
{
    private readonly ILogger<MainViewModel>? _logger;
    private readonly IThemeService _themeService;
    private ViewModelBase _currentPageViewModel;
    private NavigationItemViewModel? _selectedNavigationItem;
    private string _pageTitle = "Home";
    private bool _isNavExpanded = true;
    private bool _isDarkTheme;
    private string _searchQuery = string.Empty;
    private int _alertCount = 3;
    private string _agentStatusText = "Agent Connected";
    private bool _isAgentConnected = true;
    private string _userName = "Dinesh Kumar";
    private string _userInitials = "DK";

    /// <summary>
    /// Parameterless constructor for XAML designer and backward compatibility.
    /// </summary>
    public MainViewModel()
        : this(new HomeViewModel(), new ThemeService(null), null)
    {
    }

    /// <summary>
    /// Dependency injection constructor.
    /// </summary>
    public MainViewModel(
        HomeViewModel homeViewModel,
        IThemeService themeService,
        ILogger<MainViewModel>? logger = null)
    {
        _logger = logger;
        _themeService = themeService ?? throw new ArgumentNullException(nameof(themeService));

        _isDarkTheme = _themeService.IsDarkTheme;
        _themeService.ThemeChanged += OnThemeChanged;

        _currentPageViewModel = homeViewModel ?? new HomeViewModel();

        NavigationItems = new ObservableCollection<NavigationItemViewModel>();

        ToggleNavCommand = new RelayCommand(ExecuteToggleNav);
        ToggleThemeCommand = new RelayCommand(ExecuteToggleTheme);
        SelectNavItemCommand = new RelayCommand(ExecuteSelectNavItem);

        InitializeNavigation();

        _logger?.LogInformation("MainViewModel initialized via Dependency Injection.");
    }

    public ObservableCollection<NavigationItemViewModel> NavigationItems { get; }

    public ViewModelBase CurrentPageViewModel
    {
        get => _currentPageViewModel;
        set => SetProperty(ref _currentPageViewModel, value);
    }

    public NavigationItemViewModel? SelectedNavigationItem
    {
        get => _selectedNavigationItem;
        set => SetProperty(ref _selectedNavigationItem, value);
    }

    public string PageTitle
    {
        get => _pageTitle;
        set => SetProperty(ref _pageTitle, value);
    }

    public bool IsNavExpanded
    {
        get => _isNavExpanded;
        set
        {
            if (SetProperty(ref _isNavExpanded, value))
            {
                OnPropertyChanged(nameof(NavWidth));
            }
        }
    }

    public double NavWidth => IsNavExpanded ? 220 : 64;

    public bool IsDarkTheme
    {
        get => _isDarkTheme;
        set => SetProperty(ref _isDarkTheme, value);
    }

    public string SearchQuery
    {
        get => _searchQuery;
        set => SetProperty(ref _searchQuery, value);
    }

    public int AlertCount
    {
        get => _alertCount;
        set => SetProperty(ref _alertCount, value);
    }

    public string AgentStatusText
    {
        get => _agentStatusText;
        set => SetProperty(ref _agentStatusText, value);
    }

    public bool IsAgentConnected
    {
        get => _isAgentConnected;
        set => SetProperty(ref _isAgentConnected, value);
    }

    public string UserName
    {
        get => _userName;
        set => SetProperty(ref _userName, value);
    }

    public string UserInitials
    {
        get => _userInitials;
        set => SetProperty(ref _userInitials, value);
    }

    public ICommand ToggleNavCommand { get; }
    public ICommand ToggleThemeCommand { get; }
    public ICommand SelectNavItemCommand { get; }

    private void InitializeNavigation()
    {
        var navHome = new NavigationItemViewModel("Home", "Home", "Icon_Home", isSelected: true, command: SelectNavItemCommand);
        var navFix = new NavigationItemViewModel("FixIssue", "Fix an Issue", "Icon_FixIssue", command: SelectNavItemCommand);
        var navOpt = new NavigationItemViewModel("Optimize", "Optimize Performance", "Icon_Optimize", command: SelectNavItemCommand);
        var navAlerts = new NavigationItemViewModel("Alerts", "Alerts", "Icon_Alerts", badgeCount: 3, command: SelectNavItemCommand);
        var navMyPC = new NavigationItemViewModel("MyPC", "My PC", "Icon_MyPC", command: SelectNavItemCommand);
        var navHistory = new NavigationItemViewModel("History", "History", "Icon_History", command: SelectNavItemCommand);
        var navTools = new NavigationItemViewModel("Tools", "Tools", "Icon_Tools", command: SelectNavItemCommand);
        var navSupport = new NavigationItemViewModel("Support", "Support", "Icon_Support", command: SelectNavItemCommand);
        var navSettings = new NavigationItemViewModel("Settings", "Settings", "Icon_Settings", command: SelectNavItemCommand);

        NavigationItems.Add(navHome);
        NavigationItems.Add(navFix);
        NavigationItems.Add(navOpt);
        NavigationItems.Add(navAlerts);
        NavigationItems.Add(navMyPC);
        NavigationItems.Add(navHistory);
        NavigationItems.Add(navTools);
        NavigationItems.Add(navSupport);
        NavigationItems.Add(navSettings);

        _selectedNavigationItem = navHome;
    }

    private void ExecuteToggleNav()
    {
        IsNavExpanded = !IsNavExpanded;
        _logger?.LogInformation("Sidebar navigation toggled. IsNavExpanded: {IsNavExpanded}, NavWidth: {NavWidth}", IsNavExpanded, NavWidth);
    }

    private void ExecuteSelectNavItem(object? parameter)
    {
        if (parameter is NavigationItemViewModel item)
        {
            if (item.Id == "Home")
            {
                foreach (var nav in NavigationItems)
                {
                    nav.IsSelected = (nav == item);
                }
                SelectedNavigationItem = item;
                PageTitle = item.Title;
                _logger?.LogInformation("Navigation item selected: {Title} ({Id})", item.Title, item.Id);
            }
            else
            {
                _logger?.LogWarning("Navigated to placeholder screen: {Title} ({Id})", item.Title, item.Id);
                // In Phase 2, remaining screens are visual placeholders
                MessageBox.Show(
                    $"'{item.Title}' screen is a placeholder for design demonstration in Phase 2.\nThis screen will be implemented in subsequent phases.",
                    "Screen Placeholder",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
        }
    }

    private void ExecuteToggleTheme()
    {
        _logger?.LogInformation("Theme toggle initiated by user. Current IsDarkTheme: {IsDarkTheme}", IsDarkTheme);
        _themeService.ToggleTheme();
    }

    private void OnThemeChanged(object? sender, bool isDark)
    {
        IsDarkTheme = isDark;
    }
}
