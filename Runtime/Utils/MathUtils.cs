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
        /// Clamp an angle between the desired origin and the clamp angle in either direction
        /// This will be ignored if isAngleClamped is set to false
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

        /// <summary>
        /// Invert a Vector3 along the desired axis/axes
        /// </summary>
        /// <param name="input"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Vector3 InvertVector3(Vector3 input, InvertAxis axis)
        {
            var invertVector = Vector3.one;

            switch (axis)
            {
                case InvertAxis.x:
                    invertVector.x = -1;
                    break;
                case InvertAxis.y:
                    invertVector.y = -1;
                    break;
                case InvertAxis.z:
                    invertVector.z = -1;
                    break;
                case InvertAxis.xy:
                    invertVector.x = -1;
                    invertVector.y = -1;
                    break;
                case InvertAxis.xz:
                    invertVector.x = -1;
                    invertVector.z = -1;
                    break;
                case InvertAxis.yz:
                    invertVector.y = -1;
                    invertVector.z = -1;
                    break;
            }

            var output = Vector3.Scale(input, invertVector);

            return output;
        }


        /// <summary>
        /// Invert a Vector2 along the desired axis
        /// </summary>
        /// <param name="input"></param>
        /// <param name="axis"></param>
        /// <returns></returns>
        public static Vector2 InvertVector2(Vector2 input, InvertAxis axis)
        {
            var invertVector = Vector2.one;

            switch (axis)
            {
                case InvertAxis.x:
                    invertVector.x = -1;
                    break;
                case InvertAxis.y:
                    invertVector.y = -1;
                    break;
            }

            var output = Vector2.Scale(input, invertVector);

            return output;
        }

        public static Vector3 LerpByDistance(Vector3 origin, Vector3 destination, float distance)
        {
            Vector3 direction = destination - origin;
            Vector3 lerpVector = origin + (distance * direction);
            return lerpVector;
        }

        public static Vector2 LerpByDistance(Vector2 origin, Vector2 destination, float distance)
        {
            Vector2 direction = destination - origin;
            Vector2 lerpVector = origin + (distance * direction);
            return lerpVector;
        }
    }

    public enum InvertAxis
    { 
        x,
        y,
        z,
        xy,
        xz,
        yz
    }
}


