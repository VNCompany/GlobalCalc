using System.IO;
using System.Diagnostics;
using System.Collections.ObjectModel;

using GlobalCalc.Shared;
using GlobalCalc.Operations;
using GlobalCalc.UI.Infrastructure;

namespace GlobalCalc.UI.ViewModels;

class MainViewModel : ViewModelBase
{
    private readonly FacadeData _data;

    #region Properties

    public ObservableCollection<FacadeViewModel> Facades { get; } = new();

    private decimal _totalPrice;
    public decimal TotalPrice
    {
        get => _totalPrice;
        set
        {
            _totalPrice = value;
            OnPropertyChanged();
        }
    }

    private int _totalCount;
    public int TotalCount
    {
        get => _totalCount;
        set
        {
            _totalCount = value;
            OnPropertyChanged();
        }
    }

    #endregion
    
    #region Commands

    private RelayCommand? _addFacade;
    public RelayCommand AddFacade =>
        _addFacade ??= new RelayCommand(_ =>
        {
            var facadeVM = new FacadeViewModel
            {
                Profiles = _data.Profiles,
                AllMillings = _data.Millings,
                Screws = _data.Screws,
            };
            if (ViewsProvider.ShowWindowDialog(facadeVM) != true) 
                return;

            CalculateFacade(facadeVM);
            Facades.Add(facadeVM);
            RecalculateCounters();
        });


    private RelayCommand? _recalculate;
    public RelayCommand Recalculate =>
        _recalculate ??= new RelayCommand(_ => RecalculateCounters());


    private RelayCommand? _deleteFacade;
    public RelayCommand DeleteFacade =>
        _deleteFacade ??= new RelayCommand(param =>
        {
            Facades.Remove((FacadeViewModel)param!);
            RecalculateCounters();
        });


    private RelayCommand? _editFacade;
    public RelayCommand EditFacade =>
        _editFacade ??= new RelayCommand(param =>
        {
            var facadeVM = (FacadeViewModel)param!;
            int selectedIndex = Facades.IndexOf(facadeVM);
            var clone = facadeVM.Clone();
            if (ViewsProvider.ShowWindowDialog(clone) != true)
                return;

            CalculateFacade(clone);
            Facades.RemoveAt(selectedIndex);
            Facades.Insert(selectedIndex, clone);
            RecalculateCounters();
        });

    private RelayCommand? _openFillingTable;
    public RelayCommand OpenFillingTable =>
        _openFillingTable ??= new RelayCommand(_ =>
        {
            var tablePath = ".\\data\\table.jpg";
            if (File.Exists(tablePath))
                Process.Start(new ProcessStartInfo { FileName = tablePath, UseShellExecute = true });
        });

    #endregion
    
    public MainViewModel()
    {
        _data = Services.App.Data;
    }

    #region Methods

    private void CalculateFacade(FacadeViewModel facadeVM)
    {
        facadeVM.CalculatorResult = FacadeCalculator.CalculateFacadePrice(
            workPrice: _data.WorkPrice
            , mmSize: new Size(facadeVM.Width, facadeVM.Height)
            , profilePrice: facadeVM.SelectedColor!.Price
            , sealPrice: facadeVM.SelectedProfile!.SealPrice
            , cornerPrice: facadeVM.SelectedProfile!.CornerPrice
            , screwPrice: facadeVM.SelectedScrew!.Price
            , millingPrice: facadeVM.SelectedMilling!.Price
            , holesCount: facadeVM.HolesCount
        );
    }

    private void RecalculateCounters()
    {
        _totalPrice = 0;
        _totalCount = 0;
        foreach (var facade in Facades)
        {
            _totalPrice += facade.CalculatorResult!.TotalPrice * facade.Count;
            _totalCount += facade.Count;
        }
        
        OnPropertyChanged(nameof(TotalPrice));
        OnPropertyChanged(nameof(TotalCount));
    }

    #endregion
    
}
