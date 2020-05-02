## Durable Functions Demo app

This is a sample code base demonstrating Durable Functions. This is a forked version from [AmreshKris](https://github.com/amreshkris) repo

## Prerequisites

### For Durable Functions alone
- VS Code
- .NET Core v3.1 or above
- Azure Tools Extension in VS Code
- Azure Storage Emulator
- Azure Functions Core Tools

### For Deploying Function to Azure using Pulumi

- Pulumi 
- Azure CLI
- Azure Subscription

## Steps to run the project

1. Run the azure functions locally - From the parent directory run the below command

    `func host start` 

This should run the azure functions within function core tools

2. Deployment to Azure - From the parent directory run the below command to publish the azure function app

`dotnet publish`

3. Create a new stack; Navigate to `Deployment` folder and run the below command

    `pulumi stack init dev`

4. Add necessary configurations

    `pulumi config set AzWebJobsStorage <Your Azure Storage Account Connection String> --secure`

5. Create Azure Function App & Perform Deployment

    `pulumi up` 