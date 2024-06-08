shader_type canvas_item;
render_mode blend_mix;

uniform bool enabled = false;
uniform vec4 tint : source_color = vec4(1.000000, 1.000000, 1.000000, 1.000000);



void fragment() {
// BooleanParameter:3
	bool n_out3p0 = enabled;


// ColorParameter:2
	vec4 n_out2p0 = tint;


// Input:5
	vec4 n_out5p0 = COLOR;


	vec3 n_out4p0;
// If:4
	float n_in4p1 = 1.00000;
	float n_in4p2 = 0.00001;
	if(abs((n_out3p0 ? 1.0 : 0.0) - n_in4p1) < n_in4p2)
	{
		n_out4p0 = vec3(n_out2p0.xyz);
	}
	else if((n_out3p0 ? 1.0 : 0.0) < n_in4p1)
	{
		n_out4p0 = vec3(n_out5p0.xyz);
	}
	else
	{
		n_out4p0 = vec3(n_out5p0.xyz);
	}


// Output:0
	COLOR.rgb = n_out4p0;


}
