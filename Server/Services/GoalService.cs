using DissertationArtefact.Shared;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DissertationArtefact.Server.Services
{
    public class GoalService
    {
        private readonly IMongoCollection<Goal> goals;

        public GoalService(IPluto2021DatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            goals = database.GetCollection<Goal>(settings.GoalsCollectionName);
        }

        public List<Goal> Get() =>
           goals.Find(goal => true).ToList();

        public Goal Get(string id) =>
            goals.Find<Goal>(goal => goal.Id == id).FirstOrDefault();

        public List<Goal> Get(User user) =>
             goals.Find(goal => goal.UserId == user.Id).ToList();



        public Goal Create(Goal goal)
        {
            goals.InsertOne(goal);
            return goal;
        }

        public void Update(string id, Goal goalIn) =>
            goals.ReplaceOne(goal => goal.Id == id, goalIn);

        public void Remove(Goal goalIn) =>
            goals.DeleteOne(goal => goal.Id == goalIn.Id);

        public void Remove(string id) =>
            goals.DeleteOne(goal => goal.Id == id);
    }
}
