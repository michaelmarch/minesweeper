extends RichTextLabel


func _on_meta_clicked(meta) -> void:
	OS.shell_open(meta)
