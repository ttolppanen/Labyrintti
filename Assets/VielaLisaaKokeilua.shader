Shader "Custom/VielaLisaaKokeilua"{
	Properties {
	_MainTex("Texture", 2D) = "white"
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
				float etaisyys = pow(pow(-i.uv.x + 0.5, 2) + pow(-i.uv.y + 0.5, 2), 0.5);
				float kulma = atan2((-i.uv.y + 0.5), (-i.uv.x + 0.5));
				float4 col = tex2D(_MainTex,  float2(0.5, 0.5) - etaisyys * float2(cos(kulma + _Time[1]) , sin(kulma + _Time[1])));
				return col;
			}
			ENDCG
		}
	}
}