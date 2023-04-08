using Godot;
using System;

public partial class chest : Node3D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
	public void _PickUpKey()
	{
		Node3D chest = GetNode<Area3D>("Area3D - Player").GetOverlappingAreas()[0].GetParent() as Node3D;
		if(chest != null)
			hasKey = true;	
		
		GD.Print("HAS KEY", hasKey);
	}
}
