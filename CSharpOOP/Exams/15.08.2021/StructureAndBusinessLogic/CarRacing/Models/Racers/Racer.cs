using System;
using System.Text;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string _username;
        private string _racingBehavior;
        private int _drivingExperience;
        private ICar _car;

        protected Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }

        public string Username
        {
            get => _username;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new AggregateException(ExceptionMessages.InvalidRacerName);
                }

                _username = value;
            }
        }

        public string RacingBehavior
        {
            get => _racingBehavior;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new AggregateException(ExceptionMessages.InvalidRacerBehavior);
                }

                _racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get => _drivingExperience;
            protected set
            {
                if (value < 0 || value > 100)
                {
                    throw new AggregateException(ExceptionMessages.InvalidRacerDrivingExperience);
                }

                _drivingExperience = value;
            }
        }

        public ICar Car
        {
            get => _car;
            private set
            {
                if (value is null)
                {
                    throw new AggregateException(ExceptionMessages.InvalidRacerCar);
                }

                _car = value;
            }
        }
        public virtual void Race()
        {
            Car.Drive();
        }

        public bool IsAvailable()
        {
            return _car.FuelAvailable >= _car.FuelConsumptionPerRace;
        }

        public override string ToString()
        {
            StringBuilder result = new StringBuilder();

            result.AppendLine($"{GetType().Name}: {Username}");
            result.AppendLine($"--Driving behavior: {RacingBehavior}");
            result.AppendLine($"--Driving experience: {DrivingExperience}");
            result.AppendLine($"--Car: {Car.Make} {Car.Model} ({Car.VIN})");

            return result.ToString().TrimEnd();
        }
    }
}