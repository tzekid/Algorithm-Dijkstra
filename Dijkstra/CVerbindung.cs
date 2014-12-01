using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class CVerbindung
    {
        CKnoten start;
        CKnoten stopp;
        int wert;
        public CVerbindung(CKnoten start, CKnoten stopp, int wert)
        {
            this.start = start;
            this.stopp = stopp;
            this.wert = wert;
        }
        public CKnoten GetStart()
        {
            return start;
        }
        public CKnoten GetStopp()
        {
            return stopp;
        }
        public int GetWert()
        {
            return wert;
        }
    }
}
