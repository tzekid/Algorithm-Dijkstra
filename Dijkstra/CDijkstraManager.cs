using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Dijkstra
{
    class CDijkstraManager
    {
        private ArrayList verbindungen;
        private ArrayList knoten;
        private ArrayList pflichtKnoten;

        public CDijkstraManager(ArrayList verbindungen, ArrayList knoten, ArrayList pflichtKnoten)
        {
            this.verbindungen = verbindungen;
            this.knoten = knoten;
            this.pflichtKnoten = pflichtKnoten;
        }

        public string Solve()
        {
            string rtn = "";
            for (int i = 0; i <= 2; i++)
            {
                CDijkstra tmp = new CDijkstra(knoten, verbindungen);
                rtn += tmp.Solve();
            }
            return rtn;       
        }
    }
}
