using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    /// <summary>
    /// At a specific point in time, a snapshot of all characters with their traits and relationships
    /// </summary>
    public class SocietySnapshot
    {
        public SocietySnapshot()
        {
            allCharacters = new List<Character>();
        }

        private List<Character> allCharacters;
        public List<Character> AllCharacters { get { return allCharacters; } }

        //Future: implement generic groups (ie races, nationalities, economic classes)
    }
}
