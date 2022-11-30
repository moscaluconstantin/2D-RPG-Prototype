using Assets._2D_RPG_Prototype.Code.Enums;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface IScreenFader
    {
        void FadeInstant(FadeType fadeType);
        Coroutine Fade(FadeType fadeType);
    }
}
