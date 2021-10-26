using System;
using UnityEngine;

namespace ScriptableObjects.Level
{
    [CreateAssetMenu(menuName = "Level/New Level Container")]
    public class LevelContainer : ScriptableObject
    {
        public GameObject levelPrefab;
        [SerializeField]
        public SpellData[] levelSpells;
    }

    [Serializable]
    public class SpellData
    {
        public SpellType SpellType;
        public Effect SpellEffect;
    }
}