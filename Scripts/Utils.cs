using System.Linq.Expressions;
using Godot;
using Godot.Collections;

public class Utils
{
    public static string DefaultComment = "NCN";

    /// <summary>
    /// Create an error dictionary with an error code and an optional comment, the error code MUST be added to parameters or else the function will fail and probably make things worse.
    /// </summary>
    /// <param name="errorCode">The error code from the Godot Error Enum</param>
    /// <param name="errorMsg">The optional message to display when the error occurs</param>
    /// <param name="crash">Whether to crash the game or continue running</param>
    /// <returns>ErrorHandler</returns>
    static public ErrorHandler MakeError(Error errorCode, string errorMsg = "", bool crash = false)
    {
        if (errorMsg == "") errorMsg = DefaultComment;
        if (crash)
        {
            OS.Alert(errorMsg + "\n\nError Code:" + errorCode.ToString(), "Unknown Error Occured");
        }
        return new ErrorHandler(errorCode, errorMsg, true);
    }

    /// <summary>
    /// Check the error for whether its perfectly fine to continue or not.
    /// </summary>
    /// <param name="response">The Error dictionary, usually called using `MakeError()`</param>
    /// <returns>A true or false boolean on whether there was an error or not.</returns>
    static public bool CheckErrorResponse(ErrorHandler response)
    {
        if (response == new ErrorHandler(Error.Ok, DefaultComment)) return true;
        else return false;
    }
}
/// <summary>
/// Creates an error to store data in, such as an error code and comment
/// </summary>
public class ErrorHandler
{
    public Error ErrorCode;
    public string Comment;
    /// <summary>
    /// Creates the error
    /// </summary>
    /// <param name="code">Sets the error code to the parameter</param>
    /// <param name="comment">Sets the comment to the parameter</param>
    /// <param name="log">Whether to log the error to the console or not</param>
    public ErrorHandler(Error code, string comment, bool log = false)
    {
        ErrorCode = code;
        Comment = comment;
        if (log && code != Error.Ok && comment != Utils.DefaultComment) GD.PrintRich("[color=gold]{Hazel Error Handler}[color=white] - [color=orange]" + Comment);
    }
}