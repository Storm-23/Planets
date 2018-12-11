using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetsCore
{
    /// <summary>
    /// Model of solar system
    /// </summary>
    public class Model
    {
        /// <summary>
        /// Objects of system
        /// </summary>
        public List<Body> Bodies { get; private set; }

        /// <summary>
        /// Map
        /// </summary>
        public Texture2D Map { get; set; }

        /// <summary>
        /// Map scale
        /// </summary>
        public double MapScale { get; set; }

        public Model()
        {
            Bodies = new List<Body>();
        }

        public void Update(double modelTime)
        {
            foreach (var body in Bodies)
                body.Update(modelTime);
        }
    }
}
