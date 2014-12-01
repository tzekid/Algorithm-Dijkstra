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
        public CKnoten(string name, string state)
        {
            this.name = name;
            this.state = state;
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
    }
}
