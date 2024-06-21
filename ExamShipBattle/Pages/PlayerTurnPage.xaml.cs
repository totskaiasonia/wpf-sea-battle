using ExamShipBattle.Model;
using ExamShipBattle.ViewModel.PageViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.Pages
{
    public partial class PlayerTurnPage : Page
    {
        public PlayerTurnPage(Field firstPlayer, Field secondPlayer)
        {
            InitializeComponent();
            this.DataContext = new PlayerTurnPageVM(firstPlayer, secondPlayer, PlayerFieldGrid, PlayerMovesGrid, CurrentPlayerLabel, NewGameBtn, StatisticsGrid);

        }
    }
}
