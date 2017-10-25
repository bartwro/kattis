using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bus_problem
{
    class Program
    {
        static void Main(string[] args)
        {
            int n = 12;
            int k = 3;
            HashSet<Group> groups = new HashSet<Group>();
            var list = new int[] { 2, 3, 4, 5, 6, 7, 4, 7, 8, 8, 12, 12 };
            list = list.Select(x => x - 1).ToArray();

            var startMember = 0;
            while (startMember != -1)
            {
                if(!TryToAddToExistingGroup(groups, startMember))
                {
                    groups.Add(CreateGroups(list, startMember));
                }
                startMember = GetNextStart(list, groups);
            }

            foreach(var firstGroup in groups.Select(x => x.FirstMembers))
            {
                foreach(var element in firstGroup)
                {
                    Console.WriteLine(element);
                }
                Console.WriteLine();
            }

            Console.ReadLine();
        }

        private static bool TryToAddToExistingGroup(HashSet<Group> groups, int startMember)
        {
            var hashSet = groups.Select(x => x.FirstMembers).FirstOrDefault(g => g.Contains(startMember));
            if (hashSet!=null)
            {
                return hashSet.Add(startMember);
            }

            hashSet = groups.Select(x => x.SecondMembers).FirstOrDefault(g => g.Contains(startMember));
            if (hashSet != null)
            {
                return hashSet.Add(startMember);
            }

            return false;
        }

        private static int GetNextStart(int[] list, HashSet<Group> groups)
        {
            IEnumerable<int> candidates = list
                .Where(x => !groups.Any(g => g.FirstMembers.Contains(x) || g.SecondMembers.Contains(x)));
            if (candidates.Count() == 0)
            {
                return -1;
            }
            else
            {
                return candidates
                    .Min(x => x);
            }
        }

        private static Group CreateGroups(int[] list, int start)
        {
            var members = new HashSet<int>();
            members.Add(start);
            int nextMember = list[start];
            while (members.Add(nextMember))
            {
                nextMember = list[nextMember];
            }
            //nextMember exists already in members
            //found members between last member and first one, these are first members
            var firstMember = nextMember;
            var group = new Group();

            foreach (var member in members.TakeWhile(x => x != firstMember))
            {
                group
                    .SecondMembers
                    .Add(member);
            }

            foreach (var member in members.SkipWhile(x => x != firstMember))
            {
                group
                    .FirstMembers
                    .Add(member);
            }

            group.Min = group.FirstMembers.Count;
            group.Max = group.SecondMembers.Count;

            return group;
        }
    }
}
