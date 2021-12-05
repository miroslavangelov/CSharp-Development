namespace _02.FitGym
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> membersById = new Dictionary<int, Member>();
        private Dictionary<int, Trainer> trainersById = new Dictionary<int, Trainer>();
        private Dictionary<Trainer, HashSet<Member>> trainers = new Dictionary<Trainer, HashSet<Member>>();
        private Dictionary<Member, Trainer> members = new Dictionary<Member, Trainer>();

        public void AddMember(Member member)
        {
            if (this.Contains(member))
            {
                throw new ArgumentException();
            }

            this.membersById[member.Id] = member;
            this.members[member] = null;
        }

        public void HireTrainer(Trainer trainer)
        {
            if (this.Contains(trainer))
            {
                throw new ArgumentException();
            }

            this.trainersById[trainer.Id] = trainer;
            trainers[trainer] = new HashSet<Member>();
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!this.Contains(trainer))
            {
                throw new ArgumentException();
            }

            if (trainer.Members.Contains(member))
            {
                throw new ArgumentException();
            }

            if (members.ContainsKey(member))
            {
                if (members[member] != null)
                {
                    throw new ArgumentException();
                }
            }

            if (!this.Contains(member))
            {
                this.membersById[member.Id] = member;
            }

            this.membersById[member.Id].Trainer = trainer;
            this.trainersById[trainer.Id].Members.Add(member);
            this.members[member] = trainer;
            this.trainers[trainer].Add(member);
        }

        public int MemberCount => this.membersById.Count;

        public int TrainerCount => this.trainersById.Count;

        public bool Contains(Member member) => this.membersById.ContainsKey(member.Id);

        public bool Contains(Trainer trainer) => this.trainersById.ContainsKey(trainer.Id);

        public Trainer FireTrainer(int id)
        {
            if (!this.trainersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var toRemove = this.trainersById[id];

            this.trainersById.Remove(id);
            this.trainers.Remove(toRemove);

            foreach (var member in toRemove.Members)
            {
                member.Trainer = null;
                this.members[member] = null;
            }

            return toRemove;
        }

        public Member RemoveMember(int id)
        {
            if (!this.membersById.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var toRemove = this.membersById[id];

            this.membersById.Remove(id);
            this.members.Remove(toRemove);

            if (toRemove.Trainer != null)
            {
                trainersById[toRemove.Trainer.Id].Members.Remove(toRemove);
                trainers[toRemove.Trainer].Remove(toRemove);
            }

            return toRemove;
        }

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return members
               .Select(m => m.Key)
               .OrderBy(m => m.RegistrationDate)
               .ThenBy(m => m.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return trainers
                .Select(t => t.Key)
                .OrderBy(t => t.Popularity);
        }

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            if (!trainers.ContainsKey(trainer))
            {
                return new List<Member>();
            }

            return trainers[trainer].OrderBy(m => m.RegistrationDate).ThenBy(m => m.Name);
        }

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {
            return members
                .Where(m => m.Value.Popularity >= lo && m.Value.Popularity <= hi)
                .OrderBy(m => m.Key.Visits)
                .ThenBy(m => m.Key.Name)
                .Select(m => m.Key);
        }

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            return trainers
                .OrderBy(t => trainers.Values.Count)
                .ThenBy(t => t.Key.Popularity)
                .ToDictionary(k => k.Key, v => v.Value);
        }
    }
}