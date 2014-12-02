using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace Dijkstra
{
    class Program
    {
        private static ArrayList verbindungen = new ArrayList();
        private static ArrayList knoten = new ArrayList();
        private static CDijkstra rechnung;

        private static void Main()
        { 
            knoten.Add(new CKnoten("S", "offen"));//0
            knoten.Add(new CKnoten("A", "offen"));//1
            knoten.Add(new CKnoten("B", "offen"));//2
            knoten.Add(new CKnoten("C", "offen"));//3
            knoten.Add(new CKnoten("D", "offen"));//4
            knoten.Add(new CKnoten("E", "offen"));//5
            knoten.Add(new CKnoten("T", "offen"));//6 

            //S
            verbindungen.Add(new CVerbindung((CKnoten)knoten[0], (CKnoten)knoten[1], 4)); //S to A
            verbindungen.Add(new CVerbindung((CKnoten)knoten[0], (CKnoten)knoten[2], 6)); //S to B
            verbindungen.Add(new CVerbindung((CKnoten)knoten[0], (CKnoten)knoten[3], 5)); //S to C
            //A
            verbindungen.Add(new CVerbindung((CKnoten)knoten[1], (CKnoten)knoten[2], 1)); //A to B
            verbindungen.Add(new CVerbindung((CKnoten)knoten[1], (CKnoten)knoten[4], 7)); //A to D
            //B
            verbindungen.Add(new CVerbindung((CKnoten)knoten[2], (CKnoten)knoten[4], 5)); //B to D
            verbindungen.Add(new CVerbindung((CKnoten)knoten[2], (CKnoten)knoten[5], 4)); //B to E
            verbindungen.Add(new CVerbindung((CKnoten)knoten[2], (CKnoten)knoten[3], 2)); //B to C
            //C
            verbindungen.Add(new CVerbindung((CKnoten)knoten[3], (CKnoten)knoten[5], 5)); //C to E
            //D
            verbindungen.Add(new CVerbindung((CKnoten)knoten[4], (CKnoten)knoten[5], 1)); //D to E
            verbindungen.Add(new CVerbindung((CKnoten)knoten[4], (CKnoten)knoten[1], 6)); //D to T
            //E
            verbindungen.Add(new CVerbindung((CKnoten)knoten[5], (CKnoten)knoten[6], 8)); //E to T
            //T--->Keine weiteren Verbindungen

            rechnung = new CDijkstra(knoten, verbindungen);
            rechnung.Solve();
   
            Console.ReadKey();
        }
    }
}
