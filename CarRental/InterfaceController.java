package CarRental;

/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */

import java.net.URL;
import java.util.ResourceBundle;
import javafx.collections.FXCollections;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Button;
import javafx.scene.control.ChoiceBox;
import javafx.scene.control.ComboBox;
import javafx.scene.control.Label;
import javafx.scene.control.Tab;
import javafx.scene.control.TextArea;
import javafx.scene.control.TextField;
import javafx.scene.layout.AnchorPane;
/**
 * FXML Controller class
 *
 * @author 20002116
 */
public class InterfaceController implements Initializable {


     

    @FXML
    private Button btPrintRceipt;
    @FXML
    private TextField tfTotalKiloTravelled;
    @FXML
    private TextField tfModel;
    @FXML
    private TextField tfManufacturer;
    @FXML
    private TextField tfMadeOfYear;
    @FXML
    private TextField tfLastServiceOdo;
    @FXML
    private TextField tfServiceCount;
   
    @FXML
    private TextField tfRegistrationNo;
    @FXML
    private TextField tfServiceRequired;


    @FXML
    private Button btServiceReqired;

    @FXML
    private TextField tfFuelEconomy;
    @FXML
    private AnchorPane btServiceRequired;
    private TextField tfReceipt;
    @FXML
    private TextField tfTankCapacity;
    @FXML
    private TextField tfFuelLeft;
    @FXML
    private TextArea taReceipt;

       
   
    @Override
    public void initialize(URL url, ResourceBundle rb) {


          
 

    }    
    

    @FXML
    //Calculate whether need a service
    //if Total Scheduled Service is more than Service Count, then message "Service Required" display
    //if Total Scheduled Service is less than or equal Service Count, then message "No Service Required" display
    private void ServiceRequired(ActionEvent event) {
      Service service = new Service();
service.setLastServiceOdometerKm(Integer.parseInt(tfLastServiceOdo.getText()));
service.setServiceCount(Integer.parseInt(tfServiceCount.getText()));
    
       if(service.getTotalScheduledServices()>service.getServiceCount()){
  tfServiceRequired.setText("Service Required");
} else if (service.getTotalScheduledServices()<=service.getServiceCount()){
    tfServiceRequired.setText("No Service Required");
}else {
    tfServiceRequired.setText("Please Enter Correct Information");
} 
  }
    
    
     @FXML
    private void PrintReceipt(ActionEvent event) {
          Vehicle v = new Vehicle();
        v.setManufacturer(tfManufacturer.getText());
        v.setModel( tfModel.getText());
 v.setMakeYear(Integer.parseInt(tfMadeOfYear.getText()));
               v.setRegistrationNo(tfRegistrationNo.getText());
        v.setTankCapacity(Integer.parseInt(tfTankCapacity.getText()));
 
Journey trip = new Journey();
trip.setKilometers(Double.parseDouble(tfTotalKiloTravelled.getText()));
//add trip travelled distance to total kilometers
double totalKilo = trip.addKilometers(trip.getKilometers());


FuelPurchase fuel = new FuelPurchase();
fuel.setFuelEconomy(Double.parseDouble(tfFuelEconomy.getText()));
/*fuel.setFuel(Double.parseDouble(tfFuelLeft.getText()));*/





Service service = new Service();
service.setLastServiceOdometerKm(Integer.parseInt(tfLastServiceOdo.getText()));
service.setServiceCount(Integer.parseInt(tfServiceCount.getText()));


Rental cost = new Rental();
//Calculate Fuel Cost
double fuelCost = cost.FuelCost(Integer.parseInt(tfTankCapacity.getText()),Double.parseDouble(tfFuelLeft.getText()));
//Add Fuel Cost to Travel Cost
double revenue = cost.Revenue(Double.parseDouble(tfTotalKiloTravelled.getText()),fuelCost);

//Print Receipt
taReceipt.setText("Vehicle: " +  " " + v.getManufacturer() + " "+ v.getModel()+ " " + v.getMakeYear()
                        + " " + "\n"+
                        "Registration Number:"+" " + v.getRegistrationNo() +"\n" +" "+
                        "Tank Capacity:"+" " + v.getTankCapacity()+"L"+"\n" +" "+ 
                        "Trip Distance:" + " "+trip.getKilometers()+"KM" + "\n" +" "+
                        "Total Kilometers Travelled:" +" "+totalKilo+"KM"+ "\n" +" "+ 
                         "Revenue:" +" "+ "$"+revenue+ "\n" +" "+
                         "Fuel Economy:" +" "+ fuel.getFuelEconomy()+"L/100KM" +" " +"\n"+
                        "Last Service Odometer:" +" "+ service.getLastServiceOdometerKm() +"KM"+"\n"+
                        "Service Count:" +" "+ service.getServiceCount()+" "+"\n"+
                        "Require a Service:" + " " + tfServiceRequired.getText()+ "\n");

    };


}
