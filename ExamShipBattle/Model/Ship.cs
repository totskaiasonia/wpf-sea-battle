using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExamShipBattle.Model
{
    public class Ship : INotifyPropertyChanged
    {
        private ObservableCollection<Cell> cellsModels;

        public ObservableCollection<Cell> CellsModels
        {
            get { return cellsModels; }
            set { cellsModels = value; OnPropertyChanged("CellsModels"); }
        }


        private bool isVertical;

        public bool IsVertical
        {
            get { return isVertical; }
            set { isVertical = value; }
        }
        public int Size { get; set; }

        public int PosX { get; set; }
        public int PosY { get; set; }

        public Ship()
        {
            this.CellsModels = new ObservableCollection<Cell>();
            this.IsVertical = false;
            this.Size = 0;
            this.PosX = this.PosY = 0;
        }
        public Ship(bool isVertical, int size, int posX, int posY) : this()
        {
            this.isVertical = isVertical;
            this.Size = size;
            this.PosX = posX;
            this.PosY = posY;
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
