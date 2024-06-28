namespace Server.Helpers;

public interface IErrorMessages
{
    string ERROR_AUTHENTICATION();
    string ERROR_NOT_ALLOWED();
    string ERROR_CANNOT_RUN_ON_PRODUCTION();
    string ERROR_USER_EMAIL_NOT_CONFIRMED();
    string ERROR_INSUFFICIENT_AUTHENTICATION();
    string ERROR_USER_EMAIL_NOT_WHITELISTED();
    string ERROR_NOT_NULL_OR_EMPTY();
    string ERROR_USER_NOT_EXIST();
    string ERROR_CANNOT_SAVE();
    string ERROR_USER_EXIST();
    string ERROR_INVALID_ROLE();
    string ERROR_CREATE_USER();
    string ERROR_UPDATE_USER();
    string ERROR_SEND_EMAIL();
    string ERROR_NO_ADDRESS();
    string ERROR_RECORD_EXISTS();
    string ERROR_NOT_CLEAR_RESULT_FOR_UPDATE();
    string ERROR_RECORD_NOT_FOUND();
    string ERROR_RECORD_COULD_NOT_BE_DELETED();
    string ERROR_DOWNLOAD();
    string ERROR_FILE_NOT_FOUND();
    string ERROR_FILE_MISSING_QUERY_PARAMS();
    string ERROR_FILE_ERROR_FILE_NOT_FOUND();
}

public record ErrorMessages : IErrorMessages
{
    public string ERROR_AUTHENTICATION()
    {
        return "Chyba pri prihlasovaný, nesprávne meno alebo heslo.";
    }

    public string ERROR_NOT_ALLOWED()
    {
        return "Nemáte dostatočné oprávnenie.";
    }

    public string ERROR_CANNOT_RUN_ON_PRODUCTION()
    {
        return "Nemožno spustiť na produkčnom prostredí.";
    }

    public string ERROR_USER_EMAIL_NOT_CONFIRMED()
    {
        return "Email nie je potvrdený.";
    }

    public string ERROR_INSUFFICIENT_AUTHENTICATION()
    {
        return "Nedostatočné oprávnenie.";
    }

    public string ERROR_USER_EMAIL_NOT_WHITELISTED()
    {
        return "Email nie je povolený.";
    }

    public string ERROR_NOT_NULL_OR_EMPTY()
    {
        return "Nesmie byť prázdne.";
    }

    public string ERROR_USER_NOT_EXIST()
    {
        return "Používateľ neexistuje.";
    }

    public string ERROR_CANNOT_SAVE()
    {
        return "Nepodarilo sa uložiť dáta.";
    }

    public string ERROR_USER_EXIST()
    {
        return "Používateľ už existuje.";
    }

    public string ERROR_INVALID_ROLE()
    {
        return "Neplatná rola.";
    }

    public string ERROR_CREATE_USER()
    {
        return "Nepodarilo sa vytvoriť používateľa.";
    }

    public string ERROR_UPDATE_USER()
    {
        return "Nepodarilo sa aktualizovať používateľa.";
    }

    public string ERROR_SEND_EMAIL()
    {
        return "Nepodarilo sa odoslať email.";
    }

    public string ERROR_NO_ADDRESS()
    {
        return "Adresa nesmie byť prázdna.";
    }

    public string ERROR_RECORD_EXISTS()
    {
        return "Záznam už existuje.";
    }

    public string ERROR_NOT_CLEAR_RESULT_FOR_UPDATE()
    {
        return "Pri aktualizácii môže byť ovplyvnený len jeden záznam, ale máte viac ako jeden!";
    }

    public string ERROR_RECORD_NOT_FOUND()
    {
        return "Záznam neexistuje.";
    }

    public string ERROR_RECORD_COULD_NOT_BE_DELETED()
    {
        return "Záznam sa nepodarilo zmazať.";
    }

    public string ERROR_DOWNLOAD()
    {
        return "Chyba pri sťahovaní súboru.";
    }

    public string ERROR_FILE_NOT_FOUND()
    {
        return "Súbor neexistuje.";
    }

    public string ERROR_FILE_MISSING_QUERY_PARAMS()
    {
        return "Chýbajúce query parametre.";
    }

    public string ERROR_FILE_ERROR_FILE_NOT_FOUND()
    {
        return "Súbor neexistuje.";
    }
}
