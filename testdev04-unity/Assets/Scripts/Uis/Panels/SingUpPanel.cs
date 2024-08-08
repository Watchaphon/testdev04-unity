using APIService.Server;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SingUpPanel : Panel
{
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField userPasswordInput;
    [SerializeField] private TMP_InputField userConfirmPasswordInput;
    [Header("Buttons")]
    [SerializeField] private Button singupButton;
    [SerializeField] private Button backButton;

    public override void Initialize(IPanelHandler panelHandler)
    {
        base.Initialize(panelHandler);

        singupButton.onClick.RemoveAllListeners();
        backButton.onClick.RemoveAllListeners();

        singupButton.onClick.AddListener(OnSingUpButtonClick);
        backButton.onClick.AddListener(OnBackButtonClick);
    }

    private bool TryGetValidateUserInfo(out string userName, out string userPassword, out string invalideMessage)
    {
        userName = userNameInput.text;
        userPassword = userPasswordInput.text;
        invalideMessage = string.Empty;

        if (userName.IsNullOrEmpty())
        {
            invalideMessage = $"Please enter name";
            return false;
        }

        if (userPassword.IsNullOrEmpty())
        {
            invalideMessage = $"Please enter password";
            return false;
        }

        string userPassworldConfirm = userConfirmPasswordInput.text;
        if (userPassworldConfirm.IsNullOrEmpty())
        {
            invalideMessage = $"Please confirm name";
            return false;
        }

        if (userPassword != userPassworldConfirm)
        {
            invalideMessage = $"Password not matches";
            return false;
        }

        return true;
    }

  
    private void OnSingUpButtonClick()
    {
        if (!TryGetValidateUserInfo(out string userName, out string userPassword, out string invalideMessage))
        {
            var popupInfo = PopupInfo.GetAcceptOnlyInfoWithDoNothing(
                PopupType.Error,
                title: "Error",
                invalideMessage,
                acceptButtonLabel: "OK");

            UIManager.Instance.CreateThenOpenPopupPanel<PopupPanel>(popupInfo);
            return;
        }

        SendSingUpRequest(userName, userPassword).Forget();
    }

    private async UniTask SendSingUpRequest(string userName, string userPassword)
    {
        UIManager.Instance.OpenLoadingPanel();

        try
        {
            UserSingUpRequest request = new()
            {
                userName = userName,
                userPassword = userPassword,
            };

            await ServerAPI.Instance.userSingUpService.SendRequest(request);

            var popupInfo = PopupInfo.GetAcceptOnlyInfo(
                PopupType.Info,
                title: "Successfully",
                message: "Your sing up account successfully",
                acceptButtonLabel: "continuse",
                onAccept: Close);

            UIManager.Instance.CreateThenOpenPopupPanel<PopupPanel>(popupInfo);

            UIManager.Instance.CloseLoadingPanel();
        }
        catch (Exception exception)
        {
            UIManager.Instance.CloseLoadingPanel();

            ExceptionDisplayer.Show(exception);
        }
    }

    private void OnBackButtonClick()
    {
        Close();
    }
}