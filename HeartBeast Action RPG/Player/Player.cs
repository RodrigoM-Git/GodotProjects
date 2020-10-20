using Godot;
using System;

public class Player : KinematicBody2D
{
    // constant variables
    private const int ACCELERATION = 500;
    private const int MAX_SPEED = 80;
    private const int FRICTION = 500;
    
    // variables
    private Vector2 velocity = Vector2.Zero;
    private AnimationPlayer animationPlayer = null;
    
    // runs on load
    public override void _Ready()
    {
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    // runs every frame
    public override void _PhysicsProcess(float delta) 
    {
        // player movement
        var inputVector = Vector2.Zero;

        // checks for direction
        inputVector.x = Input.GetActionStrength("move_right") - Input.GetActionStrength("move_left");
        inputVector.y = Input.GetActionStrength("move_down") - Input.GetActionStrength("move_up");
        
        // normalizes input to prevent double speed when two directions are entered
        inputVector = inputVector.Normalized();

        // set velocity of player
        if (inputVector != Vector2.Zero)
        {
            // running animation
            if(inputVector.x > 0)
                animationPlayer.Play("RunRight");
            else
                animationPlayer.Play("RunLeft");
            
            velocity = velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
        }
        else
        {
            // stop moving and run idle animation
            animationPlayer.Play("IdleRight"); 
            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }
            

        velocity = MoveAndSlide(velocity);

    }
    
}
