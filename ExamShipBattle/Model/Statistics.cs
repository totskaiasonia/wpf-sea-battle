using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExamShipBattle.Model
{
    public class Statistics : INotifyPropertyChanged
    {
        public int ID { get; set; }
        public int Total { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }

        public Statistics()
        {
            this.ID = 1;
            this.Total = 0;
            this.Wins = 0;
            this.Losses = 0;
        }
        public Statistics(int iD, int total, int wins, int losses)
        {
            ID = iD;
            Total = total;
            Wins = wins;
            Losses = losses;
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
