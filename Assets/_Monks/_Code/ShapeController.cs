using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeController : MonoBehaviour
{
    #region Variables
    public SkinnedMeshRenderer meshRenderer;
    [SerializeField][Range(0, 100)] float cubeMorphValue;
    [SerializeField][Range(0, 100)] float sphereMorphValue;
    [SerializeField] private bool isMorphinToCube;
    [SerializeField] private bool isMorphinToSphere;
    #endregion
    private void Start()
    {
        SwipeReader.Instance.OnSwipeLeft += HandleMorphToSphere;
    }


    public void HandleMorphToCube()
    {
        isMorphinToCube = true;
        StartCoroutine(MorphToCube());
    }

    private void HandleMorphToSphere()
    {
        isMorphinToSphere = true;
        StartCoroutine(MorphToSphere());

    }

    IEnumerator MorphToCube()
    {
        while(isMorphinToCube == true)
        {
            yield return new WaitForSeconds(.1f);
            meshRenderer.SetBlendShapeWeight(0, cubeMorphValue += 10);
            meshRenderer.SetBlendShapeWeight(1, sphereMorphValue -= 10);

            if(sphereMorphValue < 0 ) 
            {
              sphereMorphValue = 0;
            }
            if(cubeMorphValue >= 100f && sphereMorphValue <=0)
            {
                isMorphinToCube=false;
            }
        }
      
    }

    IEnumerator MorphToSphere()
    {
        
        while (isMorphinToSphere == true)
        {
            yield return new WaitForSeconds(.1f);
            meshRenderer.SetBlendShapeWeight(0, cubeMorphValue -= 10);
            meshRenderer.SetBlendShapeWeight(1, sphereMorphValue += 10);

            if (cubeMorphValue < 0)
            {
                cubeMorphValue = 0;
            }
            if (sphereMorphValue >= 100f)
            {
                isMorphinToSphere = false;
            }
        }
    }
}
