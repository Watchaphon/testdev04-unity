using System;

public struct PopupInfo
{
    public PopupType type;
    public string title;
    public string message;
    public string acceptButtonLabel;
    public string cancelButtonLabel;
    public Action onAccept;
    public Action onCancel;

    public static PopupInfo GetInfo(PopupType type, string title, string message, string accepButtonLabel, string cancelButtonLabel, Action onAccept, Action onCancel)
    {
        PopupInfo popupInfo = new()
        {
            type = type,
            title = title,
            message = message,
            acceptButtonLabel = accepButtonLabel,
            cancelButtonLabel = cancelButtonLabel,
            onAccept = onAccept,
            onCancel = onCancel,
        };

        return popupInfo;
    }

    public static PopupInfo GetInfoWithDoNothingCancel(PopupType type, string title, string message, string acceptButtonLabel, string cancelButtonLabel, Action onAccept)
    {
        return GetInfo(type, title, message, acceptButtonLabel, cancelButtonLabel: string.Empty, onAccept, () => { });
    }

    public static PopupInfo GetAcceptOnlyInfo(PopupType type, string title, string message, string acceptButtonLabel, Action onAccept)
    {
        return GetInfo(type, title, message, acceptButtonLabel, cancelButtonLabel: string.Empty, onAccept, null);
    }

    public static PopupInfo GetAcceptOnlyInfoWithDoNothing(PopupType type, string title, string message, string acceptButtonLabel)
    {
        return GetInfo(type, title, message, acceptButtonLabel, cancelButtonLabel: string.Empty, () => { }, null);
    }
}

public enum PopupType
{
    Info,
    Error,
}