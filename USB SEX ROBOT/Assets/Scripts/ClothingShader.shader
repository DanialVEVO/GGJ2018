﻿Shader "Custom/ClothingShader" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MaskColor ("Clothing Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_MaskTex ("Clothing mask (A)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_Disintegration ("Disintegration", Range(0,1)) = 0.0	
	}
	SubShader {
		Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows
		#pragma multi_compile _ PIXELSNAP_ON
		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _MaskTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness; 
		half _Metallic;
		half _Disintegration;
		fixed4 _Color;
		fixed4 _MaskColor;

		// Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
		// See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
		// #pragma instancing_options assumeuniformscaling
		UNITY_INSTANCING_BUFFER_START(Props)
			// put more per-instance properties here
		UNITY_INSTANCING_BUFFER_END(Props)

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			float2 uv = IN.uv_MainTex;
			float uvDif = fmod(uv.y, 0.01f);

			if (uvDif >= 0.005f)
				uvDif = 0.01f;
			else
				uvDif = -0.01f;

			uv.x += uvDif * _Disintegration * 32.0f;

			fixed4 c1 = tex2D (_MainTex, uv) * _Color;
			fixed4 c2 = c1 * _MaskColor;
			fixed4 c = lerp(c1, c2, tex2D (_MaskTex, uv));

			if (c1.a < 0.5)
				discard;

			o.Albedo = c;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
