using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] Material highlightMaterial;
    [SerializeField] Material defaultMaterial;

    //public float interactionDistance;

    public TMPro.TextMeshProUGUI interactionText;
    public GameObject interactionHoldGO;
    public UnityEngine.UI.Image interactionHoldProgress;
    //Camera cam;
    //public Transform button;
    //bool triggering = false;
    public float fovDist = 40.0f;
    public float fovAngle = 45.0f;
    Transform selectionObj;

    void Start()
    {
        //cam = GetComponent<Camera>();

    }


    void Update()
    {
        if (selectionObj != null)
        {
            var selectionRenderer = selectionObj.GetComponent<Renderer>();
            selectionRenderer.material = defaultMaterial;
            selectionObj = null;
        }

        GameObject objectinteractable = FindClosestInteractable();

        Vector3 direction = objectinteractable.transform.position - this.transform.position;
        float angle = Vector3.Angle(direction, this.transform.forward);

        bool successfulHit = false;

        RaycastHit hit;
        if (Physics.Raycast(this.transform.position, direction, out hit) && direction.magnitude < fovDist && angle < fovAngle)
        {
            Interactable interactable = objectinteractable.GetComponent<Collider>().GetComponent<Interactable>();

            Debug.DrawRay(this.transform.position, direction, Color.blue);

            if (interactable != null)
            {
                HandleInteraction(interactable);
                interactionText.text = interactable.GetDescription();
                successfulHit = true;

                //interactionHoldGO.SetActive(interactable.interactionType == Interactable.InteractionType.Hold);
            }

            var selectionRenderer = objectinteractable.GetComponent<Renderer>();

            if (selectionRenderer != null)
            {
                selectionRenderer.material = highlightMaterial;
            }
            selectionObj = objectinteractable.transform;
            // curCol = this.GetComponentInChildren<SpriteRenderer>().color;
            // Color newCol = new Color(curCol.r, curCol.g, curCol.b, curCol.a + amount / maxHp);
            // this.GetComponentInChildren<SpriteRenderer>().color = newCol;
        }

        if (!successfulHit)
        {
            interactionText.text = "";
            // interactionHoldGO.SetActive(false);
        }

        //if (!successfulHit) interactionText.text = "";

        //if (triggering)
        //{

        //}

    }

    public GameObject FindClosestInteractable()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("Interactable");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    //    private void OnTriggerEnter(Collider other)
    //{
    //    triggering = true;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    triggering = false;
    //}

    void HandleInteraction(Interactable interactable)
    {
        KeyCode key = KeyCode.F;
        switch (interactable.interactionType)
        {
            case Interactable.InteractionType.Click:
                if (Input.GetKeyDown(key))
                {
                    interactable.Interact();
                }
                break;
            case Interactable.InteractionType.Hold:
                if (Input.GetKey(key))
                {
                    interactable.IncreaseHoldTime();
                    if (interactable.GetHoldTime() > 1f)
                    {
                        interactable.Interact();
                        interactable.ResetHoldTime();
                    }
                    else
                    {
                        interactable.ResetHoldTime();
                    }
                }
                interactionHoldProgress.fillAmount = interactable.GetHoldTime();
                break;

            default:
                throw new System.Exception("Unsupported type of interactable.");
        }
    }
}
