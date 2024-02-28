using UnityEngine;

namespace FiveBabbittGames
{ 
    public class MathUtils
    {
        /// <summary>
        /// Find and return the shortest possible rotation between two quaternions
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <returns></returns>
        public static Quaternion ShortestRotation(Quaternion to, Quaternion from)
        {
            if (Quaternion.Dot(to, from) < 0)
            {
                return to * Quaternion.Inverse(Multiply(from, -1));
            }

            else return to * Quaternion.Inverse(from);
        }

        /// <summary>
        /// Mulitply each component of a quaternion by a float value
        /// </summary>
        /// <param name="input"></param>
        /// <param name="scalar"></param>
        /// <returns></returns>
        public static Quaternion Multiply(Quaternion input, float scalar)
        {
            return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
        }

        /// <summary>
        /// Convert an angle in degrees to a Vector2
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static Vector2 AngleToVector2(float input)
        {
            Vector2 output = new(Mathf.Sin(input * Mathf.Deg2Rad), Mathf.Cos(input * Mathf.Deg2Rad));

            return output.normalized;
        }

        /// <summary>
        /// Clamp an angle between the desired origin and the clamp angle in either direction.
        /// This can be ignored is isAngleClamped is set to false
        /// </summary>
        /// <param name="angle"></param>
        /// <param name="originAngle"></param>
        /// <param name="clampAngle"></param>
        /// <param name="isAngleClamped"></param>
        /// <returns></returns>
        public static float ClampAngle(float angle, float originAngle, float clampAngle, bool isAngleClamped = true)
        {
            if (isAngleClamped)
                angle = Mathf.Clamp(angle, originAngle - clampAngle, originAngle + clampAngle);

            return angle;
        }
    }
}

