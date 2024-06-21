using ExamShipBattle.ViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.View.Custom.Ship
{
    /// <summary>
    /// Interaction logic for ShipView.xaml
    /// </summary>
    public partial class ShipView : UserControl
    {
        public ShipView()
        {
            InitializeComponent();
            this.DataContext = new ShipVM(ShipStackPanel);
        }
        public ShipView(ExamShipBattle.Model.Ship shipModel)
        {
            InitializeComponent();
            this.DataContext = new ShipVM(ShipStackPanel, shipModel);
        }
    }
}
