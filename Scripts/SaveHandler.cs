using Godot;
using Godot.Collections;

public partial class SaveHandler : Node
{
	/// <summary>
	/// Holy shit this is long as fuck - H
	/// </summary>
	static Dictionary<SaveDataSections, Dictionary<string, Variant>> Data = new Dictionary<SaveDataSections, Dictionary<string, Variant>>();

	/// <summary>
	/// Each section of save data as an enum
	/// </summary>
	public enum SaveDataSections
	{
		Options
	}

    // Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetTree().AutoAcceptQuit = false;
	}

    public override void _Notification(int what)
    {
		if (what == NotificationWMCloseRequest)
		{
			ErrorHandler SaveError = Save();
			if (!Utils.CheckErrorResponse(SaveError)) OS.Alert("An unknown error has occured during saving.\nYour save data will not update.", "Save Error");
			GetTree().Quit(0);
		}
        base._Notification(what);
    }

	/// <summary>
	/// Creates a new save file
	/// </summary>
	/// <returns>An error code of whether it failed or completed</returns>
	static public ErrorHandler CreateNewSaveFile()
	{
		try
		{
			Data = new Dictionary<SaveDataSections, Dictionary<string, Variant>>
			{
				{SaveDataSections.Options, new Dictionary<string, Variant>{
					// Contains all audio buses
					{ "Volume", new Dictionary<string, double> {
						{ "Master", 0.0f},
						{ "Sounds", 0.0f},
						{ "Music", 0.0f}
					}},
					// Whether or not to be played in fullscreen, im too lazy to do fullscreen borderless
					{ "Fullscreen", true},
					// Whether particles will follow the mouse when it moves or not
					{ "MouseParticles", true},
					// Whether popups will showup on screen when an error occurs.
					{ "ErrorPopups", true}
				}}
			};
		}
		catch
		{
			return Utils.MakeError(Error.ScriptFailed, "Failed to create dictionary for user data, caught and ignored but your options wont save.");
		}
		if (Data != null)
		{
			ErrorHandler CheckSave = Save();
			if (Utils.CheckErrorResponse(CheckSave)) return Utils.MakeError(Error.Ok);
			else return CheckSave;
		}
		else
		{
			return Utils.MakeError(Error.InvalidData, "New User data has returned null for an unknown reason.", true);
		}
	}

	/// <summary>
	/// Set a specific value in a specific catagory in save data
	/// </summary>
	/// <param name="Section">Which specific section of save data to set the value in</param>
	/// <param name="key">The save data to set the value of</param>
	/// <param name="value">The value to set the save data to in the first place</param>
	/// <returns>The value of the savedata or an error code (Please implement a check for it)</returns>
	static public ErrorHandler SetSave(SaveDataSections Section, string key, dynamic value)
	{
		if (Section == SaveDataSections.Options)
		{
			Dictionary<string, Variant> Catagory = Data[SaveDataSections.Options];
			try
			{
				Catagory[key] = value;
				return Utils.MakeError(Error.Ok, Utils.DefaultComment);
			}
			catch
			{
				return Utils.MakeError(Error.CantAcquireResource, "Cannot find dictionary catagory, did you feed in an invalid enum?");
			}
		}
		else return Utils.MakeError(Error.CantAcquireResource, "Cannot find dictionary catagory, did you feed in an invalid enum?");
	}

	/// <summary>
	/// Save the game to the user folder under the file name `SaveData.save`
	/// </summary>
	/// <returns>A dictionary dictating whether it worked or not.</returns>
	static public ErrorHandler Save()
	{
		// Open save file for writing
		using var saveFile = FileAccess.Open("user://SaveData.save", FileAccess.ModeFlags.Write);
		// Check if the file is null or not
		if (saveFile != null)
		{
			// Parse data into a json string we can save to a file.
			string parsedjson = Json.Stringify(Data);
			// Check the string, if its null we throw an error.
			if (parsedjson == null) return Utils.MakeError(Error.ParseError, "Failed to parse save data json, could the Data dictionary be null?", false);
			// Attempt to write save data to file, if failed returns an error code, if success then return Ok and NCN (No Comment Needed).
			if (saveFile.StoreString(parsedjson)) return Utils.MakeError(Error.Ok, "NCN"); else return Utils.MakeError(Error.FileCantWrite, "Failed to write save data to file, is it currently in use?", false);
		}
		// If save is null then we throw an error message.
		else return Utils.MakeError(Error.FileCantOpen, "Failed to open the save data file, could be possibly missing. (Which is weird since its supposed to be written)");
	}
}
