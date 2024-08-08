using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private RectTransform mainParent;
    [Header("Generals")]
    [SerializeField] private PanelHandler<LoginPanel> loginPanelHandler = new();
    [SerializeField] private PanelHandler<SingUpPanel> singupPanelHandler = new();
    [SerializeField] private PanelHandler<LobbyPanel> lobbyPanelHandler = new();
    [Header("Popups")]
    [SerializeField] private PanelHandler<PopupPanel> popupPanelHandler = new();
    [Header("Overlays")]
    [SerializeField] private PanelHandler<LoadingPanel> loadingPanelHandler = new();

    private Dictionary<System.Type, IPanelHandler> m_panelHandlerDict;

    public void Initialize()
    {
        m_panelHandlerDict = new()
        {
            { loginPanelHandler.PanelType, loginPanelHandler },
            { singupPanelHandler.PanelType, singupPanelHandler },
            { lobbyPanelHandler.PanelType, lobbyPanelHandler },
            { popupPanelHandler.PanelType, popupPanelHandler },
            { loadingPanelHandler.PanelType, loadingPanelHandler },
        };
    }

    private PanelHandler<TPanel> GetPanelHandler<TPanel>() where TPanel : Panel
    {
        var type = typeof(TPanel);

        if (!m_panelHandlerDict.TryGetValue(type, out IPanelHandler iHandler))
        {
            throw new System.Exception($"Panel type <{type}> are not exist");
        }

        return (PanelHandler<TPanel>)iHandler;
    }

    public TPanel CreatePanel<TPanel>() where TPanel : Panel
    {
        var panelHandler = GetPanelHandler<TPanel>();
        return panelHandler.CreatePanel(mainParent);
    }

    public TPanel CreateThenOpenPanel<TPanel>() where TPanel : Panel
    {
        var panelHandler =  GetPanelHandler<TPanel>();
        return panelHandler.CreateThenOpenPanel(mainParent);
    }

    public TPanel CreateThenOpenPopupPanel<TPanel>(PopupInfo info) where TPanel : PopupPanel
    {
        var panelHandler = GetPanelHandler<TPanel>();
        var panel = panelHandler.CreateThenOpenPanel(mainParent);
        panel.SetDisplay(info);
        return panel;
    }

    public void CloseThenDestoryPanel<TPanel>() where TPanel : Panel
    {
        var panelHandler = GetPanelHandler<TPanel>();
        panelHandler.CloseThenDestroyPanel();
    }

    public void OpenLoadingPanel()
    {
        CreateThenOpenPanel<LoadingPanel>();
    }

    public void CloseLoadingPanel()
    {
        CloseThenDestoryPanel<LoadingPanel>();
    }
}
