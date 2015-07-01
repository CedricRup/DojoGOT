namespace GOT
{
    public class Reduction
    {
        private readonly int _nombre;
        private readonly decimal _taux;

        public Reduction(int nombre, decimal taux)
        {
            _nombre = nombre;
            _taux = taux;
        }

        public int Nombre
        {
            get { return _nombre; }
        }

        public decimal Taux
        {
            get { return _taux; }
        }
    }
}