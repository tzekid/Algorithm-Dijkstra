﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace Dijkstra
{
    class CDijkstra
    {
        private ArrayList knoten = new ArrayList();
        private ArrayList verbindungen = new ArrayList();


        public CDijkstra(ArrayList knoten, ArrayList verbindungen)
        {
            this.knoten = knoten;
            this.verbindungen = verbindungen;
        }
        public void Solve() //Löst das Problem
        {
            CKnoten startKnoten = ErmittleStartKnoten(verbindungen, new ArrayList(knoten));
            CKnoten stoppKnoten = ErmittleEndKnoten(verbindungen, new ArrayList(knoten));
           // Console.WriteLine("Startpunkt: " + startKnoten.GetName());
            //Console.WriteLine("Stopppunkt: " + stoppKnoten.GetName());

            SetState(startKnoten, "aktiv");
            while(stoppKnoten.GetState() == "offen")
            {
                Console.WriteLine("=================================================================================");
                for (int i = 0; i <= knoten.Count - 1; i++)
                {
                    Console.WriteLine("----------------------------------------------------------------------------------");
                    foreach(CKnoten aktiverKnote in ErmittleAktivePunkte())
                    {
                        CVerbindung tmpVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote);
                        if (tmpVerbindung != null)
                        {
                            Console.WriteLine("Verbindung: " + tmpVerbindung.GetStart().GetName() + " to " + tmpVerbindung.GetStopp().GetName() + ", " + tmpVerbindung.GetWert());
                            SetState(tmpVerbindung.GetStopp(), "aktiv");
                            RemoveVerbindung(tmpVerbindung);
                        }
                        
                    }
                    Console.WriteLine("----------------------------------------------------------------------------------");
                }
                Console.WriteLine("=================================================================================");
            }
        }
        //Zustande:
        //  - offen (nicht erfasst)
        //  - aktiv (ist besucht)
        //  - geschlossen (erfasst; für die weitere Verwendung nicht relevant)
        private CVerbindung ErmittleKürzesteVerbindung(ArrayList verbindungen, CKnoten aktiverKnote)
        {
            CVerbindung tmpVerbindung = null;
            foreach(CKnoten offenerKnoten in ErmittleOffenePunkte(aktiverKnote))
            {
                foreach(CVerbindung verbindung in verbindungen)
                {
                    if(verbindung.GetStart() == aktiverKnote && verbindung.GetStopp() == offenerKnoten)
                    {
                        if(tmpVerbindung == null)
                        {
                            tmpVerbindung = verbindung;
                        }
                        else if(verbindung.GetWert() < tmpVerbindung.GetWert())
                        {
                            tmpVerbindung = verbindung;
                        }
                        Console.WriteLine("Verbindung: " + verbindung.GetStart().GetName() + " to " + verbindung.GetStopp().GetName());
                    }
                }
            }
            return tmpVerbindung;
        }
        
        private void RemoveVerbindung(CVerbindung verbindung)
        {
            for (int i = 0; i < verbindungen.Count; i++)
            {
                CVerbindung tmpVerbindung = (CVerbindung)verbindungen[i];
                if (tmpVerbindung.GetStart() == verbindung.GetStart() && tmpVerbindung.GetStopp() == verbindung.GetStopp() && tmpVerbindung.GetWert() == verbindung.GetWert())
                {
                    Console.WriteLine("Lösche verbindung: " + verbindung.GetStart().GetName() + " to " + verbindung.GetStopp().GetName());
                    verbindungen.RemoveAt(i);

                }
            }
        }
        private void SetState(CKnoten knote, string state) //State eines Knoten ändern
        {
           // Console.WriteLine("Knoten: " + knote.GetName());
            foreach(CKnoten knt in knoten)
            {
                if (knt == knote)
                {
                    knt.SetState(state);
                }
            }
            foreach(CVerbindung verb in verbindungen)
            {
                if (verb.GetStart() == knote) verb.GetStart().SetState(state);
                if (verb.GetStopp() == knote) verb.GetStopp().SetState(state);
            }
        }
        private CKnoten ErmittleStartKnoten(ArrayList verbindungen, ArrayList knoten) //Ermittelt Startknoten
        {
            foreach(CVerbindung verbindung in verbindungen)
            {
                for (int i = 0; i <= knoten.Count - 1; i++)
                {
                    if (verbindung.GetStopp() == (CKnoten)knoten[i]) knoten.RemoveAt(i); 
                }
            }
            return (CKnoten)knoten[knoten.Count - 1];
        }
        private CKnoten ErmittleEndKnoten(ArrayList verbindungen, ArrayList knoten) //Ermittlet Endknoten
        {
            foreach (CVerbindung verbindung in verbindungen)
            {
                for (int i = 0; i <= knoten.Count - 1; i++)
                {
                    if (verbindung.GetStart() == (CKnoten)knoten[i]) knoten.RemoveAt(i);
                }
            }
            return (CKnoten)knoten[knoten.Count - 1];
        }
        private ArrayList ErmittleAktivePunkte() //Aktive Punkte als ArrayList
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnoten knote in knoten)
            {
                if (knote.GetState() == "aktiv")
                {
                    rtn.Add(knote);
                }
            }
            return rtn;
        }
        private ArrayList ErmittleOffenePunkte(CKnoten aktivenPunkt) //Offene Punkte als ArrayList
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnoten knote in knoten)
            {
                if (knote.GetState() == "offen")
                {
                    foreach(CVerbindung verbindung in verbindungen)
                    {
                        if(verbindung.GetStart() == aktivenPunkt)
                        {
                            if (verbindung.GetStopp() == knote)
                            {
                                rtn.Add(knote);
                            }
                        }
                    }
                }
            }
            return rtn;
        }
    }
}
