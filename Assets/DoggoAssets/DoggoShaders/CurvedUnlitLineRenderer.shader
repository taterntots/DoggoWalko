Shader "Unlit/CurvedUnlitLineRenderer"
{
    Properties
    {
		_MainTex("Texture", 2D) = "green" {}
		_Color("Color", Color) = (1,1,1,1)
	}

		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work 
			#pragma multi_compile_fog

			#include "CurvedCode.cginc"

			ENDCG
		}
	}
}
