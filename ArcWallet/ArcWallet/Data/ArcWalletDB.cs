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
            _database.CreateTableAsync<MyBudget>().Wait();
        }

        public Task<List<User>> GetUserAsync()
        {
            return _database.Table<User>().ToListAsync();
        }


        public Task<List<Transaction>> GetAllTransaction()
        {
            return _database.Table<Transaction>().OrderByDescending(x => x.ID).ToListAsync();
        }

        public async Task<string> GetMoneySpent()
        {
            var nbSpent = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = False");
            float spent = 0;

            if (nbSpent.Count>0)
            {
                spent = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Type = False");

            }
            return spent.ToString();
        }

        public async Task<string> GetMoneyReceîved()
        {
            var nbRevenu = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = True");
            float revenu = 0;

            if (nbRevenu.Count > 0)
            {
                revenu = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Type = True");

            }
            return revenu.ToString();
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

        public async Task<float> GetBudget()
        {
            float budget = 0;
            var nbBudget = await _database.QueryAsync<MyBudget>("SELECT * FROM 'MyBudget'");

            if(nbBudget.Count > 0)
            {
                budget = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'MyBudget'");
            }
            
            return budget;
        }

        public async Task<List<Transaction>> GetBiggestExpenditure()
        { 

            return await _database.QueryAsync<Transaction>("SELECT Name,Category,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = False GROUP BY Name ORDER BY Amount DESC LIMIT 1;");
        }

        public async Task<List<Transaction>> GetBiggestRevenu()
        {
            return await _database.QueryAsync<Transaction>("SELECT Name,Category,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = True GROUP BY Name ORDER BY Amount DESC LIMIT 1;");
        }
        public async Task<string> GetMostUsedCategoryExpenditure()
        {
            return await _database.ExecuteScalarAsync<string>("SELECT Category,count(*) FROM 'Transaction' WHERE Type = False GROUP BY Category;");
        }

        public async Task<List<Transaction>> GetSpentByCategory()
        {
            return await _database.QueryAsync<Transaction>("SELECT Category,SUM(Amount) as Amount , count(*) as Name FROM 'Transaction' WHERE Type = False GROUP BY Category ORDER BY Amount DESC;");
        }

        public async Task<float> GetSpentLastWeek()
        {
            float amount = 0;
            var nbAmount = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction'");
            if(nbAmount.Count > 0)
            {
                amount = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Date > (SELECT DATETIME('now', '-7 day')) and Type = False");
            }
            return amount;
        }

        public Task<int> SavePersonAsync(User user)
        {
            return _database.InsertAsync(user);
        }

        public Task<int> SaveBudgetAsync(MyBudget budget)
        {
            return _database.InsertAsync(budget);
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
        public Task<int> UpdateBudget(MyBudget budget)
        {
            return _database.UpdateAsync(budget);
        }

    }
}