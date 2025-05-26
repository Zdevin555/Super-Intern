using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningManager : MonoBehaviour
{
    public static WarningManager Instance
    {
        private set; get;
    }
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
    public GameObject WarningItem;

    public Canvas WarningCanvas;

    private List<WarningItem> m_WarningItems;


    private void Start()
    {
        m_WarningItems = new List<WarningItem>();
    }

    private void Update()
    {
        for (int i = 0; i < m_WarningItems.Count; i++)
        {
            var item = m_WarningItems[i];
            item.UpdatePoint();
            if (item.IsHide())
            {
                m_WarningItems.Remove(item);
                Destroy(item);
            }
        }
    }

    public WarningItem ShowWarningItem(Transform owner)
    {
        var item = Instantiate(WarningItem, WarningCanvas.transform);
        var warningItem = item.GetComponent<WarningItem>();
        warningItem.Init(owner, WarningCanvas);
        m_WarningItems.Add(warningItem);
        return warningItem;
    }


}
