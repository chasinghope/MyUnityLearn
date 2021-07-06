Shader "Hidden/BloodHurtShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Blood("Blood img", 2D) = "white"{}
        _BloodAlpha("Blood Alpha", Range(0, 1)) = 1
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _Blood;
            fixed _BloodAlpha;
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                _BloodAlpha = abs(sin(_Time.y));
                fixed4 blood = tex2D(_Blood, i.uv);
                blood.a *= _BloodAlpha;
                col.rgb = col.rgb * (1 - blood.a) + blood.rgb * blood.a;
                
                return col;
            }
            ENDCG
        }
    }
}
