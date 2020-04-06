using SQLite;
using System;

namespace ArcWallet
{
    public class User
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Date { get; set; }
    }
}