using ExamShipBattle.Model;
using ExamShipBattle.View.Custom.Cell;
using ExamShipBattle.View.Custom.Ship;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace ExamShipBattle.ViewModel
{
    internal class FieldVM : INotifyPropertyChanged
    {
        private Field field;

        public Field Field
        {
            get { return field; }
            set { field = value; OnPropertyChanged("Field"); }
        }

        public Grid FieldGrid { get; set; }
        public ExamShipBattle.View.Custom.Field.Field FieldView { get; set; }
        public Canvas DropAreaCanvas { get; set; }
        private bool HideShips { get; set; }


        public ICommand PressOnCellCommand { get; private set; }

        private bool EnableCells;

        public FieldVM()
        {
            this.Field = new Field();
        }
        public FieldVM(Grid field, ExamShipBattle.View.Custom.Field.Field fieldView, Canvas dropArea, bool allowDrop, bool enableCells) : this()
        {
            this.FieldGrid = field;
            this.FieldView = fieldView;
            this.DropAreaCanvas = dropArea;
            this.DropAreaCanvas.AllowDrop = allowDrop;
            this.DropAreaCanvas.Drop += this.DropEvent;
            this.DropAreaCanvas.DragOver += this.DragEvent;
            this.EnableCells = enableCells;
            this.AddCells();
        }
        public FieldVM(Grid field, ExamShipBattle.View.Custom.Field.Field fieldView, Canvas dropArea, bool allowDrop, Field fieldModel, bool enableCells, bool hideShips)
        {
            this.FieldGrid = field;
            this.FieldView = fieldView;
            this.DropAreaCanvas = dropArea;
            this.DropAreaCanvas.AllowDrop = allowDrop;
            this.Field = fieldModel;
            this.EnableCells = enableCells;
            this.HideShips = hideShips;
            this.AddCells();
        }


        private bool CheckEdge(int grid_i, int grid_j, int width, int height, Orientation orientation)
        {
            if (orientation == Orientation.Horizontal)
            {
                if (10 * 19 - grid_j * 19 < width * 19)
                {
                    return false;
                }

            }
            else
            {
                if (10 * 19 - grid_i * 19 < height * 19)
                    return false;
            }
            return true;
        }

        private bool CheckAnotherShip(int grid_i, int grid_j, int width, int height, Orientation orientation)
        {
            UIElementCollection children = this.DropAreaCanvas.Children;

            int endCellIndexI = grid_i + 1;
            int endCellIndexJ = width + grid_j;

            if (orientation == Orientation.Vertical)
            {
                endCellIndexI = height + grid_i;
                endCellIndexJ = grid_j + 1;
            }
            for (int i = grid_i - 1; i <= endCellIndexI; i++)
            {
                for (int j = grid_j - 1; j <= endCellIndexJ; j++)
                {
                    if (i == grid_i && j == grid_j && orientation == Orientation.Horizontal)
                        j += width - 1;
                    if (j == grid_j && i != grid_i - 1 && i != endCellIndexI && orientation == Orientation.Vertical)
                        continue;
                    else
                    {
                        Point point = new Point(j * 19 + 8, i * 19 + 8);

                        for (int k = 0; k < children.Count; k++)
                        {
                            if (children[k] is Grid)
                                continue;

                            Rect bounds = VisualTreeHelper.GetDescendantBounds(children[k]);
                            bounds = children[k].TransformToAncestor(this.DropAreaCanvas).TransformBounds(bounds);
                            if (bounds.Contains(point))
                                return false;
                        }
                    }
                }
            }
            return true;
        }


        public void DropEvent(object sender, DragEventArgs e)
        {

            Point dropPos = e.GetPosition(this.FieldView);
            int x = (int)dropPos.X;
            int y = (int)dropPos.Y;

            int grid_i = y / 19;
            int grid_j = x / 19;



            ShipView shipView = e.Data.GetData("ShipView") as ShipView;

            if (this.CheckEdge(grid_i, grid_j, (int)shipView.Width / 19, (int)shipView.Height / 19, shipView.ShipStackPanel.Orientation) &&
                this.CheckAnotherShip(grid_i, grid_j, (int)shipView.Width / 19, (int)shipView.Height / 19, shipView.ShipStackPanel.Orientation))
            {
                this.DropAreaCanvas.Children.Remove(shipView);

                Canvas.SetTop(shipView, grid_i * 19);
                Canvas.SetLeft(shipView, grid_j * 19);
                this.DropAreaCanvas.Children.Add(shipView);
            }

        }

        private void OnMouseLeftButtonUp(ShipView shipView)
        {
            int x = (int)Canvas.GetLeft(shipView);
            int y = (int)Canvas.GetTop(shipView);
            this.DropAreaCanvas.Children.Remove(shipView);

            if (this.CheckEdge(y / 19, x / 19, (int)shipView.Height / 19, (int)shipView.Width / 19, shipView.ShipStackPanel.Orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal) &&
                this.CheckAnotherShip(y / 19, x / 19, (int)shipView.Height / 19, (int)shipView.Width / 19, shipView.ShipStackPanel.Orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal))
            {
                double tmpWidth = shipView.Width;
                shipView.Width = shipView.Height;
                shipView.Height = tmpWidth;

                shipView.ShipStackPanel.Width = shipView.Width;
                shipView.ShipStackPanel.Height = shipView.Height;

                for (int i = 0; i < shipView.ShipStackPanel.Children.Capacity; i++)
                {
                    CellView cl = shipView.ShipStackPanel.Children[i] as CellView;
                    cl.Height = cl.Width = 19;
                }

                shipView.ShipStackPanel.Orientation = shipView.ShipStackPanel.Orientation == Orientation.Horizontal ? Orientation.Vertical : Orientation.Horizontal;

            }
            this.DropAreaCanvas.Children.Add(shipView);
        }

        public void DragEvent(object sender, DragEventArgs e)
        {
            Point dropPos = e.GetPosition(this.FieldView);
            int x = (int)dropPos.X;
            int y = (int)dropPos.Y;


            ShipView shipView = e.Data.GetData("ShipView") as ShipView;
            double current_x = Canvas.GetLeft(shipView);
            double current_y = Canvas.GetTop(shipView);
            this.DropAreaCanvas.Children.Remove(shipView);


            if (this.CheckEdge(y / 19, x / 19, (int)shipView.Width / 19, (int)shipView.Height / 19, shipView.ShipStackPanel.Orientation) &&
                this.CheckAnotherShip(y / 19, x / 19, (int)shipView.Width / 19, (int)shipView.Height / 19, shipView.ShipStackPanel.Orientation))
            {
                Canvas.SetLeft(shipView, x / 19 * 19);
                Canvas.SetTop(shipView, y / 19 * 19);
                if (shipView.Parent is StackPanel)
                {
                    StackPanel sp = shipView.Parent as StackPanel;
                    sp.Children.Remove(shipView);
                    shipView.MouseLeftButtonUp += (senderr, ee) => this.OnMouseLeftButtonUp(shipView);

                }
                this.DropAreaCanvas.Children.Add(shipView);

            }
            else
            {
                if (!Double.IsNaN(current_x) && !Double.IsNaN(current_y))
                {
                    if (shipView.Parent is StackPanel)
                    {
                        StackPanel sp = shipView.Parent as StackPanel;
                        sp.Children.Remove(shipView);
                        shipView.MouseLeftButtonUp += (senderr, ee) => this.OnMouseLeftButtonUp(shipView);
                        Console.WriteLine(current_y);
                    }

                    Canvas.SetLeft(shipView, current_x / 19 * 19);
                    Canvas.SetTop(shipView, current_y / 19 * 19);
                    this.DropAreaCanvas.Children.Add(shipView);
                }
            }


        }


        private void AddCells()
        {
            this.FieldGrid.Children.Clear();
            if (this.HideShips)
                this.HideCells();
            else
                this.ShowCells();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Grid.SetRow(this.Field.Cells[i * 10 + j], i);
                    Grid.SetColumn(this.Field.Cells[i * 10 + j], j);
                    this.Field.Cells[i * 10 + j].CellBtn.IsEnabled = this.EnableCells;
                    this.FieldGrid.Children.Add(this.Field.Cells[i * 10 + j]);
                }
            }

        }
        private void ShowCells()
        {
            ResourceDictionary myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source = new Uri("../View/Custom/Cell/CellStyles.xaml", UriKind.Relative);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.Field.CellsModels[i * 10 + j].State == Model.CellModel.EState.SHIP)
                        this.Field.Cells[i * 10 + j].CellBtn.Style = (Style)myResourceDictionary["Ship_Style"];
                }
            }
        }
        private void HideCells()
        {
            ResourceDictionary myResourceDictionary = new ResourceDictionary();
            myResourceDictionary.Source = new Uri("../View/Custom/Cell/CellStyles.xaml", UriKind.Relative);
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (this.Field.CellsModels[i * 10 + j].State != Model.CellModel.EState.KILLED)
                        this.Field.Cells[i * 10 + j].CellBtn.Style = (Style)myResourceDictionary["Free_Style"];
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
