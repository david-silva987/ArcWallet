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
            _database.CreateTableAsync<Transaction>().Wait();
            _database.CreateTableAsync<Budget>().Wait();
        }

        //Get all transactions
        public Task<List<Transaction>> GetAllTransactions()
        {
            return _database.Table<Transaction>().OrderByDescending(x => x.ID).ToListAsync();
        }

        //Get amout of all expenditures
        public async Task<string> GetAmountExpenditures()
        {
            var nbSpent = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = False");
            float spent = 0;

            if (nbSpent.Count>0)
            {
                spent = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Type = False");

            }
            return spent.ToString();
        }

        //Get amount of all revenues
        public async Task<string> GetAmountRevenues()
        {
            var nbRevenu = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Type = True");
            float revenu = 0;

            if (nbRevenu.Count > 0)
            {
                revenu = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Type = True");
            }
            return revenu.ToString();
        }

        //Get Amount of difference between all expenditures and all revenues
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

        //Get Budget
        public async Task<float> GetBudget()
        {
            float budget = 0;
            var nbBudget = await _database.QueryAsync<Budget>("SELECT * FROM 'Budget'");

            if(nbBudget.Count > 0)
            {
                budget = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) FROM 'Budget'");
            }
            
            return budget;
        }

        //Get the biggest expenditure
        public async Task<List<Transaction>> GetBiggestExpenditure()
        { 

            return await _database.QueryAsync<Transaction>("SELECT Name,Category,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = False GROUP BY Name ORDER BY Amount DESC LIMIT 1;");
        }

        //Get the biggest expenditure
        public async Task<List<Transaction>> GetBiggestRevenue()
        {
            return await _database.QueryAsync<Transaction>("SELECT Name,Category,MAX(Amount) as Amount FROM 'Transaction' WHERE Type = True GROUP BY Name ORDER BY Amount DESC LIMIT 1;");
        }

        //Get spent by catergory
        public async Task<List<Transaction>> GetSpentByCategory()
        {
            return await _database.QueryAsync<Transaction>("SELECT Category,SUM(Amount) as Amount , count(*) as Name FROM 'Transaction' WHERE Type = False GROUP BY Category ORDER BY Amount DESC;");
        }

        //Get expenditure's amount of last 7 days
        public async Task<string> GetSpentLastSevenDays()
        {
            var nbAmount = await _database.QueryAsync<Transaction>("SELECT * FROM 'Transaction' WHERE Date > (SELECT DATE('now', '-7 day')) and Type = False");
            var nbBudget = await _database.QueryAsync<Budget>("SELECT * FROM 'Budget'");
            float amount = 0;
            bool typeBudget = true;
            string dateBudget = "";

       


            if (nbBudget.Count > 0 && nbAmount.Count>0)
            {
                typeBudget = await _database.ExecuteScalarAsync<bool>("SELECT Type FROM 'Budget'");
                dateBudget = await _database.ExecuteScalarAsync<string>("SELECT Date FROM 'Budget'");

                System.Console.WriteLine("typeBudget:" + typeBudget);
                System.Console.WriteLine("dateBudget:" + dateBudget);
                System.Console.WriteLine("**************************************************" );


                if (typeBudget)
                {
                    amount = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Date > (SELECT DATE('now', '-7 day')) and Type = False");
                    System.Console.WriteLine("7days: " + amount);
                }
                else
                {
                    amount = await _database.ExecuteScalarAsync<float>("SELECT SUM(Amount) as Amount FROM 'Transaction' WHERE Date > (SELECT DATE('now', '-30 day')) and Type = False");
                    System.Console.WriteLine("30days: " + amount);
                }
            }


   
            return amount.ToString();
        }

        //Get type of Budget
        public async Task<bool> GetTypeBudget()
        {
            var nbBudget = await _database.QueryAsync<Budget>("SELECT * FROM 'Budget'");
            bool typeBudget=true;

            if (nbBudget.Count > 0)
            {
                typeBudget = await _database.ExecuteScalarAsync<bool>("SELECT Type FROM 'Budget'");
            }

        
            return typeBudget;
        }

        //Add budget to DB
        public Task<int> SaveBudgetAsync(Budget budget)
        {
            return _database.InsertAsync(budget);
        }

        //Add a transaction to DB
        public Task<int> SaveTransactionAsycn(Transaction transaction)
        {
            return _database.InsertAsync(transaction);
        }

        //Delete a transaction from DB 
        public Task<string> RemoveTransaction(int id)
        {
            return _database.ExecuteScalarAsync<string>("DELETE FROM 'Transaction' WHERE id =" + id);

        }

        //Update a transaction in DB 
        public Task<int> UpdateTransaction(Transaction transaction)
        {
            return _database.UpdateAsync(transaction);
        }

        //Update a budget in DB
        public Task<int> UpdateBudget(Budget budget)
        {
            return _database.UpdateAsync(budget);
        }

    }
}

