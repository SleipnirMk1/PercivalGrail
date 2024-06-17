using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragController : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Canvas canvas;
    private GraphicRaycaster graphicRaycaster;

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!canvas)
        {
            canvas = GetComponentInParent<Canvas>();
            graphicRaycaster = canvas.GetComponent<GraphicRaycaster>();
        }

        CardController card = transform.parent.gameObject.GetComponent<CardController>();
        if (card != null)
            card.OnStackLifted();

        //Debug.Log("Begin Drag");
        transform.SetParent(canvas.transform, true);
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        this.transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("End Drag");

        List<RaycastResult> results = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, results);

        //int stacks = GetComponent<CardController>().cardsNameStack.Count;
        int count = 0;
        foreach (var hit in results)
        {
            //Debug.Log(hit);

            DropController drop = hit.gameObject.GetComponent<DropController>();
            if (drop && count > 0)
            {
                // Changing parent to card or bg
                transform.SetParent(drop.transform);

                // Set position relative to parent card, if not container
                //if (drop.gameObject.tag != "Container")
                transform.localPosition = new Vector3(0f, -10f, 0f);

                    // get card controller
                    // on get stacked
                CardController card = drop.gameObject.GetComponent<CardController>();
                if (card != null)
                    card.OnGetStacked();

                break;
            }

            count++; // Ignore self
        }
          
    }
}
