using DissertationArtefact.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationArtefact.Server.Services
{
    public class ExpenseService
    {
        private readonly IMongoCollection<Expense> expenses;

        public ExpenseService(IPluto2021DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            expenses = database.GetCollection<Expense>(settings.ExpensesCollectionName);
        }

        public List<Expense> Get() =>
           expenses.Find(expense => true).ToList();

        public Expense Get(string id) =>
            expenses.Find<Expense>(expense => expense.Id == id).FirstOrDefault();

        public List<Expense> Get(User user) =>
             expenses.Find(expense => expense.UserId == user.Id).ToList();



        public Expense Create(Expense expense)
        {
            expenses.InsertOne(expense);
            return expense;
        }

        public void Update(string id, Expense expenseIn) =>
            expenses.ReplaceOne(expense => expense.Id == id, expenseIn);

        public void Remove(Expense expenseIn) =>
            expenses.DeleteOne(expense => expense.Id == expenseIn.Id);

        public void Remove(string id) =>
            expenses.DeleteOne(expense => expense.Id == id);
    }
}
