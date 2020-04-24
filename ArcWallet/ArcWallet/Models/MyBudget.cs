using SQLite;

namespace ArcWallet
{
    public class MyBudget
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public float Amount { get; set; }

    }
}