using ExamShipBattle.Model.CellModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExamShipBattle.Model
{
    public class Cell : INotifyPropertyChanged
    {
        private EState state;

        public EState State
        {
            get { return state; }
            set { state = value; OnPropertyChanged("State"); }
        }

        private bool isInVerticalShip;

        public bool IsInVerticalShip
        {
            get { return isInVerticalShip; }
            set { isInVerticalShip = value; OnPropertyChanged("IsInVerticalShip"); }
        }

        // COORDS ?

        public Cell()
        {
            this.State = EState.FREE;
            this.isInVerticalShip = false;
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
