using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.UI.Dialog
{
    [Serializable]
    public struct DialogueLine
    {
        [SerializeField] private string _owner;
        [SerializeField] private string _text;

        public string Owner => _owner;
        public string Text => _text;
    }
}
