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
        /// <summary>
        /// this is a constructor for this window
        /// </summary>
        public FuelPurchases()
        {
            InitializeComponent();
        }
        /// <summary>
        /// this is a constructor to get vehicleId
        /// </summary>
        /// <param name="vehicleid"></param>
        public FuelPurchases(Guid vehicleid)
        {
            InitializeComponent();
            VehicleId = vehicleid;
        }
        /// <summary>
        /// this is a click event for add fuel purchases
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
        
        /// <summary>
        /// this is a click event to close the fuel purchases window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FuelPurchasesCancelButton_Clicked(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
