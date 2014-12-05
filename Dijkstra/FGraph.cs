using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace Dijkstra
{
    public partial class FGraph : Form
    {
        private ArrayList knoten;
        private ArrayList verbindungen;
        public FGraph(ArrayList knoten, ArrayList verbindungen) : base()
        {
            InitializeComponent();
            this.knoten = knoten;
            this.verbindungen = verbindungen;
        }

        private void FGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            grfx.Clear(System.Drawing.SystemColors.Control);

            //Punte zeichnen 
            int x = 20;
            int y = 90;
            bool oben = true;
            foreach(CKnote knt in knoten)
            {
                SolidBrush brush = new SolidBrush(Color.Green);
                if (oben)
                {
                    y = y - 70;
                    oben = false;
                }
                else
                {
                    y += 70;
                    oben = true;
                }
                knt.SetX(x);
                knt.SetY(y);
                foreach(CVerbindung ver in verbindungen)
                {
                    if (ver.GetStart() == knt)
                    {
                        ver.GetStart().SetX(x);
                        ver.GetStart().SetY(y);
                    }
                    if (ver.GetStopp() == knt)
                    {
                        ver.GetStopp().SetX(x);
                        ver.GetStopp().SetY(y);
                    }
                }
                grfx.FillRectangle(brush, x - 5, y - 5, 10, 10);
                x += 80;
                Label mylabel = new Label();
                mylabel.Name = knt.GetName() + "label";
                mylabel.Text = knt.GetName();
                mylabel.Size = new System.Drawing.Size(10, 10);
                mylabel.Location = new Point(knt.GetX(), knt.GetY());
                this.Controls.Add(mylabel);
            }

            //Verbindungen Zeichnen
            foreach (CVerbindung ver in verbindungen)
            {
                Pen pen = new Pen(Color.Red);
                grfx.DrawLine(pen, new Point(ver.GetStart().GetX(), ver.GetStart().GetY()), new Point(ver.GetStopp().GetX(), ver.GetStopp().GetY()));

                /*Label mylabel = new Label();
                mylabel.Text = ver.GetWert().ToString();
                mylabel.Size = new System.Drawing.Size(10, 10);
                if (ver.GetStopp().GetY() > ver.GetStart().GetY() && ver.GetStopp().GetX() > ver.GetStopp().GetX()) mylabel.Location = new Point(ver.GetStopp().GetX() - ver.GetStart().GetX(), ver.GetStopp().GetY() - ver.GetStart().GetY());
                //mylabel.Location = new Point(ver.GetStopp().GetX() - ver.GetStart().GetX(), ver.GetStart().GetY() - ver.GetStopp().GetY());
                this.Controls.Add(mylabel);*/
            }
        }

        private void FGraph_Load(object sender, EventArgs e)
        {

        }
    }
}
