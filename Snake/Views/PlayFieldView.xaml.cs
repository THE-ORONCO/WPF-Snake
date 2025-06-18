using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Snake.Views
{
    /// <summary>
    /// Interaction logic for PlayFieldView.xaml
    /// </summary>
    public partial class PlayFieldView : UserControl
    {

        public PlayFieldView()
        {
            InitializeComponent();
            this.Loaded += (_a, _b) =>
            {
                Window w = Window.GetWindow(this);
                foreach (InputBinding binding in InputBindings)
                {
                    w.InputBindings.Add(binding);
                }
            };
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
