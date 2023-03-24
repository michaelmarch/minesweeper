class_name Tile
extends Button

var tile_position: Vector2i

var tile_type := TileType.INFO
var value := 0


enum TileType {
	MINE,
	INFO
}

func _gui_input(event: InputEvent) -> void:
	if not event is InputEventMouseButton:
		return
	
	if not event.is_pressed():
		return
	
	if not event.button_index == MOUSE_BUTTON_RIGHT:
		return
	
	if len(text) > 0:
		text = ""
	else:
		text = "B"
