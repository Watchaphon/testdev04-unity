using UnityEngine;

[System.Serializable]
public class PanelHandler<TPanel> : IPanelHandler  where TPanel : Panel
{
    [SerializeField] TPanel prefab;
    [Tooltip("This use to fixed what render parent your want if let it null it away be the last order when created")]
    [SerializeField] private RectTransform fixedParent;

    private TPanel m_panelInstance;

    public System.Type PanelType
    {
        get
        {
            return typeof(TPanel);
        }
    }

    public TPanel CreatePanel(RectTransform parent)
    {
        RectTransform resultParent = fixedParent ? fixedParent : parent;

        if (m_panelInstance == null)
        {
            //***In real case shoud use pooling system instead if it request many time at shourt term for beter performance but will store more memore at runtime***.
            m_panelInstance = Object.Instantiate(prefab, resultParent);
            m_panelInstance.transform.SetAsLastSibling();
            m_panelInstance.transform.localScale = Vector3.one;
        }

        m_panelInstance.Initialize(this);
        return m_panelInstance;
    }

    public TPanel CreateThenOpenPanel(RectTransform parent)
    {
        CreatePanel(parent);

        m_panelInstance.Open();

        return m_panelInstance;
    }

    public void CloseThenDestroyPanel()
    {
        if (!m_panelInstance)
            return;

        m_panelInstance.OnPanelDisable();

        Object.Destroy(m_panelInstance.gameObject);
        m_panelInstance = null;
    }

}
