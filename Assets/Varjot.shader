Shader "Custom/Varjot"{
	Properties {
	_MainTex("Texture", 2D) = "white"
	_ListanPituus("ListanPisist", float) = 0
	_Paikka("Paikka", vector) = (0, 0, 0, 0)
	}
	SubShader
	{
	Blend SrcAlpha OneMinusSrcAlpha 

		Tags
		{
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
				UNITY_FOG_COORDS(1)
				float4 worldPos : TEXCOORD1;
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				UNITY_TRANSFER_FOG(o,o.vertex);
				o.worldPos = v.vertex;
				return o;
			}

			sampler2D _MainTex;
			float4 _MainTex_ST;
			float _ListanPituus;
			float4 reunaPisteet [100];
			float4 _Paikka;
			int objNum = 0; //Tiedetään onko uusi pala seinää

			float4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, col);
				float4 col = tex2D(_MainTex, i.uv);
				float2 suunta = float2(_Paikka.x, _Paikka.y) - i.uv;
				col.a = 0;
				for (int ind = 0; ind < _ListanPituus; ind++){
					if(ind + 1 < _ListanPituus){
						if (reunaPisteet[ind + 1][3] == 1 && reunaPisteet[ind][3] == 1){
							float2 ekaKulma = float2(reunaPisteet[ind][0], reunaPisteet[ind][1]);
							float2 kulmienSuunta = float2(reunaPisteet[ind + 1][0], reunaPisteet[ind + 1][1]) - ekaKulma;
							float v = (i.uv.y * suunta.x - ekaKulma.y * suunta.x + suunta.y * ekaKulma.x - suunta.y * i.uv.x) / (suunta.x * kulmienSuunta.y - suunta.y * kulmienSuunta.x);
							float u = (ekaKulma.x + v * kulmienSuunta.x - i.uv.x) / suunta.x;
							if (v >= 0 && u >= 0 && v <= 1 && u <= 1){
								col = float4(0, 0, 0, 1);
							}
							//if (i.uv.x < reunaPisteet[ind][0] + 0.005 && i.uv.x > reunaPisteet[ind][0] - 0.005 && i.uv.y < reunaPisteet[ind][1] + 0.005 && i.uv.y > reunaPisteet[ind][1] - 0.005){
							//	col = float4 (0, 1, 0, 1);
							//}
							//if (i.uv.x < reunaPisteet[ind + 1][0] + 0.005 && i.uv.x > reunaPisteet[ind + 1][0] - 0.005 && i.uv.y < reunaPisteet[ind + 1][1] + 0.005 && i.uv.y > reunaPisteet[ind + 1][1] - 0.005){
							//	col = float4 (1, 0, 0, 1);
							//}
						}
					}
					else{
						if (reunaPisteet[ind][3] == 1 && reunaPisteet[0][3] == 1){
							float2 ekaKulma = float2(reunaPisteet[ind][0], reunaPisteet[ind][1]);
							float2 kulmienSuunta = float2(reunaPisteet[0][0], reunaPisteet[0][1]) - ekaKulma;
							float v = (i.uv.y * suunta.x - ekaKulma.y * suunta.x + suunta.y * ekaKulma.x - suunta.y * i.uv.x) / (suunta.x * kulmienSuunta.y - suunta.y * kulmienSuunta.x);
							float u = (ekaKulma.x + v * kulmienSuunta.x - i.uv.x) / suunta.x;
							if (v >= 0 && u >= 0 && v <= 1 && u <= 1){
								col = float4(0, 0, 0, 1);
							}
							//if (i.uv.x < reunaPisteet[ind][0] + 0.005 && i.uv.x > reunaPisteet[ind][0] - 0.005 && i.uv.y < reunaPisteet[ind][1] + 0.005 && i.uv.y > reunaPisteet[ind][1] - 0.005){
							//	col = float4 (0, 1, 1, 1);
							//}
							//if (i.uv.x < reunaPisteet[0][0] + 0.005 && i.uv.x > reunaPisteet[0][0] - 0.005 && i.uv.y < reunaPisteet[0][1] + 0.005 && i.uv.y > reunaPisteet[0][1] - 0.005){
							//	col = float4 (1, 0, 1, 1);
							//}
						}
					}
				}
				//if (i.uv.x < reunaPisteet[0][0] + 0.001 && i.uv.x > reunaPisteet[0][0] - 0.001 && i.uv.y < reunaPisteet[0][1] + 0.001 && i.uv.y > reunaPisteet[0][1] - 0.001){
				//	col = float4 (0, 1, 0, 1);
				//}
				//if (i.uv.x < reunaPisteet[1][0] + 0.001 && i.uv.x > reunaPisteet[1][0] - 0.001 && i.uv.y < reunaPisteet[1][1] + 0.001 && i.uv.y > reunaPisteet[1][1] - 0.001){
				//	col = float4 (1, 0, 0, 1);
				//}
				return col;
			}
			ENDCG
		}
	}
}