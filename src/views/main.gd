extends Control

var data_to_send := FileAccess.get_file_as_string("res://StampGameDecisions.json")
var json_string := JSON.stringify(data_to_send)
  # Save data
  # ...
  # Retrieve data
var json := JSON.new()
var error := json.parse(json_string)
 
func _ready() -> void:
    
    if error == OK:
        var data_received :Variant = json.data
        if (typeof(data_received) == TYPE_DICTIONARY):
            print(data_received) # Prints array
    else:
        print("JSON Parse Error: ", json.get_error_message(), " in ", json_string, " at line ", json.get_error_line())