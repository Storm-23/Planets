using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PlanetsCore
{
    /// <summary>
    /// Contastans to calc metrics relative to Earth
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Astro unit (distance from Earth to Sun) (m)
        /// </summary>
        public const double AU = 149598000000;

        /// <summary>
        /// Orbital speed of Earth (m/s)
        /// </summary>
        public const double EARTH_ORBITAL_SPEED = 29780;

        /// <summary>
        /// Mass of Earth (kg)
        /// </summary>
        public const double EARTH_MASS = 5.9726E24;

        /// <summary>
        /// Radius of Earth (m)
        /// </summary>
        public const double EARTH_RADIUS = 6371000;

        /// <summary>
        /// Gravitational constant
        /// </summary>
        public const double G = 6.67408E-11;
    }
}
