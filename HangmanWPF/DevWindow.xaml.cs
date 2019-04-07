using HangmanWPF.ViewModels;
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
using System.Windows.Shapes;

namespace HangmanWPF
{
    /// <summary>
    /// Interaction logic for DevWindow.xaml
    /// </summary>
    public partial class DevWindow : Window
    {

        HangmanGameVM vm;

        public DevWindow()
        {
            InitializeComponent();

            vm = new HangmanGameVM();

            this.DataContext = vm;


            
        }
    }
}
