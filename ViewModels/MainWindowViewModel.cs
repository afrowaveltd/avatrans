using System;
using Avatrans.Services;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Avatrans.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = string.Empty;

    public MainWindowViewModel(ILocalizationService localizationService) 
        : base(localizationService)
    {
        UpdateLocalizedStrings();
    }

    protected override void OnLanguageChanged(object? sender, EventArgs e)
    {
        base.OnLanguageChanged(sender, e);
        UpdateLocalizedStrings();
    }

    private void UpdateLocalizedStrings()
    {
        Greeting = Localize("WelcomeMessage");
    }
}

