extends RichTextLabel


func _on_meta_clicked(meta) -> void:
	var _error := OS.shell_open(meta)
