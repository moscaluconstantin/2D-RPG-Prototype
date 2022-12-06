using Assets._2D_RPG_Prototype.Code.ScriptableObjects;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IStatsManager : IService
    {
        public CharacterStats[] Stats { get; }
    }
}
