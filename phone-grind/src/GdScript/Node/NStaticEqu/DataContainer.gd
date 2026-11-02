extends HBoxContainer

@onready var textureRect: TextureRect = $Texture

@export var texture:Texture

func _ready() -> void:
	if texture != null:
		textureRect.texture = texture;
