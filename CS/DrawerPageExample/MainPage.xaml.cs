using DevExpress.Maui.Navigation;

namespace DrawerPageExample {
	public partial class MainPage : DrawerPage {
        public MainPage() {
			InitializeComponent();
            IsDrawerOpened = true;
		}
    }
}
