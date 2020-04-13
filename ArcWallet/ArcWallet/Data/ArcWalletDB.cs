using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

//Database: https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/local-database/?tutorial-step=1&tabs=vswin

namespace ArcWallet
{
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<User>().Wait();
            _database.CreateTableAsync<Revenue>().Wait();
            _database.CreateTableAsync<Expenditure>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }

        public Task<List<Revenue>> GetRevenueAsync()
        {
            return _database.Table<Revenue>().OrderBy(x => x.Date).ToListAsync();
        }
        public Task<List<Revenue>> GetBiggestRevenuAsync()
        {
            return _database.Table<Revenue>().OrderByDescending(x => x.Amount).Take(1).ToListAsync();
        }

        public Task<string> GetAllRevenus()
        {
            return _database.ExecuteScalarAsync<string>("SELECT SUM(Amount) FROM Revenue WHERE Amount > 0");
        }


        public Task<string> GetAllExpenditures()
        {
            return _database.ExecuteScalarAsync<string>("SELECT SUM(Amount) FROM Expenditure WHERE Amount > 0");
        }

        public Task<List<Expenditure>> GetBiggestDepenseAsync()
        {
            return _database.Table<Expenditure>().OrderByDescending(x => x.Amount).Take(1).ToListAsync();
        }
        public Task<List<Expenditure>> GetExpenditureAsync()
        {
            return _database.Table<Expenditure>().OrderBy(x=> x.Date).ToListAsync();
        }

        public Task<int> SavePersonAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<int> SaveExpenditureAsync(Expenditure expenditure)
        {
            return _database.InsertAsync(expenditure);
        }
        public Task<int> SaveRevenuAsycn(Revenue revenu)
        {
            return _database.InsertAsync(revenu);
        }

        public Task<string> RemoveExpenditure(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM Expenditure WHERE id ="+id);

        }
        public Task<string> RemoveRevenu(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM Revenue WHERE id =" + id);

        }
    }
}