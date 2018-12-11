Shader "Noise/Clouds" 
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Frequency("Frequency",  Range(0,10)) = 1
		_Speed("Speed", Float) = 1
		_RandomSeed("RandomSeed", Range(0,1000)) = 1
		_HeightScale("HeightScale", Range(0,4)) = 1
		_ShadowDepth("ShadowDepth", Range(0,3)) = 1			
	}
	SubShader{
	Tags{ "Queue" = "Transparent" "RenderType" = "Transparent" }
	LOD 200
	//Cull Off

	CGPROGRAM

	#pragma surface surf Standard fullforwardshadows alpha:fade vertex:vert
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
		float res = noiseOctaves4(wp, _Time.y * _Speed, 7) * _HeightScale;
		return res;
	}

	void surf(Input IN, inout SurfaceOutputStandard o)
	{
		o.Metallic = 0;
		o.Smoothness = 0;

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

		o.Normal = normalize(float3(dx, dy, 1));

		o.Albedo = _Color;
		o.Alpha = _Color.a * saturate(height * 2);
	}

	ENDCG
	}
	FallBack "Diffuse"
}
