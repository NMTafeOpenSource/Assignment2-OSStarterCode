/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package vehiclestarter;

/**
 *
 * @author J187384
 */
public class RentalCost {
    
    private boolean isPerKilometer = false;
    private double amount = 0;
    private double costPerAmount = 0;
    
    public RentalCost(boolean isPerKilometer, double amount, double costPerAmount){
        this.isPerKilometer = isPerKilometer;
        this.amount         = amount;
        this.costPerAmount           = costPerAmount;
    }
    public boolean getPerKilometer() {
        return this.isPerKilometer;
    }
    public void setPerKilometer(boolean isPerKilometer) {
        this.isPerKilometer = isPerKilometer;
    }
    public double getAmount() {
        return this.amount;
    }
    public void setAmount(double amount) {
        this.amount = amount;
    }
    public double getCostPerAmount() {
        return this.costPerAmount;
    }
    public void setCostPerAmount(double costPerAmount) {
        this.costPerAmount = costPerAmount;
    }
    
    public double calculateCost() {
        return getCostPerAmount()*getAmount();
    }    
    public void showCost() {
        System.out.println("Cost per " + (isPerKilometer ? "Kilometer" : "Day") + ": " + getAmount() + "\nTotal cost:" + String.valueOf(calculateCost()));
    }
}
