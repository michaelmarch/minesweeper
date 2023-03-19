@tool
extends GridContainer

var stylebox := load("stylebox.tres")
var collapsed := load("collapsed.tres")
var width := 9
var height := 9

var buttons := []


enum TileType {
	EMPTY,
	BOMB,
	INFO
}

func _ready() -> void:
	columns = width
	for child in get_children():
		remove_child(child)
		child.queue_free()
	
	for y in height:
		buttons.append([])
		for x in width:
			var button := Button.new()
			var button_index := y * width + x
			button.set_name(str(button_index))
			add_child(button)
			button.set_owner(self)
			button.custom_minimum_size = Vector2(20, 20)
			button.size_flags_horizontal = Control.SIZE_EXPAND
			button.size_flags_vertical = Control.SIZE_EXPAND
			button.add_theme_stylebox_override("normal", stylebox)
			button.add_theme_stylebox_override("pressed", collapsed)
			button.add_theme_stylebox_override("disabled", collapsed)
			button.set_meta("tile_type", random_tile_type())
			button.set_meta("x", x)
			button.set_meta("y", y)
			button.alignment = HORIZONTAL_ALIGNMENT_CENTER
			button.toggle_mode = true
			button.toggled.connect(tile_toggled.bind(button))
			
			buttons[y].append(button)


func tile_toggled(button_pressed: bool, button: Button) -> void:
	button.disabled = true
	collapse_currenct(button)
	collapse_neighbors(button)
	

func random_tile_type() -> TileType:
	randomize()
	
	var r := randi() % 100
	
	if r >= 0 and r < 60:
		return TileType.EMPTY
	elif r >= 60 and r <= 93:
		return TileType.INFO
	
	return TileType.BOMB;

func collapse_neighbors(button: Button):
	
	var neighbors := get_neighbors(width, height, button)

func collapse_currenct(button: Button):
	var tile_type: TileType = button.get_meta("tile_type")
	if tile_type == TileType.BOMB:
		print("GAME OVER")
#		get_tree().paused = true
	elif tile_type == TileType.INFO:
		button.text = str(randi() % 5)
	
func get_neighbors(board_width: int, board_height: int, button: Button) -> Array[Button]:
	var point := Vector2i(button.get_meta("x"), button.get_meta("y"))
	var neighbors: Array[Button] = []
	
	# Top-Left
	if point.x - 1 >= 0 and point.y - 1 >= 0:
		#neighbors.append(Vector2i(point.x - 1, point.y - 1))
		neighbors.append(buttons[point.x - 1][point.y - 1])
	
	# Top
	if point.y - 1 >= 0:
		neighbors.append(buttons[point.x][point.y - 1])
	
	# Top-Right
	if point.x + 1 < board_width and point.y - 1 >= 0:
		neighbors.append(buttons[point.x + 1][point.y - 1])
	
	# Left
	if point.x - 1 >= 0:
		neighbors.append(buttons[point.x - 1][point.y])
	
	# Right
	if point.x + 1 < board_width:
		neighbors.append(buttons[point.x + 1][point.y])
	
	# Bottom-Left
	if point.x - 1 >= 0 and point.y + 1 < board_height:
		neighbors.append(buttons[point.x - 1][point.y + 1])
	
	# Bottom
	if point.y + 1 < board_height:
		neighbors.append(buttons[point.x][point.y + 1])
	
	# Bottom-Right
	if point.x + 1 < board_width and point.y + 1 < board_height:
		neighbors.append(buttons[point.x + 1][point.y + 1])
		
	return neighbors
	








