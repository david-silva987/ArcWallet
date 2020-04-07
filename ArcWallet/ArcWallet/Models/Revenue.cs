using SQLite;

namespace ArcWallet
{
    public class Revenue
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public float Amount { get; set; }
        public string Name { get; set; }
        public int Permanent { get; set; }
        public string Date { get; set; }

    }
}