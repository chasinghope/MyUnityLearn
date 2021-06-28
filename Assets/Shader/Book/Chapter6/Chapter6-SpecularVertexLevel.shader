//高光反射 逐顶点光照
Shader "Custom/Chapter6-SpecularVertexLevel" {
	Properties{
		_Diffuse("Diffuse", Color) = (1, 1, 1, 1)      //漫反射颜色
		_Specular("Specular", Color) = (1, 1, 1, 1)    //高光反射颜色
		_Gloss("Gloss", Range(8.0, 256)) = 20          //控制高光区域的大小
	}

	SubShader{
		Pass{
			Tags{
				"LightMode" = "ForwardBase"   //可以得到一些Unity的内置光照变量
			}

			CGPROGRAM 
			#include "Lighting.cginc"
			#pragma vertex vert 
			#pragma fragment frag 

			fixed4 _Diffuse;
			fixed4 _Specular;
			float _Gloss;

			struct a2v{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct v2f{
				float4 pos : SV_POSITION;
				fixed3 color : COLOR;
			};

			v2f vert(a2v v){
				v2f o;
				o.pos = UnityObjectToClipPos(v.vertex);
				
				fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;
				//transform the normal fram object space to world space
				fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
				fixed3 worldLightDir = normalize(_WorldSpaceLightPos0.xyz);

				fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate(dot(worldNormal, worldLightDir));

				//Get the reflect direction in world space
				fixed3 reflectDir = normalize(reflect(-worldLightDir, worldNormal));
				//Get the view direction in world space
				fixed3 viewDir = normalize(_WorldSpaceCameraPos.xyz - mul(unity_WorldToObject, v.vertex).xyz);
				//Compute specular term
				fixed specular = _LightColor0.rgb * _Specular.rgb * pow(saturate(dot(viewDir, reflectDir)), _Gloss);
				o.color = ambient + diffuse + specular;
				return o;
			}

			fixed4 frag(v2f i) : SV_Target{
				return fixed4(i.color, 1.0);
			}

			ENDCG

		}
	}

	
}
