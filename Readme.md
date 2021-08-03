<!-- default file list -->
*Files to look at*:

* [Startup.cs](./CS/DrawerPageExample/Startup.cs)
* [MainPage.xaml](./CS/DrawerPageExample/MainPage.xaml)
* [MainPage.xaml.cs](./CS/DrawerPageExample/MainPage.xaml.cs)
* [CarModel.cs](./CS/DrawerPageExample/CarModel.cs)
* [CarBrandViewModel.cs](./CS/DrawerPageExample/CarBrandViewModel.cs)
* [MainViewModel.cs](./CS/DrawerPageExample/MainViewModel.cs)
<!-- default file list end -->

# DevExpress Drawer Page for .NET MAUI

This example allows you to get started with the [DrawerPage](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.DrawerPage) component - use it to add a [navigation drawer](https://material.io/design/components/navigation-drawer.html) to your .NET MAUI application.

1. Install a [.NET MAUI development](https://docs.microsoft.com/en-gb/dotnet/maui/get-started/installation) environment and open the solution in Visual Studio 22 Preview.
2. Register the following NuGet feed in Visual Studio: https://nuget.devexpress.com/free/api.  
	If you are an active DevExpress [Universal](https://www.devexpress.com/subscriptions/universal.xml) customer or have registered our [free Xamarin UI controls](https://www.devexpress.com/xamarin/), this MAUI preview will be available in your personal NuGet feed automatically.
4. Restore NuGet packages.  
5. Run the application on an Android device or emulator.  

<img src="./img/devexpress-maui-drawer-page.png"/>

The following step-by-step instructions describe how to create the same application.

## Create a New MAUI Application and Add a Drawer Page

Create a new .NET MAUI solution in Visual Studio 22 Preview.  
Refer to the following Microsoft documentation for more information on how to get started with .NET MAUI: [.NET Multi-platform App UI](https://docs.microsoft.com/en-gb/dotnet/maui/).

Register https://nuget.devexpress.com/free/api as a package source in Visual Studio, if you are not an active DevExpress [Universal](https://www.devexpress.com/subscriptions/universal.xml) customer or have not yet registered our [free Xamarin UI controls](https://www.devexpress.com/xamarin/).

Install the **DevExpress.Maui.Navigation** package from your NuGet feed.

In the *Startup.cs* file, register a handler for the DevExpress DrawerPage:

```cs
using Microsoft.Maui;
using Microsoft.Maui.Hosting;
using Microsoft.Maui.Controls.Hosting;
using DevExpress.Maui.Navigation;

namespace DrawerPageExample {
	public class Startup : IStartup {
		public void Configure(IAppHostBuilder appBuilder) {
			appBuilder
				.ConfigureMauiHandlers((_, handlers) => 
                                        handlers.AddHandler<DrawerPage, DrawerPageHandler>())
				.UseMauiApp<App>()
				.ConfigureFonts(fonts => {
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				});
		}
	}
}
```

In the *MainPage.xaml* file, use the *dxn* prefix to declare the **DevExpress.Maui.Navigation** namespace and create a [DrawerPage](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.DrawerPage) instance:

```xaml
<dxn:DrawerPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
             xmlns:dxn="clr-namespace:DevExpress.Maui.Navigation;assembly=DevExpress.Maui.Navigation"
             x:Class="DrawerPageExample.MainPage">
</dxn:DrawerPage>
```

In the *MainPage.xaml.cs* file, change the MainPage’s base class from ContentPage to DrawerPage:

```cs
using DevExpress.Maui.Navigation;

namespace DrawerPageExample {
    public partial class MainPage : DrawerPage {
        public MainPage() {
            InitializeComponent();
        }
    }
}
```


## Create Models and View Models
Add a **CarModel** class that specifies a data object in the application:

```cs
namespace DrawerPageExample {
    public class CarModel {
        public string BrandName { get; }
        public string ModelName { get; }
        public string FullName => $"{BrandName} {ModelName}";

        public CarModel(string brand, string model) {
            this.BrandName = brand;
            this.ModelName = model;
        }
    }
}
```

Create a **CarBrandViewModel** class that defines content for the drawer page: car make and corresponding models. The application will display brands in the drawer and matching models in the main content area:

```cs
using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DrawerPageExample {
    public class CarBrandViewModel : INotifyPropertyChanged  {
        public string BrandName { get; }
        public IReadOnlyList<CarModel> CarModels { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public CarBrandViewModel(string brandName, IEnumerable<CarModel> carModels) {
            if (String.IsNullOrEmpty(brandName)) {
                this.BrandName = String.Empty;
            }
            else {
                this.BrandName = brandName;
            }
            if (carModels == null) {
                this.CarModels = new List<CarModel>();
            }
            else {
                this.CarModels = carModels.ToList();
            }
        }
        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
```

Create a **MainViewModel** class that defines content for the MainPage (models grouped by make/brand):

```cs
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DrawerPageExample {
    public class MainViewModel : INotifyPropertyChanged {
        private static readonly IReadOnlyList<CarModel> allCarModels = new List<CarModel> {
            new CarModel("Mercedes-Benz", "SL500 Roadster"),
            new CarModel("Mercedes-Benz", "CLK55 AMG Cabriolet"),
            new CarModel("Mercedes-Benz", "C230 Kompressor Sport Coupe"),
            new CarModel("BMW", "530i"),
            new CarModel("Rolls-Royce", "Corniche"),
            new CarModel("Jaguar", "S-Type 3.0"),
            new CarModel("Cadillac", "Seville"),
            new CarModel("Cadillac", "DeVille"),
            new CarModel("Lexus", "LS430"),
            new CarModel("Lexus", "GS430"),
            new CarModel("Ford", "Ranger FX-4"),
            new CarModel("Dodge", "RAM 1500"),
            new CarModel("GMC", "Siera Quadrasteer"),
            new CarModel("Nissan", "Crew Cab SE"),
            new CarModel("Toyota", "Tacoma S-Runner"),
        };

        public IReadOnlyList<CarBrandViewModel> CarModelsByBrand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel() {
            List<CarBrandViewModel> carBrandViewModels = new List<CarBrandViewModel>();
            carBrandViewModels.Add(new CarBrandViewModel("All", allCarModels));

            IEnumerable<IGrouping<string, CarModel>> groupedCarModels = 
                                                        allCarModels.GroupBy(v => v.BrandName);
            foreach (IGrouping<string, CarModel> carModelGroup in groupedCarModels) {
                carBrandViewModels.Add(new CarBrandViewModel(carModelGroup.Key, carModelGroup));
            }
            CarModelsByBrand = carBrandViewModels;
        }

        private void RaisePropertyChanged([CallerMemberName] string caller = "") {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) {
                handler.Invoke(this, new PropertyChangedEventArgs(caller));
            }
        }
    }
}
```

## Specify the Drawer Page Content
In the *MainPage.xaml* file:
1. Set the **DrawerPage.BindingContext** property to a **MainViewModel** object.
2. Set the [DrawerPage.DrawerContent](http://docs.devexpress.com/MAUI/DevExpress.Maui.Navigation.DrawerPage.DrawerContent) property to a **ListView** object. Bind the list’s **ItemsSource** property to the **CarModelsByBrand** property of the view model, and set up list items to display brand names.
3. Set the [DrawerPage.MainContent](http://docs.devexpress.devx/MAUI/DevExpress.Maui.Navigation.DrawerPage.MainContent) property to a **ListView** object. Specify the list’s **ItemsSource** binding. The bound list should contain car models corresponding to the selected brand.

```xaml
<dxn:DrawerPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                xmlns:dxn="clr-namespace:DevExpress.Maui.Navigation;assembly=DevExpress.Maui.Navigation"
                xmlns:local="clr-namespace:DrawerPageExample"
                x:Class="DrawerPageExample.MainPage">
    <dxn:DrawerPage.BindingContext>
        <local:MainViewModel/>
    </dxn:DrawerPage.BindingContext>
    <dxn:DrawerPage.DrawerContent>
        <ListView x:Name="carBrandList" 
                    ItemsSource="{Binding CarModelsByBrand}">
            <ListView.ItemTemplate>
                <DataTemplate>
                    <TextCell Text="{Binding BrandName}" />
                </DataTemplate>
            </ListView.ItemTemplate>
        </ListView>
    </dxn:DrawerPage.DrawerContent>
    <dxn:DrawerPage.MainContent>
        <ContentPage>
            <ListView BindingContext="{x:Reference carBrandList}"
                      ItemsSource="{Binding SelectedItem.CarModels}">
                <ListView.ItemTemplate>
                    <DataTemplate>
                        <TextCell Text="{Binding FullName}"/>
                    </DataTemplate>
                </ListView.ItemTemplate>
            </ListView>
        </ContentPage>
    </dxn:DrawerPage.MainContent>
</dxn:DrawerPage>
```

## Customize the Drawer Appearance
Use the following properties to customize the drawer size and shadow:

```xaml
<dxn:DrawerPage xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
                xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
                xmlns:dxn="clr-namespace:DevExpress.Maui.Navigation;assembly=DevExpress.Maui.Navigation"
                xmlns:local="clr-namespace:DrawerPageExample"
                x:Class="DrawerPageExample.MainPage"
                DrawerWidth="180"
                DrawerShadowHeight="10"
                DrawerShadowRadius="40"
                DrawerShadowColor="#808080"
                IsScrimEnabled="False">
    <!-- Other properties of the drawer page are here. -->
</dxn:DrawerPage>
```
