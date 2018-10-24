package vehiclestarter;

 
public class Vehicle {
	private String	manufacturer;
	private String	model;
	private int	makeYear;
        private int  registrationNo;
        private double  odemeterReading;
        private double  tankCapacity;
               
	private FuelPurchase	fuelPurchase;

	/**
	 * Class constructor specifying name of make (manufacturer), model and year
	 * of make.
	 * @param manufacturer
	 * @param model
	 * @param makeYear
	 * @param registrationNo
	 * @param odemeterReading
	 * @param tankCapacity
	 */
	public Vehicle(String manufacturer, String model, int makeYear, int registrationNo, double odemeterReading, double tankCapacity) {
		this.manufacturer = manufacturer;
		this.model = model;
		this.makeYear = makeYear;
		this.registrationNo = registrationNo;
		this.odemeterReading = odemeterReading;
		this.tankCapacity = tankCapacity;
		fuelPurchase = new FuelPurchase();
	}
        
        // getters and setters
        
        public String getManufacturer(){
            return this.manufacturer;
        }
	public void setManufacturer(String manufacturer) {
		this.manufacturer = manufacturer;
	}
        
        public String getModel(){
            return this.model;
        }
	public void setModel(String model) {
		this.model = model;
	}
        
        public int getMakeYear(){
            return this.makeYear;
        }
	public void setMakeYear(int makeYear) {
		this.makeYear = makeYear;
	}
        
        public int getRegistrationNo(){
            return this.registrationNo;
        }
	public void setRegistrationNo(int registrationNo) {
		this.registrationNo = registrationNo;
	}
        
        public double getOdemeterReading(){
            return this.odemeterReading;
        }
	public void setOdemeterReading(double odemeterReading) {
		this.odemeterReading = odemeterReading;
	}
        
        public double getTankCapacity(){
            return this.tankCapacity;
        }
	public void setTankCapacity(double tankCapacity) {
		this.tankCapacity = tankCapacity;
	}
        
	/**
	 * Prints details for {@link Vehicle}
	 */
	public void printDetails() {
		System.out.println("Vehicle: " + makeYear + " " + manufacturer + " " + model + ", registration: " + registrationNo + ", odemeter: " + odemeterReading + ", tank capacity: " + tankCapacity);
	}

        
        // adds kilometers to the odometer reading. 
        public void addKilometers(double distanceTravelled) {
            this.odemeterReading += distanceTravelled;
        }
        
        // adds fuel to the car
        public void addFuel(double litres, double price){            
            fuelPurchase.purchaseFuel(litres, price);
        }        

}
