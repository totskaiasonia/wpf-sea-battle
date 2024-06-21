using ExamShipBattle.Model;
using ExamShipBattle.Pages;
using System.Windows.Controls;

namespace ExamShipBattle
{
    public static class NavigationService
    {
        private static Frame MainContentFrame;
        public static void Initialize(Frame frame)
        {
            MainContentFrame = frame;
        }

        public static void NavigateToPage()
        {
            SetUpShipsPage setUpShipsPage = new SetUpShipsPage();

            MainContentFrame.Navigate(setUpShipsPage);
        }

        public static void NavigateToEnemyPage(Field field)
        {
            SetUpEnemyShipsPage setUpEnemyShipsPage = new SetUpEnemyShipsPage(field);

            MainContentFrame.Navigate(setUpEnemyShipsPage);
        }
        public static void NavigateToGamePage(Field firstPlayer, Field secondPlayer)
        {
            PlayerTurnPage playerTurnPage = new PlayerTurnPage(firstPlayer, secondPlayer);

            MainContentFrame.Navigate(playerTurnPage);
        }
    }
}
