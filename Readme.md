# This lab will help you create an Azure Storage account and manage blob operations using .Net SDK 

## Services used:

- [Azure Storage](https://azure.microsoft.com/en-us/products/category/storage/)
- [Azure Storage Explorer](https://azure.microsoft.com/en-us/products/storage/storage-explorer/)

## Instructions to make the demo work

### Step 1 
Create an Azure Storage Account using CLI with a container called images

Create a Storage Account
```
az storage account create -n <mystorageaccountname> -g <My-Resource-Group> -l eastus --sku Standard_LRS
az storage account create -n ibstrg02 -g Sid-RG-01 -l eastus --sku Standard_LRS

```
Create a container
```
az storage container create -n images --account-name ibstrg02 --fail-on-exist 
```

### Step 2 - Connect using Azure Storage Explorer

### Step 3
Clone the GitHub repo in Visual Studio 2022 IDE - [Working with Azure Blobs](https://github.com/Developing-Scalable-Apps-using-Azure/Working-with-Azure-Blobs.git)

Connect using connection string 
```
Navigate in portal: Settings -> Access Keys -> Connection String
Replace the var connectionString value in index.cshtml.cs with the above connection 
Run the project using Visual Studio and upload a file to the images container
```


Connect using Shared access Signature 
Navigate in portal: Settings -> Shared Access Signature -> Allow all resource types -> Generate SAS and Connection String
Replace the var sasURL value in index.cshtml.cs with the Blob service SAS URL
Run the project using Visual Studio and upload a file to the images container
```

