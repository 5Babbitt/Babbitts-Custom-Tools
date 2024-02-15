using UnityEngine;

namespace Babbitt.Tools
{ 
    public class MathUtils
    {
        public static Quaternion ShortestRotation(Quaternion to, Quaternion from)
        {
            if (Quaternion.Dot(to, from) < 0)
            {
                return to * Quaternion.Inverse(Multiply(from, -1));
            }

            else return to * Quaternion.Inverse(from);
        }

        public static Quaternion Multiply(Quaternion input, float scalar)
        {
            return new Quaternion(input.x * scalar, input.y * scalar, input.z * scalar, input.w * scalar);
        }

    }
}

