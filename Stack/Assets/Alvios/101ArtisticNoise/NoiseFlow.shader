Shader "Alvios/Noise Flow" {
	Properties {
		[HDR]_TintColor("Tint Color", Color) = (0.5,0.5,0.5,0.5)
		[HDR]_BaseColor("Base Color", Color) = (0,0,0,1)
		_MainTex("Main Texture", 2D) = "white" {}
		_Distortion("Distortion Texture", 2D) = "white" {}
		_Speed("Distortion Speed", Float) = 1
		_Size("Distortion Size", Float) = 1
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Geometry" }
		
		CGPROGRAM
		#pragma surface surf Lambert fullforwardshadows
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Distortion;
		half4 _TintColor, _BaseColor;
		float _Speed;
		float _Size;
		fixed4 _GlowColor;

		struct Input {
			float2 uv_MainTex;
			float2 uv_Distortion;
		};

		void surf(Input IN, inout SurfaceOutput o) {
			float4 distort = tex2D(_Distortion, IN.uv_Distortion) * 2 - 1;
			float2 speed = _Speed * _Time.xx;
			
			float4 output = tex2D(_MainTex, IN.uv_MainTex + distort / 3 * _Size + speed);
			float4 flowMid = tex2D(_MainTex, IN.uv_MainTex - distort / 11 * _Size - speed * 1.37 + float2(0.23, 0.71));
			
			output.rgba *= flowMid.rgba;

			o.Emission = _TintColor * output;
			o.Albedo = o.Emission + _BaseColor;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
