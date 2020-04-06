using SQLite;

namespace ArcWallet
{
    public class Revenue
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public float Amount { get; set; }
    }
}