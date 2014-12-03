using System;
using System.Collections;

namespace Dijkstra
{
    class CDijkstra
    {
        private ArrayList knoten = new ArrayList();
        private ArrayList verbindungen = new ArrayList();

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
        /// Ermittelt die Kürzeste Strecke durch die Verbindungen
        /// </summary>
        public string Solve() //Löst das Problem
        {
            CKnoten startKnoten = ErmittleStartKnoten(verbindungen, new ArrayList(knoten));
            CKnoten stoppKnoten = ErmittleEndKnoten(verbindungen, new ArrayList(knoten));
            SetState(startKnoten, "aktiv");
            //ArrayList weg = new ArrayList();
            while (stoppKnoten.GetState() == "offen") 
            {
            //for(int i = 0; i <=6; i++)
            //{ 
                CVerbindung verbindung = ErmittleKürzesteVerbindungUnterallenAktivenVerbindungen();
                RemoveVerbindung(verbindung);
                if (verbindung != null)
                {
                    int summe = verbindung.GetWert() + verbindung.GetStart().GetKnotenWert();
                    SetKnotenWert(verbindung.GetStopp(), summe);
                    SetState(verbindung.GetStopp(), "aktiv");
                    //weg.Add(verbindung);
                    Console.WriteLine(verbindung.GetStopp().GetName() + "(" + verbindung.GetStart().GetName() + "), " + (verbindung.GetWert() + verbindung.GetStart().GetKnotenWert()));
                    foreach (CKnoten knt in ErmittleOffeneKnoten(verbindung.GetStopp()))
                    {
                        CVerbindung tmpVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), knt);
                        if (tmpVerbindung != null)
                        {
                            RemoveVerbindung(tmpVerbindung);
                        }
                    }
                }
                
            }
            return "im debug";
        }
        /// <summary>
        /// Ermittelt die kürzeste Verbindung von allen Verbindungen von aktiven Knoten zu einem offenen Knoten
        /// </summary>
        /// <returns>Verbindung</returns>
        private CVerbindung ErmittleKürzesteVerbindungUnterallenAktivenVerbindungen()
        {
            CVerbindung kürzesteVerbindungZuEinemOffenenPunkt = null;
            foreach (CKnoten aktiverKnote in ErmittleAktivePunkte())
            {
                if (ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote) == null) SetState(aktiverKnote, "geschlossen");
                else
                {
                    CVerbindung aktVerbindung = ErmittleKürzesteVerbindung(new ArrayList(verbindungen), aktiverKnote);
                    //Console.WriteLine("====> Von: " + aktVerbindung.GetStart().GetName() + " to " + aktVerbindung.GetStopp().GetName() + (aktVerbindung.GetWert() + aktVerbindung.GetStart().GetKnotenWert()));
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
        /// <returns>Verbindung</returns>
        private CVerbindung ErmittleKürzesteVerbindung(ArrayList verbindungen, CKnoten aktiverKnote)
        {
            CVerbindung tmpVerbindung = null;
            foreach (CVerbindung verbindung in verbindungen)
                foreach(CKnoten knote in ErmittleOffeneKnoten(aktiverKnote))
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
                {// WHY GIBT ES HIER IMMER BEI DER LETZTEN VERBINDUNG EINEN FEHLER!!!!!!!!!!!!!!!!!!! ;-(((((
                    try
                    {
                        if (tmpVerbindung.GetStart() == verbindung.GetStart() && tmpVerbindung.GetStopp() == verbindung.GetStopp() && tmpVerbindung.GetWert() == verbindung.GetWert())
                        {
                            verbindungen.RemoveAt(i);
                        }
                    }
                    catch { }
                }
            }
        }
        /// <summary>
        /// State eines Knoten ändern
        /// </summary>
        /// <param name="knote"></param>
        /// <param name="state"></param>
        private void SetState(CKnoten knote, string state) 
        {
            foreach(CKnoten knt in knoten)
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
        private void SetKnotenWert(CKnoten knote, int wert)
        {
            foreach(CKnoten knt in knoten)
                if (knt == knote) knt.SetKnotenWert(wert);

            foreach(CVerbindung verbindung in verbindungen)
                if (verbindung.GetStopp() == knote) verbindung.GetStopp().SetKnotenWert(wert);
        }
        /// <summary>
        /// Ermittelt den Startknoten
        /// </summary>
        /// <param name="verbindungen"></param>
        /// <param name="knoten"></param>
        /// <returns>Startknoten</returns>
        private CKnoten ErmittleStartKnoten(ArrayList verbindungen, ArrayList knoten) 
        {
            foreach(CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStopp() == (CKnoten)knoten[i]) knoten.RemoveAt(i);
            return (CKnoten)knoten[knoten.Count - 1];
        }
        /// <summary>
        /// Ermittelt den Zielknoten.
        /// </summary>
        /// <param name="verbindungen"></param>
        /// <param name="knoten"></param>
        /// <returns>Zielknoten</returns>
        private CKnoten ErmittleEndKnoten(ArrayList verbindungen, ArrayList knoten) 
        {
            foreach (CVerbindung verbindung in verbindungen)
                for (int i = 0; i <= knoten.Count - 1; i++)
                    if (verbindung.GetStart() == (CKnoten)knoten[i]) knoten.RemoveAt(i);
            return (CKnoten)knoten[knoten.Count - 1];
        }
        /// <summary>
        /// Aktive Knoten ermitteln
        /// </summary>
        /// <returns>ArrayList</returns>
        private ArrayList ErmittleAktivePunkte() //Aktive Knoten als ArrayList
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnoten knote in knoten)
                if (knote.GetState() == "aktiv")
                    rtn.Add(knote);
            return rtn;
        }
        /// <summary>
        /// Offene Punkte ermitteln
        /// </summary>
        /// <param name="aktivenKnoten">AktiverKnoten für den offene Knoten ermittelt werden sollen.</param>
        /// <returns>ArrayList</returns>
        private ArrayList ErmittleOffeneKnoten(CKnoten aktivenKnoten)
        {
            ArrayList rtn = new ArrayList();
            foreach(CKnoten knote in knoten)
                if (knote.GetState() == "offen")
                    foreach(CVerbindung verbindung in verbindungen)
                        if(verbindung.GetStart() == aktivenKnoten) 
                            if (verbindung.GetStopp() == knote)  
                                rtn.Add(knote);
            return rtn;
        }
        public ArrayList GetKnoten()
        {
            return knoten;
        }
        public ArrayList GetVerbindungen()
        {
            return verbindungen;
        }
    }
}