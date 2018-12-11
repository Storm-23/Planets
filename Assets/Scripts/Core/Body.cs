using UnityEngine;

namespace PlanetsCore
{
    /// <summary>
    /// Celestial object
    /// </summary>
    public abstract class Body
    {
        /// <summary>
        /// Name of body
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mass of body (kg)
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Radius of body (m)
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Current position (real world coordinates)
        /// </summary>
        public Vector3 Position { get; protected set; }

        /// <summary>
        /// Current rotation
        /// </summary>
        public Quaternion Rotation { get; protected set; }

        /// <summary>
        /// Update position and rotation for the time
        /// </summary>
        public abstract void Update(double time);

        /// <summary>
        /// Get position for the time (real world coordinates)
        /// </summary>
        public abstract Vector3 GetPosition(double time);

        public Body()
        {
            Rotation = Quaternion.identity;
        }
    }
}