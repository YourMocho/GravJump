Shader "Gradient/ScreenSpaceOverlay" {
	Properties{
		_Color("Top Color", Color) = (1,1,1,1)
		_Color2("Bottom Color", Color) = (1,1,1,1)
		_Color3("Main Color", Color) = (1,1,1,1)
		_X("X multi", Float) = 0.5
		_Y("Y multi", Float) = 0.5
	_MainTex ("Base (RGB)", 2D) = "white" {}
}
SubShader {
	Tags { "RenderType"="Opaque" }
	LOD 200

CGPROGRAM
#pragma surface surf NoLighting

sampler2D _MainTex;
fixed4 _Color;
fixed4 _Color2;
fixed4 _Color3;
float _X;
float _Y;

struct Input {
	float2 uv_MainTex;
	float4 screenPos;
};

void surf (Input IN, inout SurfaceOutput o) {
	float2 screenUV = IN.screenPos.xy / IN.screenPos.w;

	fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * lerp(_Color2, _Color, screenUV.x * _X + _Y * screenUV.y) * _Color3;
	o.Albedo = c.rgb;
	o.Alpha = c.a;
}

fixed4 LightingNoLighting(SurfaceOutput s, fixed3 lightDir, fixed atten)
{
	fixed4 c;
	c.rgb = s.Albedo;
	c.a = s.Alpha;
	return c;
}
ENDCG
}

Fallback "Legacy Shaders/VertexLit"
}
