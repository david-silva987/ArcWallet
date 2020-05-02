using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

//Database: https://docs.microsoft.com/en-us/xamarin/get-started/tutorials/local-database/?tutorial-step=1&tabs=vswin

namespace ArcWallet
{
    /// <summary>
    /// Database interaction with application
    /// </summary>
    public class Database
    {
        readonly SQLiteAsyncConnection _database;

        public Database(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);
            _database.CreateTableAsync<Transaction>().Wait();
            _database.CreateTableAsync<Budget>().Wait();
        }

        /// <summary>
        /// Get all transactions in database
        /// </summary>
        /// <returns></returns>
        public Task<List<Transaction>> GetAllTransactions()
        {
            return _database.Table<Transaction>().OrderByDescending(x => x.ID).ToListAsync();
        }

        /// <summary>
        /// Get Money spent using SUM condition
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAmountExpenditures()
        {
            var nbSpent = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = False");
            float spent = 0;

            if (nbSpent.Count>0) //check if there's at least one entry
            {
                spent = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Type = False");

            }
            return spent.ToString();
        }

        /// <summary>
        /// Get Money received using SUM condition
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetAmountRevenues()
        {
            var nbRevenu = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = True");
            float revenu = 0;

            if (nbRevenu.Count > 0) //check if there's at least one entry
            {
                revenu = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Type = True");
            }
            return revenu.ToString();
        }

        /// <summary>
        /// Get total balance (difference between money received and money spent) using SUM condition
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetBalance()
        {
            var nbSpent = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = False");
            var nbRevenu = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = True");

            float spent=0;
            float revenu = 0;

            if (nbSpent.Count > 0) //check if there's at least one entry
            {
                spent = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'Transaction' WHERE Type = False");
            }

            if (nbRevenu.Count > 0) //check if there's at least one entry
            {
                revenu = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'Transaction' WHERE Type = True");
            }

            float balance = revenu - spent;
            return balance.ToString();
        }
        
        /// <summary>
        /// Get budget of user
        /// </summary>
        /// <returns></returns>
        public async Task<float> GetBudget()
        {
            float budget = 0;
            var nbBudget = await _database.QueryAsync<Budget>("SELECT * FROM 'Budget'");

            if(nbBudget.Count > 0) //check if there's at least one entry
            {
                budget = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'Budget'");
            }
            
            return budget;
        }

        /// <summary>
        /// Get Biggest expenditure from user, using MAX condition 
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> GetBiggestExpenditure()
        { 

            return await _database.QueryAsync<Transaction>("SELECT Name,Category,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = False GROUP BY Name ORDER BY Amount DESC LIMIT 1;");
        }

        /// <summary>
        /// Get biggest Revenue, using MAX condition
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> GetBiggestRevenue()
        {
            return await _database.QueryAsync<Transaction>("SELECT Name,Category,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = True GROUP BY Name ORDER BY Amount DESC LIMIT 1;");
        }

        /// <summary>
        /// Get spent by category, using SUM and GROUP BY condition
        /// </summary>
        /// <returns></returns>
        public async Task<List<Transaction>> GetSpentByCategory()
        {
            return await _database.QueryAsync<Transaction>("SELECT Category,SUM(Amount) as Amount , count(*) as Name FROM 'Transaction' WHERE Type = False GROUP BY Category ORDER BY Amount DESC;");
        }

        /// <summary>
        /// Get amount spent in last seven days
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetSpentLastXDays()
        {
            var nbAmount = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Date > (SELECT DATE('now', '-7 day')) and Type = False");
            var nbBudget = await _database.QueryAsync<Budget>("SELECT * FROM 'Budget'");
            float amount = 0; //default value to prevent app from crashing
            bool typeBudget = true;


            if (nbBudget.Count > 0 && nbAmount.Count>0) //check if there's at least one entry
            {
                typeBudget = await _database.ExecuteScalarAsync<bool>("SELECT Type FROM 'Budget'");

                if (typeBudget) //if it's weekly
                {
                    amount = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Date > (SELECT DATE('now', '-7 day')) and Type = False");
                }
                else //it's monhly
                {
                    amount = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Date > (SELECT DATE('now', '-30 day')) and Type = False");
                }
            }

            return amount.ToString();
        }

        /// <summary>
        /// Check if we have weekly/monthly budget
        /// </summary>
        /// <returns></returns>
        public async Task<bool> GetTypeBudget()
        {
            var nbBudget = await _database.QueryAsync<Budget>("SELECT * FROM 'Budget'");
            bool typeBudget=true;

            if (nbBudget.Count > 0) //check if there's at least one entry
            {
                typeBudget = await _database.ExecuteScalarAsync<bool>("SELECT Type FROM 'Budget'");
            }
        
            return typeBudget;
        }

        /// <summary>
        /// Add Budget to database
        /// </summary>
        /// <param name="budget"> the row to be entered in database</param>
        /// <returns></returns>
        public Task<int> SaveBudgetAsync(Budget budget)
        {
            return _database.InsertAsync(budget);
        }

        /// <summary>
        /// Add transaction to database
        /// </summary>
        /// <param name="transaction"> the row to be entered in database</param>
        /// <returns></returns>
        public Task<int> SaveTransactionAsycn(Transaction transaction)
        {
            return _database.InsertAsync(transaction);
        }

        /// <summary>
        /// Delete transaction to database
        /// </summary>
        /// <param name="id"> the id of the row to be deleted in database</param>
        /// <returns></returns>
        public Task<string> RemoveTransaction(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM 'Transaction' WHERE id =" + id);

        }

        /// <summary>
        /// Update transaction in database
        /// </summary>
        /// <param name="transaction">The transaction to be updated</param>
        /// <returns></returns>
        public Task<int> UpdateTransaction(Transaction transaction)
        {
            return _database.UpdateAsync(transaction);
        }

        /// <summary>
        /// Update budget in database
        /// </summary>
        /// <param name="budget">Budget to be updated in database</param>
        /// <returns></returns>
        public Task<int> UpdateBudget(Budget budget)
        {
            return _database.UpdateAsync(budget);
        }

    }
}

