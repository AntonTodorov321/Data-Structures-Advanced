namespace _02.FitGym
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    public class FitGym : IGym
    {
        private Dictionary<int, Member> members;
        private Dictionary<int, Trainer> trainers;
        private Dictionary<Trainer, HashSet<Member>> trainerMembers;

        public FitGym()
        {
            this.members = new Dictionary<int, Member>();
            this.trainers = new Dictionary<int, Trainer>();
            this.trainerMembers = new Dictionary<Trainer, HashSet<Member>>();
        }

        public int MemberCount => this.members.Count;

        public int TrainerCount => this.trainers.Count;

        public void AddMember(Member member)
        {
            if (this.members.ContainsKey(member.Id))
            {
                throw new ArgumentException();
            }

            this.members.Add(member.Id, member);
        }

        public void HireTrainer(Trainer trainer)
        {
            if (this.trainers.ContainsKey(trainer.Id))
            {
                throw new ArgumentException();
            }

            this.trainers.Add(trainer.Id, trainer);
        }

        public void Add(Trainer trainer, Member member)
        {
            if (!this.trainers.ContainsKey(trainer.Id) || member.Trainer != null)
            {
                throw new ArgumentException();
            }

            if (!this.members.ContainsKey(member.Id))
            {
                this.members.Add(member.Id, member);
            }

            if (!this.trainerMembers.ContainsKey(trainer))
            {
                this.trainerMembers[trainer] = new HashSet<Member>();
            }

            this.trainerMembers[trainer].Add(member);
            member.Trainer = trainer;
        }

        public bool Contains(Member member)
        {
            return this.members.ContainsKey(member.Id);
        }

        public bool Contains(Trainer trainer)
        {
            return this.trainers.ContainsKey(trainer.Id);
        }

        public Trainer FireTrainer(int id)
        {
            if (!this.trainers.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var trainer = this.trainers[id];

            this.trainers.Remove(id);
            this.trainerMembers.Remove(trainer);

            return trainer;
        }

        public Member RemoveMember(int id)
        {
            if (!this.members.ContainsKey(id))
            {
                throw new ArgumentException();
            }

            var member = this.members[id];
            this.members.Remove(id);

            if (member.Trainer != null
                && this.trainerMembers.ContainsKey(member.Trainer))
            {
                this.trainerMembers[member.Trainer].Remove(member);
            }

            return member;
        }

        public IEnumerable<Member>
            GetMembersInOrderOfRegistrationAscendingThenByNamesDescending()
        {
            return this.members.Values
                .OrderBy(m => m.RegistrationDate)
                .ThenByDescending(m => m.Name);
        }

        public IEnumerable<Trainer> GetTrainersInOrdersOfPopularity()
        {
            return this.trainers.Values
                .OrderBy(t => t.Popularity);
        }

        public IEnumerable<Member>
            GetTrainerMembersSortedByRegistrationDateThenByNames(Trainer trainer)
        {
            return this.trainerMembers[trainer]
                .OrderBy(m => m.RegistrationDate)
                .ThenBy(m => m.Name);
        }

        public IEnumerable<Member>
            GetMembersByTrainerPopularityInRangeSortedByVisitsThenByNames(int lo, int hi)
        {
            return this.members.Values
                .Where(m => m.Trainer.Popularity >= lo && m.Trainer.Popularity <= hi)
                .OrderBy(m => m.Visits)
                .ThenBy(m => m.Name);
        }

        public Dictionary<Trainer, HashSet<Member>>
            GetTrainersAndMemberOrderedByMembersCountThenByPopularity()
        {
            return this.trainerMembers
                .OrderBy(kvp => kvp.Value.Count)
                .ThenBy(kvp => kvp.Key.Popularity)
                .ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }
    }
}