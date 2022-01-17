Shader "Custom/Lesson10-Alpha测试"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Alpha("a",Range(0,1)) = 0
    }
    SubShader
    {

        Pass
        {
            AlphaTest Greater[_Alpha]

            SetTexture[_MainTex]{
                combine texture * previous
            }
        }
    }

}
