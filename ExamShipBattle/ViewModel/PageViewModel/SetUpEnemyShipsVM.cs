using ExamShipBattle.Model;
using ExamShipBattle.View.Custom.Ship;
using ExamShipBattle.View.Custom.ShipStock;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ExamShipBattle.ViewModel.PageViewModel
{
    internal class SetUpEnemyShipsVM : INotifyPropertyChanged
    {
        private Field AnotherPlayerField { get; set; }


        private ExamShipBattle.View.Custom.Field.Field setUpEnemyField;

        public ExamShipBattle.View.Custom.Field.Field SetUpEnemyField
        {
            get { return setUpEnemyField; }
            set { setUpEnemyField = value; OnPropertyChanged("SetUpEnemyField"); }
        }

        private ShipStockView shipsStock;

        public ShipStockView ShipsStock
        {
            get { return shipsStock; }
            set { shipsStock = value; OnPropertyChanged("ShipsStock"); }
        }


        public SetUpEnemyShipsVM(Field anotherPlayerField, ExamShipBattle.View.Custom.Field.Field enemyField, ShipStockView shipStock)
        {
            this.AnotherPlayerField = anotherPlayerField;
            this.SetUpEnemyField = enemyField;
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
                        ExamShipBattle.ViewModel.FieldVM viewModel = this.SetUpEnemyField.DataContext as ExamShipBattle.ViewModel.FieldVM;

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
                        NavigationService.NavigateToGamePage(this.AnotherPlayerField, viewModel.Field);
                    }
                });
            }
            set
            {
            }
        }
    }
}
