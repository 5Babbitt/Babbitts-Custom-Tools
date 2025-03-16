using UnityEngine;

namespace FiveBabbittGames
{
    public static class PhysicsUtils
    {
        public static void ApplyForceToReachVelocity(Rigidbody rigidbody, Vector3 velocity, float force = 1, ForceMode mode = ForceMode.Force)
        {
            if (force == 0 || velocity.magnitude == 0)
                return;

            velocity = velocity + velocity.normalized * 0.2f * rigidbody.linearDamping;

            //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
            force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

            //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude
            if (rigidbody.linearVelocity.magnitude == 0)
            {
                rigidbody.AddForce(velocity * force, mode);
            }
            else
            {
                var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.linearVelocity) / velocity.magnitude);
                rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
            }
        }

        public static void ApplyForceToReachVelocity2D(Rigidbody2D rigidbody, Vector2 velocity, float force = 1, ForceMode2D mode = ForceMode2D.Force)
        {
            if (force == 0 || velocity.magnitude == 0)
                return;

            velocity = velocity + velocity.normalized * 0.2f * rigidbody.linearDamping;

            //force = 1 => need 1 s to reach velocity (if mass is 1) => force can be max 1 / Time.fixedDeltaTime
            force = Mathf.Clamp(force, -rigidbody.mass / Time.fixedDeltaTime, rigidbody.mass / Time.fixedDeltaTime);

            //dot product is a projection from rhs to lhs with a length of result / lhs.magnitude
            if (rigidbody.linearVelocity.magnitude == 0)
            {
                rigidbody.AddForce(velocity * force, mode);
            }
            else
            {
                var velocityProjectedToTarget = (velocity.normalized * Vector3.Dot(velocity, rigidbody.linearVelocity) / velocity.magnitude);
                rigidbody.AddForce((velocity - velocityProjectedToTarget) * force, mode);
            }
        }

        public static void ApplyTorqueToReachRPS(Rigidbody rigidbody, Quaternion rotation, float rps, float force = 1)
        {
            var radPerSecond = rps * 2 * Mathf.PI + rigidbody.angularDamping * 20;

            float angleInDegrees;
            Vector3 rotationAxis;
            rotation.ToAngleAxis(out angleInDegrees, out rotationAxis);

            if (force == 0 || rotationAxis == Vector3.zero)
                return;

            rigidbody.maxAngularVelocity = Mathf.Max(rigidbody.maxAngularVelocity, radPerSecond);

            force = Mathf.Clamp(force, -rigidbody.mass * 2 * Mathf.PI / Time.fixedDeltaTime, rigidbody.mass * 2 * Mathf.PI / Time.fixedDeltaTime);

            var currentSpeed = Vector3.Project(rigidbody.angularVelocity, rotationAxis).magnitude;

            rigidbody.AddTorque(rotationAxis * (radPerSecond - currentSpeed) * force);
        }

        public static Vector3 QuaternionToAngularVelocity(Quaternion rotation)
        {
            float angleInDegrees;
            Vector3 rotationAxis;
            rotation.ToAngleAxis(out angleInDegrees, out rotationAxis);

            return rotationAxis * angleInDegrees * Mathf.Deg2Rad;
        }

        public static Quaternion AngularVelocityToQuaternion(Vector3 angularVelocity)
        {
            var rotationAxis = (angularVelocity * Mathf.Rad2Deg).normalized;
            float angleInDegrees = (angularVelocity * Mathf.Rad2Deg).magnitude;

            return Quaternion.AngleAxis(angleInDegrees, rotationAxis);
        }

        public static Vector3 GetNormal(Vector3[] points)
        {
            //https://www.ilikebigbits.com/2015_03_04_plane_from_points.html
            if (points.Length < 3)
                return Vector3.up;

            var center = GetCenter(points);
            float xx = 0f, xy = 0f, xz = 0f, yy = 0f, yz = 0f, zz = 0f;

            for (int i = 0; i < points.Length; i++)
            {
                var r = points[i] - center;
                xx += r.x * r.x;
                xy += r.x * r.y;
                xz += r.x * r.z;
                yy += r.y * r.y;
                yz += r.y * r.z;
                zz += r.z * r.z;
            }

            var det_x = yy * zz - yz * yz;
            var det_y = xx * zz - xz * xz;
            var det_z = xx * yy - xy * xy;

            if (det_x > det_y && det_x > det_z)
                return new Vector3(det_x, xz * yz - xy * zz, xy * yz - xz * yy).normalized;
            if (det_y > det_z)
                return new Vector3(xz * yz - xy * zz, det_y, xy * xz - yz * xx).normalized;
            else
                return new Vector3(xy * yz - xz * yy, xy * xz - yz * xx, det_z).normalized;
        }

        public static Vector3 GetCenter(Vector3[] points)
        {
            var center = Vector3.zero;
            for (int i = 0; i < points.Length; i++)
                center += points[i] / points.Length;
            return center;
        }

        /// <summary>
        /// Perform a raycast along an arc shape defined by a center, rotation, angle, and radius
        /// </summary>
        /// <param name="center">Center of the arc</param>
        /// <param name="rotation">Rotation of the object casting</param>
        /// <param name="angle">The angular range of the arc</param>
        /// <param name="radius">Radius of the arc</param>
        /// <param name="resolution">How smooth the arc is (higher = smoother)</param>
        /// <param name="layer">Layers to detect hits</param>
        /// <param name="hit"></param>
        /// <param name="drawGizmos"></param>
        /// <returns>True if hit the target layer</returns>
        static public bool ArcCast(Vector3 center, Quaternion rotation, float angle, float radius, int resolution, LayerMask layer, out RaycastHit hit, bool drawGizmos = false)
        {
            // Adjust the initial rotation by half of the specified angle around the x-axis
            rotation *= Quaternion.Euler(-angle / 2, 0, 0);

            // Calculate the angular step size for each resolution unit
            float deltaAngle = angle / resolution;
            Vector3 forwardRadius = Vector3.forward * radius;

            Vector3 a, b, ab; // Define vectors for points A, B, and the vector AB
            a = forwardRadius; // Set the initial point A as the forward radius vector
            b = Quaternion.Euler(deltaAngle, 0, 0) * forwardRadius; // Calculate point B after applying a rotation
            ab = b - a; // Calculate the vector AB between points A and B
            float abMagnitude = ab.magnitude * 1.001f; // Add a small margin to the magnitude of AB

            // Iterate over the specified resolution
            for (int i = 0; i < resolution; i++)
            {
                a = center + rotation * forwardRadius; // Calculate the position of point A in world space
                rotation *= Quaternion.Euler(deltaAngle, 0, 0); // Rotate the direction for the next iteration
                b = center + rotation * forwardRadius; // Calculate the position of point B in world space
                ab = b - a; // Update the vector AB for the current iteration

                // Perform a raycast from point A towards AB to check for collisions
                if (Physics.Raycast(a, ab, out hit, abMagnitude, layer))
                {
                    if (drawGizmos)
                        Gizmos.DrawLine(a, hit.point); // Draw a line from A to the hit point if visual debugging is enabled

                    return true; // Return true if a collision is detected
                }

                if (drawGizmos)
                    Gizmos.DrawLine(a, b); // Draw a line from A to B for visual debugging
            }

            hit = new RaycastHit(); // Reset the hit information if no collision is detected
            return false; // Return false if no collisions are found within the specified resolution
        }
    }
}
