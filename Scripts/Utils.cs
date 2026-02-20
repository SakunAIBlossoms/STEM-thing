using Godot;
using Godot.Collections;

public partial class Utils : Node
{
    static string DefaultComment = "NCN";

    /// <summary>
    /// Create an error dictionary with an error code and an optional comment, the error code MUST be added to parameters or else the function will fail and probably make things worse.
    /// </summary>
    /// <param name="errorCode">The error code from the Godot Error Enum</param>
    /// <param name="errorMsg">The optional message to display when the error occurs</param>
    /// <param name="crash">Whether to crash the game or continue running</param>
    /// <returns>A dictionary of the error and its comment as a string.</returns>
    static public Dictionary<Error, string> MakeError(Error errorCode, string errorMsg = "", bool crash = false)
    {
        if (errorMsg == "") errorMsg = DefaultComment;
        if (crash)
        {
            OS.Alert(errorMsg + "\n\nError Code:" + errorCode.ToString(), "Unknown Error Occured");
        }
        return new Dictionary<Error, string> { { errorCode, errorMsg } };
    }

    /// <summary>
    /// Check the error for whether its perfectly fine to continue or not.
    /// </summary>
    /// <param name="response">The Error dictionary, usually called using `MakeError()`</param>
    /// <returns>A true or false boolean on whether there was an error or not.</returns>
    static public bool CheckErrorResponse(Dictionary<Error, string> response)
    {
        if (response == new Dictionary<Error, string> { { Error.Ok, DefaultComment } }) return true;
        else return false;
    }
}
