using ExamShipBattle.Model;
using ExamShipBattle.View.Custom.Cell;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ExamShipBattle.ViewModel
{

    public class ShipVM : INotifyPropertyChanged
    {
        private Ship shipModel;

        public Ship ShipModel
        {
            get { return shipModel; }
            set { shipModel = value; OnPropertyChanged("ShipModel"); }
        }

        public StackPanel ShipStackPanel { get; set; }

        public ShipVM()
        {
            this.ShipModel = new Ship();
            this.AddShipCells();
        }
        public ShipVM(StackPanel shipStackPanel) : this()
        {
            this.ShipStackPanel = shipStackPanel;
            this.AddShipCells();
        }
        public ShipVM(StackPanel shipStackPanel, Ship shipModel) : this(shipStackPanel)
        {
            this.ShipModel = shipModel;
            this.AddShipCells();
        }
        public void AddShipCells()
        {
            for (int i = 0; i < this.ShipModel.Size; i++)
            {
                Cell cl = new Cell();
                cl.State = Model.CellModel.EState.SHIP;
                this.shipModel.CellsModels.Add(cl);
                CellView cellView = new CellView(cl);
                cellView.CellBtn.IsEnabled = false;
                ResourceDictionary myResourceDictionary = new ResourceDictionary();
                myResourceDictionary.Source = new Uri("../View/Custom/Cell/CellStyles.xaml", UriKind.Relative);
                cellView.CellBtn.Style = (Style)myResourceDictionary["Ship_Style"];
                cellView.Width = 19;
                this.ShipStackPanel.Children.Add(cellView);
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
