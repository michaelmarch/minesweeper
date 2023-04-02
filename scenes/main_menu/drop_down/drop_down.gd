extends OptionButton

# Handles custom drop button that displays options list of a OptionButton

@export
var drop_button: Button


func _ready() -> void:
	# Fixes some issues that I had with syncing both buttons
	get_popup().exclusive = true
	
	var _error := drop_button.toggled.connect(_on_drop_button_toggled)
	_error = get_popup().popup_hide.connect(_on_popup_hide)

func _on_drop_button_toggled(p_button_pressed: bool) -> void:
	if p_button_pressed and not get_popup().visible:
		show_popup()
	
	# Pretty sure this actually emits OptionButton.toggled signal
	set_pressed_no_signal(p_button_pressed)

func _toggled(p_button_pressed: bool) -> void:
	# In case of a normal button no signals are emitted
	drop_button.set_pressed_no_signal(p_button_pressed)

func _on_popup_hide() -> void:
	drop_button.set_pressed_no_signal(false)
