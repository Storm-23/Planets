Shader "Noise/Ring" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Frequency("Frequency",  Range(0,10)) = 1
		_RandomSeed("RandomSeed", Range(0,1000)) = 1
		_Palette("Palette", Range(0,16)) = 5
		_Width("Width", Range(0,1)) = 0.2
	}
	SubShader{
	Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
	LOD 200
	Cull Off

	CGPROGRAM

	#pragma surface surf Standard fullforwardshadows vertex:vert alpha:fade
	#pragma target 3.0

	sampler2D _MainTex;

	struct Input
	{
		float2 uv_MainTex;
		float3 worldPos;
		float3 localPos;
	};

	fixed4 _Color;
	float _RandomSeed;
	float _Palette;
	float _Frequency;
	float _Speed;
	float _Width;
	float distToCamera;

	UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_INSTANCING_BUFFER_END(Props)

	void vert (inout appdata_full v, out Input o) 
	{
	   UNITY_INITIALIZE_OUTPUT(Input,o);
	   o.localPos = v.vertex.xyz;
	}

	#include "noise.cginc"

	float GetHeight(float3 wp)
	{
		float a = atan2(wp.y, wp.x);
		float r = length(wp.xy);
		wp.x = sin(a) / 8;
		wp.y = r * 5 * (1 + _Frequency);
		wp.z = cos(a) / 8;
		wp += _RandomSeed;
		float res = noiseOctaves(wp, 7);
		res = exp(1 - abs(res)) / 1.7;
	
		return res;
	}

	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		o.Metallic = 0;
		o.Smoothness = 0.2;

		distToCamera = length(ObjSpaceViewDir(float4(IN.localPos, 1)));

		float3 p = IN.localPos;
		float r = length(p.xy) * 2;
		if (r > 1 || r < 1 - _Width)
			discard;

		float albedo = GetHeight(p);
		float iU = _Palette;
		fixed4 c1 = tex2D(_MainTex, float2(albedo, 1/32 + iU / 16)) * _Color;
		o.Albedo = 5 * c1;
		o.Alpha = 0.9 * c1.a;
		o.Normal = float3(0, 0, 1);
	}

	ENDCG
	}
	FallBack "Diffuse"
}

