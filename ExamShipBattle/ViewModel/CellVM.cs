using ExamShipBattle.Model;
using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ExamShipBattle.ViewModel
{
    internal class CellVM : INotifyPropertyChanged
    {
        private Cell cellModel;

        public Cell CellModel
        {
            get { return cellModel; }
            set { cellModel = value; OnPropertyChanged("CellModel"); }
        }

        public Grid CellGrid { get; set; }
        public Button CellBtn { get; set; }
        public Image CellImg { get; set; }



        public ICommand ChangeStyleCommand { get; private set; }

        public CellVM()
        {
            this.CellModel = new Cell();
        }
        public CellVM(Grid cell, Button cellBtn, Image cellImg)
        {
            this.CellModel = new Cell();
            this.CellGrid = cell;
            this.CellBtn = cellBtn;
            this.CellImg = cellImg;
        }
        public CellVM(Grid cellGrid, Button cellBtn, Image cellImg, Cell cell)
        {
            this.CellGrid = cellGrid;
            this.CellBtn = cellBtn;
            this.CellImg = cellImg;
            this.CellModel = cell;
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string args = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(args));
            }
        }



        public RelayCommand ChangeStyle
        {
            get
            {
                return new RelayCommand(() =>
                {
                    ResourceDictionary myResourceDictionary = new ResourceDictionary();
                    myResourceDictionary.Source = new Uri("../View/Custom/Cell/CellStyles.xaml", UriKind.Relative);
                    if (this.CellModel.State == Model.CellModel.EState.UNAVAILABLE)
                    {
                        this.CellBtn.Style = (Style)myResourceDictionary["Free_Style"];
                        this.CellImg.Style = (Style)myResourceDictionary["Unavailable_Style"];
                    }
                    else if (this.CellModel.State == Model.CellModel.EState.KILLED)
                    {
                        this.CellBtn.Style = (Style)myResourceDictionary["Ship_Style"];
                        this.CellImg.Style = (Style)myResourceDictionary["Killed_Style"];
                    }
                    else
                    {
                        this.CellBtn.Style = (Style)myResourceDictionary["Ship_Style"];
                    }
                });
            }
            set
            {
            }
        }
    }
}
