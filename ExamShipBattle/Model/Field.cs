using ExamShipBattle.View.Custom.Cell;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExamShipBattle.Model
{
    public class Field : INotifyPropertyChanged
    {
        private ObservableCollection<CellView> cells;

        public ObservableCollection<CellView> Cells
        {
            get { return cells; }
            set { cells = value; OnPropertyChanged("Cells"); }
        }


        private ObservableCollection<Cell> cellsModels;

        public ObservableCollection<Cell> CellsModels
        {
            get { return cellsModels; }
            set { cellsModels = value; OnPropertyChanged("CellsModels"); }
        }



        public Field()
        {
            this.Cells = new ObservableCollection<CellView>();

            this.CellsModels = new ObservableCollection<Cell>();
            for (int i = 0; i < 100; i++)
            {
                this.CellsModels.Add(new Cell());
            }
            for (int i = 0; i < 100; i++)
            {
                this.Cells.Add(new CellView(this.CellsModels[i]));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string args = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(args));
            }
        }
    }
}
