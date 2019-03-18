using Battleship.Domain;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using System.Windows;
using System.Windows.Input;

namespace Battleship.ViewModel
{
    public class BoardViewModel : ViewModelBase
    {
        public BoardViewModel(Board board)
        {
            Board = board;
            fieldCliecked = new Subject<Coordinates>();
            OnClickCommand = new RelayCommand<Point>(OnClick);
            IsActive = true;
        }

        private void OnClick(Point point)
        {
            if (IsActive)
            {
                Coordinates coord = new Coordinates((int)point.Y, (int)point.X);
                fieldCliecked.OnNext(coord);
            }
        }

        public void UpdateBoard(Board board)
        {
            Board = board;
            RaisePropertyChanged(() => Board);
        }
        public Board Board { get; set; }

        private Subject<Coordinates> fieldCliecked;

        public IObservable<Coordinates> FieldClicked
        {
            get { return this.fieldCliecked.AsObservable(); }
        }

        public ICommand OnClickCommand { get; set; }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                isActive = value;
                RaisePropertyChanged(() => IsActive);
            }
        }
    }
}
