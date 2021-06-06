Shader "Custom/VertexColour"
{
    SubShader
    {
        //Start
        CGPROGRAM

        #pragma surface surf Lambert

        //Naming this Input is just convention
        struct Input
        {
            float4 vertColour : COLOR;
        };

        void surf(Input IN, inout SurfaceOutput o)
        {
            //set the mesh's albedo to be the vertex colour
            o.Albedo = IN.vertColour;
        }

        //end
        ENDCG
    }
    FallBack "Diffuse"
}
