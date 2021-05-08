using DissertationArtefact.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationArtefact.Server.Services
{
    public class IncomeService
    {
        private readonly IMongoCollection<Income> incomes;

        public IncomeService(IPluto2021DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            incomes = database.GetCollection<Income>(settings.IncomesCollectionName);
        }

        public List<Income> Get() =>
           incomes.Find(income => true).ToList();

        public Income Get(string id) =>
            incomes.Find<Income>(income => income.Id == id).FirstOrDefault();

        public List<Income> Get(User user) =>
             incomes.Find(income => income.UserId == user.Id).ToList();



        public Income Create(Income income)
        {
            incomes.InsertOne(income);
            return income;
        }

        public void Update(string id, Income incomeIn) =>
            incomes.ReplaceOne(income => income.Id == id, incomeIn);

        public void Remove(Income incomeIn) =>
            incomes.DeleteOne(income => income.Id == incomeIn.Id);

        public void Remove(string id) =>
            incomes.DeleteOne(income => income.Id == id);
    }
}
