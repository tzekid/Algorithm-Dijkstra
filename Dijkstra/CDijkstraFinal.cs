using System;
using System.Collections;

namespace Dijkstra
{
    class CDijkstraFinal
    {
        public CDijkstraFinal()
        {
        }
        public ArrayList Solve(ArrayList knoten, ArrayList verbindungen)
        {
            CKnote startKnoten = ErmittleStartKnoten(verbindungen, new ArrayList(knoten));
            CKnote stoppKnoten = ErmittleEndKnoten(verbindungen, new ArrayList(knoten));
            SetState(startKnoten, ref knoten, ref verbindungen, "aktiv");
            ArrayList rtnWeg = new ArrayList();

            while (stoppKnoten.GetState() == "offen")
            {
                CVerbindung verbindung = ErmittleKürzesteVerbindungUnterallenAktivenVerbindungen(new ArrayList(verbindungen), new ArrayList(knoten));
                RemoveVerbindung(verbindung, ref verbindungen);
                foreach (CKnote knt in ErmittleOffeneKnoten(verbindung.GetStopp(),new ArrayList(knoten), new ArrayList(verbindungen)))
                {
                    CVerbindung tmpVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), knt, new ArrayList(knoten));
                    if (tmpVerbindung != null && tmpVerbindung.GetStopp() != stoppKnoten) RemoveVerbindung(tmpVerbindung, ref verbindungen);
                }

                if (verbindung != null)
                {
                    int summe = verbindung.GetWert() + verbindung.GetStart().GetKnotenWert();
                    SetKnotenWert(verbindung.GetStopp(), summe, ref knoten, ref verbindungen);
                    SetState(verbindung.GetStopp(), ref knoten, ref verbindungen, "aktiv");
                    rtnWeg.Add(verbindung);
                }
            }
            return ErmittleEndWeg(rtnWeg);
        }

        private ArrayList ErmittleEndWeg(ArrayList rtnWeg)
        {
            CVerbindung stoppVer = (CVerbindung)rtnWeg[rtnWeg.Count - 1];
            ArrayList rtnFinal = new ArrayList();
            rtnFinal.Add(stoppVer);
            CVerbindung last = stoppVer;
            for (int i = 0; i < rtnWeg.Count; i++)
            {
                foreach (CVerbindung tmpVer in rtnWeg)
                {
                    if (tmpVer.GetStopp() == last.GetStart())
                    {
                        rtnFinal.Add(tmpVer);
                        last = tmpVer;
                        break;
                    }
                }
            }
            return rtnFinal;
        }
        private CVerbindung ErmittleKürzesteVerbindungUnterallenAktivenVerbindungen(ArrayList verbindungen, ArrayList knoten)
        {
            CVerbindung kürzesteVerbindungZuEinemOffenenPunkt = null;
            foreach (CKnote aktiverKnote in ErmittleAktivePunkte(new ArrayList(knoten)))
            {
                if (ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote, new ArrayList(knoten)) == null) SetState(aktiverKnote, ref knoten, ref verbindungen, "geschlossen");
                else
                {
                    CVerbindung aktVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote, new ArrayList(knoten));
                    if (kürzesteVerbindungZuEinemOffenenPunkt == null || (aktVerbindung.GetWert() + aktVerbindung.GetStart().GetKnotenWert()) < (kürzesteVerbindungZuEinemOffenenPunkt.GetWert() + kürzesteVerbindungZuEinemOffenenPunkt.GetStart().GetKnotenWert()))
                        kürzesteVerbindungZuEinemOffenenPunkt = aktVerbindung;
                }
            }
            return kürzesteVerbindungZuEinemOffenenPunkt;
        }
        private CVerbindung ErmittleKürzesteVerbindung(ArrayList verbindungen, CKnote aktiverKnote, ArrayList knoten)
        {
            CVerbindung tmpVerbindung = null;
            foreach (CVerbindung verbindung in verbindungen)
                foreach (CKnote knote in ErmittleOffeneKnoten(aktiverKnote, new ArrayList(knoten), new ArrayList(verbindungen)))
                    if (verbindung.GetStart().GetName() == aktiverKnote.GetName() && verbindung.GetStopp().GetName() == verbindung.GetStopp().GetName())
                        if (tmpVerbindung == null || verbindung.GetWert() < tmpVerbindung.GetWert()) tmpVerbindung = verbindung;
            return tmpVerbindung;
        }
        private void RemoveVerbindung(CVerbindung verbindung, ref ArrayList verbindungen)
        {
            for (int i = 0; i < verbindungen.Count; i++)
            {
                CVerbindung tmpVerbindung = (CVerbindung)verbindungen[i];
                if (tmpVerbindung != null)
                    if (tmpVerbindung.GetStart() == verbindung.GetStart() && tmpVerbindung.GetStopp() == verbindung.GetStopp())
                        verbindungen.RemoveAt(i);
            }
        }
        private void SetState(CKnote knote, ref ArrayList knoten, ref ArrayList verbindungen, string state)
        {
            foreach (CKnote knt in knoten)
                if (knt == knote)
                    knt.SetState(state);

            foreach (CVerbindung verb in verbindungen)
            {
                if (verb.GetStart() == knote) verb.GetStart().SetState(state);
                if (verb.GetStopp() == knote) verb.GetStopp().SetState(state);
            }
        }
        private void SetKnotenWert(CKnote knote, int wert, ref ArrayList knoten, ref ArrayList verbindungen)
        {
            foreach (CKnote knt in knoten)
                if (knt == knote) knt.SetKnotenWert(wert);

            foreach (CVerbindung verbindung in verbindungen)
                if (verbindung.GetStopp() == knote) verbindung.GetStopp().SetKnotenWert(wert);
        }
        private CKnote ErmittleStartKnoten(ArrayList verbindungen, ArrayList knoten)
        {
            foreach (CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStopp() == (CKnote)knoten[i]) knoten.RemoveAt(i);
            return (CKnote)knoten[knoten.Count - 1];
        }
        private CKnote ErmittleEndKnoten(ArrayList verbindungen, ArrayList knoten)
        {
            foreach (CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStart() == (CKnote)knoten[i]) knoten.RemoveAt(i);
            return (CKnote)knoten[knoten.Count - 1];
        }
        private ArrayList ErmittleAktivePunkte(ArrayList knoten)
        {
            ArrayList rtn = new ArrayList();
            foreach (CKnote knote in knoten)
                if (knote.GetState() == "aktiv")
                    rtn.Add(knote);
            return rtn;
        }
        private ArrayList ErmittleOffeneKnoten(CKnote aktivenKnoten, ArrayList knoten, ArrayList verbindungen)
        {
            ArrayList rtn = new ArrayList();
            foreach (CKnote knote in knoten)
                if (knote.GetState() == "offen")
                    foreach (CVerbindung verbindung in verbindungen)
                        if (verbindung.GetStart() == aktivenKnoten)
                            if (verbindung.GetStopp() == knote)
                                rtn.Add(knote);
            return rtn;
        }
    }
}