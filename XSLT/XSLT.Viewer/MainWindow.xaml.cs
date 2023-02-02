using System.Windows;

namespace XSLT.Viewer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainWindowVM m_model;

        public MainWindow(MainWindowVM model)
        {
            InitializeComponent();
            DataContext = m_model = model;
        }

        private void BrowseButton_Click(object sender, RoutedEventArgs e)
        {
            var tag = (sender as System.Windows.Controls.Button).Tag.ToString();
            m_model.Browse(tag);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            m_model.Start();
        }
    }
}
