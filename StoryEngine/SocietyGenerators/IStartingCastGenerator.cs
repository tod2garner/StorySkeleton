﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine.SocietyGenerators
{
    public interface IStartingCastGenerator
    {
        SocietySnapshot CreateStartingCast(int characterCount, Random rng);
    }
}
