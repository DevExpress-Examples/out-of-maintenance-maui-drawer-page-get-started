using DevExpress.Maui.Navigation;

namespace DrawerPageExample {
	public partial class MainPage : DrawerPage {
        public MainPage() {
			InitializeComponent();
            IsDrawerOpened = true;
		}

        protected override void OnAppearing() {
            base.OnAppearing();
            carBrandList.SelectedItem = ((MainViewModel)BindingContext).CarModelsByBrand[0];
        }
    }
}
