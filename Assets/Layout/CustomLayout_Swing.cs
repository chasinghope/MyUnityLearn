using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomLayout_Swing : CustomLayout
{
    public float SwingX;
    public bool IsModify;
    public List<Vector3> ChildsLocalPos;

    public List<Vector3> OriginChildrenPos;

    private void Awake()
    {
        if(transform.childCount != 0)
        {
            this.OriginChildrenPos = new List<Vector3>();
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                this.OriginChildrenPos.Add(transform.GetChild(i).localPosition);
            }
        }
    }


    protected override void SetChildrenPosition()
    {
        base.SetChildrenPosition();
        if (!IsModify)
        {
            ChildsLocalPos = new List<Vector3>();

            for (int i = 0; i < this.ActiveChilds.Count; i++)
            {
                if (i % 2 == 1)
                    this.ActiveChilds[i].localPosition = new Vector3(this.ActiveChilds[i].localPosition.x + this.SwingX, this.ActiveChilds[i].localPosition.y, this.ActiveChilds[i].localPosition.z);

                ChildsLocalPos.Add(this.ActiveChilds[i].localPosition);
            }

            IsModify = true;
        }
        else
        {
            for (int i = 0; i < this.ActiveChilds.Count; i++)
            {
                this.ActiveChilds[i].localPosition = this.ChildsLocalPos[i];
            }
        }
    }


    private void OnDisable()
    {
        IsModify = false;
        ChildsLocalPos = null;
    }


    protected override void OnValidate()
    {
        if (transform.childCount != 0)
        {
            for (int i = 0; i < transform.childCount - 1; i++)
            {
                transform.GetChild(i).localPosition = this.OriginChildrenPos[i];
            }
        }

        IsModify = false;
        ChildsLocalPos = null;
        base.OnValidate();
    }
}
