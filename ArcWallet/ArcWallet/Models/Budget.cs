using SQLite;

namespace ArcWallet
{
     /// <summary>
     /// Budget Table in database
     /// </summary>
    public class Budget
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public float Amount { get; set; }
        public string Date { get; set; }
        public bool Type { get; set; } //true -> hebdomadaire  false -> mensuel
    }
}