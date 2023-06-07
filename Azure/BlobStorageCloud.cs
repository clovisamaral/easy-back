using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace EasyInvoice.API.Azure
{
    public static class BlobStorageCloud
    {
        public static BlobServiceClient GetBlobServiceClient(string connectionString)
        {
            return new BlobServiceClient(connectionString);
        }
        public static BlobContainerClient GetBlobContainerClient(BlobServiceClient blobServiceClient, string containerName)
        {
            BlobContainerClient containerClient = blobServiceClient.GetBlobContainerClient(containerName);
            containerClient.CreateIfNotExists();
            return containerClient;
        }
        public static void UploadFile(BlobContainerClient containerClient, string filePath, string blobName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            using FileStream fileStream = File.OpenRead(filePath);
            blobClient.Upload(fileStream, true);
        }
        public static string DownloadFile(BlobContainerClient containerClient, string blobName)
        {
            BlobClient blobClient = containerClient.GetBlobClient(blobName);
            BlobDownloadInfo downloadInfo = blobClient.Download();
            using StreamReader reader = new StreamReader(downloadInfo.Content);
            return reader.ReadToEnd();
        }

    }
}
