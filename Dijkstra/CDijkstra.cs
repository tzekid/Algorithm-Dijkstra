using System;
using System.Collections;

namespace Dijkstra
{
    class CDijkstra
    {
        private ArrayList knoten;
        private ArrayList verbindungen;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="knoten"></param>
        /// <param name="verbindungen"></param>
        public CDijkstra(ArrayList knoten, ArrayList verbindungen)
        {
            this.knoten = knoten;
            this.verbindungen = verbindungen;
        }
        /// <summary>
        /// Ermittelt die Kürzeste Strecke durch die Verbindungen und Knoten
        /// </summary>
        public string Solve()
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
            return ErmittleEndWeg(rtnWeg);
        }
        /// <summary>
        /// Wandelt den kürzesten Weg (ArrayList) in einem string um
        /// </summary>
        /// <param name="rtnWeg"></param>
        /// <returns></returns>
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
                rtnFinal += rtn[i] + " ";
            rtnFinal += ", " + stoppVer.GetStopp().GetKnotenWert();
            return rtnFinal;
        }
        /// <summary>
        /// Ermittelt die kürzeste Verbindung von allen Verbindungen von aktiven Knoten zu einem offenen Knoten
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Ermittelt die kürzeste Verbindung von einem aktiven Konten zu einem offen Knoten
        /// </summary>
        /// <param name="verbindungen"></param>
        /// <param name="aktiverKnote"></param>
        /// <returns></returns>
        private CVerbindung ErmittleKürzesteVerbindung(ArrayList verbindungen, CKnote aktiverKnote)
        {
            CVerbindung tmpVerbindung = null;
            foreach (CVerbindung verbindung in verbindungen)
                foreach(CKnote knote in ErmittleOffeneKnoten(aktiverKnote))
                    if(verbindung.GetStart().GetName() == aktiverKnote.GetName() && verbindung.GetStopp().GetName() == verbindung.GetStopp().GetName())
                        if (tmpVerbindung == null || verbindung.GetWert() < tmpVerbindung.GetWert()) tmpVerbindung = verbindung;
            return tmpVerbindung;
        }
        /// <summary>
        /// Entfernt eine Verbindung
        /// </summary>
        /// <param name="verbindung"></param>
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
        /// <summary>
        /// State eines Knoten ändern
        /// </summary>
        /// <param name="knote"></param>
        /// <param name="state"></param>
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
        /// <summary>
        /// Setzt den Wert des Knotens
        /// </summary>
        /// <param name="knote"></param>
        /// <param name="wert"></param>
        private void SetKnotenWert(CKnote knote, int wert)
        {
            foreach(CKnote knt in knoten)
                if (knt == knote) knt.SetKnotenWert(wert);

            foreach(CVerbindung verbindung in verbindungen)
                if (verbindung.GetStopp() == knote) verbindung.GetStopp().SetKnotenWert(wert);
        }
        /// <summary>
        /// Ermittelt den Startknoten
        /// </summary>
        /// <param name="verbindungen"></param>
        /// <param name="knoten"></param>
        /// <returns></returns>
        public CKnote ErmittleStartKnoten(ArrayList verbindungen, ArrayList knoten) 
        {
            foreach(CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStopp() == (CKnote)knoten[i]) knoten.RemoveAt(i);
            return (CKnote)knoten[knoten.Count - 1];
        }
        /// <summary>
        /// Ermittelt den Zielknoten.
        /// </summary>
        /// <param name="verbindungen"></param>
        /// <param name="knoten"></param>
        /// <returns></returns>
        public CKnote ErmittleEndKnoten(ArrayList verbindungen, ArrayList knoten) 
        {
            foreach (CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStart() == (CKnote)knoten[i]) knoten.RemoveAt(i);
            return (CKnote)knoten[knoten.Count - 1];
        }
        /// <summary>
        /// Aktive Knoten ermitteln
        /// </summary>
        /// <returns></returns>
        private ArrayList ErmittleAktivePunkte() 
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnote knote in knoten)
                if (knote.GetState() == "aktiv")
                    rtn.Add(knote);
            return rtn;
        }
        /// <summary>
        /// Offene Punkte ermitteln
        /// </summary>
        /// <param name="aktivenKnoten"></param>
        /// <returns>ArrayList</returns>
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