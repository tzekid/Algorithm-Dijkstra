using System;
using System.Collections;

namespace Dijkstra
{
    class CDijkstra
    {
        private ArrayList knoten;
        private ArrayList verbindungen;
        private string kürzesterWeg = "Fehler: Bitte erst Methode Solve() aufrufen!";
        private int längeWeg = -1;

        public CDijkstra(ArrayList knoten, ArrayList verbindungen)
        {
            this.knoten = knoten;
            this.verbindungen = verbindungen;
        }
        public void Solve()
        {
            CKnote startKnoten = ErmittleStartKnoten(verbindungen, new ArrayList(knoten));
            CKnote stoppKnoten = ErmittleEndKnoten(verbindungen, new ArrayList(knoten));
            SetState(startKnoten, "aktiv");
            ArrayList rtnWeg = new ArrayList();

            while (stoppKnoten.GetState() == "offen") 
            {
                CVerbindung verbindung = ErmittleKürzesteVerbindungUnterallenAktivenVerbindungen();
                RemoveVerbindung(verbindung);
                foreach (CKnote knt in ErmittleOffeneKnoten(verbindung.GetStopp()))
                {
                    CVerbindung tmpVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), knt);
                    if (tmpVerbindung != null && tmpVerbindung.GetStopp() != stoppKnoten) RemoveVerbindung(tmpVerbindung);
                }
                
                if (verbindung != null)
                { 
                    int summe = verbindung.GetWert() + verbindung.GetStart().GetKnotenWert();
                    SetKnotenWert(verbindung.GetStopp(), summe);
                    SetState(verbindung.GetStopp(), "aktiv");
                    rtnWeg.Add(verbindung);
                }
            }
            kürzesterWeg = ErmittleEndWeg(rtnWeg);
        }
        public string GetWeg()
        {
            return kürzesterWeg;
        }
        public int GetWegLänge()
        {
            return längeWeg;
        }
        private string ErmittleEndWeg(ArrayList rtnWeg)
        {
            CVerbindung stoppVer = (CVerbindung)rtnWeg[rtnWeg.Count - 1];
            string rtn = stoppVer.GetStopp().GetName();
            CVerbindung last = stoppVer;
            for (int i = 0; i < rtnWeg.Count; i++)
            {
                foreach(CVerbindung tmpVer in rtnWeg)
                {
                    if (tmpVer.GetStopp() == last.GetStart())
                    {
                        rtn += tmpVer.GetStopp().GetName();
                        last = tmpVer;
                        break;
                    }
                }
            }
            CVerbindung startVer = (CVerbindung)rtnWeg[0];
            rtn += startVer.GetStart().GetName();
            string rtnFinal = "";
            for (int i = rtn.Length - 1; i > -1; i--)
            {
                rtnFinal += rtn[i] + " "; //if (i != 0) rtnFinal += "=>";
            }
            längeWeg = stoppVer.GetStopp().GetKnotenWert();
            return rtnFinal;
        }
        private CVerbindung ErmittleKürzesteVerbindungUnterallenAktivenVerbindungen()
        {
            CVerbindung kürzesteVerbindungZuEinemOffenenPunkt = null;
            foreach (CKnote aktiverKnote in ErmittleAktivePunkte())
            {
                if (ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote) == null) SetState(aktiverKnote, "geschlossen");
                else
                {
                    CVerbindung aktVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote);
                    if (kürzesteVerbindungZuEinemOffenenPunkt == null || (aktVerbindung.GetWert() + aktVerbindung.GetStart().GetKnotenWert()) < (kürzesteVerbindungZuEinemOffenenPunkt.GetWert() + kürzesteVerbindungZuEinemOffenenPunkt.GetStart().GetKnotenWert()))
                        kürzesteVerbindungZuEinemOffenenPunkt = aktVerbindung;
                }
            }
            return kürzesteVerbindungZuEinemOffenenPunkt;
        }
        private CVerbindung ErmittleKürzesteVerbindung(ArrayList verbindungen, CKnote aktiverKnote)
        {
            CVerbindung tmpVerbindung = null;
            foreach (CVerbindung verbindung in verbindungen)
                foreach(CKnote knote in ErmittleOffeneKnoten(aktiverKnote))
                    if(verbindung.GetStart().GetName() == aktiverKnote.GetName() && verbindung.GetStopp().GetName() == verbindung.GetStopp().GetName())
                        if (tmpVerbindung == null || verbindung.GetWert() < tmpVerbindung.GetWert()) tmpVerbindung = verbindung;
            return tmpVerbindung;
        }
        private void RemoveVerbindung(CVerbindung verbindung)
        {
            for (int i = 0; i < verbindungen.Count; i++)
            {
                CVerbindung tmpVerbindung = (CVerbindung)verbindungen[i];
                if (tmpVerbindung != null)
                    if (tmpVerbindung.GetStart() == verbindung.GetStart() && tmpVerbindung.GetStopp() == verbindung.GetStopp()) 
                        verbindungen.RemoveAt(i);
            }
        }
        private void SetState(CKnote knote, string state) 
        {
            foreach(CKnote knt in knoten)
                if (knt == knote)
                    knt.SetState(state);

            foreach(CVerbindung verb in verbindungen)
            {
                if (verb.GetStart() == knote) verb.GetStart().SetState(state);
                if (verb.GetStopp() == knote) verb.GetStopp().SetState(state);
            }
        }
        private void SetKnotenWert(CKnote knote, int wert)
        {
            foreach(CKnote knt in knoten)
                if (knt == knote) knt.SetKnotenWert(wert);

            foreach(CVerbindung verbindung in verbindungen)
                if (verbindung.GetStopp() == knote) verbindung.GetStopp().SetKnotenWert(wert);
        }
        public CKnote ErmittleStartKnoten(ArrayList verbindungen, ArrayList knoten) 
        {
            foreach(CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStopp() == (CKnote)knoten[i]) knoten.RemoveAt(i);
            return (CKnote)knoten[knoten.Count - 1];
        }
        public CKnote ErmittleEndKnoten(ArrayList verbindungen, ArrayList knoten) 
        {
            foreach (CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStart() == (CKnote)knoten[i]) knoten.RemoveAt(i);
            return (CKnote)knoten[knoten.Count - 1];
        }
        private ArrayList ErmittleAktivePunkte() 
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnote knote in knoten)
                if (knote.GetState() == "aktiv")
                    rtn.Add(knote);
            return rtn;
        }
        private ArrayList ErmittleOffeneKnoten(CKnote aktivenKnoten)
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnote knote in knoten)
                if (knote.GetState() == "offen")
                    foreach(CVerbindung verbindung in verbindungen)
                        if(verbindung.GetStart() == aktivenKnoten) 
                            if (verbindung.GetStopp() == knote)  
                                rtn.Add(knote);
            return rtn;
        }
    }
}