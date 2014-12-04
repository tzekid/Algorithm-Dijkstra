﻿using System;
using System.Collections;

namespace Dijkstra
{
    class Program
    {
        private static void Main()
        {
            Console.SetWindowSize(Console.WindowWidth * 2, Console.WindowHeight * 2);
    
            Console.WriteLine("Dijkstra-Algorythmus - Version 1.0.1 - Coded in Frankfurt with love");
            Console.WriteLine("_______________________________________________________________________________");
            
            ArrayList knoten = new ArrayList();
            knoten.Add(new CKnote("S", "offen"));//0
            knoten.Add(new CKnote("A", "offen"));//1
            knoten.Add(new CKnote("B", "offen"));//2
            knoten.Add(new CKnote("C", "offen"));//3
            knoten.Add(new CKnote("D", "offen"));//4
            knoten.Add(new CKnote("E", "offen"));//5
            knoten.Add(new CKnote("T", "offen"));//6

            ArrayList verbindungen = new ArrayList();
            //S (Start)
            verbindungen.Add(new CVerbindung((CKnote)knoten[0], (CKnote)knoten[1], 4)); //S to A
            verbindungen.Add(new CVerbindung((CKnote)knoten[0], (CKnote)knoten[2], 6)); //S to B
            verbindungen.Add(new CVerbindung((CKnote)knoten[0], (CKnote)knoten[3], 5)); //S to C
            //A
            verbindungen.Add(new CVerbindung((CKnote)knoten[1], (CKnote)knoten[2], 1)); //A to B
            verbindungen.Add(new CVerbindung((CKnote)knoten[1], (CKnote)knoten[4], 7)); //A to D
            //B
            verbindungen.Add(new CVerbindung((CKnote)knoten[2], (CKnote)knoten[4], 5)); //B to D
            verbindungen.Add(new CVerbindung((CKnote)knoten[2], (CKnote)knoten[5], 4)); //B to E
            verbindungen.Add(new CVerbindung((CKnote)knoten[2], (CKnote)knoten[3], 2)); //B to C
            //C
            verbindungen.Add(new CVerbindung((CKnote)knoten[3], (CKnote)knoten[5], 5)); //C to E
            //D
            verbindungen.Add(new CVerbindung((CKnote)knoten[4], (CKnote)knoten[5], 1)); //D to E
            verbindungen.Add(new CVerbindung((CKnote)knoten[4], (CKnote)knoten[6], 6)); //D to T
            //E
            verbindungen.Add(new CVerbindung((CKnote)knoten[5], (CKnote)knoten[6], 8)); //E to T
            //T (Ziel)

            Console.WriteLine("\nAlle Knoten:");
            foreach(CKnote knt in knoten)
                Console.WriteLine("  " + knt.GetName());
            Console.WriteLine("\nAlle Verbindungen:");
            foreach(CVerbindung verbindung in verbindungen)
                Console.WriteLine("  " + verbindung.GetStart().GetName() + " zu " + verbindung.GetStopp().GetName());
            CDijkstra graph = new CDijkstra(new ArrayList(knoten), new ArrayList(verbindungen));
            Console.WriteLine("\nStart: \n  " + graph.ErmittleStartKnoten(new ArrayList(verbindungen), new ArrayList(knoten)).GetName());
            Console.WriteLine("\nStopp: \n  " + graph.ErmittleEndKnoten(new ArrayList(verbindungen), new ArrayList(knoten)).GetName() + "\n");
            
            graph.Solve();
            
            Console.WriteLine("Kürzester Weg: \n  " + graph.GetWeg());
            Console.WriteLine("\nLänge des Weges: \n  " + graph.GetWegLänge());
            Console.WriteLine("\n\n  (Press any key to exit...)");
            Console.ReadKey();
        }
    }
}



//Pen pen = new Pen(Color.FromArgb(255, 0, 0, 0));
//e.Graphics.DrawLine(pen, 20, 10, 300, 100);
/*int x = 0;

foreach(CKnote knt in knoten)
{
    Label lbn = new Label();
    lbn.Location = new Point(x, 50);
    lbn.Text = knt.GetName();
    this.Controls.Add(lbn);
    x += 20;
}*/
