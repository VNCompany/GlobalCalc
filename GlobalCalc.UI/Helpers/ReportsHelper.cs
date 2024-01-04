using System.Text;
using GlobalCalc.UI.ViewModels;

namespace GlobalCalc.UI.Helpers
{
    internal static class ReportsHelper
    {
        public static string GenerateReport(MainViewModel mainViewModel)
        {
            StringBuilder reportItemsSB = new StringBuilder();
            int counter = 1;
            foreach (FacadeViewModel facadeViewModel in mainViewModel.Facades)
            {
                reportItemsSB.AppendLine(GenerateReportItem(counter, facadeViewModel));
                counter++;
            }

            return string.Format(Resources.report_template
                , reportItemsSB
                , mainViewModel.TotalCount
                , mainViewModel.TotalPrice
            );
        }

        private static string GenerateReportItem(int number, FacadeViewModel facadeViewModel)
        {
            return string.Format(Resources.report_item_template
                , number
                , facadeViewModel.SelectedProfile?.Id
                , $"{facadeViewModel.SelectedColor?.Article} {facadeViewModel.SelectedProfileName}"
                , facadeViewModel.CalculatorResult?.TotalPrice
                , facadeViewModel.Count
                , facadeViewModel.CalculatorResult?.TotalPrice * facadeViewModel.Count
                , facadeViewModel.SizeString
                , facadeViewModel.CalculatorResult?.Perimeter
                , facadeViewModel.AddSeal ? "Да" : "Нет"
                , facadeViewModel.HolesCount > 0
                    ? $"{facadeViewModel.SelectedMilling?.Name} - {facadeViewModel.HolesCount} шт."
                    : "Без фрезеровки"
            );
        }
    }
}
