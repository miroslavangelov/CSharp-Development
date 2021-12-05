using System.Linq;

namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogsById;
        private Dictionary<string, Dictionary<string, Dog>> dogsByOwnerAndName;

        public DogVet()
        {
            this.dogsById = new Dictionary<string, Dog>();
            this.dogsByOwnerAndName = new Dictionary<string, Dictionary<string, Dog>>();
        }

        public int Size
        {
            get => this.dogsById.Count;
        }

        public void AddDog(Dog dog, Owner owner)
        {
            if (this.Contains(dog))
            {
                throw new ArgumentException();
            }

            if (this.dogsByOwnerAndName.ContainsKey(owner.Id) &&
                this.dogsByOwnerAndName[owner.Id].ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }

            if (!this.dogsByOwnerAndName.ContainsKey(owner.Id))
            {
                this.dogsByOwnerAndName[owner.Id] = new Dictionary<string, Dog>();
            }

            dog.Owner = owner;
            this.dogsByOwnerAndName[owner.Id][dog.Name] = dog;
            this.dogsById[dog.Id] = dog;
        }

        public bool Contains(Dog dog)
        {
            return this.dogsById.ContainsKey(dog.Id);
        }

        public Dog GetDog(string name, string ownerId)
        {
            if (!this.dogsByOwnerAndName.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!this.dogsByOwnerAndName[ownerId].ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return this.dogsByOwnerAndName[ownerId][name];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            if (!this.dogsByOwnerAndName.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!this.dogsByOwnerAndName[ownerId].ContainsKey(name))
            {
                throw new ArgumentException();
            }

            Dog dog = this.dogsByOwnerAndName[ownerId][name];
            this.dogsById.Remove(dog.Id);
            this.dogsByOwnerAndName[ownerId].Remove(dog.Name);
            if (this.dogsByOwnerAndName[ownerId].Count == 0)
            {
                this.dogsByOwnerAndName.Remove(ownerId);
            }

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!this.dogsByOwnerAndName.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return this.dogsByOwnerAndName[ownerId].Values.ToList();
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            List<Dog> dogs = this.dogsById.Values.Where(dog => dog.Breed == breed).ToList();

            if (dogs.Count == 0)
            {
                throw new ArgumentException();
            }

            return dogs;
        }

        public void Vaccinate(string name, string ownerId)
        {
            if (!this.dogsByOwnerAndName.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!this.dogsByOwnerAndName[ownerId].ContainsKey(name))
            {
                throw new ArgumentException();
            }

            Dog dog = this.dogsByOwnerAndName[ownerId][name];
            dog.Vaccines += 1;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            if (!this.dogsByOwnerAndName.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            if (!this.dogsByOwnerAndName[ownerId].ContainsKey(oldName))
            {
                throw new ArgumentException();
            }

            if (this.dogsByOwnerAndName[ownerId].ContainsKey(newName))
            {
                throw new ArgumentException();
            }

            Dog dog = this.dogsByOwnerAndName[ownerId][oldName];
            dog.Name = newName;
            this.dogsByOwnerAndName[ownerId].Remove(oldName);
            this.dogsByOwnerAndName[ownerId][newName] = dog;
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            List<Dog> dogs = this.dogsById.Values.Where(dog => dog.Age == age).ToList();

            if (dogs.Count == 0)
            {
                throw new ArgumentException();
            }

            return dogs;
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            return this.dogsById.Values.Where(dog => dog.Age >= lo && dog.Age <= hi).ToList();
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            return this.dogsById.Values
                .OrderBy(dog => dog.Age)
                .ThenBy(dog => dog.Name)
                .ThenBy(dog => dog.Owner.Name)
                .ToList();
            
        }
    }
}