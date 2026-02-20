using Godot;
using Godot.Collections;

public partial class SaveHandler : Node
{
	static Dictionary Data = new Dictionary();

	enum SaveDataSections {
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
			Dictionary<Error, string> SaveError = Save();
			//if (SaveError != Utils.CheckErrorResponse(SaveError)
			GetTree().Quit(0);
		}
        base._Notification(what);
    }


	static public Dictionary<Error, string> CreateNewSaveFile()
	{
		try
		{
			Data = (Dictionary)new Dictionary<SaveDataSections, Dictionary<string, Variant>>
			{
				{SaveDataSections.Options, new Dictionary<string, Variant>{
					{"Volume", new Dictionary<string, double> {
						{"Master", 0.0f},
						{"Sounds", 0.0f},
						{"Music", 0.0f}
					}},
					{"Fullscreen", true},
					{"MouseParticles", true},
					{"ErrorPopups", true}
				}}
			};
		}
		catch
		{
			return Utils.MakeError(Error.ScriptFailed, "Failed to create dictionary for user data, caught and ignored but your options wont save.");
		}
		if (Data != null)
		{
			Dictionary<Error, string> CheckSave = Save();
			if (Utils.CheckErrorResponse(CheckSave)) return Utils.MakeError(Error.Ok);
			else return CheckSave;
		}
		else
		{
			return Utils.MakeError(Error.InvalidData, "New User data has returned null for an unknown reason.", true);
		}
	}

	/// <summary>
	/// Save the game to the user folder under the file name `SaveData.save`
	/// </summary>
	/// <returns>A dictionary dictating whether it worked or not.</returns>
	static public Dictionary<Error, string> Save()
	{
		// Open save file for writing
		using var saveFile = FileAccess.Open("user://SaveData.save", FileAccess.ModeFlags.Write);
		// Check if the file is null or not
		if (saveFile != null)
		{
			// Parse data into a json string we can save to a file.
			string parsedjson = Json.Stringify(Data);
			// Check the string, if its null we throw an error.
			if (parsedjson == null) return new Dictionary<Error, string> { { Error.ParseError, "Failed to parse save data json, could the Data dictionary be null?" } };
			// Attempt to write save data to file, if failed returns an error code, if success then return Ok and NCN (No Comment Needed).
			if (saveFile.StoreString(parsedjson)) return new Dictionary<Error, string> { { Error.Ok, "NCN" } }; else return new Dictionary<Error, string> { { Error.FileCantWrite, "Failed to write save data to file, is it currently in use?" } }; ;
		}
		// If save is null then we throw an error message.
		else return new Dictionary<Error, string> { { Error.FileCantOpen, "Failed to open the save data file, could be possibly missing. (Which is weird since its supposed to be written)" } };
	}
}
