﻿using Gum.Managers;

namespace Gum.ToolStates
{
    public class GumState : Singleton<GumState>
    {
        public ProjectState ProjectState => ProjectState.Self;
        public ISelectedState SelectedState => Gum.ToolStates.SelectedState.Self;
    }
}
