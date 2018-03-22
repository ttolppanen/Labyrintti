Shader "Custom/Uudet Varjot"{
	Properties {
	_MainTex("Texture", 2D) = "white"
	_ReunaPisteet("Pisteet", vector) = (0, 0, 0, 0)
	_UkonPaikka("Ukon Pisteet", vector) = (0, 0, 0, 0)
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
			float4 _ReunaPisteet;
			float4 _UkonPaikka;

			float4 frag(v2f i) : SV_Target
			{
				UNITY_APPLY_FOG(i.fogCoord, col);
				float4 col = tex2D(_MainTex, i.uv);
				float2 suunta = float2(_UkonPaikka.x, _UkonPaikka.y) - i.uv;
				col.a = 0;
				float2 ekaKulma = float2(_ReunaPisteet.x, _ReunaPisteet.y);
				float2 kulmienSuunta = float2(_ReunaPisteet.z, _ReunaPisteet.w) - ekaKulma;
				float v = (i.uv.y * suunta.x - ekaKulma.y * suunta.x + suunta.y * ekaKulma.x - suunta.y * i.uv.x) / (suunta.x * kulmienSuunta.y - suunta.y * kulmienSuunta.x);
				float u = (ekaKulma.x + v * kulmienSuunta.x - i.uv.x) / suunta.x;
				if (v >= 0 && u >= 0 && v <= 1 && u <= 1){
					col = float4(0, 0, 0, 1);
				}
				return col;
			}
			ENDCG
		}
	}
}