Shader "Hidden/GrayscaleEffect"
{
    Properties
    {
        _Blend ("Blend", Range(0, 1)) = 1
    }
    SubShader
    {
        Cull Off ZWrite Off ZTest Always
        Pass
        {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _Blend;

            fixed4 frag(v2f_img i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                float gray = dot(col.rgb, float3(0.299, 0.587, 0.114)); // Standard grayscale conversion
                col.rgb = lerp(col.rgb, gray.xxx, _Blend);
                return col;
            }
            ENDCG
        }
    }
}
