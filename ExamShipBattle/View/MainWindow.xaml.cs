using System.Windows;

namespace ExamShipBattle
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            NavigationService.Initialize(MainContentFrame);
            NavigationService.NavigateToPage();
        }
    }
}
