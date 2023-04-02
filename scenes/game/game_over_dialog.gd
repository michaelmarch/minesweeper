extends VBoxContainer


func _ready() -> void:
	# Just like popups it's hidden by default
	visible = false

func show_centered(p_size: Vector2i) -> void:
	size = p_size
	position = (get_tree().root.size - p_size) / 2
	
	visible = true


func _on_exit_button_pressed() -> void:
	visible = false
