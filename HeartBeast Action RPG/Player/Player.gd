extends KinematicBody2D

# constant variables
const ACCELERATION = 50
const MAX_SPEED = 200
const FRICTION = 400

# variables
var velocity = Vector2.ZERO

# runs every frame
func _physics_process(delta):
	
	# player movement code
	var input_vector = Vector2.ZERO
	
	# checks for direction
	input_vector.x = Input.get_action_strength('ui_right') - Input.get_action_strength('ui_left')
	input_vector.y = Input.get_action_strength('ui_down') - Input.get_action_strength('ui_up')
	
	# normalizes input to normal (prevents double speed when two directions are pressed)
	input_vector = input_vector.normalized()
	
	# sets direction and speed to velocity (delta compensates frame lag)
	if(input_vector != Vector2.ZERO):
		velocity += input_vector * ACCELERATION * delta
		velocity = velocity.clamped(MAX_SPEED * delta)
	else:
		velocity = velocity.move_toward(Vector2.ZERO, FRICTION * delta)
		
	# sets velocity to player
	move_and_collide(velocity) 
