using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DissertationArtefact.Shared
{
    public class Pluto2021DatabaseSettings : IPluto2021DatabaseSettings
    {
        public string UsersCollectionName { get; set; }
        public string ExpensesCollectionName { get; set; }
        public string IncomesCollectionName { get; set; }
        public string GoalsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

    public interface IPluto2021DatabaseSettings
    {
        string UsersCollectionName { get; set; }
        string ExpensesCollectionName { get; set; }
        string IncomesCollectionName { get; set; }
        string GoalsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }

}


