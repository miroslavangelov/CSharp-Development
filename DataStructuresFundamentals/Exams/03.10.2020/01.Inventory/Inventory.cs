using System.Linq;

namespace _01.Inventory
{
    using _01.Inventory.Interfaces;
    using _01.Inventory.Models;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Inventory : IHolder
    {
        private List<IWeapon> weapons;

        public Inventory()
        {
            this.weapons = new List<IWeapon>();
        }

        public int Capacity => this.weapons.Count;

        public void Add(IWeapon weapon)
        {
            this.weapons.Add(weapon);
        }

        public void Clear()
        {
            this.weapons.Clear();
        }

        public bool Contains(IWeapon weapon)
        {
            return this.weapons.Contains(weapon);
        }

        public void EmptyArsenal(Category category)
        {
            foreach (var weapon in this.weapons)
            {
                if (weapon.Category == category)
                {
                    weapon.Ammunition = 0;
                }
            }
        }

        public bool Fire(IWeapon weapon, int ammunition)
        {
            int weaponIndex = this.weapons.IndexOf(weapon);
            this.ValidateWeapon(weaponIndex);

            IWeapon currentWeapon = this.weapons[weaponIndex];

            if (currentWeapon.Ammunition < ammunition)
            {
                return false;
            }

            currentWeapon.Ammunition -= ammunition;

            return true;
        }

        public IWeapon GetById(int id)
        {
            for (int i = 0; i < this.weapons.Count; i++)
            {
                var current = this.weapons[i];

                if (current.Id == id)
                {
                    return current;
                }
            }

            return null;
        }

        public IEnumerator GetEnumerator()
        {
            return this.weapons.GetEnumerator();
        }

        public int Refill(IWeapon weapon, int ammunition)
        {
            int weaponIndex = this.weapons.IndexOf(weapon);
            this.ValidateWeapon(weaponIndex);

            IWeapon currentWeapon = this.weapons[weaponIndex];

            if (currentWeapon.Ammunition + ammunition >= currentWeapon.MaxCapacity)
            {
                currentWeapon.Ammunition = currentWeapon.MaxCapacity;
            }
            else
            {
                currentWeapon.Ammunition += ammunition;
            }

            return currentWeapon.Ammunition;
        }

        public IWeapon RemoveById(int id)
        {
            IWeapon weapon = this.GetById(id);

            if (weapon == null)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }

            this.weapons.Remove(weapon);

            return weapon;
        }

        public int RemoveHeavy()
        {
            return this.weapons.RemoveAll(weapon => weapon.Category == Category.Heavy);
        }

        public List<IWeapon> RetrieveAll()
        {
            return new List<IWeapon>(this.weapons);
        }

        public List<IWeapon> RetriveInRange(Category lower, Category upper)
        {
            List<IWeapon> result = new List<IWeapon>(this.Capacity);

            result.AddRange(this.weapons.Where(weapon => weapon.Category >= lower && weapon.Category <= upper));

            return result;
        }

        public void Swap(IWeapon firstWeapon, IWeapon secondWeapon)
        {
            int firstWeaponIndex = this.weapons.IndexOf(firstWeapon);
            int secondWeaponIndex = this.weapons.IndexOf(secondWeapon);

            this.ValidateWeapon(firstWeaponIndex);
            this.ValidateWeapon(secondWeaponIndex);

            IWeapon first = this.weapons[firstWeaponIndex];
            IWeapon second = this.weapons[secondWeaponIndex];

            if (firstWeapon.Category == secondWeapon.Category)
            {
                this.weapons[firstWeaponIndex] = second;
                this.weapons[secondWeaponIndex] = first;
            }
        }

        private void ValidateWeapon(int index)
        {
            if (index == -1)
            {
                throw new InvalidOperationException("Weapon does not exist in inventory!");
            }
        }
    }
}
