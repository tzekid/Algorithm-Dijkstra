using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;

namespace Dijkstra
{
    public partial class FGraph : Form
    { 
        private ArrayList knoten;
        private ArrayList verbindungen;
        private ArrayList weg;
        public FGraph(ArrayList knoten, ArrayList verbindungen, ArrayList weg) : base()
        {
            InitializeComponent();
            this.knoten = knoten;
            this.verbindungen = verbindungen;
            this.weg = weg;
        }

        private void FGraph_Paint(object sender, PaintEventArgs e)
        {
            Graphics grfx = e.Graphics;
            grfx.Clear(System.Drawing.SystemColors.Control);

            //Punte zeichnen 
            int x = 72;
            int y = 90;
            bool oben = true;
            foreach(CKnote knt in knoten)
            {
                SolidBrush brush = new SolidBrush(Color.Green);
                if (oben)
                {
                    y = y - 69;
                    oben = false;
                }
                else
                {
                    y += 75;
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
                grfx.FillRectangle(brush, x - 9, y - 9, 15, 15);
                x += 81;
                Label mylabel = new Label();
                mylabel.Name = knt.GetName() + "label";
                mylabel.Text = knt.GetName();
                mylabel.Size = new System.Drawing.Size(9, 9);
                mylabel.Location = new Point(knt.GetX(), knt.GetY());
                this.Controls.Add(mylabel);
            }

            //Verbindungen Zeichnen
            foreach (CVerbindung ver in verbindungen)
            {
                Pen pen = new Pen(Color.Red);
                grfx.DrawLine(pen, new Point(ver.GetStart().GetX(), ver.GetStart().GetY()), new Point(ver.GetStopp().GetX(), ver.GetStopp().GetY()));
            }
            this.Left = 1000;
            foreach (CVerbindung ver in weg)
            {
                Pen pen = new Pen(Color.Blue);
                grfx.DrawLine(pen, new Point(ver.GetStart().GetX(), ver.GetStart().GetY()), new Point(ver.GetStopp().GetX(), ver.GetStopp().GetY()));
            }
        }
    }
}
