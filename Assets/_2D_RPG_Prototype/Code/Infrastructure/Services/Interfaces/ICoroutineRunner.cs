﻿using System.Collections;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Infrastructure.Services.Interfaces
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
        void StopCoroutine(Coroutine coroutine);
    }
}
