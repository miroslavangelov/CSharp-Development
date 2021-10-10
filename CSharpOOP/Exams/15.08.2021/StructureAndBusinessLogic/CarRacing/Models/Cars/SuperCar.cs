namespace CarRacing.Models.Cars
{
    public class SuperCar: Car
    {
        private const double SuperCarFuelAvailable = 80;
        private const double SuperCarFuelConsumptionPerRace = 10;

        public SuperCar(string make, string model, string vin, int horsePower) 
            : base(make, model, vin, horsePower, SuperCarFuelAvailable, SuperCarFuelConsumptionPerRace)
        {
        }
    }
}