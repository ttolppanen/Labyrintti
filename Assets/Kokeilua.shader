Shader "Custom/Kokeilua"{
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
				float2 paikka = float2(0.5 + 0.2 * cos(_Time[1] / 2), 0.5 + 0.2 * sin(2 * _Time[1]));
				float etaisyys = pow(pow(i.uv.x - paikka.x, 2) + pow(i.uv.y - paikka.y, 2), 0.5);
				float kerroin = -(1 / (pow(0.01 + etaisyys, 2))) * 0.001;
				float4 col = tex2D(_MainTex, i.uv + kerroin * (paikka - i.uv)); // + float2(0.01 * sin(i.vertex.x / 20 + _Time[1]), 0.01 * cos(i.vertex.y / 20 + _Time[1]))); //float2((0.5 - i.worldPos.x) * abs(0.5 - i.worldPos.x) * 0.3, (0.5 - i.worldPos.y) * abs(0.5 - i.worldPos.y) * 0.3));
				col.r = col.r - kerroin * 0.2;
				col.b = col.b - kerroin * 4 * 0.2;
				col.g = col.g  - kerroin * 0.4;
				//float4 col = tex2D(_MainTex, float2(i.uv.x * pow(i.uv.x, 0.2), i.uv.y));
				//if (sin(i.worldPos.y * 100) > 0.5 && sin(i.worldPos.y * 100) < 1){
				//if (i.worldPos.y > 0.3 + 0.2 * sin(i.worldPos.x * 10 + _Time[1]) && i.worldPos.y < 0.4 + 0.3 * sin(i.worldPos.x * 10 + _Time[1])){
				//	col = float4(col.r, col.r, col.r, 1);
				//}
				//col = float4(col.r, col.g ,col.b * 1.5, 1);
				//if (i.worldPos.x > 0.4 && i.worldPos.x < 0.6 && i.worldPos.y > 0.4 && i.worldPos.y < 0.6){
				//	col = float4(0.5, 0.8, 0.2, 1);
				//}
				return col;
			}
			ENDCG
		}
	}
}