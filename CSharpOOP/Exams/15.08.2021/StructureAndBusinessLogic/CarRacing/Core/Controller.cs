using System;
using System.Linq;
using System.Text;
using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Utilities.Messages;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private CarRepository _cars;
        private RacerRepository _racers;
        private IMap _map;

        public Controller()
        {
            _cars = new CarRepository();
            _racers = new RacerRepository();
            _map = new Map();
        }
        
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            ICar car = null;

            if (type == nameof(SuperCar))
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else if (type == nameof(TunedCar))
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }

            _cars.Add(car);

            return string.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            ICar car = _cars.FindBy(carVIN);
            IRacer racer = null;

            if (car is null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }

            if (type == nameof(ProfessionalRacer))
            {
                racer = new ProfessionalRacer(username, car);
            }
            else if (type == nameof(StreetRacer))
            {
                racer = new StreetRacer(username, car);
            }
            else
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }

            _racers.Add(racer);

            return string.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = _racers.FindBy(racerOneUsername);
            IRacer racerTwo = _racers.FindBy(racerTwoUsername);

            if (racerOne is null)
            {
                throw new AggregateException(string.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }

            if (racerTwo is null)
            {
                throw new AggregateException(string.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return _map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            StringBuilder result = new StringBuilder();

            foreach (IRacer racer in _racers.Models.OrderByDescending(racer => racer.DrivingExperience).ThenBy(racer => racer.Username))
            {
                result.AppendLine(racer.ToString());
            }

            return result.ToString().TrimEnd();
        }
    }
}