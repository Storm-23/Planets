using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using PlanetsCore;

namespace PlanetsEditor.Controls
{
    /// <summary>
    /// Editor of object's properties
    /// </summary>
    public partial class PropertiesPanel : UserControl
    {
        /// <summary>
        /// Object was changed
        /// </summary>
        public event Action Changed = delegate { };

        #region Private fields

        private OrbitalBodyDescription desc;
        int updating;

        #endregion

        public PropertiesPanel()
        {
            InitializeComponent();


            //init comboboxes
            updating++;

            cbOrbitDirection.DataSource = Enum.GetValues(typeof(OrbitDirection));
            cbSurfaceType.DataSource = Enum.GetValues(typeof(SurfaceType));

            updating--;
        }

        public void Build(OrbitalBodyDescription desc)
        {
            this.desc = desc;

            //copy properties to controls
            updating++;

            tbName.Text = desc.Name;
            nudMass.Value = (decimal)(desc.Mass / Constants.EARTH_MASS);
            nudOrbitRadius.Value = (decimal)(desc.OrbitRadius / Constants.AU);
            nudOrbitalSpeed.Value = (decimal)desc.OrbitalSpeed / 1000;
            cbOrbitDirection.SelectedItem = desc.OrbitDirection;
            nudRadius.Value = (decimal) (desc.Radius / Constants.EARTH_RADIUS);
            nudRandomSeed.Value = desc.RandomSeed;
            cbSurfaceType.SelectedItem = desc.SurfaceType;
            nudStartOrbitAngle.Value = (decimal)(desc.StartOrbitAngle * 180 / Math.PI);

            updating--;
        }

        private void UpdateObject()
        {
            if (updating > 0) return;//we are in updating already

            //copy controls to properties
            desc.Name = tbName.Text;
            desc.Mass = (double)nudMass.Value * Constants.EARTH_MASS;
            desc.OrbitRadius = (double)nudOrbitRadius.Value * Constants.AU;
            desc.OrbitalSpeed = (double)nudOrbitalSpeed.Value * 1000;
            desc.OrbitDirection = (OrbitDirection)cbOrbitDirection.SelectedItem;
            desc.Radius = (double)nudRadius.Value * Constants.EARTH_RADIUS;
            desc.RandomSeed = (int)nudRandomSeed.Value;
            desc.SurfaceType = (SurfaceType)cbSurfaceType.SelectedItem;
            desc.StartOrbitAngle = (double) nudStartOrbitAngle.Value * Math.PI / 180;

            //fire event Changed
            Changed();
        }

        private void tbName_TextChanged(object sender, EventArgs e)
        {
            UpdateObject();
        }
    }
}
