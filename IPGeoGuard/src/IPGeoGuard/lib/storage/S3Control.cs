using Amazon.S3;
using Amazon.S3.Transfer;

namespace IPGeoGuard.lib.storage;

public static class S3Control
{
    const string S3_ERRORCODE_NOSUCHKEY = "NoSuchKey";
    const string S3_BUCKET_NOT_EXIST = "The specified bucket does not exist";
    static AmazonS3Client GetAmazonS3Client()
    {
        return new AmazonS3Client();
    }

    static TransferUtility GetTransferUtility()
    {
        return new TransferUtility(GetAmazonS3Client());
    }
    public static void UploadSingleFile(
        string bucketName,
        string keyName,
        string content
    )
    {
        UploadSingleFile(
            GetTransferUtility(),
            bucketName,
            keyName,
            content
        );
    }

    static void UploadSingleFile(
        TransferUtility transferUtil,
        string bucketName,
        string keyName,
        string content
    )
    {
        using (var stream = ReadStreamFromString(content))
        {
            transferUtil.Upload(stream, bucketName, keyName);
        }
    }
    static Stream ReadStreamFromString(string s)
    {
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        writer.Write(s);
        writer.Flush();
        stream.Position = 0;
        return stream;
    }

    public static string? DownloadSingleFile(
        string bucketName,
        string filePath,
        bool throwNotFoundException
    )
    {
        try
        {
            using (var stream = GetTransferUtility().OpenStream(bucketName, filePath))
            {
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
        catch(AmazonS3Exception ex)
        {
            if (
                throwNotFoundException || 
                (
                    !IsS3NoSuchKeyException(ex)
                )
            )
            {
                throw new Exception(filePath, ex);
            }
        }
        return null;
    }
    static bool IsS3NoSuchKeyException(AmazonS3Exception ex)
    {
        return 
            ex.ErrorCode.Equals(
                S3_ERRORCODE_NOSUCHKEY, 
                StringComparison.InvariantCultureIgnoreCase
            );
    }
    static bool IsS3BucketNotExistException(AmazonS3Exception ex)
    {
        return 
            ex.ErrorCode.Equals(
                S3_BUCKET_NOT_EXIST ,
                StringComparison.InvariantCultureIgnoreCase
            );
    }
}