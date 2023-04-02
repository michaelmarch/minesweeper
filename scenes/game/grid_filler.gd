#class_name GridFiller
#extends GridContainer
#
#signal tile_pressed(button: Tile)
#
#
#var tile_scene: PackedScene = preload("res://scenes/game/tile/tile.tscn")
#var tiles: Array[Tile] = []
#
#
#func _enter_tree() -> void:
#	get_tree().get_root().min_size = Vector2i(720, 720)
#
#func _ready() -> void:
#	for child in get_children():
#		remove_child(child)
#		child.queue_free()
#
#	columns = Settings.width
#
#	for y in Settings.height:
#		for x in Settings.width:
#			var tile: Tile = tile_scene.instantiate()
#			tile.name = str(y * Settings.width + x)
#			add_child(tile)
#			tile.owner = get_tree().edited_scene_root
#			tile.add_theme_font_size_override("font_size", 34 - Settings.width)
#			tile.tile_position = Vector2i(x, y)
#			tile.pressed.connect(_tile_pressed.bind(tile))
#			tiles.append(tile)
#
#	#seed(2)
#	var mines_left := Settings.mine_count
#	while mines_left > 0:
#		var random_index := randi() % len(tiles)
#
#		if tiles[random_index].tile_type == Tile.TileType.INFO:
#			tiles[random_index].tile_type = Tile.TileType.MINE
#			tiles[random_index].text = "B"
#			mines_left -= 1
#
#
#func _tile_pressed(tile: Tile) -> void:
#	tile_pressed.emit(tile)
