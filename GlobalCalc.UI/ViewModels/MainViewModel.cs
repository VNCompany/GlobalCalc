using System;
using System.IO;
using System.Linq;
using System.Diagnostics;
using System.Collections.ObjectModel;
using Microsoft.Win32;

using GlobalCalc.Shared;
using GlobalCalc.Operations;
using GlobalCalc.UI.Helpers;
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
            FacadeViewModel facadeVM = new()
            {
                Profiles = _data.Profiles,
                AllMillings = _data.Millings,
                SelectedScrew = _data.Screws[0],
                ApplyButtonText = "Добавить"
            };
            if (ViewsProvider.ShowWindowDialog("Facade", facadeVM) != true) 
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
            FacadeViewModel facadeVM = (FacadeViewModel)param!;
            int selectedIndex = Facades.IndexOf(facadeVM);
            FacadeViewModel clone = facadeVM.Clone();
            clone.ApplyButtonText = "Изменить";
            if (ViewsProvider.ShowWindowDialog("Facade", clone) != true)
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
            const string tablePath = ".\\data\\table.jpg";
            if (File.Exists(tablePath))
                Process.Start(new ProcessStartInfo { FileName = tablePath, UseShellExecute = true });
        });

    private RelayCommand? _save;
    public RelayCommand Save =>
        _save ??= new RelayCommand(Save_Execute, Save_CanExecute);

    private bool Save_CanExecute(object? _) => Facades.Any();

    private void Save_Execute(object? _)
    {
        string report = ReportsHelper.GenerateReport(this);
        SaveFileDialog sfd = new SaveFileDialog();
        sfd.OverwritePrompt = true;
        sfd.Filter = "HTML Document|*.html";
        sfd.DefaultExt = "html";
        sfd.FileName = string.Concat("GlobalCalcReport_", DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss"));
        if (sfd.ShowDialog() == true)
        {
            Stream fileStream = sfd.OpenFile();
            using (StreamWriter sw = new StreamWriter(fileStream))
            {
                sw.Write(report);
                sw.Flush();
            }
            fileStream.Close();
        }
    }

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
            , sealPrice: facadeVM.AddSeal ? facadeVM.SelectedProfile!.SealPrice : 0
            , cornerPrice: facadeVM.SelectedProfile!.CornerPrice
            , screwPrice: facadeVM.SelectedScrew!.Price
            , millingPrice: facadeVM.SelectedMilling!.Price
            , holesCount: facadeVM.HolesCount
        );
    }

    private void RecalculateCounters()
    {
        TotalPrice = Facades.Sum(f => f.CalculatorResult!.TotalPrice * f.Count);
        TotalCount = Facades.Sum(f => f.Count);
    }

    #endregion
    
}
