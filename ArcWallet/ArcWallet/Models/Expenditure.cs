using SQLite;
using System;

namespace ArcWallet
{
    public class Expenditure
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public float Amount { get; set; }
    }
}