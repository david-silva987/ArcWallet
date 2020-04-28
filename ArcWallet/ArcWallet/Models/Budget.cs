using SQLite;

namespace ArcWallet
{
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public float Amount { get; set; }

    }
}