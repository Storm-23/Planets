Shader "Noise/GasPlanet" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Frequency("Frequency",  Range(0,10)) = 1
		_Speed("Speed", Float) = 1
		_RandomSeed("RandomSeed", Range(0,1000)) = 1
		_HeightScale("HeightScale", Range(0,4)) = 1
		_Palette("Palette", Range(0,16)) = 5	
			
		_Type("Type", Range(0,5)) = 1
	}
	SubShader{
	Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" }
	LOD 200

	CGPROGRAM

	#pragma surface surf Standard fullforwardshadows vertex:vert
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

	UNITY_INSTANCING_BUFFER_START(Props)
	UNITY_INSTANCING_BUFFER_END(Props)

	void vert (inout appdata_full v, out Input o) 
	{
	   UNITY_INITIALIZE_OUTPUT(Input,o);
	   o.localPos = v.vertex.xyz;
	}

	int _Type;
	float _HeightScale;
	float distToCamera;

	#include "noise.cginc"

	float GetHeight(float3 wp)
	{
		wp += _RandomSeed;
		wp.x /= 20;
		wp.z /= 20;
		wp *= _Frequency;
		float res = noiseOctaves(wp, 7) * _HeightScale;
		if (_Type == 0)
		{
			res = exp(res);
		}else
		if (_Type == 1 || _Type == 2)
		{
			res = exp(1 - abs(res)) / 2;
		}else
		{
			res += 1;
		}
		return res;
	}

	float3 RotateAroundY(float3 vertex, float degrees)
	{
		float alpha = degrees * UNITY_PI / 180.0;
		float sina, cosa;
		sincos(alpha, sina, cosa);
		float2x2 m = float2x2(cosa, -sina, sina, cosa);
		float2 t = mul(m, vertex.xz);
		return float3(t.x, vertex.y, t.y);
	}


	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		o.Metallic = 0;
		o.Smoothness = 0;

		distToCamera = length(ObjSpaceViewDir(float4(IN.localPos, 1)));

		float3 p = IN.localPos;
		float albedo = GetHeight(p);
		float albedo2 = GetHeight(RotateAroundY(p + 10, _Time.y * _Speed));

		// Albedo comes from a texture tinted by color
		float iU = _Palette;
		fixed4 c1 = tex2D(_MainTex, float2(albedo, 1/32 + iU / 16)) * _Color;
		fixed4 c2 = tex2D(_MainTex, float2(albedo2, 1/32 + iU / 16)) * _Color;
		o.Albedo = (albedo/2 + 1) * (c1 * 2 + c2) / 3;
		o.Alpha = 1;
	}

	ENDCG
	}
	FallBack "Diffuse"
}

