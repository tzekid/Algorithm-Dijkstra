
namespace Dijkstra
{
    class CVerbindung
    {
        CKnoten start;
        CKnoten stopp;
        int wert;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stopp"></param>
        /// <param name="wert"></param>
        public CVerbindung(CKnoten start, CKnoten stopp, int wert)
        {
            this.start = start;
            this.stopp = stopp;
            this.wert = wert;
        }
        /// <summary>
        /// Gibt den Startknoten zurück
        /// </summary>
        /// <returns></returns>
        public CKnoten GetStart()
        {
            return start;
        }
        /// <summary>
        /// Gibt den Zielknoten zurück
        /// </summary>
        /// <returns></returns>
        public CKnoten GetStopp()
        {
            return stopp;
        }
        /// <summary>
        /// Gibt den Wert der Verbindung zurück
        /// </summary>
        /// <returns></returns>
        public int GetWert()
        {
            return wert;
        }
    }
}
