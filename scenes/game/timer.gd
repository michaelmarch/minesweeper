extends Label

func _on_timer_timeout() -> void:
	var values := text.split(':', false, 2)
	
	var minutes := int(values[0])
	var seconds := int(values[1])
	
	if minutes >= 99 and seconds >= 59:
		return
	
	seconds += 1
	
	if seconds >= 60:
		minutes += (seconds / 60)
		seconds %= 60
	
	text = "%s:%s" % [str(minutes).pad_zeros(2), str(seconds).pad_zeros(2)]
