﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public interface IRandomCharacterGenerator
    {
        Character CreateCharacter(int id, string name);
    }
}
