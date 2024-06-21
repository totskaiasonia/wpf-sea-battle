using ExamShipBattle.Model;
using ExamShipBattle.View.Custom.Ship;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamShipBattle.ViewModel
{
    internal class ShipStockVM : INotifyPropertyChanged
    {
        private ShipStock shipStockModel;

        public ShipStock ShipStockModel
        {
            get { return shipStockModel; }
            set { shipStockModel = value; OnPropertyChanged("ShipStockModel"); }
        }



        public StackPanel StoreSP1 { get; set; }
        public StackPanel StoreSP2 { get; set; }
        public StackPanel StoreSP3 { get; set; }
        public StackPanel StoreSP4 { get; set; }

        public ShipStockVM()
        {
            this.ShipStockModel = new ShipStock();
            this.AddShipsToStock();
        }
        public ShipStockVM(StackPanel sp1, StackPanel sp2, StackPanel sp3, StackPanel sp4)
        {
            this.ShipStockModel = new ShipStock();
            this.StoreSP1 = sp1;
            this.StoreSP2 = sp2;
            this.StoreSP3 = sp3;
            this.StoreSP4 = sp4;
            this.AddShipsToStock();
        }

        public void AddShipsToStock()
        {
            for (int i = 0; i < this.ShipStockModel.ShipsModels.Count; i++)
            {
                ShipView ship = new ShipView(ShipStockModel.ShipsModels[i]);
                ship.Name = $"StockShip{i}";
                ship.PreviewMouseLeftButtonDown += this.OnPreviewMouseLeftButtonDown;
                ship.PreviewMouseMove += this.OnPreviewMouseMove;

                ship.Margin = new Thickness(0, 0, 10, 0);
                ship.Width = 19 * this.ShipStockModel.ShipsModels[i].Size;
                ship.Height = 19;
                if (this.ShipStockModel.ShipsModels[i].Size == 1)
                    this.StoreSP4.Children.Add(ship);
                else if (this.ShipStockModel.ShipsModels[i].Size == 2)
                    this.StoreSP3.Children.Add(ship);
                else if (this.ShipStockModel.ShipsModels[i].Size == 3)
                    this.StoreSP2.Children.Add(ship);
                else
                    this.StoreSP1.Children.Add(ship);
            }
        }


        private bool isDragging = false;
        private Point startPoint;

        private void OnPreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.startPoint = e.GetPosition(null);
            this.isDragging = true;
        }

        public void OnPreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point currentPosition = e.GetPosition(null);
            Vector difference = startPoint - currentPosition;
            if (this.isDragging)
            {
                ShipView shipView = sender as ShipView;
                if (e.LeftButton == MouseButtonState.Pressed &&
                    (Math.Abs(difference.X) > SystemParameters.MinimumHorizontalDragDistance ||
                    Math.Abs(difference.Y) > SystemParameters.MinimumVerticalDragDistance))
                {
                    DataObject data = new DataObject();
                    data.SetData("ShipView", shipView);
                    DragDrop.DoDragDrop(shipView, data, DragDropEffects.Copy | DragDropEffects.Move);
                    this.isDragging = false;
                }


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
