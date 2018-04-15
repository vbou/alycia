using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ExecuteInEditMode]
public class PixellateScript : MonoBehaviour {

    public Material effectMaterial;


	// Use this for initialization
	void Start ()
    {
        PreProcessTex preProcessTex = new PreProcessTex(effectMaterial.mainTexture as Texture2D);
        preProcessTex.PreProcessTexture();
        List<Vector4> vectors = preProcessTex.GetVectors();
        List<float> vectorsAreas = preProcessTex.GetAreas();
        effectMaterial.SetVectorArray(Shader.PropertyToID("_Vectors"), vectors);
        effectMaterial.SetFloatArray(Shader.PropertyToID("_Areas"), vectorsAreas);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        Graphics.Blit(source, destination, effectMaterial);
    }

}
