Shader "Custom/LearnShader02"
{
    Properties
    {
        _A ("A", float) = 1.0
        _K ("K", float) = 1.0
        _MainTex("_MainTex", 2D) = "while"{}
    }
    SubShader
    {
        Pass{
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag 

            #include "UnityCG.cginc"
            float _A;
            float _K;

            struct a2v{
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f{
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };


            v2f vert(a2v v){
                v2f o;
                v.vertex.y += _A * sin((_Time.y + v.vertex.x) * _K);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            fixed4 frag(v2f f) : SV_Target {
                float2 uv = f.uv + _Time.y;
                return tex2D(_MainTex, uv);
            }

            ENDCG            
        }
    }

}
