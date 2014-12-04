
namespace Dijkstra
{
    class CKnote
    {
        string name;
        string state;
        int wert;
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        public CKnote(string name, string state)
        {
            this.name = name;
            this.state = state;
            this.wert = 0;
        }
        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="name"></param>
        /// <param name="state"></param>
        /// <param name="wert"></param>
        public CKnote(string name, string state, int wert)
        {
            this.name = name;
            this.state = state;
            this.wert = wert;
        }
        /// <summary>
        /// Gibt den Namen des Knotens zurück
        /// </summary>
        /// <returns></returns>
        public string GetName()
        {
            return name;
        }
        /// <summary>
        /// Gibt den Status des Knoten zurück
        /// </summary>
        /// <returns></returns>
        public string GetState()
        {
            return state;
        }
        /// <summary>
        /// Setzt den Status eines Knotens
        /// </summary>
        /// <param name="state"></param>
        public void SetState(string state)
        {
            this.state = state;
        }
        /// <summary>
        /// Gibt den Wert des Knotens zurück
        /// </summary>
        /// <returns></returns>
        public int GetKnotenWert()
        {
            return wert;
        }
        /// <summary>
        /// Setzt den Wert des Knotens
        /// </summary>
        /// <param name="value"></param>
        public void SetKnotenWert(int value)
        {
            wert = value;
        }
    }
}
