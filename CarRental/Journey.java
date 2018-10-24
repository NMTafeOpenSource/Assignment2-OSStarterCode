package CarRental;

 
public class Journey {
	
	public void setKilometers(double kilometers) {
        this.kilometers = kilometers;
    }
 private double kilometers;

	/**
	 * Class constructor
	 */
	public Journey() {
		this.kilometers = 0;
	}

	/** 
	 * Appends the distance parameter to {@link #kilometers}
	 * @param kilometers the distance traveled 
	 */
        
        //add trip travel distance to total Kilometers
	public static double addKilometers(double kilometers) {
            
            double totalKilo = 0;
		totalKilo += kilometers;
                return totalKilo;
               
	}


	/**
	 * Getter method for total Kilometers traveled in this journey.
	 * @return {@link #kilometers}
	 */
	public double getKilometers() {
		return kilometers;
	}
   

}
