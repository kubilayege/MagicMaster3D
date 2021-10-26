using DG.Tweening;
using UnityEngine;

namespace Utils
{
    public static class Helper
    {
        public static Vector2 WithY(this Vector2 v, float val)
        {
            v.y = val;
            return v;
        }
        
        public static Vector3 WithY(this Vector3 v, float val)
        {
            v.y = val;
            return v;
        }
        
        public static Vector3 WithZ(this Vector3 v, float val)
        {
            v.z = val;
            return v;
        }
        
        public static Vector3 WithX(this Vector3 v, float val)
        {
            v.x = val;
            return v;
        }
        
        public static Vector2 WithX(this Vector2 v, float val)
        {
            v.x = val;
            return v;
        }
        
        public static Vector3 Plus(this Vector3 v, Vector3 other)
        {
            return v + other;
        }

        public static Vector3 Minus(this Vector3 v, Vector3 other)
        {
            return v - other;
        }
        
        public static Vector2 WithXClamp(this Vector2 v, float clampValue)
        {
            v.x = Mathf.Clamp(v.x, -clampValue, clampValue);
            return v;
        }
        
        public static float AngleOffAroundAxis(Vector3 v, Vector3 forward, Vector3 axis, bool clockwise = false)
        {
            Vector3 right;
            if(clockwise)
            {
                right = Vector3.Cross(forward, axis);
                forward = Vector3.Cross(axis, right);
            }
            else
            {
                right = Vector3.Cross(axis, forward);
                forward = Vector3.Cross(right, axis);
            }
            return Mathf.Rad2Deg * (Mathf.Atan2(Vector3.Dot(v, right), Vector3.Dot(v, forward)));
        }


        public static void LerpTo(this Transform t, Vector3 p, float speed)
        {
            t.position = Vector3.Lerp(t.position, p, Time.unscaledDeltaTime * speed);
        }

        public static void DoTimeScaleManipulation(float endValue, float timeItTakesToGetToThatValue)
        {
            DOTween.Kill("TimeScaleManipulation");
            var myValue = Time.timeScale;
            DOTween.To(()=> myValue, x=> myValue = x, endValue, timeItTakesToGetToThatValue)
                .OnUpdate(() =>
                {
                    Time.timeScale = myValue;
                    Time.fixedDeltaTime = Time.timeScale * 0.02f;
                }).SetId("TimeScaleManipulation");
        }

        public static float Distance(this Vector3 v, Vector3 other)
        {
            return Vector3.Distance(v, other);
        }

    }
}

