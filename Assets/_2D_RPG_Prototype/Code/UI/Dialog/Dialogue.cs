using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Dialog
{
    [Serializable]
    public struct Dialogue
    {
        [SerializeField] private DialogueLine[] _lines;

        public DialogueLine[] Lines => _lines;
    }
}
