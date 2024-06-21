using ExamShipBattle.ViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.View.Custom.Cell
{
    public partial class CellView : UserControl
    {

        public CellView()
        {
            InitializeComponent();
            this.DataContext = new CellVM(CellGrid, CellBtn, CellImg);
        }
        public CellView(object cell)
        {
            InitializeComponent();
            this.DataContext = new CellVM(CellGrid, CellBtn, CellImg, (ExamShipBattle.Model.Cell)cell);
        }
    }
}
