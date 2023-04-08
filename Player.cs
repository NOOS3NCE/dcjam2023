using Godot;
using System;
using System.Threading.Tasks;

public partial class Player : Node3D
{
	public int health = 8;

	private Vector3 forward = new Vector3(0,0,2);
	private Vector3 backward = new Vector3(0,0,-2);
	private Vector3 left = new Vector3(2,0,0);
	private Vector3 right = new Vector3(-2,0,0);
	public float moveDistance = 2.0f;
	public bool pauseKeyPresses = false;
	private Transform3D forwardTransform = new Transform3D();
	public bool hasKey = false;

	private void _OnTimerEnd()
	{
		pauseKeyPresses = false;
	}

	public override void _Ready()
	{
		GetNode<AnimatedSprite3D>("HealthBar").Frame = 8;	
	}

	public override void _Process(double delta)
	{ 
		var timer = GetNode<Timer>("Timer");
		Tween tween = CreateTween();
		var rayCast3D = GetNode<RayCast3D>("RayCast3D");
		Vector3 moveDirection = Vector3.Zero;
		if (Input.IsActionPressed("ui_up") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
			}
			pauseKeyPresses = true;
			rayCast3D.TargetPosition = new Vector3(0, 0, 1);
			rayCast3D.ForceRaycastUpdate();

			if (!rayCast3D.IsColliding())
			{	
				moveDirection += Transform.Basis.Z;
				moveDirection = moveDirection * moveDistance;
				tween.TweenProperty(this, "global_transform", new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + moveDirection), 0.2f);
			}
		}
		else if (Input.IsActionPressed("ui_down") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
			}
			pauseKeyPresses = true;
			rayCast3D.TargetPosition = new Vector3(0, 0, -1);
			rayCast3D.ForceRaycastUpdate();
			if (!rayCast3D.IsColliding())
			{
				moveDirection -= Transform.Basis.Z;
				moveDirection *= moveDistance;
				tween.TweenProperty(this, "global_transform", new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + moveDirection), 0.2f);
			}
		}
		else if (Input.IsActionPressed("ui_left") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
			}
			pauseKeyPresses = true;
			rayCast3D.TargetPosition = new Vector3(1, 0, 0);
			rayCast3D.ForceRaycastUpdate();
			if (!rayCast3D.IsColliding())	
			{
				moveDirection += Transform.Basis.X;
				moveDirection *= moveDistance;
				tween.TweenProperty(this, "global_transform", new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + moveDirection), 0.2f);
			}
		}
		else if (Input.IsActionPressed("ui_right") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
			}
			pauseKeyPresses = true;
			rayCast3D.TargetPosition = new Vector3(-1, 0, 0);
			rayCast3D.ForceRaycastUpdate();
			if (!rayCast3D.IsColliding())	
			{
				moveDirection -= Transform.Basis.X;
				moveDirection *= moveDistance;
				tween.TweenProperty(this, "global_transform", new Transform3D(GlobalTransform.Basis, GlobalTransform.Origin + moveDirection), 0.2f);
			}
		}
		else if (Input.IsActionPressed("ui_turn_r") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
			}
			pauseKeyPresses = true;
			tween.TweenProperty(this, "rotation_degrees", RotationDegrees + new Vector3(0.0f, -90.0f, 0.0f), 0.2f);
		}
		else if (Input.IsActionPressed("ui_turn_l") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
			}
			pauseKeyPresses = true;
			tween.TweenProperty(this, "rotation_degrees", RotationDegrees + new Vector3(0.0f, 90.0f, 0.0f), 0.2f);
		}
		else if (Input.IsActionJustPressed("ui_attack") && !pauseKeyPresses)
		{
			if (pauseKeyPresses != true){
				timer.Timeout += _OnTimerEnd;
				timer.Start(0.2f);
				_Attack();
			}
			pauseKeyPresses = true;
		}
	}

	public void _TakeDamage()
	{
		health -= 1;
		GD.Print("HEALTH: ", health);
		Godot.AnimatedSprite3D HealthBar = GetNode<Godot.AnimatedSprite3D>("HealthBar");
		HealthBar.Frame = health;
		GD.Print("FRAME: ", HealthBar.Frame);
		if (health <= 0)
		{
			GD.Print("SHOULD BE DEAD");
			_PlayerDead();
		}
	}

	public void _Attack()
	{
		baddie enemies = GetNode<Area3D>("Area3D - Player").GetOverlappingAreas()[0].GetParent() as baddie;
		enemies._TakeDamage();
		GD.Print("BaddieBox:::: ", enemies);
	}
	
	public void _PickUpKey()
	{
		hasKey = true;	
	}

	public void _PlayerDead()
	{
		GD.Print("You Died");	
	}
}
