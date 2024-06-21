using ExamShipBattle.DataStore;
using ExamShipBattle.Model;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExamShipBattle.ViewModel
{
    internal class StatisticsVM : INotifyPropertyChanged
    {

        private Statistics statisticsModel;

        public Statistics StatisticsModel
        {
            get { return statisticsModel; }
            set { statisticsModel = value; OnPropertyChanged("StatisticsModel"); }
        }

        private string playerNameStr;

        public string PlayerNameStr
        {
            get { return playerNameStr; }
            set { playerNameStr = value; OnPropertyChanged("PlayerNameStr"); }
        }

        private string totalStr;

        public string TotalStr
        {
            get { return totalStr; }
            set { totalStr = value; OnPropertyChanged("TotalStr"); }
        }

        private string winsStr;

        public string WinsStr
        {
            get { return winsStr; }
            set { winsStr = value; OnPropertyChanged("WinsStr"); }
        }

        private string lossesStr;

        public string LossesStr
        {
            get { return lossesStr; }
            set { lossesStr = value; OnPropertyChanged("LossesStr"); }
        }


        private StatisticsDataStore DataStore { get; set; }

        public StatisticsVM()
        {
            this.DataStore = new StatisticsDataStore();

            this.StatisticsModel = new Statistics(1, 0, 0, 0);

            this.TotalStr = "0";
            this.WinsStr = "0";
            this.LossesStr = "0";
            this.PlayerNameStr = "Player1";

        }

        public StatisticsVM(int id)
        {
            this.DataStore = new StatisticsDataStore();

            this.StatisticsModel = new Statistics(id, 0, 0, 0);

            this.TotalStr = "0";
            this.WinsStr = "0";
            this.LossesStr = "0";
            this.PlayerNameStr = "Player1";

            this.LoadInfo();
            this.ShowInfo();
        }

        private void LoadInfo()
        {
            string[] res = this.DataStore.GetField(this.StatisticsModel.ID);
            this.StatisticsModel.Wins = Int32.Parse(res[0]);
            this.StatisticsModel.Losses = Int32.Parse(res[1]);
            this.StatisticsModel.Total = Int32.Parse(res[0]) + Int32.Parse(res[1]);
        }

        public void ShowInfo()
        {
            this.PlayerNameStr = $"Player{this.StatisticsModel.ID}";
            this.TotalStr = $"Total: {this.StatisticsModel.Total}";
            this.WinsStr = $"Wins: {this.StatisticsModel.Wins}";
            this.LossesStr = $"Losses: {this.StatisticsModel.Losses}";
        }
        public void AddWin()
        {
            this.StatisticsModel.Wins += 1;
            this.WinsStr = $"Wins: {this.StatisticsModel.Wins}";

            this.StatisticsModel.Total++;
            this.TotalStr = $"Total: {this.StatisticsModel.Total}";

            this.DataStore.UpdateItem(this.StatisticsModel);
        }
        public void AddLoss()
        {
            this.StatisticsModel.Losses += 1;
            this.LossesStr = $"Losses: {this.StatisticsModel.Losses}";

            this.StatisticsModel.Total++;
            this.TotalStr = $"Total: {this.StatisticsModel.Total}";

            this.DataStore.UpdateItem(this.StatisticsModel);
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
