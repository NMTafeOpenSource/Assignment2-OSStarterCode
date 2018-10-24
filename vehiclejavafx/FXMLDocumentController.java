/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vehiclejavafx;

import java.net.URL;
import java.util.Random;
import java.util.ResourceBundle;
import javafx.collections.ObservableList;
import javafx.event.ActionEvent;
import javafx.fxml.FXML;
import javafx.fxml.Initializable;
import javafx.scene.control.Label;
import javafx.scene.control.TableColumn;
import javafx.scene.control.TableView;
import javafx.scene.control.TextField;
import vehiclestarter.Journey;
import vehiclestarter.Service;
import vehiclestarter.Vehicle;

/**
 *
 * @author J187384
 */
public class FXMLDocumentController implements Initializable {
    
    @FXML private TableView<VehicleTable> table;
    @FXML private TextField year;
    @FXML private TextField manufacturer;
    @FXML private TextField model;
    @FXML private TextField registrationNo;
    @FXML private TextField odemeterReading;
    @FXML private TextField tankCapacity;
    @FXML private TextField journey;
    @FXML private TextField serviceCount;
    @FXML private TextField totalScheduledServices;
    @FXML private TextField lastServiceOdemeterKM;
    @FXML
    private void handleButtonAction(ActionEvent event) {
            VehicleTable vehicleTable = new VehicleTable(manufacturer.getText(),model.getText(),year.getText(),
            registrationNo.getText(),odemeterReading.getText(),tankCapacity.getText(),
            journey.getText(),serviceCount.getText(),totalScheduledServices.getText(),lastServiceOdemeterKM.getText());
            
            ObservableList<VehicleTable> vehicleData = table.getItems();
            
            vehicleData.add(vehicleTable);
        
    }
    
    @Override
    public void initialize(URL url, ResourceBundle rb) {
        
        Vehicle v = new Vehicle("Ford", "T812", 2014, 4321, 0, 60);
		
		// Vehicle sample distance
            v.addFuel(new Random().nextDouble()*10, 1.3);

            v.printDetails();
            Journey j = new Journey();
            System.out.println("Journey: " + j.getKilometers());
            Service s = new Service();
            System.out.println("Service count: " + s.getServiceCount());
            System.out.println("Total scheduled services: " + s.getTotalScheduledServices());
            System.out.println("Last service odometer km: " + s.getLastServiceOdometerKm());
            System.out.println("\n\n");
            
            VehicleTable vehicleTable = new VehicleTable(v.getManufacturer(),v.getModel(),Integer.toString(v.getMakeYear()),
                            Integer.toString(v.getRegistrationNo()),Double.toString(v.getOdemeterReading()),Double.toString(v.getTankCapacity()),
                            Double.toString(j.getKilometers()),Integer.toString(s.getServiceCount()),Integer.toString(s.getTotalScheduledServices()),Integer.toString(s.getLastServiceOdometerKm()));
            
            ObservableList<VehicleTable> vehicleData = table.getItems();
            
            vehicleData.add(vehicleTable);
            
    }    
    
}
