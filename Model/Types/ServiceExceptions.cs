﻿namespace Model.Types
{
    public class ServiceExceptions
    {
        public class JException
        {
            public int ExceptionCode;
            public string Message;
            public int StatusCode;
        }

        public static readonly JException[] JExceptions = 
        {
            new JException {ExceptionCode = 1, Message = "Invalid user name.", StatusCode = 400},
            new JException {ExceptionCode = 2, Message = "Username exits.", StatusCode = 400},
            new JException {ExceptionCode = 3, Message = "Invalid email.", StatusCode = 400},
            new JException {ExceptionCode = 4, Message = "Email exits.", StatusCode = 403},
            new JException {ExceptionCode = 5, Message = "Unverified email.", StatusCode = 403},
            new JException {ExceptionCode = 6, Message = "Invalid phone number.", StatusCode = 400},
            new JException {ExceptionCode = 7, Message = "Phone exits.", StatusCode = 403},
            new JException {ExceptionCode = 8, Message = "Unverified phone number.", StatusCode = 403},
            new JException {ExceptionCode = 9, Message = "Password too short.", StatusCode = 403},
            new JException {ExceptionCode = 10, Message = "Password too long.", StatusCode = 403},
            new JException {ExceptionCode = 11, Message = "Invalid password.", StatusCode = 400},
            new JException {ExceptionCode = 12, Message = "Invalid first name.", StatusCode = 400},
            new JException {ExceptionCode = 13, Message = "Invalid last name.", StatusCode = 400},
            new JException {ExceptionCode = 14, Message = "Incorrect username or password.", StatusCode = 403},
            new JException {ExceptionCode = 15, Message = "Account locked.", StatusCode = 403},
            new JException {ExceptionCode = 16, Message = "Too many login attempt.", StatusCode = 403},
            new JException {ExceptionCode = 17, Message = "Session has been expired.", StatusCode = 403},
            new JException {ExceptionCode = 18, Message = "Session missmatched.", StatusCode = 403},
            new JException {ExceptionCode = 19, Message = "Failed to start session.", StatusCode = 403},
            new JException {ExceptionCode = 20, Message = "Failed to kill session.", StatusCode = 403},
            new JException {ExceptionCode = 21, Message = "Failed to get all sessions.", StatusCode = 403},
            new JException {ExceptionCode = 22, Message = "Failed to persist.", StatusCode = 403},
            new JException {ExceptionCode = 23, Message = "Could not fetch records.", StatusCode = 403},
            new JException {ExceptionCode = 24, Message = "Could not push to broker.", StatusCode = 403},
            new JException {ExceptionCode = 25, Message = "Common exception related to user service.", StatusCode = 400},
            new JException {ExceptionCode = 26, Message = "Common exception related to session service.", StatusCode = 400},
            new JException {ExceptionCode = 27, Message = "Invalid birthdate.", StatusCode = 400},
            new JException {ExceptionCode = 28, Message = "Invalid gender.", StatusCode = 400},
            new JException {ExceptionCode = 29, Message = "Invalid timezone.", StatusCode = 400},
            new JException {ExceptionCode = 30, Message = "Invalid join date.", StatusCode = 400},
            new JException {ExceptionCode = 31, Message = "Invalid country code.", StatusCode = 400},
            new JException {ExceptionCode = 32, Message = "Invalid visibility.", StatusCode = 400},
            new JException {ExceptionCode = 33, Message = "Invalid headline.", StatusCode = 400},
            new JException {ExceptionCode = 34, Message = "Invalid about.", StatusCode = 400},
            new JException {ExceptionCode = 35, Message = "Invalid url.", StatusCode = 400},
            new JException {ExceptionCode = 36, Message = "Invalid picture.", StatusCode = 400},
            new JException {ExceptionCode = 37, Message = "Invalid last update.", StatusCode = 400},
            new JException {ExceptionCode = 38, Message = "Invalid date.", StatusCode = 400},
            new JException {ExceptionCode = 39, Message = "Invalid location.", StatusCode = 400},
            new JException {ExceptionCode = 40, Message = "Invalid friend ID.", StatusCode = 400},
            new JException {ExceptionCode = 41, Message = "Invalid reception mode.", StatusCode = 400},
            new JException {ExceptionCode = 42, Message = "Invalid user category ID.", StatusCode = 400},
            new JException {ExceptionCode = 43, Message = "Invalid category name.", StatusCode = 400},
            new JException {ExceptionCode = 44, Message = "Invalid category description.", StatusCode = 400},
            new JException {ExceptionCode = 45, Message = "Invalid title.", StatusCode = 400},
            new JException {ExceptionCode = 46, Message = "Invalid interests.", StatusCode = 400},
            new JException {ExceptionCode = 47, Message = "Invalid primary contact number.", StatusCode = 400},
            new JException {ExceptionCode = 48, Message = "Invalid ctag.", StatusCode = 400},
            new JException {ExceptionCode = 49, Message = "Invalid chat status.", StatusCode = 400},
            new JException {ExceptionCode = 50, Message = "Invalid has deactivated.", StatusCode = 400},
            new JException {ExceptionCode = 51, Message = "Invalid is approved.", StatusCode = 400},
            new JException {ExceptionCode = 52, Message = "Invalid is locked out.", StatusCode = 400},
            new JException {ExceptionCode = 53, Message = "Invalid is activated.", StatusCode = 400},
            new JException {ExceptionCode = 54, Message = "Invalid device ID.", StatusCode = 400},
            new JException {ExceptionCode = 55, Message = "Common mail exception.", StatusCode = 400},
            new JException {ExceptionCode = 56, Message = "Invalid to address.", StatusCode = 400},
            new JException {ExceptionCode = 57, Message = "Invalid sender address.", StatusCode = 400},
            new JException {ExceptionCode = 58, Message = "Invalid mail body.", StatusCode = 400},
            new JException {ExceptionCode = 59, Message = "Invalid email subject.", StatusCode = 400},
            new JException {ExceptionCode = 60, Message = "Invalid email.", StatusCode = 400},
            new JException {ExceptionCode = 61, Message = "Invalid user ID.", StatusCode = 400},
            new JException {ExceptionCode = 62, Message = "Invalid row ID.", StatusCode = 400},
            new JException {ExceptionCode = 63, Message = "Data model null pointer exception.", StatusCode = 400},
            new JException {ExceptionCode = 64, Message = "All category related common exception.", StatusCode = 400},
            new JException {ExceptionCode = 65, Message = "Invalid Group ID.", StatusCode = 400},
            new JException {ExceptionCode = 66, Message = "Old password mismatch.", StatusCode = 403},
            new JException {ExceptionCode = 67, Message = "Invalid token.", StatusCode = 400},
            new JException {ExceptionCode = 68, Message = "Invalid group name.", StatusCode = 400},
            new JException {ExceptionCode = 69, Message = "User ID mismatch with original owner.", StatusCode = 403},
            new JException {ExceptionCode = 70, Message = "Invalid model type.", StatusCode = 400},
            new JException {ExceptionCode = 71, Message = "Empty or null group settings.", StatusCode = 404},
            new JException {ExceptionCode = 72, Message = "Invalid broker ip.", StatusCode = 400},
            new JException {ExceptionCode = 73, Message = "Exception in route table.", StatusCode = 400},
            new JException {ExceptionCode = 74, Message = "Mode value beyond 1 and 2.", StatusCode = 400},
            new JException {ExceptionCode = 75, Message = "Invalid instance type.", StatusCode = 400},
            new JException {ExceptionCode = 76, Message = "Invalid rule type user in inbox folder rule creation.", StatusCode = 400},
            new JException {ExceptionCode = 77, Message = "Error in modification of default folder.", StatusCode = 400},
            new JException {ExceptionCode = 78, Message = "Invalid inbox folder ID.", StatusCode = 400},
            new JException {ExceptionCode = 79, Message = "Null / Empty to list.", StatusCode = 404},
            new JException {ExceptionCode = 80, Message = "Null / Empty instance ID.", StatusCode = 404},
            new JException {ExceptionCode = 81, Message = "Null / Empty message ID.", StatusCode = 404},
            new JException {ExceptionCode = 82, Message = "Null / Empty local timestamp.", StatusCode = 404},
            new JException {ExceptionCode = 83, Message = "Actual message missing.", StatusCode = 400},
            new JException {ExceptionCode = 84, Message = "Invalid message type.", StatusCode = 400},
            new JException {ExceptionCode = 85, Message = "Invalid message format.", StatusCode = 400},
            new JException {ExceptionCode = 86, Message = "Contact list is empty.", StatusCode = 404},
            new JException {ExceptionCode = 87, Message = "Group list is empty.", StatusCode = 404},
            new JException {ExceptionCode = 88, Message = "All general exception related to inbox service.", StatusCode = 400},
            new JException {ExceptionCode = 89, Message = "All general exception related to inbox rule service.", StatusCode = 400},
            new JException {ExceptionCode = 90, Message = "All general exception related to group service.", StatusCode = 400},
            new JException {ExceptionCode = 91, Message = "All general exception related to chat system.", StatusCode = 400},
            new JException {ExceptionCode = 92, Message = "Null / Empty session ID.", StatusCode = 403},
            new JException {ExceptionCode = 93, Message = "Invalid Topic Code.", StatusCode = 400},
            new JException {ExceptionCode = 94, Message = "Folder list empty.", StatusCode = 404},
            new JException {ExceptionCode = 95, Message = "Group ID not found.", StatusCode = 404},
            new JException {ExceptionCode = 96, Message = "An exception when 'Created by' field is missing / null.", StatusCode = 400},
            new JException {ExceptionCode = 97, Message = "When the database doesn't consists information for given userId (rowid).", StatusCode = 400},
            new JException {ExceptionCode = 98, Message = "An exception when 'members' field is missing / null.", StatusCode = 400},
            new JException {ExceptionCode = 99, Message = "Missing device type.", StatusCode = 404},
            new JException {ExceptionCode = 100, Message = "Elif exception.", StatusCode = 400},
            new JException {ExceptionCode = 101, Message = "Invalid file ID.", StatusCode = 400},
            new JException {ExceptionCode = 102, Message = "Nlp exception.", StatusCode = 400},
            new JException {ExceptionCode = 103, Message = "Invalid Nlp event input string.", StatusCode = 400}
        };
    }
}