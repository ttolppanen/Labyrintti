Shader "Unlit/VesiSärö"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_SaroTex ("Saro", 2D) = "white" {}
	}
	SubShader
	{
		Blend SrcAlpha OneMinusSrcAlpha 

		Tags { 
			"RenderType"="Opaque"
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
		}
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
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			sampler2D _SaroTex;

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				float2 muutos = 0.01 * float2(sin(_Time[1] / 3) + 1, cos(_Time[1] / 2) + 1);
				fixed4 col = tex2D(_MainTex, i.uv);
				fixed4 saro = tex2D(_SaroTex, i.uv + muutos);
				saro = tex2D(_SaroTex, float2(saro.r, saro.g));
				saro = tex2D(_SaroTex, float2(saro.r, saro.g));
				col = float4(1, col.g + 0.2 * saro.g, 1, col.a + 0.15 * saro.g);
				UNITY_APPLY_FOG(i.fogCoord, col);
				return col;
			}
			ENDCG
		}
	}
}
