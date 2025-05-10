namespace _01.DogVet
{
    using System;
    using System.Collections.Generic;

    public class DogVet : IDogVet
    {
        public int Size { get; }
        public void AddDog(Dog dog, Owner owner)
        {
            throw new NotImplementedException();
        }

        public bool Contains(Dog dog)
        {
            throw new NotImplementedException();
        }

        public Dog GetDog(string name, string ownerId)
        {
            throw new NotImplementedException();
        }

        public Dog RemoveDog(string name, string ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetDogsByOwner(string ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetDogsByBreed(Breed breed)
        {
            throw new NotImplementedException();
        }

        public void Vaccinate(string name, string ownerId)
        {
            throw new NotImplementedException();
        }

        public void Rename(string oldName, string newName, string ownerId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetAllDogsByAge(int age)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetDogsInAgeRange(int lo, int hi)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Dog> GetAllOrderedByAgeThenByNameThenByOwnerNameAscending()
        {
            throw new NotImplementedException();
        }
    }
}