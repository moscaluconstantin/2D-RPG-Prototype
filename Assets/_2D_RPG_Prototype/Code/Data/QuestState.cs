using System;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public struct QuestState
    {
        public bool ActiveState;
        public bool CompletedState;

        public QuestState(bool activeState, bool completedState) : this()
        {
            ActiveState = activeState;
            CompletedState = completedState;
        }
    }
}
