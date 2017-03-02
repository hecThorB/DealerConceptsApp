using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using DealerConceptsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DealerConceptsApp.Services.Storage
{
    public class CloudStorageService : BaseService, IStorageService
    {
        private IConfigService _configService;
        AmazonS3Client _client;

        public CloudStorageService(IConfigService configService)
        {
            _configService = configService;
            _client = new AmazonS3Client(_configService.Key, _configService.Secret, RegionEndpoint.USWest2);
        }

        public string Upload(HttpPostedFile file)
        {
            try
            {
                PutObjectRequest putRequest1 = new PutObjectRequest
                {
                    BucketName = _configService.Bucket,
                    Key = "dealer-concepts/" + Guid.NewGuid().ToString() + "-" + file.FileName,
                    InputStream = file.InputStream
                };

                string url = "https://dealer-concepts.s3-us-west-2.amazonaws.com/" + putRequest1.Key; // This link is not a real url. Once We get going we will need to create and update this URL
                PutObjectResponse response1 = _client.PutObject(putRequest1);
                return url;
            }
            catch (AmazonS3Exception)
            {
                throw;
            }
        }
    }
}