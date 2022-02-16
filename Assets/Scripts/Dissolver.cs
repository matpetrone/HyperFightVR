using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dissolver : MonoBehaviour
{
    private Material material;
    public float dissolvenceSpeed = 2f;
    public float initialStrenght = -0.1f;
    public float secBeforeDissolve = 3.5f;

    private bool startDissolving = false;

    // Start is called before the first frame update
    void Start()
    {
        material = GetComponent<Renderer>().material;
        material.SetFloat("_Dissolve_Strenght", initialStrenght);
    }

    void Update()
    {
        if (startDissolving)
            material.SetFloat("_Dissolve_Strenght", Mathf.MoveTowards(material.GetFloat("_Dissolve_Strenght"), 1.1f, dissolvenceSpeed * Time.deltaTime));
    }

    IEnumerator _Dissolve()
    {
        yield return new WaitForSeconds(secBeforeDissolve);
        startDissolving = true;
    }

    public void Dissolve()
    {
        StartCoroutine("_Dissolve");
    }
}
