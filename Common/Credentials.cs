namespace Employee_Management_System.Common
{
    public class Credentials
    {
        public static readonly string CosmosUrl = Environment.GetEnvironmentVariable("cosmosUrl");

        public static readonly string PrimaryKey = Environment.GetEnvironmentVariable("primaryKey");

        public static readonly string DatabaseName = Environment.GetEnvironmentVariable("databaseName");

        public static readonly string Container = Environment.GetEnvironmentVariable("container");

        public static readonly string EmployeeBasicDetailsDocumentType = "EmployeeBasicDetails";

        public static readonly string EmployeeAdditionalDetailsDocumentType = "EmployeeAdditionalDetails";

    }
}
