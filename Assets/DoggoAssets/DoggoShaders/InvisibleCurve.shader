Shader "Unlit/InvisibleCurve"
{
	Properties
	{
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" "Queue" = "Geometry-1" }
		LOD 100

		Blend Zero One

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