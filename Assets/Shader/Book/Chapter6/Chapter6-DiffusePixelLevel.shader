//漫反射 逐像素光照
Shader "Custom/Chapter6-DiffusePixelLevel"{
	Properties{
		_Diffuse("Diffuse", Color) = (1, 1, 1, 1)
	}

	SubShader{
		Pass{
			Tags{
				"LightMode" = "ForwardBase"
			}

			CGPROGRAM

			#include "Lighting.cginc"
			#pragma vertex vert 
			#pragma fragment frag 

			fixed4 _Diffuse;

			struct a2v{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f{
				float4 pos : SV_POSITION;
				float3 worldNormal : TEXCOORD0;
			};


			v2f vert(a2v v){
				v2f o;
				//transform the vertex from object space to projection space
				o.pos = UnityObjectToClipPos(v.vertex);

				//transform the normal fram object to world space      ?
				o.worldNormal = mul(v.normal, (float3x3)unity_WorldToObject);
				return o;
			}

			fixed4 frag(v2f i) : SV_Target{
				//Get ambient term
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				//Get the normal in world space
				fixed3 worldNormal = normalize(i.worldNormal);
				//Get the light direction in world space
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

				//Compute diffuse term
				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLightDir) );
				fixed3 color = ambient + diffuse;
				return fixed4(color, 1.0);
			}


			ENDCG			
		}
	}

}