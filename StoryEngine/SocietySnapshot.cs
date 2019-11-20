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

        public SocietySnapshot Copy()
        {
            var theCopy = new SocietySnapshot();

            foreach (Character c in AllCharacters)
                theCopy.AllCharacters.Add(c.Copy());

            return theCopy;
        }

        public List<string> GetTextSummary()
        {
            var theSummary = new List<string>();
            theSummary.Add("CHARACTERS:");            

            foreach(Character c in AllCharacters)
            {
                theSummary.AddRange(c.DescribeSelf());
            }
            
            theSummary.Add(String.Empty);
            theSummary.Add("RELATIONSHIPS:");
            for (int i = 0; i < AllCharacters.Count; i++)
            {
                var c1 = AllCharacters[i];

                for (int j = 0; j < AllCharacters.Count; j++)
                {
                    if (j == i)
                        continue;

                    var c2 = AllCharacters[j];
                    theSummary.Add(c1.DescribeTrustTowards(c2));
                    theSummary.Add(c1.DescribeEthicsTowards(c2));
                    theSummary.Add(String.Empty);
                }
            }
            
            return theSummary;
        }
    }
}
