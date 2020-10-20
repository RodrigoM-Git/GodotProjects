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
    private AnimationTree animationTree;
    private AnimationNodeStateMachinePlayback animationState;
    
    // runs on load
    public override void _Ready()
    {
        animationTree = GetNode<AnimationTree>("AnimationTree");
        animationState = (AnimationNodeStateMachinePlayback) animationTree.Get("parameters/playback");
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
            animationTree.Set("parameters/Idle/blend_position", inputVector);
            animationTree.Set("parameters/Run/blend_position", inputVector);
            animationState.Travel("Run");
            velocity = velocity.MoveToward(inputVector * MAX_SPEED, ACCELERATION * delta);
        }
        else
        {
            animationState.Travel("Idle");
            velocity = velocity.MoveToward(Vector2.Zero, FRICTION * delta);
        }
            
        
            

        velocity = MoveAndSlide(velocity);

    }
    
}
