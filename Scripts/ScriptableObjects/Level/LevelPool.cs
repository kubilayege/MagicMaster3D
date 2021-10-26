using UnityEngine;

namespace ScriptableObjects.Level
{
    [CreateAssetMenu(menuName = "Level/Pool")]
    public class LevelPool : ScriptableObject
    {
        public LevelContainer[] levels;
        public LevelContainer tutorialLevel;
        public int Length => levels.Length;
    }
}