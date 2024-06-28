namespace Shared.Helpers;

public static class ErrorCodes
{
    /// <summary>
    /// General Errors 000
    /// </summary>
    public const string CODE_NOT_AUTHORIZED = "000"; // No authorization, not logged in
    public const string CODE_ERROR_NOT_ALLOWED = "001"; // No authorization, business logic (=may only change things to which one also has rights)
    public const string CODE_NOT_AN_UNIQUE_RESULT = "002"; // When updating anything, only 1 data set may be affected but you have more then 1 !
    public const string CODE_ERROR_INTERNAL = "003"; // Internal service error, that is bad!
    public const string CODE_NOT_FOUND = "004"; // Something does not exist
    public const string CODE_ERROR_GRAPHQL = "005"; // Global GraphQL HotChocolate error -> check HotChocolateErrorCodes.md file
    public const string CODE_NOT_FOUND_GRAPHQL_FILTER = "006"; // Global GraphQL HotChocolate error -> check HotChocolateErrorCodes.md file
    public const string CODE_ERROR_RECORD_EXISTS = "007"; // Cannot save the same value into DB Entity
    public const string CODE_ERROR_CANNOT_SENT_EMAIL = "008"; // Something wrong with SMTP or config
    public const string CODE_ERROR_CANNOT_SAVE = "009"; // Cannot save data into DB
    public const string CODE_ERROR_CANNOT_GENERATE_EMAIL_TEMPLATE = "010"; // Problem with Fluid Template
    public const string CODE_ERROR_NOT_NULL_OR_EMPTY = "011"; // Value cannot by null or empty

    /// <summary>
    /// Validation Errors 100
    /// </summary>
    public const string CODE_VALIDATION_ERROR_ISO_3166 = "100"; // Needs to be in ISO-3166 Alpha-2 format.
    public const string CODE_VALIDATION_ERROR_NO_START_WHITESPACES = "101"; // Should not start with whitespace
    public const string CODE_VALIDATION_ERROR_NO_END_WHITESPACES = "102"; // Should not end with whitespace
    public const string CODE_VALIDATION_ERROR_NO_WHITESPACES = "103"; // Should not contain any whitespaces
    public const string CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY = "104"; // Should be null or not empty
    public const string CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY_SIZE_BIGGER = "105"; // Should be null or not empty and bigger than
    public const string CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY_SIZE_SMALLER = "106"; // Should be null or not empty and smaller than
    public const string CODE_VALIDATION_ERROR_NULL_OR_NOT_EMPTY_SIZE_BETWEEN = "107"; // Should be null or not empty and between
    public const string CODE_VALIDATION_ERROR_INVALID_AGE = "108"; // Invalid age

    /// <summary>
    /// Authorization Errors 200
    /// </summary>
    public const string CODE_AUTHORIZATION_ERROR_USED_EMAIL_USERNAME = "200"; // Use Email or Username, not both!
    public const string CODE_AUTHORIZATION_ERROR_CANNOT_LOGGIN = "201"; // User doesn't exist or was deleted
    public const string CODE_AUTHORIZATION_ERROR_NOT_CONFIRMED_EMAIL = "202"; // New registered user did not confirm his email address yet
    public const string CODE_AUTHORIZATION_ERROR_USERNAME_PASSWORD_IS_WRONG = "203"; // Username, email or password is wrong
    public const string CODE_AUTHORIZATION_ERROR_USER_DELETED = "204";

    /// <summary>
    /// File Errors 300
    /// </summary>
    public const string CODE_FILE_ERROR_FILE_NOT_FOUND = "300"; // File doesn't exist
    public const string CODE_FILE_ERROR_PROVIDE_NO_FILE = "301"; // Provide no file/s
    public const string CODE_FILE_ERROR_NOT_ALLOWED_CONTENT_TYPE = "302"; // Content file type is not supported yet!
    public const string CODE_FILE_ERROR_MAX_FILESIZE_EXCEEDED = "303"; // File is to big, make it smaller
    public const string CODE_ERROR_DOWNLOAD = "430";

    /// <summary>
    /// Services Errors 400
    /// </summary>

    // Seeder Errors 400
    public const string CODE_SEEDER_ERROR_CANNOT_RUN_ON_PRODUCTION = "400"; // Can not run in production environment

    // User Errors 410
    public const string CODE_USER_ERROR_NOT_VERIFIED_EMAIL = "410"; // At first user need to verified his email, without that user cannot log in
    public const string CODE_USER_ERROR_EMAIL_NOT_WHITELISTED = "411"; // Email or email domain is not allowed - create or register new user
    public const string CODE_USER_ERROR_NOT_EXIST = "412"; // User not exists
    public const string CODE_USER_ERROR_SEND_EMAIL = "413"; // Cannot send email to user after create / invite or something else
    public const string CODE_USER_ERROR_INVALID_ROLE = "414"; // User role is not allow
    public const string CODE_USER_ERROR_EXIST = "415"; // User already exists
    public const string CODE_USER_ERROR_CANNOT_CREATE = "416"; // Cannot create user, something bad is happened

    // EmailWhiteBlackList Errors 420
    public const string CODE_EMAIL_WHITE_BLACK_LIST_ERROR_CANNOT_SAVE = "420"; // Cannot save data into DB
    public const string CODE_EMAIL_WHITE_BLACK_LIST_ERROR_CANNOT_UPDATE = "421"; // Cannot save data into DB
}
