using ExamShipBattle.ViewModel.PageViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.Pages
{
    public partial class SetUpShipsPage : Page
    {
        public SetUpShipsPage()
        {
            InitializeComponent();
            this.DataContext = new SetUpShipsVM(SetUpField, ShipsStock);
        }
    }
}
