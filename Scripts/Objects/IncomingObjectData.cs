using UnityEngine;

namespace Objects
{
    public class IncomingObjectData
    {
        public Vector3 Direction;
        public Vector3 SpawnPos;

        public IncomingObjectData(Vector3 direction, Vector3 spawnPos)
        {
            Direction = direction;
            SpawnPos = spawnPos;
        }
    } 
}