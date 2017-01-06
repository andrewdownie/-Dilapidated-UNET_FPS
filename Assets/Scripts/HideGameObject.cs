using UnityEngine;
using System.Collections;


[System.Serializable]
public class HideGameObject
{
    [SerializeField]
    private Renderer[] renderers;

    [SerializeField]
    private Collider[] colliders;

    public void Hide()
    {

        foreach(Renderer r in renderers)
        {
            r.enabled = false;
        }

        foreach(Collider c in colliders)
        {
            c.enabled = false;
        }
    }
}
