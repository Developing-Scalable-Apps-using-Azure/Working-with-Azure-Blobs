using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using System.IO;
using Azure.Storage;
using Microsoft.AspNetCore.Http;

namespace NetClientExamplev12.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        public string Message { get; set; }

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        [HttpPost]
        public async Task<IActionResult> OnPostUploadFilesAsync(List<IFormFile> files)
        {
            //////////////////////////////////////////
            /// Shared Key Authentication
            /// 
            var containerName = "librarycontainer";

            /// Connect using connection string (in portal: Settings -> Access Keys -> Connection String)
            //var connectionString = "";
            //BlobServiceClient blobServiceClient = new BlobServiceClient(connectionString);

            /// Connect using Storage Account Key (in portal: Settings -> Access Keys -> Key)
            //string accountName = "nmstgacct1";
            //string accountKey = "yrJ0IuEKR0+r1JuguDZ42Y52VJ4f+3wOqIRm+1TkSg+ury0myNqhCMVjVnbDL73RmhxMdbnpkihYd0CdFGlY1g==";
            /// Blob service URL can be found in portal: Settings -> Properties -> Blob Service
            //Uri serviceUri = new Uri("https://nmstgacct1.blob.core.windows.net/");
            //StorageSharedKeyCredential credential = new StorageSharedKeyCredential(accountName, accountKey);
            //BlobServiceClient blobServiceClient = new BlobServiceClient(serviceUri, credential);

            //////////////////////////////////////////
            /// Shared Access Signature (SAS) Authentication
            /// 
            var sasURL = "https://ibsa01.blob.core.windows.net/library?sp=rawl&st=2022-02-06T08:55:38Z&se=2022-02-26T16:55:38Z&spr=https&sv=2020-08-04&sr=c&sig=3Fo3oCLfsqfYBUgpzpKmDUoFau%2BggDWM%2FzDuNaUN56o%3D";
            UriBuilder sasUri = new UriBuilder(sasURL);
            BlobServiceClient blobServiceClient = new BlobServiceClient(sasUri.Uri);

            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);

            long size = files.Sum(f => f.Length);

            // full path to file in temp location (buffer file locally before uploading).
            // note: for larger files and workloads, consider streaming instead.
            var filePath = Path.GetTempFileName();

            int uploadFileCount = 0;

            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // create a local reference to a blob
                    BlobClient blobClient = containerClient.GetBlobClient(Path.GetFileName(formFile.FileName));

                    using (var stream = formFile.OpenReadStream())
                    {
                        // upload the blob to Azure Storage
                        await blobClient.UploadAsync(stream, true);
                        uploadFileCount++;
                    }
                }
            }

            Message = "Number of files uploaded to Azure Storage: " + uploadFileCount;
            return Page();

        }

    }
}
