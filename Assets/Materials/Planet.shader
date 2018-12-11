Shader "Noise/Planet" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_WaterColor("WaterColor", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
		_Smoothness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Frequency("Frequency",  Range(0,10)) = 1
		_Speed("Speed", Float) = 1
		_WaterLevel("WaterLevel", Range(0,1)) = 1
		_RandomSeed("RandomSeed", Range(0,1000)) = 1
		_HeightScale("HeightScale", Range(0,4)) = 1
		_Palette("Palette", Range(0,16)) = 5
		_WaterPalette("WaterPalette", Range(0,16)) = 0
		_WaterSmoothness("WaterSmoothness", Range(0,1)) = 0.7
		_ShadowDepth("ShadowDepth", Range(0,3)) = 1
			
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

	half _Smoothness;
	half _Metallic;
	fixed4 _Color;
	fixed4 _WaterColor;
	float _WaterLevel;
	float _RandomSeed;
	float _Palette;
	float _WaterPalette;
	float _WaterSmoothness;

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
	float _ShadowDepth;

	#include "noise.cginc"

	float GetHeight(float3 wp)
	{
		wp += _RandomSeed;
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
		if (_Type == 3)
		{
			res = 1.2 - exp(1 - abs(res * 100)) / 2 ;
		}else
		{
			res += 1;
		}
		res -= _WaterLevel;
		return res;
	}


	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		o.Metallic = _Metallic;
		o.Smoothness = _Smoothness;

		distToCamera = length(ObjSpaceViewDir(float4(IN.localPos, 1)));

		float3 p = IN.localPos;
		float dN = 0.02;
		float3 nx = normalize(cross(p, float3(0, 1, 0))) * dN;
		float3 ny = normalize(cross(p, nx)) * dN;
		float3 wpX = p + nx;
		float3 wpY = p + ny;

		float height = GetHeight(p);
		float height1 = GetHeight(wpX);
		float height2 = GetHeight(wpY);

		float dx = (height - height1) * _ShadowDepth;
		float dy = (height - height2) * _ShadowDepth;

		float albedo = GetHeight(p * 2);

		float iU = height < 0 ? _WaterPalette : _Palette;
		fixed4 c = tex2D(_MainTex, float2((albedo + height)/2, 1/32 + iU / 16)) * _Color;
		o.Albedo = (height/2 + 1) * c;

		if (height < 0)
		{
			float3 cc = lerp(o.Albedo, _WaterColor, _WaterColor.a) * (1 - height);
			o.Albedo = cc;
			o.Smoothness = _WaterSmoothness;
			o.Emission = o.Albedo * 0.02;
		}
		else
		{
			o.Normal = normalize(float3(dx, dy, 1));
		}

		o.Alpha = 1;
	}

	ENDCG
	}
	FallBack "Diffuse"
}
