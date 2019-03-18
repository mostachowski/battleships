using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Battleship.View
{
    /// <summary>
    /// Interaction logic for BoardView.xaml
    /// </summary>
    public partial class BoardView : UserControl
    {
        public BoardView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            ColorConverter converter = new ColorConverter();

            for (int i = 1; i < 11; i++)
            {
                for (int j = 1; j < 11; j++)
                {
                    Button btn = new Button();
                    Grid.SetColumn(btn, i);
                    Grid.SetRow(btn, j);

                    Binding boardBinding = new Binding("Board");
                    boardBinding.Converter = converter;
                    boardBinding.ConverterParameter = new Point(i, j);
                    boardBinding.Source = DataContext;

                    Binding clickBinding = new Binding("OnClickCommand");
                    clickBinding.Source = DataContext;
                    btn.SetBinding(Button.BackgroundProperty, boardBinding);
                    btn.SetBinding(Button.CommandProperty, clickBinding);

                    btn.CommandParameter = new Point(i, j);
                    BoardGrid.Children.Add(btn);
                }
            }
        }
    }
}
