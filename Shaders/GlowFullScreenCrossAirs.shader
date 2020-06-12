shader_type canvas_item;

uniform vec2 target = vec2(0.5);
uniform float intensity: hint_range(0f, 1f);

void fragment()
{
	COLOR = vec4(1f);
	float x = SCREEN_PIXEL_SIZE.x / distance(vec2(target.x, UV.y),UV);
	float y = SCREEN_PIXEL_SIZE.x / distance(vec2(UV.x, target.y),UV);
	COLOR.a = (x+y) * intensity;
}