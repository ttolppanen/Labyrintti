Shader "Unlit/VesiJuttu"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
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

			float4 pisteet [30];

			fixed4 frag (v2f i) : SV_Target
			{
				// sample the texture
				fixed4 col = tex2D(_MainTex, i.uv);
				float aika = 0.05;
				for(int o = 0; o < 30; o++){
					if(pisteet[o].w != 1){
						float2 piste = float2(pisteet[o].x, pisteet[o].y);
						float2 vektori = i.uv - piste;
						float ympMin = 1 - (1 / (1 + 0.2 * pisteet[o].z)); //aika
						float ympMax = ympMin + 0.02;
						float c = (ympMax + ympMin) / 2.0; //Pituus vesi jutun keskelle
						c = c - length(vektori);   //Pituus vesi jutun keskeltä!
						c = c / 0.1;
						float suuntaKerroin = 8.0 / 3 * pow(c, 3) - 8.0 / 3 * c;
						float tasoitusKerroin = 0.8 / (1 + 3 * pisteet[o].z);
						if(length(vektori) > ympMin && length(vektori) < ympMax){
							col = float4(1, tasoitusKerroin * suuntaKerroin * 0.5 * normalize(vektori).y + 0.5, 1, tasoitusKerroin * suuntaKerroin * 0.5 * normalize(vektori).x + 0.5);
							return col;
						}
					}
				}
				return col;
			}
			ENDCG
		}
	}
}
