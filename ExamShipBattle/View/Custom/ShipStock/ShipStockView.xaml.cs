using ExamShipBattle.ViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.View.Custom.ShipStock
{
    /// <summary>
    /// Interaction logic for ShipStockView.xaml
    /// </summary>
    public partial class ShipStockView : UserControl
    {
        public ShipStockView()
        {
            InitializeComponent();
            this.DataContext = new ShipStockVM(Stock1StackPanel, Stock2StackPanel, Stock3StackPanel, Stock4StackPanel);
        }
    }
}
