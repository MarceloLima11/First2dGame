using Godot;

public partial class Mob : RigidBody2D
{
	public override void _Ready()
	{
		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		string[] mobTypes = animatedSprite2D.SpriteFrames.GetAnimationNames();
		animatedSprite2D.Play(mobTypes[GD.Randi() % mobTypes.Length]);
	}

	public override void _Process(double delta)
	{
	}

	private void OnVisibleOnScreenNotifier2DScreenExited()
	{
		QueueFree();
	}
}
