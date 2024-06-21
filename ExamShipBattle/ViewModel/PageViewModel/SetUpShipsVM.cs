using ExamShipBattle.View.Custom.Ship;
using ExamShipBattle.View.Custom.ShipStock;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamShipBattle.ViewModel.PageViewModel
{
    internal class SetUpShipsVM : INotifyPropertyChanged
    {
        public ICommand NavigateToNextPageCommand { get; private set; }


        private ExamShipBattle.View.Custom.Field.Field setUpField;

        public ExamShipBattle.View.Custom.Field.Field SetUpField
        {
            get { return setUpField; }
            set { setUpField = value; OnPropertyChanged("SetUpField"); }
        }

        private ShipStockView shipsStock;

        public ShipStockView ShipsStock
        {
            get { return shipsStock; }
            set { shipsStock = value; OnPropertyChanged("ShipsStock"); }
        }



        public SetUpShipsVM(ExamShipBattle.View.Custom.Field.Field field, ShipStockView shipStock)
        {
            this.SetUpField = field;
            this.ShipsStock = shipStock;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string args = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(args));
            }
        }


        public RelayCommand NavigateToNextPage
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (this.ShipsStock.Stock1StackPanel.Children.Count == 0 &&
                        this.ShipsStock.Stock2StackPanel.Children.Count == 0 &&
                        this.ShipsStock.Stock3StackPanel.Children.Count == 0 &&
                        this.ShipsStock.Stock4StackPanel.Children.Count == 0)
                    {
                        ExamShipBattle.ViewModel.FieldVM viewModel = this.SetUpField.DataContext as ExamShipBattle.ViewModel.FieldVM;
                        ResourceDictionary myResourceDictionary = new ResourceDictionary();
                        myResourceDictionary.Source = new Uri("../../View/Custom/Cell/CellStyles.xaml", UriKind.Relative);
                        foreach (var child in viewModel.DropAreaCanvas.Children)
                        {
                            if (child is Grid)
                                continue;
                            ShipView shipView = child as ShipView;
                            int grid_i = (int)Canvas.GetTop(shipView) / 19;
                            int grid_j = (int)Canvas.GetLeft(shipView) / 19;


                            if (shipView.ShipStackPanel.Orientation == Orientation.Horizontal)
                            {
                                for (int j = grid_j; j < grid_j + shipView.Width / 19; j++)
                                {
                                    viewModel.Field.CellsModels[grid_i * 10 + j].State = Model.CellModel.EState.SHIP;
                                    viewModel.Field.Cells[grid_i * 10 + j].CellBtn.Style = (Style)myResourceDictionary["Ship_Style"];
                                }
                            }
                            else
                            {
                                for (int i = grid_i; i < grid_i + shipView.Height / 19; i++)
                                {
                                    viewModel.Field.CellsModels[i * 10 + grid_j].State = Model.CellModel.EState.SHIP;
                                    viewModel.Field.Cells[i * 10 + grid_j].CellBtn.Style = (Style)myResourceDictionary["Ship_Style"];
                                }
                            }
                        }
                        viewModel.FieldGrid.Children.Clear();
                        NavigationService.NavigateToEnemyPage(viewModel.Field);
                    }
                });
            }
            set
            {
            }
        }

    }
}
