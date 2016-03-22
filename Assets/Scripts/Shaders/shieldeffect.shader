Shader "Shieldeffect" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
      	_BumpMap ("Bumpmap", 2D) = "bump" {}
    	_Position ("Collision", Vector) = (-1, -1, -1, -1)    	
    	_MaxDistance ("Effect Size", float) = 40    	
    	_ShieldColor ("Color (RGBA)", Color) = (0.7, 1, 1, 0.02)
    	_EmissionColor ("Emission color (RGB)", Color) = (0.7, 1, 1)    	
    	_EffectTime ("Effect Time (ms)", float) = 0
    }
    
    SubShader {
    	Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
    	LOD 2000
    	Cull Off
      
    	CGPROGRAM
      	#pragma surface surf BlinnPhong alpha
      	#pragma target 3.0
      
     	struct Input {
     		float2 uv_MainTex;
        	float2 uv_BumpMap;
     		float3 worldPos;
      	};
      
      	float4 _Position;      	
      	float  _MaxDistance;      	
      	float4 _ShieldColor;
      	float3 _EmissionColor;      	
      	float  _EffectTime;
      	
      	sampler2D _MainTex;
      	sampler2D _BumpMap;
      
      	void surf (Input IN, inout SurfaceOutput o)
      	{
        	half4 c = tex2D (_MainTex, IN.uv_MainTex);
        	o.Albedo = c.rgb * _ShieldColor.rgb;
        	o.Normal = UnpackNormal (tex2D (_BumpMap, IN.uv_BumpMap));
        	o.Emission = c.rgb * _EmissionColor.rgb;
        	
			if(_EffectTime > 0)
        	{
            	float myDist = distance(_Position.xyz, IN.worldPos);
            	if(myDist < _MaxDistance){
            		o.Alpha = _EffectTime/500 - (myDist / _MaxDistance);
                	if(o.Alpha < _ShieldColor.a){
                		o.Alpha = _ShieldColor.a;
                	}
            	}
            	else
            	{
	            	o.Alpha = _ShieldColor.a;
	        	}
         	}
            else
            {
	            o.Alpha = _ShieldColor.a;
	        }
	    }
      
      	ENDCG
    } 
    Fallback "Transparent/Diffuse"
}