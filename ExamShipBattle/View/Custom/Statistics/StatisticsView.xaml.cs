using ExamShipBattle.ViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.View.Custom.Statistics
{
    public partial class StatisticsView : UserControl
    {
        public StatisticsView()
        {
            InitializeComponent();
            this.DataContext = new StatisticsVM();
        }
        public StatisticsView(int id)
        {
            InitializeComponent();
            this.DataContext = new StatisticsVM(id);
        }
    }
}
