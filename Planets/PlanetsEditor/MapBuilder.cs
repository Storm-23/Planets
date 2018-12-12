using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using PlanetsCore;

namespace PlanetsEditor
{
    /// <summary>
    /// Builds map (png image) of orbits
    /// </summary>
    class MapBuilder
    {
        /// <summary>
        /// Build map for the root
        /// </summary>
        public void Build(OrbitalBodyDescription root)
        {
            const int SIZE = 1001;

            using (var img = new Bitmap(SIZE, SIZE))
            using (var gr = Graphics.FromImage(img))
            {
                gr.SmoothingMode = SmoothingMode.HighQuality;

                //max orbit
                var max = root.GetMeAndChildren().Select(b => b.OrbitRadius).Max();

                //scale
                var scale = 0.5 * (SIZE - 5) / max;

                //draw orbits
                DrawOrbit(gr, scale, root);

                //convert to png
                using (var ms = new MemoryStream())
                {
                    img.Save(ms, ImageFormat.Png);

                    //save to root body
                    root.Map = ms.ToArray();
                    root.MapScale = scale;
                }
            }
        }

        private void DrawOrbit(Graphics gr, double scale, OrbitalBodyDescription parent)
        {
            if (parent.OrbitRadius > float.Epsilon &&
                parent.Mass > Constants.EARTH_MASS / 50)
            {
                //draw my orbit
                var cx = (int)(gr.VisibleClipBounds.Width / 2);
                var cy = (int)(gr.VisibleClipBounds.Height / 2);
                var r = (int)(parent.OrbitRadius * scale);
                gr.DrawEllipse(Pens.White, cx - r, cy - r, r * 2, r * 2);
            }
            else
            {
                //draw children
                foreach (var child in parent)
                    DrawOrbit(gr, scale, child);
            }
        }
    }
}
