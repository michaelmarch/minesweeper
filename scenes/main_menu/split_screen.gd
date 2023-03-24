extends HBoxContainer

@export
var options: VBoxContainer

@export
var credits: VBoxContainer

@export
var options_button: Button

@export
var credits_button: Button


func _ready() -> void:
	$CenterContainer.hide()
	credits.hide()
	options.hide()

func _on_options_button_toggled(p_button_pressed: bool) -> void:
	options_button.disabled = p_button_pressed
	if p_button_pressed:
		$CenterContainer.show()
		credits.hide()
		options.show()
	

func _on_credits_button_toggled(p_button_pressed: bool) -> void:
	credits_button.disabled = p_button_pressed
	if p_button_pressed:
		$CenterContainer.show()
		credits.show()
		options.hide()

func _on_exit_button_pressed():
	credits_button.button_pressed = false
	options_button.button_pressed = false
	$CenterContainer.hide()

func _on_play_button_pressed():
	get_tree().change_scene_to_file("res://scenes/game/game.tscn")


func _on_quit_button_pressed() -> void:
	get_tree().quit()
