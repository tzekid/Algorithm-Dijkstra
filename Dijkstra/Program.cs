using System;
using System.Collections;

namespace Dijkstra
{
    class Program
    {
        private static void Main()
        {
            ArrayList knoten = new ArrayList();
            knoten.Add(new CKnote("S", "offen"));//0
            knoten.Add(new CKnote("A", "offen"));//1
            knoten.Add(new CKnote("B", "offen"));//2
            knoten.Add(new CKnote("C", "offen"));//3
            knoten.Add(new CKnote("D", "offen"));//4
            knoten.Add(new CKnote("E", "offen"));//5
            knoten.Add(new CKnote("T", "offen"));//6

            ArrayList verbindungen = new ArrayList();
            //S
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
            //Z--->Keine weiteren Verbindungen


            CDijkstra graph = new CDijkstra(knoten, verbindungen);
            Console.WriteLine(graph.Solve());


            Console.ReadKey();
        }
    }
}
