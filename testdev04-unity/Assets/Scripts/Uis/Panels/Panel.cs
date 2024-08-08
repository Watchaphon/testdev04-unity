using UnityEngine;

public abstract class Panel : MonoBehaviour
{
    private IPanelHandler m_panelHandler;

    public virtual void Initialize(IPanelHandler panelHandler)
    {
        this.m_panelHandler = panelHandler;
    }

    public void Open()
    {
        OnPanelEnable();
    }

    public void Close() 
    {
        m_panelHandler.CloseThenDestroyPanel();
    }

    public virtual void OnPanelEnable()
    {
        gameObject.SetActive(true);
    }

    public virtual void OnPanelDisable()
    {
        gameObject.SetActive(false);
    }
}
