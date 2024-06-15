using Employee_Management_System.Common;
using Employee_Management_System.Entity;
using Microsoft.Azure.Cosmos;

namespace Employee_Management_System.CosmosDB
{
    public class CosmosDBService : ICosmoseDBService 
    {
        private readonly CosmosClient _cosmosClient;
        private readonly Container _container;

        public CosmosDBService()
        {
            _cosmosClient = new CosmosClient(Credentials.CosmosUrl, Credentials.PrimaryKey);
            _container = _cosmosClient.GetContainer(Credentials.DatabaseName, Credentials.Container);
        }

        public async Task<dynamic> AddEntity(dynamic Entity)
        {
            var response = await _container.CreateItemAsync(Entity);
            return response;
        }

        public async Task<dynamic> UpdateEntity(dynamic entity)
        {
            var response = await _container.ReplaceItemAsync(entity, entity.Id);
            return response;
        }

        public async Task<List<EmployeeAdditionalDetails>> GetAllEmployeeAdditionalDetails()
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(q =>
            q.Active && q.Archived == false && q.DocumentType == Credentials.EmployeeAdditionalDetailsDocumentType).ToList();
            return response;
        }

        public async Task<List<EmployeeBasicDetails>> GetAllEmployeeBasicDetails()
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(q =>
           q.Active && q.Archived == false && q.DocumentType == Credentials.EmployeeBasicDetailsDocumentType).ToList();
            return response;
        }

        public async Task<EmployeeAdditionalDetails> GetEmployeeAdditionalDetailsByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeAdditionalDetails>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.EmployeeAdditionalDetailsDocumentType).FirstOrDefault();
            return response;
        }

        public async Task<EmployeeBasicDetails> GetEmployeeBasicDetailsByUId(string uid)
        {
            var response = _container.GetItemLinqQueryable<EmployeeBasicDetails>(true).Where(q => q.UId == uid &&
            q.Active && q.Archived == false && q.DocumentType == Credentials.EmployeeBasicDetailsDocumentType).FirstOrDefault();
            return response;
        }

    }
}
