using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionFloating : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material defaultMaterial;
    [SerializeField, Range(1, 500)] float x, y;

    Vector3 pos;
    Transform selectionObj;

    void Start()
    {
        pos = new Vector3(x, y, 0);
    }

    void Update()
    {
        if (selectionObj != null)
        {
            var selectionRenderer = selectionObj.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            selectionObj = null;
        }

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(pos);

        if (Physics.Raycast(ray, out hit))
        {
            var selection = hit.transform;

            if (selection.CompareTag("Selectable"))
            {
                var selectionRenderer = selection.GetComponent<Renderer>();

                if (selectionRenderer != null)
                {
                    selectionRenderer.material = highlightMaterial;
                }
                selectionObj = selection;
            }
        }
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
    }
}
