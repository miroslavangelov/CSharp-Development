using System;
using System.Collections.Generic;
using System.Linq;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private List<ICar> _models;

        public CarRepository()
        {
            _models = new List<ICar>();
        }

        public IReadOnlyCollection<ICar> Models => new List<ICar>(_models);

        public void Add(ICar model)
        {
            if (model is null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }

            _models.Add(model);
        }

        public bool Remove(ICar model)
        {
            return _models.Remove(model);
        }

        public ICar FindBy(string property)
        {
            return _models.FirstOrDefault(car => car.VIN == property);
        }
    }
}