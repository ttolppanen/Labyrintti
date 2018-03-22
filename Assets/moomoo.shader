Shader "Custom/moomoo"{
	Properties {
	_MainTex("Texture", 2D) = "white"
	_Suunta("Suunta", float) = 0
	}
	SubShader
	{
		Tags
		{
			"PreviewType" = "Plane"
		}
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
				float4 vertex : SV_POSITION;
				float2 uv : TEXCOORD0;
				float4 worldPos : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				o.worldPos = v.vertex;
				return o;
			}

			sampler2D _MainTex;

			float4 frag(v2f i) : SV_Target
			{
				float4 col = tex2D(_MainTex, i.uv);
				float kerroin = 3.14 * 20;
				float aikaKerroin = 8;
				if ((i.uv.x > 0.3 && i.uv.x < 0.7) && (i.uv.y > 0.3 + 0.1 * sin(aikaKerroin * _Time[1] + kerroin * i.uv.x) && i.uv.y < 0.31 +  0.1 * sin(aikaKerroin * _Time[1] + kerroin * i.uv.x))){
					col = float4(pow(1 - i.uv.x, 2), col.g, pow(i.uv.x, 2), 1);
				}
				return col;
			}
			ENDCG
		}
	}
}