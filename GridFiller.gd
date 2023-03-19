@tool
extends GridContainer

var test := preload("res://test.tres")

@export_range(8, 30, 1)
var width := 8

@export_range(8, 30, 1)
var height := 8

func _ready() -> void:
	columns = width
	
	for child in get_children():
		remove_child(child)
		child.queue_free()
	
	for y in height:
		for x in width:
			var button := Button.new()
			add_child(button)
			button.set_owner(self)
			button.size_flags_horizontal = Control.SIZE_EXPAND_FILL
			button.size_flags_vertical = Control.SIZE_EXPAND_FILL
			button.add_theme_stylebox_override("normal", test)
			button.set_name(str(y * width + x))
func create_button() -> Button:
	var button := Button.new()
	button.size_flags_horizontal = Control.SIZE_EXPAND_FILL
	button.size_flags_vertical = Control.SIZE_EXPAND_FILL
	button.add_theme_stylebox_override("normal", test)
	
	return button
