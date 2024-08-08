using APIService.Server;
using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : Panel
{
    [SerializeField] private TMP_InputField userNameInput;
    [SerializeField] private TMP_InputField userPasswordInput;
    [Header("Buttons")]
    [SerializeField] private Button loginButton;
    [SerializeField] private Button singUpButton;

    private Action m_onCompletedLogin;

    public override void Initialize(IPanelHandler panelHandler)
    {
        base.Initialize(panelHandler);

        loginButton.onClick.RemoveAllListeners();
        singUpButton.onClick.RemoveAllListeners();

        loginButton.onClick.AddListener(OnLoginButtonClick);
        singUpButton.onClick.AddListener(OnSingupButtonClick);

    }

    public void SetEventListener(Action onCompletedLogin)
    {
        this.m_onCompletedLogin = onCompletedLogin;
    }

    private bool TryGetValidateUserInfo(out string userName, out string userPassword, out string message)
    {
        userName = userNameInput.text;
        userPassword = userPasswordInput.text;
        message = string.Empty;

        if (userName.IsNullOrEmpty())
        {
            message = $"Please enter name";
            return false;
        }

        if (userPassword.IsNullOrEmpty())
        {
            message = $"Please enter password";
            return false;
        }

        return true;
    }

    private void OnLoginButtonClick()
    {
        if (!TryGetValidateUserInfo(out string userName, out string userPassword, out string message))
        {
            var popupInfo = PopupInfo.GetAcceptOnlyInfoWithDoNothing(
                PopupType.Error,
                title: "Error",
                message,
                acceptButtonLabel: "OK");

            UIManager.Instance.CreateThenOpenPopupPanel<PopupPanel>(popupInfo);
            return;
        }


        SendLoginRequest(userName, userPassword).Forget();
    }

    private async UniTask SendLoginRequest(string userName, string userPassword)
    {

        UIManager.Instance.OpenLoadingPanel();

        try
        {
            UserLoginRequest request = new()
            {
                userName = userName,
                userPassword = userPassword,
            };

            UserLoginResponse response = await ServerAPI.Instance.userLoginService.SendRequest(request);

            UserManager.Instance.SetUserId(response.userId);

            var popupInfo = PopupInfo.GetAcceptOnlyInfo(
                PopupType.Info,
                title: "Successfully",
                message: "Your login successfully",
                acceptButtonLabel: "continuse",
                onAccept: Continuse);

            UIManager.Instance.CloseLoadingPanel();
            UIManager.Instance.CreateThenOpenPopupPanel<PopupPanel>(popupInfo);
        }
        catch (Exception exception)
        {
            UIManager.Instance.CloseLoadingPanel();

            ExceptionDisplayer.Show(exception);
        }
    }

    private void Continuse()
    {
        m_onCompletedLogin?.Invoke();
    }

    private void OnSingupButtonClick()
    {
        UIManager.Instance.CreateThenOpenPanel<SingUpPanel>();
    }
}
