using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public interface IRelationshipGenerator
    {
        Relationship CreateRelationship(Character self, Character other, Random rng = null);
    }
}
