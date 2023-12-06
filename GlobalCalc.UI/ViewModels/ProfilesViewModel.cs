using System.Linq;
using System.Collections.Generic;

using GlobalCalc.UI.Infrastructure;
using GlobalCalc.Models;


namespace GlobalCalc.UI.ViewModels;

internal class ProfilesViewModel : ViewModelBase
{
    #region Properties

    private IEnumerable<Profile> _profiles;
    public IEnumerable<Profile> Profiles
    {
        get => _profiles;
        set
        {
            _profiles = value;
            OnPropertyChanged();
        }
    }

    private Profile? _selectedProfile;
    public Profile? SelectedProfile
    {
        get => _selectedProfile;
        set
        {
            _selectedProfile = value;
            OnPropertyChanged();
        }
    }

    #endregion

    public ProfilesViewModel()
    {
        _profiles = Enumerable.Empty<Profile>();
    }

    public ProfilesViewModel(IEnumerable<Profile> profiles)
    {
        _profiles = profiles;
    }

    #region Commands 

    private RelayCommand? _selectProfile;
    public RelayCommand SelectProfile =>
        _selectProfile ??= new RelayCommand(_ =>
        {
            if (WindowContext == null)
                return;

            WindowContext.DialogResult = SelectedProfile != null;
            WindowContext.Close();
        });

    #endregion
}