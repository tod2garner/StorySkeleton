﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoryEngine
{
    public interface IOutcome
    {
        List<string> ExecuteAndGiveSummary();

        IOutcome Copy(List<Role> replacementRoles);
    }
}
