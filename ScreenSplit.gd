extends HBoxContainer

@onready
var options := $CenterContainer/MarginContainer/VBoxContainer/MarginContainer/PanelContainer/MarginContainer/Options
@onready
var credits := $CenterContainer/MarginContainer/VBoxContainer/MarginContainer/PanelContainer/MarginContainer/Credits
@onready
var options_button := $Main/VBoxContainer/PanelContainer2/MarginContainer/VBoxContainer/OptionsButton
@onready
var credits_button := $Main/VBoxContainer/PanelContainer2/MarginContainer/VBoxContainer/CreditsButton


func _ready() -> void:
	$CenterContainer.hide()
	credits.hide()
	options.hide()

func _on_options_button_toggled(button_pressed: bool) -> void:
	options_button.disabled = button_pressed
	if button_pressed:
		$CenterContainer.show()
		credits.hide()
		options.show()


func _on_credits_button_toggled(button_pressed: bool) -> void:
	credits_button.disabled = button_pressed
	if button_pressed:
		$CenterContainer.show()
		credits.show()
		options.hide()


func _on_exit_button_pressed():
	credits_button.button_pressed = false
	options_button.button_pressed = false
	$CenterContainer.hide()

