using Godot;
using System;
using System.Threading.Tasks;

public partial class baddie : Node3D
{
	public Sprite3D sprite3D;
	public Area3D baddieBox;
	public Tween tween;
	public Player player;
	public Node3D level;
	public WorldEnvironment worldEnv;
	private bool isAttacking = false;
	public int health = 4;
	Timer timer; 

	public override void _Ready()
	{
		sprite3D = GetNode<Sprite3D>("Sprite3D");
		sprite3D.Transparency = 1.0f;
		timer = GetNode<Timer>("Timer");
		timer.OneShot = true;
	}
	public void _TakeDamage()
	{
		health -= 1;
		if (health <= 0)
		{
			QueueFree();
		}
	}
	private void _OnTimerEnd()
	{
		Player player = GetNode<Player>("/root/Game/Player");
		var random = new Random();
		var value = random.NextDouble(); 
		if (value > .7)
		{
			GD.Print("HIT::::::::::::::", value);
			player._TakeDamage();
		}
		isAttacking = false;
	}

	private void _StartAttack()
	{
		timer.Timeout += _OnTimerEnd;
		timer.Start(1.0f);
		tween.TweenProperty(sprite3D, "transparency", 0.0f, 0.8f);
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		level = GetNode("/root/Game").GetNode<Node3D>("test_level");
		worldEnv = level.GetNode<WorldEnvironment>("WorldEnvironment");
		player = GetNode<Player>("/root/Game/Player");
		Area3D playerBox = player.GetNode<Area3D>("Area3D - Player");
		tween = CreateTween();

		baddieBox = GetNode<Area3D>("Area3D - Baddie");
		if (baddieBox.OverlapsArea(playerBox) && !isAttacking && timer.IsStopped())
		{
			isAttacking = true;
			_StartAttack();
		}
	}

	private async void DelayMethod()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(1000));
		GD.Print("DELAYED");
	}

	public void _on_area_3d_body_entered()
	{
		GD.Print("help");
	}
}
