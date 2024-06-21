using ExamShipBattle.View.Custom.Statistics;
using GalaSoft.MvvmLight.Command;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;

namespace ExamShipBattle.ViewModel.PageViewModel
{
    internal class PlayerTurnPageVM : INotifyPropertyChanged
    {
        public bool IsFirstPlayerTurn { get; set; }


        private ExamShipBattle.Model.Field firstPlayerField;

        public ExamShipBattle.Model.Field FirstPlayerField
        {
            get { return firstPlayerField; }
            set { firstPlayerField = value; OnPropertyChanged("FirstPlayerField"); }
        }


        private ExamShipBattle.Model.Field secondPlayerField;

        public ExamShipBattle.Model.Field SecondPlayerField
        {
            get { return secondPlayerField; }
            set { secondPlayerField = value; OnPropertyChanged("SecondPlayerField"); }
        }


        public Grid PlayerFieldGrid { get; set; }
        public Grid PlayerMovesGrid { get; set; }
        public Grid StatisticsGrid { get; set; }

        private Label CurrentPlayerLabel { get; set; }

        private Button NewGameBtn { get; set; }

        public PlayerTurnPageVM(ExamShipBattle.Model.Field firstPlayerField, ExamShipBattle.Model.Field secondPlayerField, Grid playerFieldGrid, Grid playerMovesGrid, Label currentPlayerLabel, Button newGameBtn, Grid statsGrid)
        {
            this.IsFirstPlayerTurn = true;

            this.FirstPlayerField = firstPlayerField;
            this.SecondPlayerField = secondPlayerField;

            this.StatisticsGrid = statsGrid;

            this.PlayerFieldGrid = playerFieldGrid;
            this.PlayerMovesGrid = playerMovesGrid;

            this.CurrentPlayerLabel = currentPlayerLabel;

            this.NewGameBtn = newGameBtn;
            this.NewGameBtn.Visibility = Visibility.Hidden;

            this.ShowPlayerField();
            this.ShowPlayerMoves();
            this.ShowCurrentPlayerLabel();
            this.ShowStatistics(1);
            this.ShowStatistics(2);
        }

        private void ShowStatistics(int id)
        {
            StatisticsView statsView = new StatisticsView(id);
            Grid.SetColumn(statsView, id - 1);
            this.StatisticsGrid.Children.Add(statsView);
        }

        private void ShowPlayerField()
        {
            if (this.IsFirstPlayerTurn)
            {
                View.Custom.Field.Field fieldView = new View.Custom.Field.Field(this.FirstPlayerField, false, false);
                Grid.SetRow(fieldView, 1);
                this.PlayerFieldGrid.Children.Add(fieldView);
            }
            else
            {
                View.Custom.Field.Field fieldView = new View.Custom.Field.Field(this.SecondPlayerField, false, false);
                Grid.SetRow(fieldView, 1);
                this.PlayerFieldGrid.Children.Add(fieldView);
            }

        }

        private void ShowPlayerMoves()
        {
            if (this.IsFirstPlayerTurn)
            {
                View.Custom.Field.Field fieldView = new View.Custom.Field.Field(this.SecondPlayerField, true, true);
                Grid.SetRow(fieldView, 1);
                this.AddClickHandlers(fieldView.DataContext as FieldVM);
                this.PlayerMovesGrid.Children.Add(fieldView);
            }
            else
            {
                View.Custom.Field.Field fieldView = new View.Custom.Field.Field(this.FirstPlayerField, true, true);
                Grid.SetRow(fieldView, 1);
                this.AddClickHandlers(fieldView.DataContext as FieldVM);
                this.PlayerMovesGrid.Children.Add(fieldView);
            }
        }

        private void ShowCurrentPlayerLabel()
        {
            if (this.IsFirstPlayerTurn)
            {
                this.CurrentPlayerLabel.Content = "Player1`s turn";
            }
            else
            {
                this.CurrentPlayerLabel.Content = "Player2`s turn";
            }
        }

        private void AddClickHandlers(FieldVM fieldVM)
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    int isend = i;
                    int jsend = j;
                    fieldVM.Field.Cells[i * 10 + j].CellBtn.Click += (sender, e) => PressOnCell(isend, jsend, e);
                }
            }
        }

        private void PressOnCell(int i, int j, RoutedEventArgs e)
        {
            if (this.IsFirstPlayerTurn)
            {
                if (this.SecondPlayerField.CellsModels[i * 10 + j].State == Model.CellModel.EState.SHIP)
                {
                    this.SecondPlayerField.CellsModels[i * 10 + j].State = Model.CellModel.EState.KILLED;
                    if (this.CheckWinner(this.SecondPlayerField))
                    {
                        this.NewGameBtn.Visibility = Visibility.Visible;
                        this.UpdateStatistics(1, 2);
                        this.PlayerMovesGrid.IsEnabled = false;
                    }
                }
                else if (this.SecondPlayerField.CellsModels[i * 10 + j].State == Model.CellModel.EState.FREE)
                {
                    this.SecondPlayerField.CellsModels[i * 10 + j].State = Model.CellModel.EState.UNAVAILABLE;
                    this.ChangePlayer();
                }
                e.Handled = true;
            }
            else
            {
                if (this.FirstPlayerField.CellsModels[i * 10 + j].State == Model.CellModel.EState.SHIP)
                {
                    this.FirstPlayerField.CellsModels[i * 10 + j].State = Model.CellModel.EState.KILLED;
                    if (this.CheckWinner(this.FirstPlayerField))
                    {
                        this.NewGameBtn.Visibility = Visibility.Visible;
                        this.UpdateStatistics(2, 1);
                        this.PlayerMovesGrid.IsEnabled = false;
                    }
                }
                else if (this.FirstPlayerField.CellsModels[i * 10 + j].State == Model.CellModel.EState.FREE)
                {
                    this.FirstPlayerField.CellsModels[i * 10 + j].State = Model.CellModel.EState.UNAVAILABLE;
                    this.ChangePlayer();
                }
                e.Handled = true;
            }
        }

        private void UpdateStatistics(int winnerId, int loserId)
        {
            StatisticsView statsView = this.StatisticsGrid.Children[winnerId] as StatisticsView;
            StatisticsVM statsVM = statsView.DataContext as StatisticsVM;
            statsVM.AddWin();

            statsView = this.StatisticsGrid.Children[loserId] as StatisticsView;
            statsVM = statsView.DataContext as StatisticsVM;
            statsVM.AddLoss();


        }

        private bool CheckWinner(ExamShipBattle.Model.Field fieldToCheck)
        {
            for (int i = 0; i < fieldToCheck.CellsModels.Count; i++)
            {
                if (fieldToCheck.CellsModels[i].State == Model.CellModel.EState.SHIP)
                    return false;
            }
            return true;
        }


        private void ChangePlayer()
        {
            ExamShipBattle.View.Custom.Field.Field fieldView = this.PlayerFieldGrid.Children[1] as ExamShipBattle.View.Custom.Field.Field;
            ExamShipBattle.ViewModel.FieldVM fieldVM = fieldView.DataContext as ExamShipBattle.ViewModel.FieldVM;
            fieldVM.FieldGrid.Children.Clear();
            this.PlayerFieldGrid.Children.Remove(fieldView);

            fieldView = this.PlayerMovesGrid.Children[1] as ExamShipBattle.View.Custom.Field.Field;
            fieldVM = fieldView.DataContext as ExamShipBattle.ViewModel.FieldVM;
            fieldVM.FieldGrid.Children.Clear();
            this.PlayerMovesGrid.Children.Remove(fieldView);


            this.IsFirstPlayerTurn = !this.IsFirstPlayerTurn;
            this.ShowPlayerField();
            this.ShowPlayerMoves();
            this.ShowCurrentPlayerLabel();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string args = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(args));
            }
        }

        public RelayCommand StartANewGame
        {
            get
            {
                return new RelayCommand(() =>
                {
                    NavigationService.NavigateToPage();
                });
            }
            set
            {
            }
        }
    }
}
