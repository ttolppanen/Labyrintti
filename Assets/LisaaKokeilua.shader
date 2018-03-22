﻿Shader "Custom/LisaaKokeilua"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha 

		Tags { "RenderType"="Opaque" 
				"Queue"="Transparent" 
				"IgnoreProjector"="True" 
				"RenderType"="Transparent"}
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
				float4 worldPos : TEXCOORD1;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.worldPos = v.vertex;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target

			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				// apply fog
				UNITY_APPLY_FOG(i.fogCoord, col);

				col.a = 0.2;
				float etaisyys = pow(pow(i.uv.x - 0.5, 2) + pow(i.uv.y - 0.5, 2), 0.5);
				float vaiheSiirto = 3.14 * 1.3;

				if (
				(etaisyys > -0.1 + 0.2 * sin(_Time[1] / 2 + vaiheSiirto) && etaisyys < -0.1 + 0.2 * (0.06 + sin(_Time[1] / 2 + vaiheSiirto)))
				||
				(etaisyys > 0.2 * sin(_Time[1] / 2 + vaiheSiirto) && etaisyys < 0.2 * (0.06 + sin(_Time[1] / 2 + vaiheSiirto)))
				||
				(etaisyys > 0.1 + 0.2 * sin(_Time[1] / 2 + vaiheSiirto) && etaisyys < 0.1 + 0.2 * (0.06 + sin(_Time[1] / 2 + vaiheSiirto)))
				||
				(etaisyys > 0.2 + 0.2 * sin(_Time[1] / 2 + vaiheSiirto) && etaisyys < 0.2 + 0.2 * (0.06 + sin(_Time[1] / 2 + vaiheSiirto)))
				){
					if(cos(_Time[1] / 2 + vaiheSiirto) > 0){
						col.a = 0.2 + pow(0.45 * cos(_Time[1] / 2 + vaiheSiirto), 2);
					}
				}
				col.a *= pow(0.5 - etaisyys, 0.7);
				return col;
			}
			ENDCG
		}
	}
}
