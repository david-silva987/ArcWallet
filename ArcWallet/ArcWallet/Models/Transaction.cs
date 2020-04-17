using SQLite;

namespace ArcWallet
{
    public class Transaction
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public bool Type { get; set; } //Depense --> 0 // Revenue --> 1
        public string Name { get; set; }
        public string Category { get; set; }
        public float Amount { get; set; }
        public string Date { get; set; }

    }
}