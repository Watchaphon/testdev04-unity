using System;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PopupPanel : Panel
{
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text message;
    [Header("Button Label")]
    [SerializeField] private TMP_Text acceptButtonLabel;
    [SerializeField] private TMP_Text cancelButtonLabel;
    [Header("Button Sprite")]
    [SerializeField] private Sprite greenButtonSprite;
    [SerializeField] private Sprite redButtonSprite;
    [Header("Button Image")]
    [SerializeField] private Image acceptButtonImage;
    [SerializeField] private Image cancelButtonImage;
    [Header("Button")]
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button cancelButton;

    private PopupInfo m_info;

    public override void Initialize(IPanelHandler panelHandler)
    {
        base.Initialize(panelHandler);

        SetButtonListener(acceptButton, OnAccept);
        SetButtonListener(cancelButton, OnCancel);
    }

    private void OnAccept()
    {
        m_info.onAccept?.Invoke();

        Close();
    }

    private void OnCancel()
    {
        m_info.onCancel?.Invoke();

        Close();
    }

    public void SetDisplay(PopupInfo info)
    {
        this.m_info = info;

        title.text = info.title;
        message.text = info.message;

        SetLabel(acceptButtonLabel, info.acceptButtonLabel);
        SetLabel(cancelButtonLabel, info.cancelButtonLabel);

        SetActiveButtonFollowCallbackAction(acceptButton, info.onAccept);
        SetActiveButtonFollowCallbackAction(cancelButton, info.onCancel);

        switch (info.type)
        {
            case PopupType.Info:
                SetButtonImageSprite(acceptButtonImage, greenButtonSprite);
                SetButtonImageSprite(cancelButtonImage, redButtonSprite);
                break;
            case PopupType.Error:
                SetButtonImageSprite(acceptButtonImage, redButtonSprite);
                SetButtonImageSprite(cancelButtonImage, redButtonSprite);
                break;
        }
    }

    private void SetLabel(TMP_Text label, string labelTitle)
    {
        if (!label)
            return;

        label.text = labelTitle;
    }

    private void SetButtonListener(Button button, UnityAction action) 
    {
        if (!button)
            return;

        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(action);
    }
    
    private void SetActiveButtonFollowCallbackAction(Button button, Action action)
    {
        if (!button)
            return;

        button.gameObject.SetActive(action != null);
    }

    private void SetButtonImageSprite(Image image, Sprite sprite)
    {
        if (!image)
            return;

        image.sprite = sprite;
    }
}