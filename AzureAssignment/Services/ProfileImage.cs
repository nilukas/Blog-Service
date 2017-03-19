using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureAssignment.Services
{
    public class ProfileImage
    {
        private static CloudBlobClient blobClient;
        private static CloudBlobContainer container;

        public static string errorMessage;
        static ProfileImage()
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationSettings.AppSettings["StorageConnectionString"]);

                var blobStorage = storageAccount.CreateCloudBlobClient();
                container = blobStorage.GetContainerReference("profiles");

                if (container.CreateIfNotExists())
                {
                    var permissions = container.GetPermissions();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions);
                }

            }
            catch (Exception e)
            {
                errorMessage = e.Message.ToString();
            }
        }

        public string UploadPhoto(string blobName, HttpPostedFileBase photoToUpload)
        {
            if (photoToUpload == null || photoToUpload.ContentLength == 0)
            {
                return null;
            }

            string fullPath = string.Empty;         
            try
            {
                CloudBlockBlob blob = ProfileImage.container.GetBlockBlobReference(blobName);
                blob.Properties.ContentType = photoToUpload.ContentType;
                blob.UploadFromStream(photoToUpload.InputStream);
                                
                var uriBuilder = new UriBuilder(blob.Uri);
                uriBuilder.Scheme = "https";
                fullPath = uriBuilder.ToString();
              }
            catch (Exception ex)
            {
            }

            return fullPath;
        }

    }
}