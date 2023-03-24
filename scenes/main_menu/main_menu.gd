extends PanelContainer

@export
var width_spin_box: SpinBox

@export
var height_spin_box: SpinBox

@export
var mine_count_spin_box: SpinBox


func _on_play_button_pressed() -> void:
	Settings.width = int(width_spin_box.value)
	Settings.height = int(height_spin_box.value)
	Settings.mine_count = int(mine_count_spin_box.value)
	get_tree().change_scene_to_file("res://scenes/game/game.tscn")

func _on_mine_count_changed(p_value: float) -> void:
	var _size := int(width_spin_box.value * height_spin_box.value)
	if int(p_value) >= _size:
		mine_count_spin_box.value = _size - 1
