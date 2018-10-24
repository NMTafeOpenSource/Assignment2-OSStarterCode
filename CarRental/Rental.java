/*
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */
package CarRental;

/**
 *
 * @author 20002116
 */
public class Rental {
    //Calculate Fuel Cost, and assume fuel price is $1.5/L
    //Fuel used is equal to Tank Capacity minus Fuel Left in the Tank
    public static double FuelCost(int tankCapacity, double fuel) {
        double fuelCost;
        fuelCost = (tankCapacity-fuel)*1.5;
        return fuelCost;
    }

  //Calculate total Revenue
    //assume travel cost is $1/km
    //total Revenue is equal to travel cost plus Fuel Cost
    public static double Revenue(double distance, double fuelcost) {
        double revenue;
        revenue = (distance *1+ fuelcost);
        return revenue;
    }
    
}
