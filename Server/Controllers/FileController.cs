using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Server.Common;
using Server.Helpers;
using ErrorCodes = Shared.Helpers.ErrorCodes;

namespace Server.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
[Produces("application/json")]
public class FileController : ControllerBase
{
    private readonly IFileItemCommon _fileItemCommon;
    private readonly ILogger<FileController> _logger;
    private readonly IErrorMessages _errorMessages;

    public FileController(IFileItemCommon fileItemCommon, ILogger<FileController> logger, IErrorMessages errorMessages)
    {
        _fileItemCommon = fileItemCommon;
        _logger = logger;
        _errorMessages = errorMessages;
    }

    #region Public methods

    /// <summary>
    /// Download file
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Downloaded file</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     Some request
    ///
    /// </remarks>
    /// <response code="200">Returns Image</response>
    /// <response code="400">Something is wrong</response>
    [HttpGet("{fileName}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Download(string fileName)
    {
        if (fileName.IsNullOrEmpty())
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_FILE_NOT_FOUND())
                    .SetCode(ErrorCodes.CODE_FILE_ERROR_PROVIDE_NO_FILE)
                    .Build()
            );

        // Read from querystring.
        string folderType = System.Web.HttpUtility.UrlDecode(Request.Query["folder-type"]);
        string folderId = System.Web.HttpUtility.UrlDecode(Request.Query["folder-id"]);

        if (folderType.IsNullOrEmpty() || folderId.IsNullOrEmpty())
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_FILE_MISSING_QUERY_PARAMS())
                    .SetCode(ErrorCodes.CODE_FILE_ERROR_PROVIDE_NO_FILE)
                    .Build()
            );

        try
        {
            fileName = System.Web.HttpUtility.UrlDecode(fileName);
            MemoryStream? MyFile;

            MyFile = await _fileItemCommon.GetFileMemoryStream(fileName, folderType, folderId);

            return File(MyFile, _fileItemCommon.GetContentType(fileName), fileName);
        }
        catch (Exception exception)
        {
            _logger.LogError("Download: {Exception}", exception);
            throw new GraphQLException(
                ErrorBuilder
                    .New()
                    .SetMessage(_errorMessages.ERROR_DOWNLOAD())
                    .SetCode(ErrorCodes.CODE_ERROR_DOWNLOAD)
                    .Build()
            );
        }
    }

    #endregion Public methods
}
