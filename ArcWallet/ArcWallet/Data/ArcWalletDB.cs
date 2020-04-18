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
            _database.CreateTableAsync<Transaction>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }


        public Task<List<Transaction>> GetAllTransaction()
        {
            return _database.Table<Transaction>().OrderByDescending(x => x.ID).ToListAsync();
        }

        public async Task<string> GetBalance()
        {
            var nbSpent = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = False");
            var nbRevenu = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = True");

            float spent=0;
            float revenu = 0;

            if (nbSpent.Count > 0)
            {
                spent = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'Transaction' WHERE Type = False");
            }

            if (nbRevenu.Count > 0)
            {
                revenu = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'Transaction' WHERE Type = True");
            }

            float balance = revenu - spent;
            return balance.ToString();
        }

        public async Task<List<Transaction>> GetBiggestExpenditure()
        {
            return await _database.QueryAsync<Transaction>("SELECT Name,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = False ;");
        }

        public async Task<List<Transaction>> GetBiggestRevenu()
        {
            return await _database.QueryAsync<Transaction>("SELECT Name,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = True ;");
        }
        public async Task<string> GetMostUsedCategoryExpenditure()
        {
            return await _database.ExecuteScalarAsync<string>("SELECT Category,count(*) FROM 'Transaction' WHERE Type = False GROUP BY Category;");
        }


        public Task<int> SavePersonAsync(User user)
        {
            return _database.InsertAsync(user);
        }



        public Task<int> SaveTransactionAsycn(Transaction transaction)
        {
            return _database.InsertAsync(transaction);
        }

        public Task<string> RemoveExpenditure(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM Expenditure WHERE id ="+id);

        }
        public Task<string> RemoveRevenu(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM Revenue WHERE id =" + id);
        }

        public Task<string> RemoveTransaction(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM 'Transaction' WHERE id =" + id);

        }

        /* public Task<string> UpdateExpenditure(string name,string category,string date,float amount,string old_name,string old_category,string old_date,float old_amount)
         {
             return _database.ExecuteScalarAsync<string>("UPDATE Expenditure SET Name="+name+", Category="+category+",Amout="+amount+",Date="+date+"  WHERE Name="+ old_name+" and Category="+old_category+" and Date="+old_date+" and Amount ="+old_amount+";");
         }*/

        public Task<int> UpdateExpenditure(Expenditure expenditure)
        {
            return _database.UpdateAsync(expenditure);
        }

        public Task<int> UpdateRevenue(Revenue revenue)
        {
            return _database.UpdateAsync(revenue);
        }

        public Task<int> UpdateTransaction(Transaction transaction)
        {
            return _database.UpdateAsync(transaction);
        }
    }
}