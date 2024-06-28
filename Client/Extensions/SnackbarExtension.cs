using System.Net;
using MudBlazor;

namespace Client.Extensions;

public static class SnackbarExtensions
{
    public static Snackbar Add(this ISnackbar snackbar, Exception? exception, string message)
    {
        if (snackbar is null)
        {
            throw new ArgumentNullException(nameof(snackbar));
        }

        if (exception is null)
        {
            throw new ArgumentNullException(nameof(exception));
        }

        if (string.IsNullOrEmpty(message))
        {
            throw new ArgumentException($"'{nameof(message)}' nemôže byť null alebo prázdne");
        }

        if (exception is HttpRequestException httpRequestException)
        {
            return httpRequestException.StatusCode switch
            {
                HttpStatusCode.BadRequest
                    => snackbar.Add("Zdá sa, že s práve zadanými údajmi nie je niečo v poriadku!", Severity.Error),
                HttpStatusCode.Forbidden => snackbar.Add("Nemáte opravávnenie!", Severity.Error),
                HttpStatusCode.Unauthorized => snackbar.Add("Nemáte opravávnenie!", Severity.Error),
                HttpStatusCode.InternalServerError => snackbar.Add("Vyskytla sa neznáma chyba!", Severity.Error),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        return snackbar.Add($"{message}: {exception.Message}", Severity.Error);
    }
}
