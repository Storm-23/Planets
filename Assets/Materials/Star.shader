Shader "Noise/Star" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Frequency("Frequency",  Range(0,20)) = 1
		_Speed("Speed", Float) = 1
		_RandomSeed("RandomSeed", Range(0,1000)) = 1
		_HeightScale("HeightScale", Range(0,10)) = 1
	}
	SubShader{
	Tags{ "Queue" = "Geometry" "RenderType" = "Opaque" }
	LOD 200

	CGPROGRAM

	#pragma surface surf Standard vertex:vert
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
	float _Frequency;
	float _Speed;
	float _HeightScale;
	float distToCamera;
	float _ShadowDepth;

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
		wp += _RandomSeed;
		wp *= _Frequency;
		float res = noiseOctaves4(wp, _Time.y * _Speed, 3) * _HeightScale;
		return res;
	}

	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		o.Metallic = 0;
		o.Smoothness = 0;

		distToCamera = length(ObjSpaceViewDir(float4(IN.localPos, 1)));

		float3 p = IN.localPos;
		float height = GetHeight(p);

		o.Emission = o.Albedo = _Color * (0.7 + saturate(height));
		o.Alpha = 1;
		o.Normal = float3(0, 0, 0.1);
	}

	ENDCG
	}
	FallBack "Diffuse"
}
