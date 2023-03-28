using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : Singleton<Destroyer>
{
    public Todestroyer[] destroyer;
    public void DestroyThem()
    {
        StartCoroutine(DestroyCorautine());
    }
   public IEnumerator DestroyCorautine()
    {
        foreach(Todestroyer guy in destroyer)
        {
            if (guy != null)
            {
                Destroy(guy.gameObject);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }
}
