using System;
using System.Collections;
using UnityEngine;

namespace PlanetsCore
{
    /// <summary>
    /// Orbital celestial object
    /// The Body have stable circle orbit around other body
    /// </summary>
    public class OrbitalBody : Body
    {
        /// <summary>
        /// Parent body.
        /// We suppose that we are rotate around centre of parent body.
        /// If not - Parent can be null.
        /// Note, that Parent node can be just centre of mass and it can be virtual point, w/o physical body.
        /// </summary>
        public Body Parent { get; set; }

        /// <summary>
        /// Radius of orbit around Parent body (m).
        /// We suppose that orbit is stable and it is ideally circle (w/o eccentricity).
        /// </summary>
        public double OrbitRadius { get; set; }

        /// <summary>
        /// Orbital speed (m/s)
        /// It is linear speed along trajectory.
        /// Because orbit is ideally circle, we believe that speed is constant.
        /// </summary>
        public double OrbitalSpeed { get; set; }

        /// <summary>
        /// Direction of moving around parent
        /// </summary>
        public OrbitDirection OrbitDirection { get; set; }

        /// <summary>
        /// Start angle on orbit (rad)
        /// </summary>
        public double StartOrbitAngle { get; set; }

        /// <summary>
        /// Update position and rotation for the time
        /// </summary>
        public override void Update(double time)
        {
            //calc position
            Position = GetPosition(time);

            //calc rotation
            var rotSpeed = -10f;
            Rotation *= Quaternion.Euler(0, rotSpeed * Time.deltaTime, 0) ;
        }

        /// <summary>
        /// Get position for the time (real world coordinates)
        /// </summary>
        public override Vector3 GetPosition(double time)
        {
            var res = Vector3.zero;

            if (OrbitRadius > 0)
            {
                var angularSpeed = OrbitalSpeed / OrbitRadius;

                if (OrbitDirection == OrbitDirection.CW)
                    angularSpeed = -angularSpeed;

                var angle = time * angularSpeed + StartOrbitAngle;

                var x = Math.Cos(angle) * OrbitRadius;
                var z = Math.Sin(angle) * OrbitRadius;
                res = new Vector3((float)x, 0, (float)z);

                if (Parent != null)
                    res += Parent.GetPosition(time);
            }

            return res;
        }
    }

}