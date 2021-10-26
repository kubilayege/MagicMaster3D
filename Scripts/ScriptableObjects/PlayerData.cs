using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(menuName = "Player/Player Data")]
    public class PlayerData : ScriptableObject
    {
        public float playerForwardSpeed = 1f;
        // public float playerHorizontalSpeed = 1f;
        // public float playerBorderSize = 2.5f;
        // public float turnThreshold = 4f;
        // public float positionSnapThreshold = 1f;
        // public float positionSnapSpeed = 2f;
        
        public float Health;
    }
}