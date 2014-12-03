using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dijkstra
{
    class CKnoten
    {
        string name;
        string state;
        int wegWert;
        public CKnoten(string name, string state)
        {
            this.name = name;
            this.state = state;
this.wegWert = 0;
        }
        public string GetName()
        {
            return name;
        }
        public string GetState()
        {
            return state;
        }
        public void SetState(string state)
        {
            this.state = state;
        }
        public int GetWegWert()
        {
            return wegWert;
        }
        public void SetWegWert(int value)
        {
            wegWert = value;
        }
    }
}
