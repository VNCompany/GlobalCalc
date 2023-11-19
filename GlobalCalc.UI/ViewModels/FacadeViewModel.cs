using System;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

using GlobalCalc.Models;
using GlobalCalc.Operations;
using GlobalCalc.UI.Infrastructure;

namespace GlobalCalc.UI.ViewModels;

class FacadeViewModel : ViewModelBase, ICloneable
{
    private bool _hasChanges;

    #region Properties

    public IEnumerable<Milling> AllMillings { get; init; } = Enumerable.Empty<Milling>();
    public CalculatorResult? CalculatorResult { get; set; }
    public string? SizeString
    => CalculatorResult != null
        ? $"{Width} x {Height} мм ({CalculatorResult.ProfileSize.Width} x {CalculatorResult.ProfileSize.Height} м)"
        : null;

    // ApplyBtnText
    private string? _applyBtnText = "Добавить";
    public string? ApplyBtnText
    {
        get => _applyBtnText;
        set
        {
            _applyBtnText = value;
            OnPropertyChanged();
        }
    }

    // Profile
    private IEnumerable<Profile> _profiles = Enumerable.Empty<Profile>();
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

            if (_selectedProfile != null)
            {
                SelectedProfileName = _selectedProfile.Name;
                ProfileColors = _selectedProfile.Colors;
                Millings = AllMillings.Where(m => m.ProfileType == _selectedProfile.Type);
            }
        }
    }

    private string? _selectedProfileName = "Выбрать";
    public string? SelectedProfileName
    {
        get => _selectedProfileName;
        set
        {
            _selectedProfileName = value;
            OnPropertyChanged();
        }
    }

    // Profile color
    private IEnumerable<ProfileColor> _profileColors = Enumerable.Empty<ProfileColor>();
    public IEnumerable<ProfileColor> ProfileColors
    {
        get => _profileColors;
        set
        {
            _profileColors = value;
            OnPropertyChanged();
        }
    }

    private ProfileColor? _selectedColor;
    public ProfileColor? SelectedColor
    {
        get => _selectedColor;
        set
        {
            _selectedColor = value;
            OnPropertyChanged();
        }
    }
    
    // Screw
    private IEnumerable<Screw> _screws = Enumerable.Empty<Screw>();
    public IEnumerable<Screw> Screws
    {
        get => _screws;
        set
        {
            _screws = value;
            OnPropertyChanged();

            SelectedScrew = _screws.FirstOrDefault();
        }
    }

    private Screw? _selectedScrew;
    public Screw? SelectedScrew
    {
        get => _selectedScrew;
        set
        {
            _selectedScrew = value;
            OnPropertyChanged();
        }
    }

    // Size
    private int _width = 1;
    public int Width
    {
        get => _width;
        set
        {
            _width = value;
            OnPropertyChanged();
        }
    }

    private int _height = 1;
    public int Height
    {
        get => _height;
        set
        {
            _height = value;
            OnPropertyChanged();
        }
    }

    // Milling
    private IEnumerable<Milling> _millings = Enumerable.Empty<Milling>();
    public IEnumerable<Milling> Millings
    {
        get => _millings;
        set
        {
            _millings = value;
            OnPropertyChanged();

            SelectedMilling = _millings.FirstOrDefault();
        }
    }

    private Milling? _selectedMilling;
    public Milling? SelectedMilling
    {
        get => _selectedMilling;
        set
        {
            _selectedMilling = value;
            OnPropertyChanged();
        }
    }

    private int _holesCount;
    public int HolesCount
    {
        get => _holesCount;
        set
        {
            _holesCount = value;
            OnPropertyChanged();
        }
    }

    // Seal
    private bool _addSeal;
    public bool AddSeal
    {
        get => _addSeal;
        set
        {
            _addSeal = value;
            OnPropertyChanged();
        }
    }

    // Count
    private int _count = 1;
    public int Count
    {
        get => _count;
        set
        {
            _count = value;
            OnPropertyChanged();
        }
    }
    
    #endregion
    
    #region Commands

    private RelayCommand? _apply;
    public RelayCommand Apply 
        => _apply ??= new RelayCommand(Apply_Execute, Apply_CanExecute);

    private RelayCommand? _openProfileSelector;
    public RelayCommand OpenProfileSelector =>
        _openProfileSelector ??= new RelayCommand(_ =>
        {
            var profilesVM = new ProfilesViewModel(Profiles);
            if (ViewsProvider.ShowWindowDialog(profilesVM) != true)
                return;

            SelectedProfile = profilesVM.SelectedProfile;
        });
    
    #endregion

    #region Methods

    private bool Apply_CanExecute(object? _)
    {
        return _hasChanges 
            && SelectedProfile != null 
            && SelectedColor != null 
            && SelectedScrew != null 
            && Width > 0 && Height > 0;
    }

    private void Apply_Execute(object? _)
    {
        WindowContext!.DialogResult = true;
        WindowContext!.Close();
    }

    public object Clone()
    {
        return SimpleMapper.CloneObject(this
            , MapperField.Property(nameof(CalculatorResult))
            , nameof(_apply)
            , nameof(_hasChanges)
            , nameof(PropertyChanged)
        );
    }

    protected override void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        if (!_hasChanges)
            _hasChanges = true;

        base.OnPropertyChanged(propertyName);
    }

    #endregion
}