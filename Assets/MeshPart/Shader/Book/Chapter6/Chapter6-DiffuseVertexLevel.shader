// Upgrade NOTE: replaced '_World2Object' with 'unity_WorldToObject'
//漫反射 逐顶点光照
Shader "Custom/Chaper6-DiffuseVertexLevel" {

    Properties{
        _Diffuse("Diffuse", Color) = (1, 1, 1, 1)
    }

    SubShader{
        Pass{
            
            Tags{ "LightMode" = "ForwardBase" }

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
                fixed3 color : COLOR;
            };

            v2f vert(a2v v){
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);

                //Get ambient term    MVP   UNITY_LIGHTMODEL_AMBIENT  环境光
                fixed3 ambient = UNITY_LIGHTMODEL_AMBIENT.xyz;

                //Transform the normal fram object space to world space    _World2Object - world space to model space Matrix
                fixed3 worldNormal = normalize(mul(v.normal, (float3x3)unity_WorldToObject));
                //Get the light direction in world space   _WorldSpaceLightPos0  光源的方向
                fixed3 worldLight = normalize(_WorldSpaceLightPos0.xyz);
                //Compute diffuse term   _LightColor0 - 光源的颜色和强度信息      saturate function - limit the value in [0, 1]
                fixed3 diffuse = _LightColor0.rgb * _Diffuse.rgb * saturate( dot(worldNormal, worldLight) );

                o.color = ambient + diffuse;
                return o;
            }
            
            fixed4 frag(v2f i) : SV_Target{
                return fixed4(i.color, 1.0);
            }
            ENDCG
        }
    }  
}
