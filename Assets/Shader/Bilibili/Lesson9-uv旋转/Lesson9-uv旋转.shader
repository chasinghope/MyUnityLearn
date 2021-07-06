Shader "Custom/Lesson9-uv旋转"
{
    Properties
    {
        //_Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Albedo (RGB)", 2D) = "white" {}
        _Angle("Angle", Float) = 1
    }
    SubShader
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UNITYCG.cginc"

            sampler2D _MainTex;
            float _Angle;
            
            struct v2f
            {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            v2f vert(appdata_base v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = v.texcoord;
                return o;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                float angle = _Time.y * _Angle;
                i.uv -= float2(0.5, 0.5);
                if(length(i.uv > 0.5))
                    return fixed4(0, 0, 0, 0);
                float2 newUV = (0, 0);
                newUV.x = i.uv.x * cos(angle) + i.uv.y * sin(angle);
                newUV.y = i.uv.y * cos(angle) - i.uv.x * sin(angle);
                newUV += float2(0.5, 0.5);
                
                fixed4 color = tex2D(_MainTex, newUV);
                return color;
            }


            ENDCG 
        }
    }

}
