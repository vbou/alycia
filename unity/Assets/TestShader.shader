﻿Shader "Hidden/TestShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_CustomTex("Texture2", 2D) = "white" {}
		
	}
	SubShader
	{
		// No culling or depth
		Cull Off ZWrite Off ZTest Always

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			sampler2D _MainTex;
			sampler2D _CustomTex;
			uniform float4 _Vectors[1000];
			uniform float _Areas[1000];

			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 texCol = tex2D(_CustomTex, i.uv);

				if (all(texCol.rgb != float4(0, 0, 0, 1))) {
					float4 mycol = float4(0, 0, 0, 1);
					col.rgb = mycol;
				}

				return col;
			}
			ENDCG
		}
	}
}
