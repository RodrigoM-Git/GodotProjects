using Godot;
using System;

public class Icon : Sprite
{
    // Declare member variables here. Examples:
    private int speed = 400;
    private float angularSpeed = Mathf.Pi;

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        
    }

    //  // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {

        // left right movement
        var direction = 0;
        if (Input.IsActionPressed("ui_left"))
        {
            direction = -1;
        }
        if (Input.IsActionPressed("ui_right"))
        {
            direction = 1;
        }
        Rotation += angularSpeed * direction * delta;

        // up movement
        var velocity = Vector2.Zero;
        if (Input.IsActionPressed("ui_up"))
        {
            velocity = Vector2.Up.Rotated(Rotation) * speed;
        }
        if (Input.IsActionPressed("ui_down"))
        {
            velocity = Vector2.Down.Rotated(Rotation) * speed;
        }

        Position += velocity * delta;
    }
}
