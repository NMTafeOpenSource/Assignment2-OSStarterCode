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

namespace Assignment2_TDD_Fleet
{
    /// <summary>
    /// Interaction logic for FuelPurchases.xaml
    /// </summary>
    public partial class FuelPurchases : Window
    {
        public Guid VehicleId;

        public FuelPurchases()
        {
            InitializeComponent();
        }

        public FuelPurchases(Guid vehicleid)
        {
            InitializeComponent();
            VehicleId = vehicleid;
        }

        private void AddFuelPurchasesButton_Clicked(object sender, RoutedEventArgs e)
        {
            double FuelQuantity = double.Parse(TextBoxFuelQuantity.Text);
            double FuelPrice = double.Parse(TextBoxFuelPrice.Text);

            FuelPurchase fuelPurchase = new FuelPurchase(FuelQuantity, FuelPrice);
            fuelPurchase.VId = VehicleId;

            CarList.fuelPurchases.Add(fuelPurchase);
            FuelPurchase.SaveFuelPurchases(CarList.fuelPurchases);
            Close();
        }
        

        private void FuelPurchasesCancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
