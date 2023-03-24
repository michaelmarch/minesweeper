extends PanelContainer

@export
var _grid_filler: GridFiller

var _tiles: Array[Tile]

var collapsed_tiles := 0

var clicks := 0

func _ready() -> void:
	_tiles = _grid_filler.tiles

func _on_tile_pressed(tile: Tile) -> void:
	match tile.tile_type:
		Tile.TileType.MINE:
			$GameOverDialog.popup_centered()
		Tile.TileType.INFO:
			collapse_neighbors(tile)
	
	clicks += 1
	
	if collapsed_tiles + Settings.mine_count == len(_tiles):
		$VictoryDialog.popup_centered()

func collapse_neighbors(tile: Tile):
	if tile.disabled:
		return
	
	tile.disabled = true
	collapsed_tiles += 1
	
	var mines_count := 0
	for neighbor_tile in get_neighbors(tile.tile_position):
		if neighbor_tile.tile_type == Tile.TileType.MINE:
			mines_count += 1
	
	if mines_count > 0:
		tile.text = str(mines_count)
		return
		
	for neighbor_tile in get_neighbors(tile.tile_position):
		collapse_neighbors(neighbor_tile)

func get_neighbors(p_pos: Vector2i) -> Array[Tile]:
	var neighbors: Array[Tile] = []
	
	# Top-Left
	if p_pos.x - 1 >= 0 and p_pos.y - 1 >= 0:
		neighbors.append(_tiles[to_1d_index(p_pos.x - 1, p_pos.y - 1)])

	# Top-Right
	if p_pos.x + 1 < Settings.width and p_pos.y - 1 >= 0:
		neighbors.append(_tiles[to_1d_index(p_pos.x + 1, p_pos.y - 1)])
	
	# Bottom-Left
	if p_pos.x - 1 >= 0 and p_pos.y + 1 < Settings.height:
		neighbors.append(_tiles[to_1d_index(p_pos.x - 1, p_pos.y + 1)])
	
	# Bottom-Right
	if p_pos.x + 1 < Settings.width and p_pos.y + 1 < Settings.height:
		neighbors.append(_tiles[to_1d_index(p_pos.x + 1, p_pos.y + 1)])
	
	# Top
	if p_pos.y - 1 >= 0:
		neighbors.append(_tiles[to_1d_index(p_pos.x, p_pos.y - 1)])
	
	# Left
	if p_pos.x - 1 >= 0:
		neighbors.append(_tiles[to_1d_index(p_pos.x - 1, p_pos.y)])
	
	# Right
	if p_pos.x + 1 < Settings.width:
		neighbors.append(_tiles[to_1d_index(p_pos.x + 1, p_pos.y)])
	
	# Bottom
	if p_pos.y + 1 < Settings.height:
		neighbors.append(_tiles[to_1d_index(p_pos.x, p_pos.y + 1)])
	
	return neighbors

func to_1d_index(x: int, y: int) -> int:
	return y * Settings.width + x


func _on_game_over_dialog_canceled() -> void:
	get_tree().change_scene_to_file("res://scenes/main_menu/main_menu.tscn")


func _on_game_over_dialog_confirmed() -> void:
	get_tree().reload_current_scene()
