using Godot;
using System;
using System.Threading.Tasks;

public partial class baddie : Node3D
{
	public Sprite3D sprite3D;
	public Area3D baddieBox;
	public Tween tween;
	public Node3D player;
	public Node3D level;
	public WorldEnvironment worldEnv;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		sprite3D = GetNode<Sprite3D>("Sprite3D");
		// sprite3D.GetNode<GeometryInstance3D>("GeometryInstance3D").Transparency = 1.0f;
		sprite3D.Transparency = 1.0f;
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		level = GetNode("/root/Game").GetNode<Node3D>("test_level");
		worldEnv = level.GetNode<WorldEnvironment>("WorldEnvironment");
		player = GetNode("/root/Game").GetNode<Node3D>("Player");
		Area3D playerBox = player.GetNode<Area3D>("Area3D - Player");
		tween = CreateTween();
		// Environment bleak = new Environment();
		// bleak.AdjustmentEnabled = true;
		// bleak.AdjustmentSaturation = 0.0f;
		// bleak.AdjustmentBrightness = 2.5f;

		baddieBox = GetNode<Area3D>("Area3D - Baddie");
		if (baddieBox.OverlapsArea(playerBox))
		{
			tween.TweenProperty(sprite3D, "transparency", 0.0f, 0.8f);
			 for (int i = 0; i < 3; i++)
			 {
				// GD.Print("GOTEM", worldEnv.Environment.AdjustmentEnabled);
				// worldEnv.Environment = bleak;	
				DelayMethod();
				// worldEnv.Environment.AdjustmentEnabled = false;	
			 }
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
