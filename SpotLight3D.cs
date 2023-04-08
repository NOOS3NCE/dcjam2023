using Godot;
using System;
using System.Threading.Tasks;

public partial class SpotLight3D : Godot.SpotLight3D
{
	// Called when the node enters the scene tree for the first time.
	public float timePassed = 0.0f;	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		LightEnergy = 1f;
	}

	public override void _Process(double delta)
	{
		timePassed += (float)delta;
		GD.Print(timePassed);
		if ((int)timePassed % 2 == 1)
		{
			DelayMethod();
			var random = new Random();
			var value = random.NextDouble(); 
			value = Mathf.Clamp(value, 0.4, 0.8);
			if ((float)value + timePassed % 2 == 1)
				LightEnergy = (float)value;
		}
	}

	private async void DelayMethod()
	{
		await Task.Delay(TimeSpan.FromMilliseconds(200));
	}
}
