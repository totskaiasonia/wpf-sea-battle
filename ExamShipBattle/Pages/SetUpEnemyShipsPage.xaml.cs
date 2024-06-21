using ExamShipBattle.Model;
using ExamShipBattle.ViewModel.PageViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.Pages
{
    public partial class SetUpEnemyShipsPage : Page
    {
        public SetUpEnemyShipsPage(Field anotherPlayerField)
        {
            InitializeComponent();
            this.DataContext = new SetUpEnemyShipsVM(anotherPlayerField, SetUpEnemyField, ShipsStock);
        }

    }
}
