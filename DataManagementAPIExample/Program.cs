using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using DataManagementAPI.AuthenticationUtility;
using Microsoft.WindowsAzure.Storage.Blob;
using Newtonsoft.Json;
using RestSharp;

namespace DataManagementAPI
{
    class Program
    {
        static void Main(string[] args)
        {

            //MultiThreadedImport();
            SingleThreadedImport();

            Console.WriteLine("Press key to terminate!");
            Console.ReadKey();

        }


        public static void ImportDataPackage(string fileName)
        {
            
            // 1. Get writable URL
            RestClient client = RestClientBuilder.Client(ClientConfiguration.Default.UriString +
                                                         "/data/DataManagementDefinitionGroups/Microsoft.Dynamics.DataEntities.GetAzureWriteUrl");
            var request = new RestRequest(Method.POST);
            request.AddJsonBody(new
            {
                uniqueFileName = Guid.NewGuid()
            });


            IRestResponse<ODataResponse> response = client.Execute<ODataResponse>(request);

            AzureUrlResult azureUrlResult = JsonConvert.DeserializeObject<AzureUrlResult>(response.Data.Value);

            // 2. Upload the file
            string filePath = @"..\debug\SampleData\" + fileName + ".zip";


            using (FileStream stream = new FileStream(filePath, FileMode.Open))
            {
                var blob = new CloudBlockBlob(new Uri(azureUrlResult.BlobUrl));
                blob.UploadFromStream(stream);
            }


            // 3. Import the data package 
            RestClient importClient = RestClientBuilder.Client(
                ClientConfiguration.Default.UriString +
                "/data/DataManagementDefinitionGroups/Microsoft.Dynamics.DataEntities.ImportFromPackage");

            var importRequest = new RestRequest(Method.POST);
            importRequest.AddJsonBody(new
            {
                packageUrl = azureUrlResult.BlobUrl,
                definitionGroupId = "sample-package-" + Guid.NewGuid(), // unique data project
                executionId = string.Empty,
                execute = true,
                overwrite = true,
                legalEntityId = "USMF"
            });

            importRequest.Timeout = 900000;

            Console.WriteLine("Import data package request start.");
            IRestResponse importResponse = importClient.Execute(importRequest);
            

            Console.WriteLine(importResponse.StatusDescription);
            Console.WriteLine(importResponse.Content);

            if (importResponse.ResponseStatus == ResponseStatus.Error)
            {
                Console.WriteLine(importResponse.ErrorException);
                Console.WriteLine(importResponse.ErrorMessage);
            }

        }

       
        public static void MultiThreadedImport()
        {
            Func<object, int> action = (object obj) =>
            {
                int i = (int)obj;
                i++;
                ImportDataPackage("sample-package");
                return i;
            };

            var tasks = new List<Task<int>>();

            // Construct started tasks
            for (int i = 0; i < 11; i++)
            {
                int index = i;
                tasks.Add(Task<int>.Factory.StartNew(action, index));
            }

            // Wait for all the tasks to finish.
            Task.WaitAll(tasks.ToArray());
        }


        public static void SingleThreadedImport()
        {
            ImportDataPackage("sample-package");            
        }

    }
}
