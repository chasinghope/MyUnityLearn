using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class CustomLayout : MonoBehaviour
{
    [SerializeField]
    private RectTransform rectTrans;

    //public float top;
    //public float bottom;
    //public float left;
    //public float rigth;

    public List<RectTransform> ActiveChilds;


    private bool hasChild => this.ActiveChilds != null && this.ActiveChilds.Count != 0;

    private void Awake()
    {
        rectTrans = this.GetComponent<RectTransform>();
        ActiveChilds = new List<RectTransform>();
    }

    

    protected virtual void OnValidate()
    {
        GetChildren();
        Debug.Log($"child number: {this.ActiveChilds.Count}");
        if (hasChild)
        {
            SetChildrenPosition();
            SetSize();
        }
    }

    private void Update()
    {
        //Rect rect = this.rectTrans.rect;
        //Debug.Log($"x: {rect.x}     y: {rect.y}     w: {rect.width}     h: {rect.height}");
        GetChildren();
        Debug.Log($"child number: {this.ActiveChilds.Count}");
        if (hasChild)
        {
            SetChildrenPosition();
            SetSize();
        }


    }


    private void GetChildren()
    {
        this.ActiveChilds.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).gameObject.activeSelf)
            {
                this.ActiveChilds.Add(transform.GetChild(i).GetComponent<RectTransform>());
            }
        }
    }

    protected virtual void SetChildrenPosition()
    {
    }

    private void SetSize()
    {
        float up = float.MinValue;
        float right = float.MinValue;
        float down = float.MaxValue;
        float left = float.MaxValue;

        for (int i = 0; i < ActiveChilds.Count; i++)
        {
            if (left > ActiveChilds[i].localPosition.x + ActiveChilds[i].rect.x)
                left = ActiveChilds[i].localPosition.x + ActiveChilds[i].rect.x;
            if (down > ActiveChilds[i].localPosition.y + ActiveChilds[i].rect.y)
                down = ActiveChilds[i].localPosition.y + ActiveChilds[i].rect.y;
            if (right < ActiveChilds[i].localPosition.x + ActiveChilds[i].rect.x + ActiveChilds[i].rect.width)
                right = ActiveChilds[i].localPosition.x + ActiveChilds[i].rect.x + ActiveChilds[i].rect.width;
            if (up < ActiveChilds[i].localPosition.y + ActiveChilds[i].rect.y + ActiveChilds[i].rect.height)
                up = ActiveChilds[i].localPosition.y + ActiveChilds[i].rect.y + ActiveChilds[i].rect.height;
        }
        Debug.Log($"width: {right - left}   height: {up - down}");
        rectTrans.sizeDelta = new Vector2(right - left, up - down);
    }

}
