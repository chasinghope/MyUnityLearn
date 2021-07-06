//透明测试的双向渲染
Shader "Custom/Chapter8-AlphaTestBothSided" 
{
	Properties
	{
		_Color("Main Tint", Color) = (1, 1, 1, 1)
		_MainTex("Main Tex", 2D) = "white"{}
		_Cutoff("Alpha Cutoff", Range(0, 1)) = 0.5   //决定调用clip进行透明度测试时使用的判断条件
	}

	SubShader
	{
		Tags
		{ 
			"Queue" = "AlphaTest"  
			"IgnoreProjector" = "True" 
			"RenderType" = "TransparentCutout" 
		}
		
		Pass 
		{
			Tags
			{
				"LightMode" = "ForwardBase"
			}

			Cull Off

			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag 
			#include "Lighting.cginc"
			
			fixed4 _Color;
			sampler2D _MainTex;
			float4 _MainTex_ST;
			fixed _Cutoff;

			struct v2f
			{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
				float3 worldPos : TEXCOORD1;
				float2 uv : TEXCOORD2;
			};

			v2f vert(appdata_base v)
			{
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				o.worldNormal = UnityObjectToWorldNormal(v.normal);
				o.worldPos = UnityObjectToWorldDir(v.vertex);
				o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target 
			{
				fixed3 worldNormal = i.worldNormal;
				fixed3 worldLightDir = normalize(UnityWorldSpaceLightDir(i.worldPos));
				fixed4 texColor = tex2D(_MainTex, i.uv);
				//AlphaTest
				clip(texColor.a - (_Cutoff + 0.01) );

				fixed3 albedo = texColor.rgb * _Color.rgb;
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz * albedo;
				fixed3 diffuse = _LightColor0.rgb * albedo * max(0, dot(worldNormal, worldLightDir));
				return fixed4(ambient + diffuse, 1.0);
			}


			ENDCG
		}
	}

}
