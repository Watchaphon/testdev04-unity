using Cysharp.Threading.Tasks;
using System;
using UnityEngine;

public static class ExceptionDisplayer
{
    public static void Show(Exception exception)
    {
        Debug.LogException(exception);

        string message = exception.Message;

        if(exception is UnityWebRequestException webRequestException)
        {
            message = $"{webRequestException.HelpLink} {webRequestException.Error}";
        }

        //In real case don't use message form exception to show on popup, instead use res code to get localization string to show error because in game user can atcualy change the langues.
        var popupInfo = PopupInfo.GetAcceptOnlyInfoWithDoNothing(PopupType.Error, title: "Error", message, acceptButtonLabel: "OK");
        UIManager.Instance.CreateThenOpenPopupPanel<PopupPanel>(popupInfo);
    }
}
