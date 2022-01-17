Shader "Custom/Lesson12-表面着色器"
{
    Properties
    {
        _MainTex("Albedo (RGB)", 2D) = "while"{}
        _Color ("Color", Color) = (1,1,1,1)
        _NormalMap("Normal Map", 2D) = "bump"{}
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;
        fixed4 _Color;
        sampler2D _NormalMap;


        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb * _Color.rgb;
            o.Normal = UnpackNormal( tex2D(_NormalMap, IN.uv_MainTex) );
        }
        ENDCG
    }
    FallBack "Diffuse"
}
