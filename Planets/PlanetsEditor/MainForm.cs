using System;
using System.Windows.Forms;
using PlanetsCore;

namespace PlanetsEditor
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void UpdateInterface()
        {
            //upate interface
            var selectedBody = tvSolarSystem.SelectedNode as OrbitalBodyDescription;
            if (selectedBody == null)
            {
                pnProperties.Hide();
                btAdd.Enabled = btRemove.Enabled = false;
                return;
            }

            btAdd.Enabled = true;
            btRemove.Enabled = selectedBody.Parent != null;

            pnProperties.Build(selectedBody);
            pnProperties.Show();

            tvSolarSystem.Rebuild();
        }

        private void tvSolarSystem_NodeSelectedStateChanged(object sender, FastTreeNS.NodeSelectedStateChangedEventArgs e)
        {
            if(e.Selected)
                UpdateInterface();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            //add body to model
            var parent = (tvSolarSystem.SelectedNode as OrbitalBodyDescription);
            var newBody = AddBody(parent);
            tvSolarSystem.Rebuild();
            tvSolarSystem.ExpandNode(parent);
            tvSolarSystem.SelectNode(newBody);
            OnDocumentChanged();
        }

        private OrbitalBodyDescription AddBody(OrbitalBodyDescription parent)
        {
            //create new body
            var newBody = new OrbitalBodyDescription { Name = "Planet " + DateTime.Now.Millisecond };
            parent.Add(newBody);
            newBody.Parent = parent;

            return newBody;
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            //remove body
            var body = (tvSolarSystem.SelectedNode as OrbitalBodyDescription);
            body.Parent.Remove(body);
            tvSolarSystem.Rebuild();
            OnDocumentChanged();
            UpdateInterface();
        }

        private void fm_DocOpenedOrCreated(object sender, EventArgs e)
        {
            //refresh interface for new model
            tvSolarSystem.Build(fm.Document);
            tvSolarSystem.ExpandAll();
            UpdateInterface();
        }

        private void fm_NewDocNeeded(object sender, DocEventArgs e)
        {
            //create new model
            e.Document = new OrbitalBodyDescription()
            {
                Name = "Center of System",
                Mass = 0,
                OrbitalSpeed = 0,
                OrbitRadius = 0,
                Radius = 0,
                SurfaceType = SurfaceType.None
            };
        }

        private void pnProperties_Changed()
        {
            OnDocumentChanged();
        }

        private void OnDocumentChanged()
        {
            fm.IsDocumentChanged = true;
            UpdateInterface();
        }

        private void tvSolarSystem_NodeIconNeeded(object sender, FastTreeNS.ImageNodeEventArgs e)
        {
            var body = e.Node as OrbitalBodyDescription;
            switch (body.SurfaceType)
            {
                case SurfaceType.Star: e.Result = Properties.Resources.sun; break;
                case SurfaceType.RockPlanet:
                case SurfaceType.GasPlanet: e.Result = Properties.Resources.globe_21; break;
                default: e.Result = Properties.Resources.target; break;
            }
        }

        private void fm_Saving(object sender, DocEventArgs e)
        {
            //befiore saving - build map of orbits
            new MapBuilder().Build(fm.Document as OrbitalBodyDescription);
        }
    }
}
