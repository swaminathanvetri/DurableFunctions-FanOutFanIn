using Pulumi;
using Pulumi.Azure.AppService;
using Pulumi.Azure.AppService.Inputs;
using Pulumi.Azure.Core;
using Pulumi.Azure.Storage;

class FunctionStack : Stack
{
    public FunctionStack()
    {
        //Read the WebStorage Connection String from Config 
        var config = new Config();
        var azWebJobsStorage = config.Require("AzWebJobsStorage");

        // Create an Azure Resource Group
        var resourceGroup = new ResourceGroup("pulumi-func-rg");

        // Create an Azure Storage Account
        var storageAccount = new Account("funcstorage", new AccountArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AccountReplicationType = "LRS",
            AccountTier = "Standard"
        });
        
        //Create an App Service Plan
        var appServicePlan = new Plan("funcasp", new PlanArgs
        {
            ResourceGroupName = resourceGroup.Name,
            Kind = "FunctionApp",
            Sku = new PlanSkuArgs
            {
                Tier = "Dynamic",
                Size = "Y1",
            }
        });

        //Create a Storage Container
        var container = new Container("funczips", new ContainerArgs
        {
            StorageAccountName = storageAccount.Name,
            ContainerAccessType = "private"
        });

        //Create a Blog for pushing zip files
        var blob = new Blob("funczip", new BlobArgs
        {
            StorageAccountName = storageAccount.Name,
            StorageContainerName = container.Name,
            Type = "Block",
            Source = new FileArchive("../bin/Debug/netcoreapp3.1/publish")
        });

        var codeBlobUrl = SharedAccessSignature.SignedBlobReadUrl(blob, storageAccount);

        //Create a function app and deploy the function package
        var app = new FunctionApp("bdotnet-demo-func", new FunctionAppArgs
        {
            ResourceGroupName = resourceGroup.Name,
            AppServicePlanId = appServicePlan.Id,
            AppSettings =
            {
                {"runtime", "dotnet"},
                {"WEBSITE_RUN_FROM_PACKAGE", codeBlobUrl},
                {"AzureWebJobsStorage", azWebJobsStorage}
            },
            StorageConnectionString = storageAccount.PrimaryConnectionString,
            Version = "~3"
        });

        this.Endpoint = Output.Format($"https://{app.DefaultHostname}/api/TriggerBookVacation");
    }

[Output] public Output<string> Endpoint { get; set; }
}
