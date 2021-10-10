using System;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Cars
{
    public abstract class Car: ICar
    {
        private const int VinSize = 17;
        
        private string _make;
        private string _model;
        private string _vin;
        private int _horsePower;
        private double _fuelAvailable;
        private double _fuelConsumptionPerRace;

        protected Car(string make, string model, string vin, int horsePower, double fuelAvailable, double fuelConsumptionPerRace)
        {
            Make = make;
            Model = model;
            VIN = vin;
            HorsePower = horsePower;
            FuelAvailable = fuelAvailable;
            FuelConsumptionPerRace = fuelConsumptionPerRace;
        }

        public string Make
        {
            get => _make;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarMake);
                }

                _make = value;
            }
        }

        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarModel);
                }

                _model = value;
            }
        }

        public string VIN
        {
            get => _vin;
            private set
            {
                if (value.Length != VinSize)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarVIN);
                }

                _vin = value;
            }
        }

        public int HorsePower
        {
            get => _horsePower;
            protected set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarHorsePower);
                }

                _horsePower = value;
            }
        }

        public double FuelAvailable
        {
            get => _fuelAvailable;
            private set
            {
                if (value < 0)
                {
                    _fuelAvailable = 0;
                }

                _fuelAvailable = value;
            }
        }

        public double FuelConsumptionPerRace
        {
            get => _fuelConsumptionPerRace;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCarFuelConsumption);
                }

                _fuelConsumptionPerRace = value;
            }
        }

        public virtual void Drive()
        {
            FuelAvailable -= FuelConsumptionPerRace;
        }
    }
}