using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public class RandomCharacterGenerator_Default : IRandomCharacterGenerator
    {
        public Character CreateCharacter(int id, string name)
        {
            return CreateCharacter(id, name, null);
        }
        

        public Character CreateCharacter(int id, string name, Random rng = null)
        {
            var c = new Character(id, name);

            if(rng == null)
                rng = new Random();

            int diceRoll = rng.Next(0, 100);
            if (diceRoll < 10)
                c.BaseSuspicion = SuspicionScale.Naive;
            else if (diceRoll < 30)
                c.BaseSuspicion = SuspicionScale.Relaxed;
            else if (diceRoll < 70)
                c.BaseSuspicion = SuspicionScale.Average;
            else if (diceRoll < 90)
                c.BaseSuspicion = SuspicionScale.Guarded;
            else
                c.BaseSuspicion = SuspicionScale.Paranoid;

            diceRoll = rng.Next(0, 100);
            if (diceRoll < 25)
                c.BaseMorality = Morality.Forgive;
            else if (diceRoll < 75)
                c.BaseMorality = Morality.Reciprocate;
            else
                c.BaseMorality = Morality.Exploit;

            return c;
        }
    }
}
