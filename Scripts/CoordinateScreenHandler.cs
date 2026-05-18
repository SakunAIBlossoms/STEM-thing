using Godot;

public partial class CoordinateScreenHandler : Control
{
	enum LookingDirection
	{
		Forward,
		Left,
		Right,
		Back
	}
	private Sprite2D ShipSprite;
	private LookingDirection PreviousDirection = LookingDirection.Forward;
	private LookingDirection CurrentDirection = LookingDirection.Forward;

	public override void _Ready()
	{
		ShipSprite = GetNode<Sprite2D>("Ship");
	}
	public override void _Process(double delta)
	{
		if (CurrentDirection == LookingDirection.Forward) ShipSprite.RotationDegrees = (float)Mathf.Lerp(ShipSprite.RotationDegrees, 0, 12 * delta);
		if (CurrentDirection == LookingDirection.Back) ShipSprite.RotationDegrees = (float)Mathf.Lerp(ShipSprite.RotationDegrees, 180, 12 * delta);
		if (CurrentDirection == LookingDirection.Left) ShipSprite.RotationDegrees = (float)Mathf.Lerp(ShipSprite.RotationDegrees, 270, 12 * delta);
		if (CurrentDirection == LookingDirection.Right) ShipSprite.RotationDegrees = (float)Mathf.Lerp(ShipSprite.RotationDegrees, 90, 12 * delta);
	}
    public override void _UnhandledKeyInput(InputEvent @event)
	{
		if (@event is InputEventKey)
		{
			switch (@event.AsText())
			{
				case "W":
					CurrentDirection = LookingDirection.Forward;
					break;
				case "S":
					CurrentDirection = LookingDirection.Back;
					break;
				case "A":
					CurrentDirection = LookingDirection.Left;
					break;
				case "D":
					CurrentDirection = LookingDirection.Right;
					break;
			}
		}
	}
}
