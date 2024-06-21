using ExamShipBattle.ViewModel;
using System.Windows.Controls;

namespace ExamShipBattle.View.Custom.Field
{
    public partial class Field : UserControl
    {
        public Field()
        {
            InitializeComponent();
            this.DataContext = new FieldVM(FieldGrid, this, DropCanvas, true, false);
        }
        public Field(ExamShipBattle.Model.Field fieldModel, bool enableCells, bool hideShips)
        {
            InitializeComponent();
            this.DataContext = new FieldVM(FieldGrid, this, DropCanvas, false, fieldModel, enableCells, hideShips);
        }
    }
}
