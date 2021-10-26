using System.Collections.Generic;
using Controllers;
using Objects;
using UnityEngine;

namespace Utils
{
    public static class SpellHelper
    {
        public static Vector3 GetWorldPos(Vector2 mousePoint)
        {
            Ray ray =Camera.main.ScreenPointToRay(mousePoint);

            if (Physics.Raycast(ray, out RaycastHit hit, float.MaxValue, PlayerController.Instance.ground))
            {
                return hit.point;
            }

            return Vector3.zero;
        }

        public static Vector3 GetCenterPoint(this List<Enemy> enemies)
        {
            Vector3 sum = Vector3.zero;
            foreach (var enemy in enemies)
            {
                sum += enemy.transform.position;
            }

            return (sum / enemies.Count).WithY(1f);
        }
    }
}