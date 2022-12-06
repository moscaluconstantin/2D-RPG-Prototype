using Assets._2D_RPG_Prototype.Code.Constants;
using Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces;
using Assets._2D_RPG_Prototype.Code.ScriptableObjects;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public class StatsManager : IStatsManager
    {
        public CharacterStats[] Stats => _stats;

        private CharacterStats[] _stats;

        public StatsManager() => 
            _stats = Resources.LoadAll<CharacterStats>(ResourcePaths.STATS);
    }
}
