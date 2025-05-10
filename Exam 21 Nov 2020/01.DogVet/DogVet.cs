namespace _01.DogVet
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class DogVet : IDogVet
    {
        private Dictionary<string, Dog> dogs;
        private Dictionary<string, Dictionary<string, Dog>> ownerDogsByName;

        public DogVet()
        {
            this.dogs = new Dictionary<string, Dog>();
            this.ownerDogsByName = new Dictionary<string, Dictionary<string, Dog>>();
        }

        public int Size => this.dogs.Count;

        public void AddDog(Dog dog, Owner owner)
        {
            if (this.dogs.ContainsKey(dog.Id))
            {
                throw new ArgumentException();
            }

            if (!this.ownerDogsByName.ContainsKey(owner.Id))
            {
                this.ownerDogsByName[owner.Id] = new Dictionary<string, Dog>();
            }

            if (this.ownerDogsByName[owner.Id].ContainsKey(dog.Name))
            {
                throw new ArgumentException();
            }

            dog.OwnerName = owner.Name;
            this.ownerDogsByName[owner.Id].Add(dog.Name, dog);
            this.dogs.Add(dog.Id, dog);
        }

        public bool Contains(Dog dog)
        {
            return this.dogs.ContainsKey(dog.Id);
        }

        public Dog GetDog(string name, string ownerId)
        {
            if (!this.ownerDogsByName.ContainsKey(ownerId) ||
                !this.ownerDogsByName[ownerId].ContainsKey(name))
            {
                throw new ArgumentException();
            }

            return this.ownerDogsByName[ownerId][name];
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            if (!this.ownerDogsByName.ContainsKey(ownerId) ||
                !this.ownerDogsByName[ownerId].ContainsKey(name))
            {
                throw new ArgumentException();
            }

            var dog = this.ownerDogsByName[ownerId][name];
            this.ownerDogsByName[ownerId].Remove(name);
            this.dogs.Remove(dog.Id);

            return dog;
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            if (!this.ownerDogsByName.ContainsKey(ownerId))
            {
                throw new ArgumentException();
            }

            return this.ownerDogsByName[ownerId].Values;
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            if (this.dogs.Count == 0)
            {
                throw new ArgumentException();
            }

            return this.dogs.Values.Where(dog => dog.Breed == breed);
        }

        public void Vaccinate(string name, string ownerId)
        {
            if (!this.ownerDogsByName.ContainsKey(ownerId) ||
                !this.ownerDogsByName[ownerId].ContainsKey(name))
            {
                throw new ArgumentException();
            }

            this.ownerDogsByName[ownerId][name].Vaccines++;
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            if (!this.ownerDogsByName.ContainsKey(ownerId) ||
                !this.ownerDogsByName[ownerId].ContainsKey(oldName))
            {
                throw new ArgumentException();
            }

            var dog = this.ownerDogsByName[ownerId][oldName];

            this.RemoveDog(oldName, ownerId);
            dog.Name = newName;
            this.ownerDogsByName[ownerId].Add(newName, dog);
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            if (this.dogs.Values.Where(dog => dog.Age == age).Count() == 0)
            {
                throw new ArgumentException();
            }

            return this.dogs.Values.Where(dog => dog.Age == age);
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            return this.dogs.Values.Where(dog => dog.Age >= lo && dog.Age <= hi);
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            return this.dogs.Values
                .OrderBy(dog => dog.Age)
                .ThenBy(dog => dog.Name)
                .ThenBy(dog => dog.OwnerName);
        }
    }
}