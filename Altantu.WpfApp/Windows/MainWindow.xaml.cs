using Altantu.Core.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace Altantu.WpfApp.Windows
{
    public partial class MainWindow : Window, INotifyPropertyChanged, IDisposable
    {
        #region Events

        public event EventHandler StartStopButtonClicked;
        public event EventHandler ClearButtonClicked;

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Constructors

        public MainWindow(App app)
        {
            InitializeComponent();
            this.App = app;
            this.DataContext = this;
        }

        #endregion

        #region Methods

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Event handlers

        private void This_Loaded(object sender, RoutedEventArgs e)
        {
            //this.OnLoad();
        }

        #endregion

        #region Properties

        private App App { get; set; }

        #endregion
    }
}
