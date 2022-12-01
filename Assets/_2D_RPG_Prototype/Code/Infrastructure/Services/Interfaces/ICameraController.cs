using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Implementations
{
    public interface ICameraController : IService
    {
        void Follow(Transform target);
    }
}
