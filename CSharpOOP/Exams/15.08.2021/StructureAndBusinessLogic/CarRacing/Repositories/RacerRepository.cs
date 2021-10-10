using System;
using System.Collections.Generic;
using System.Linq;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Repositories
{
    public class RacerRepository : IRepository<IRacer>
    {
        private List<IRacer> _models;

        public RacerRepository()
        {
            _models = new List<IRacer>();
        }

        public IReadOnlyCollection<IRacer> Models => new List<IRacer>(_models);

        public void Add(IRacer model)
        {
            if (model is null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddRacerRepository);
            }

            _models.Add(model);
        }

        public bool Remove(IRacer model)
        {
            return _models.Remove(model);
        }

        public IRacer FindBy(string property)
        {
            return _models.FirstOrDefault(racer => racer.Username == property);
        }
    }
}