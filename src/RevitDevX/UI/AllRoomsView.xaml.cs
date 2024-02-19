using System.Windows;
using System.Windows.Controls;

namespace RevitDevX.UI
{
    /// <summary>
    /// Interaction logic for AllRoomsView.xaml
    /// </summary>
    public partial class AllRoomsView : UserControl
    {
        public AllRoomsView()
        {
            InitializeComponent();
        }

        private void TreeView_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (DataContext is AllRoomsViewModel vm)
            {
                vm.SelectedRoom = e.NewValue as RoomPresenter;
            }
        }
    }
}
