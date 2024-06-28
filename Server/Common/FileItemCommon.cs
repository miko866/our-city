using Server.Helpers;
using ErrorCodes = Shared.Helpers.ErrorCodes;
using Path = System.IO.Path;

namespace Server.Common;

#region Interface

public interface IFileItemCommon
{
    Task<MemoryStream> GetFileMemoryStream(string fileName, string folderType, string folderId);
    string GetContentType(string fileName);
}

#endregion Interface

public class FileItemCommon : IFileItemCommon
{
    private readonly IConfiguration _configuration;
    private readonly IErrorMessages _errorMessages;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="serviceProvider"></param>
    /// <param name="configuration"></param>
    /// <param name="logger"></param>
    /// <param name="errorMessages"></param>
    public FileItemCommon(IConfiguration configuration, IErrorMessages errorMessages)
    {
        _configuration = configuration;
        _errorMessages = errorMessages;
    }

    #region Implementation

    #region Public methods

    /// <summary>
    /// GetFileMemoryStream
    /// </summary>
    /// <param name="fileName"></param>
    /// <returns></returns>
    public async Task<MemoryStream> GetFileMemoryStream(string fileName, string folderType, string folderId)
    {
        string fullFilePath = GetFullFilePath(fileName, folderType, folderId);

        if (!File.Exists(fullFilePath))
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_FILE_ERROR_FILE_NOT_FOUND())
                    .SetCode(ErrorCodes.CODE_FILE_ERROR_FILE_NOT_FOUND)
                    .Build()
            );

        var memory = new MemoryStream();
        await using (var stream = new FileStream(fullFilePath, FileMode.Open))
        {
            await stream.CopyToAsync(memory);
        }
        memory.Position = 0;
        return memory;
    }

    public string GetContentType(string fileName)
    {
        Dictionary<string, string> types = GetMimeTypes();
        string extension = Path.GetExtension(fileName).ToLowerInvariant();
        return types[extension];
    }

    #endregion Public methods

    #region Private methods

    private string GetFullFilePath(string fileName, string folderType, string folderId)
    {
        string storagePath = _configuration["FileStorage:StoragePath"]!;
        return Path.Combine(storagePath, folderType, folderId, fileName);
    }

    private static Dictionary<string, string> GetMimeTypes()
    {
        // Content-Type values in the header
        // https://developer.mozilla.org/en-US/docs/Web/HTTP/Basics_of_HTTP/MIME_types/Common_types
        return new Dictionary<string, string>
        {
            { ".pdf", "application/pdf" },
            { ".doc", "application/vnd.ms-word" },
            { ".docx", "application/vnd.ms-word" },
            { ".xls", "application/vnd.ms-excel" },
            { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
            { ".png", "image/png" },
            { ".jpg", "image/jpeg" },
            { ".jpeg", "image/jpeg" },
            { ".gif", "image/gif" },
            { ".svg", "image/svg" },
            { ".webp", "image/webp" },
            { ".csv", "text/csv" },
            { ".txt", "text/plain" },
        };
    }

    #endregion Private methods

    #endregion
}
