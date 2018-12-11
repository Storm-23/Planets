using System;
using System.Collections;
using System.Collections.Generic;

namespace PlanetsCore
{
    /// <summary>
    /// Description of object with stable orbital trajectory
    /// </summary>
    [Serializable]
    public class OrbitalBodyDescription : List<OrbitalBodyDescription>
    {
        /// <summary>
        /// Unique identifier
        /// </summary>
        public Guid Guid { get; internal set; }

        /// <summary>
        /// Name of body
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Mass of body (kg)
        /// </summary>
        public double Mass { get; set; }

        /// <summary>
        /// Parent body.
        /// We suppose that we are rotate around centre of parent body.
        /// If not - Parent can be null.
        /// Note, that Parent node can be just centre of mass and it can be virtual point, w/o physical body.
        /// </summary>
        public OrbitalBodyDescription Parent { get; set; }

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
        /// Position of object on orbit at zero time
        /// </summary>
        public double StartOrbitAngle { get; set; }

        /// <summary>
        /// Radius of body (m)
        /// </summary>
        public double Radius { get; set; }

        /// <summary>
        /// Surface Type
        /// </summary>
        public SurfaceType SurfaceType { get; set; }

        /// <summary>
        /// Random seed for procedural surface builder
        /// </summary>
        public int RandomSeed { get; set; }

        /// <summary>
        /// Map of orbits (serialized PNG image)
        /// </summary>
        public byte[] Map { get; set; }

        /// <summary>
        /// Scale of Map
        /// </summary>
        public double MapScale { get; set; }

        #region Private fields

        private static Random rnd = new Random();

        #endregion

        public OrbitalBodyDescription()
        {
            Guid = Guid.NewGuid();
            Mass = 1 * Constants.EARTH_MASS;
            OrbitRadius = 1 * Constants.AU;
            OrbitalSpeed = 1 * Constants.EARTH_ORBITAL_SPEED;
            Radius = 1 * Constants.EARTH_RADIUS;
            SurfaceType = SurfaceType.RockPlanet;
            RandomSeed = rnd.Next(1000000);
        }
        
        public override string ToString()
        {
            return Name;
        }

        public IEnumerable<OrbitalBodyDescription> GetMeAndChildren()
        {
            yield return this;

            foreach(var child in this)
            foreach(var c in child.GetMeAndChildren())
                yield return c;
        }
    }

    /// <summary>
    /// Surface type
    /// </summary>
    [Serializable]
    public enum SurfaceType
    {
        None,
        RockPlanet,
        GasPlanet,
        Star
    }

    /// <summary>
    /// Direction of moving along orbit
    /// </summary>
    [Serializable]
    public enum OrbitDirection
    {
        CW, //clockwise
        CCW //counterclockwise
    }
}