Shader "Hidden/应用-图片模糊"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Offset("Dim offset", Range(0, 0.02)) = 0
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
            float _Offset;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // just invert the colors
                //col.rgb = 1 - col.rgb;
                fixed4 col1 = tex2D(_MainTex, i.uv + fixed2(_Offset, 0));
                fixed4 col2 = tex2D(_MainTex, i.uv - fixed2(_Offset, 0));
                fixed4 col3 = tex2D(_MainTex, i.uv + fixed2(0, _Offset));
                fixed4 col4 = tex2D(_MainTex, i.uv - fixed2(0, _Offset));                
                return (col + col1 + col2 + col3 + col4) / 5;
            }
            ENDCG
        }
    }
}
