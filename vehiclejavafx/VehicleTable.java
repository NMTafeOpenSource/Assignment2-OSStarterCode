/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vehiclejavafx;

import javafx.beans.property.SimpleStringProperty;

/**
 *
 * @author J187384
 */
public class VehicleTable {
    private final SimpleStringProperty vehicleYear =                    new SimpleStringProperty("");
    private final SimpleStringProperty vehicleManufacturer =            new SimpleStringProperty("");
    private final SimpleStringProperty vehicleModel =                   new SimpleStringProperty("");
    private final SimpleStringProperty vehicleRegistrationNo =          new SimpleStringProperty("");
    private final SimpleStringProperty vehicleOdemeterReading =         new SimpleStringProperty("");
    private final SimpleStringProperty vehicleTankCapacity =            new SimpleStringProperty("");
    private final SimpleStringProperty vehicleJourney =                 new SimpleStringProperty("");
    private final SimpleStringProperty vehicleServiceCount =            new SimpleStringProperty("");
    private final SimpleStringProperty vehicleTotalScheduledServices =  new SimpleStringProperty("");
    private final SimpleStringProperty vehicleLastServiceOdemeterKM =   new SimpleStringProperty("");
    
    
    public VehicleTable() {
        this("","","","","","","","","","");
    }
    public VehicleTable(String vehicleManufacturer, String vehicleModel, String vehicleYear, String vehicleRegistrationNo, 
                        String vehicleOdemeterReading, String vehicleTankCapacity, String vehicleJourney, String vehicleServiceCount,
                        String vehicleTotalScheduledServices, String vehicleLastServiceOdemeterKM) {
        setYearProperty(vehicleYear);
        setManufacturerProperty(vehicleManufacturer);
        setModelProperty(vehicleModel);
        setRegistrationNoProperty(vehicleRegistrationNo);
        setOdemeterReadingProperty(vehicleOdemeterReading);
        setTankCapacityProperty(vehicleTankCapacity);
        setJourneyProperty(vehicleJourney);
        setServiceCountProperty(vehicleServiceCount);
        setTotalScheduledServicesProperty(vehicleTotalScheduledServices);
        setLastServiceOdemeterKMProperty(vehicleLastServiceOdemeterKM);
    }
    
    
    public SimpleStringProperty vehicleYearProperty() {
        return vehicleYear;
    }
    public void setYearProperty(String set) {
        vehicleYear.set(set);
    }
    
    public SimpleStringProperty vehicleManufacturerProperty() {
        return vehicleManufacturer;
    }
    public void setManufacturerProperty(String set) {
        vehicleManufacturer.set(set);
    }
    
    public SimpleStringProperty vehicleModelProperty() {
        return vehicleModel;
    }
    public void setModelProperty(String set) {
        vehicleModel.set(set);
    }
    
    public SimpleStringProperty vehicleRegistrationNoProperty() {
        return vehicleRegistrationNo;
    }
    public void setRegistrationNoProperty(String set) {
        vehicleRegistrationNo.set(set);
    }
    
    public SimpleStringProperty vehicleOdemeterReadingProperty() {
        return vehicleOdemeterReading;
    }
    public void setOdemeterReadingProperty(String set) {
        vehicleOdemeterReading.set(set);
    }
    
    public SimpleStringProperty vehicleTankCapacityProperty() {
        return vehicleTankCapacity;
    }
    public void setTankCapacityProperty(String set) {
        vehicleTankCapacity.set(set);
    }
    
    public SimpleStringProperty vehicleJourneyProperty() {
        return vehicleJourney;
    }
    public void setJourneyProperty(String set) {
        vehicleJourney.set(set);
    }
    
    public SimpleStringProperty vehicleServiceCountProperty() {
        return vehicleServiceCount;
    }
    public void setServiceCountProperty(String set) {
        vehicleServiceCount.set(set);
    }
    
    public SimpleStringProperty vehicleTotalScheduledServicesProperty() {
        return vehicleTotalScheduledServices;
    }
    public void setTotalScheduledServicesProperty(String set) {
        vehicleTotalScheduledServices.set(set);
    }
    
    public SimpleStringProperty vehicleLastServiceOdemeterKMProperty() {
        return vehicleLastServiceOdemeterKM;
    }
    public void setLastServiceOdemeterKMProperty(String set) {
        vehicleLastServiceOdemeterKM.set(set);
    }
}
