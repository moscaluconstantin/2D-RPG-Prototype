using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface ISceneLoader : IService
    {
        Coroutine LoadScene(string sceneName);
    }
}
