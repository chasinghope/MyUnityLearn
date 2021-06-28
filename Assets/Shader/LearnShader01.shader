Shader "Custom/BobShader" 
{
    Properties{
        //声明一个Color类型的属性
        _Color("Color Tint", Color) = (1, 1, 1, 1)
    }
    SubShader 
    {
        Pass
        {
            CGPROGRAM
            #pragma vertex vert 
            #pragma fragment frag 

            //Cg代码中，需要定义一个与属性名称和类型都匹配的变量
            fixed4 _Color;
    
            //使用一个结构体来定义顶点着色器的输入
            struct a2v{
                //POSITION = 用模型空间的顶点坐标填充vertex variable
                float4 vertex : POSITION;
                //NORMAL = 用模型空间的法线向量填充normal variable
                float3 normal : NORMAL;
                //TEXCOORD0 = 用模型的第一套纹理坐标填充texcoord 
                float4 texcoord : TEXCOORD0;
            };

            //使用一个结构来定义顶点着色器得输出
            struct v2f{
                //SV_POSITION = 告诉unity, pos里包含了顶点在裁剪空间中的位置信息
                float4 pos : SV_POSITION;
                //COLOR0 = 用于存储颜色信息
                fixed3 color : COLOR0;
            };

            v2f vert(a2v v) {
                //声明输出结构
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5);
                //使用v.vertex来访问模型空间的顶点坐标
                return o;
            }
         
            fixed4 frag(v2f i) : SV_TARGET0 {
                fixed3 c = i.color;
                //使用_Color属性来控制输出颜色
                c *= _Color.rgb;
                return fixed4(c, 1.0);
            }
            ENDCG
        }
    }

}