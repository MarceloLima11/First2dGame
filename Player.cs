using Godot;

public partial class Player : Area2D
{
	[Signal]
	public delegate void HitEventHandler();
	[Export]
	public int Speed { get; set; } = 400;
	public Vector2 ScreenSize;

	public override void _Ready()
	{ ScreenSize = GetViewportRect().Size; }

	public override void _Process(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		if (Input.IsActionPressed("move_right")) velocity.X += 1;

		if (Input.IsActionPressed("move_left")) velocity.X -= 1;

		if (Input.IsActionPressed("move_down")) velocity.Y += 1;

		if (Input.IsActionPressed("move_up")) velocity.Y -= 1;

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{ animatedSprite2D.Stop(); }

		Position += velocity * (float)delta;
		Position = new Vector2(
			x: Mathf.Clamp(Position.X, 0, ScreenSize.X),
			y: Mathf.Clamp(Position.Y, 0, ScreenSize.X)
		);

		if (velocity.X < 0) animatedSprite2D.FlipH = true;
		else animatedSprite2D.FlipH = false;

	}

	public void Start(Vector2 position)
	{
		Position = position;
		Show();
		GetNode<CollisionShape2D>("CollisionShape2D").Disabled = false;
	}

	public void OnBodyEntered(PhysicsBody2D body)
	{
		Hide();
		EmitSignal(SignalName.Hit);
		GetNode<CollisionShape2D>("CollisionShape2D").SetDeferred(CollisionShape2D.PropertyName.Disabled, true);
	}
}
