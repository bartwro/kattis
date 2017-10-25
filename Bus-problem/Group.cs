using System.Collections.Generic;

namespace Bus_problem
{
    public class Group
    {
        public int Min;
        public int Max;

        public HashSet<int> FirstMembers = new HashSet<int>();
        public HashSet<int> SecondMembers = new HashSet<int>();
    }
}
