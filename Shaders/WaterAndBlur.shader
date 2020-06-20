shader_type canvas_item;

uniform vec2 target = vec2(0.5);
uniform float intensity: hint_range(0f, 1f);

uniform vec4 blueTint : hint_color;

uniform vec2 spriteScale;

float rand(vec2 coord)
{
	return fract(sin(dot(coord, vec2(12.9898, 78.233)))* 43758.5453123);
}
	
float noise(vec2 coord)
{
	vec2 i = floor(coord);
	vec2 f = fract(coord);
	//4 corners of a rect surrounding our point
	float a = rand(i);
	float b  = rand(i + vec2(1.0, 0.0));
	float c = rand(i + vec2(0.0, 1.0));
	float d = rand(i + vec2(1.0, 1.0));
	
	vec2 cubic = f * f * (3.0 - 2.0 * f);
	
	return mix(a, b, cubic.x)+ (c - a) * cubic.y * (1.0 - cubic .x ) + (d - b) * cubic.x * cubic.y;
}

void fragment()
{
		//Blur section
	vec3 blurFinal = textureLod(SCREEN_TEXTURE, SCREEN_UV, 6f * intensity  * (distance(target, UV) / sqrt(2))).rgb;
	//Water color and deformation
	vec2 noisecoord1 = UV * spriteScale;
	vec2 noisecoord2 = UV * spriteScale + 4.0;
	
	vec2 motion1 = vec2(TIME * 0.3, TIME * -0.4);
	vec2 motion2 = vec2(TIME * 0.1, TIME * 0.5);
	
	vec2 distort1 = vec2(noise(noisecoord1 + motion1), noise(noisecoord2 + motion1)) - vec2(0.5);
	vec2 distort2 = vec2(noise(noisecoord1 + motion2), noise(noisecoord2 + motion2)) - vec2(0.5);
	
	vec2 distort_sum = (distort1 + distort2) /320.0;
		
	vec4 waterFinal = textureLod(SCREEN_TEXTURE, SCREEN_UV + distort_sum, 0.0);
	
	waterFinal = mix(waterFinal, blueTint, 0.3);
	waterFinal.rgb = mix(vec3(0.5), waterFinal.rgb, 1.4);
	
	vec4 finalMix = mix(vec4(blurFinal, 1), waterFinal,0.3);
	
	COLOR = finalMix;

}