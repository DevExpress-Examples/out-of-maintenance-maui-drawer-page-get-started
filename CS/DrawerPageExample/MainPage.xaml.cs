using System;
using System.Linq;
using Microsoft.Maui.Controls;

namespace DrawerPageExample {
	public partial class MainPage : FlyoutPage
    {
        public MainPage() {
			InitializeComponent();

            Flyout.IsVisible = true;
            IsPresented = true;
            carBrandList.SelectionChanged += OnSelectionChanged;
        }

        void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            IsPresented = false;
        }

        protected override void OnAppearing() {
            base.OnAppearing();
            carBrandList.SelectedItem = ((MainViewModel)BindingContext).CarModelsByBrand[0];
        }
    }
}
