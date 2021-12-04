namespace _02.LegionSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using _02.LegionSystem.Interfaces;
    using Wintellect.PowerCollections;

    public class Legion : IArmy
    {
        private OrderedSet<IEnemy> enemies;

        public Legion()
        {
            this.enemies = new OrderedSet<IEnemy>();
        }

        public int Size => this.enemies.Count;

        public bool Contains(IEnemy enemy)
        {
            return this.enemies.Contains(enemy);
        }

        public void Create(IEnemy enemy)
        {
            this.enemies.Add(enemy);
        }

        public IEnemy GetByAttackSpeed(int speed)
        {
            foreach (var enemy in this.enemies)
            {
                if (enemy.AttackSpeed == speed)
                {
                    return enemy;
                }
            }

            return null;
        }

        public List<IEnemy> GetFaster(int speed)
        {
            List<IEnemy> result = new List<IEnemy>(this.Size);

            foreach (var enemy in this.enemies)
            {
                if (enemy.AttackSpeed > speed)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        public IEnemy GetFastest()
        {
            this.ValidateLegion();

            return this.enemies.GetFirst();
        }

        public IEnemy[] GetOrderedByHealth()
        {
            return this.enemies.OrderByDescending(enemy => enemy.Health).ToArray();
        }

        public List<IEnemy> GetSlower(int speed)
        {
            List<IEnemy> result = new List<IEnemy>(this.Size);

            foreach (var enemy in this.enemies)
            {
                if (enemy.AttackSpeed < speed)
                {
                    result.Add(enemy);
                }
            }

            return result;
        }

        public IEnemy GetSlowest()
        {
            this.ValidateLegion();

            return this.enemies.GetLast();
        }

        public void ShootFastest()
        {
            this.ValidateLegion();
            this.enemies.RemoveFirst();
        }

        public void ShootSlowest()
        {
            this.ValidateLegion();
            this.enemies.RemoveLast();
        }

        private void ValidateLegion()
        {
            if (this.enemies.Count == 0)
            {
                throw new InvalidOperationException("Legion has no enemies!");
            }
        }
    }
}
