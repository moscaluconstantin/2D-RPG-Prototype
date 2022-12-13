using Assets._2D_RPG_Prototype.Code.Enums;
using System;
using UnityEngine;

namespace Assets._2D_RPG_Prototype.Code.Data
{
    [Serializable]
    public struct StatValue
    {
        [SerializeField] private StatType _type;
        [SerializeField] private int _value;

        public StatType Type => _type;
        public int Value => _value;
    }
}
