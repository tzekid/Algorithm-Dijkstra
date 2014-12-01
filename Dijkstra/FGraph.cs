using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;

namespace Dijkstra
{ 
    public partial class FGraph : Form
    {
        private ArrayList knoten = new ArrayList();
        private ArrayList verbindungen = new ArrayList();

        public FGraph(ArrayList knoten, ArrayList verbindungen)
        {
            InitializeComponent();
            this.knoten = knoten;
            this.verbindungen = verbindungen;
            int x = 0;
            int y = 0;
            foreach (CKnoten knote in knoten)
            {
                Console.WriteLine(knote.GetName());
                Label lbn = new Label();
                lbn.Text = knote.GetName();
                lbn.Location = new Point(x, y);
                this.Controls.Add(lbn);

                x += 50;
                y += 50;
            }
        }

        private void FGraph_Load(object sender, EventArgs e)
        {

        }
    }
}
