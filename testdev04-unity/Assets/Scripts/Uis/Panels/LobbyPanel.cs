using Cysharp.Threading.Tasks;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyPanel : Panel
{
    [SerializeField] private TMP_Text userDiamondText;
    [SerializeField] private Slider userHeartSlider;
    [Header("Buttons")]
    [SerializeField] private Button userDiamondInceaseButton;
    [SerializeField] private Button userHeartInceaseButton;
    [SerializeField] private Button userHeartDecreaseButton;

    private bool m_isListenToUserDataEvent = false;

    public override void Initialize(IPanelHandler panelHandler)
    {
        base.Initialize(panelHandler);

        userDiamondInceaseButton.onClick.RemoveAllListeners();
        userDiamondInceaseButton.onClick.AddListener(OnUserDiaomondIncreaseButtonClick);

        userHeartInceaseButton.onClick.RemoveAllListeners();
        userHeartInceaseButton.onClick.AddListener(OnUserIncreaseHeartButtonClick);

        userHeartDecreaseButton.onClick.RemoveAllListeners();
        userHeartDecreaseButton.onClick.AddListener(OnUserDecreaseHeartButtonClick);

        userHeartSlider.minValue = 0;
        userHeartSlider.maxValue = 100;
    }

    public override void OnPanelEnable()
    {
        base.OnPanelEnable();

        //Manuap call on entry.
        OnUserDataUpdate(UserManager.Instance.userDiamonds, UserManager.Instance.userHeart);

        if (!m_isListenToUserDataEvent)
        {
            UserManager.Instance.onServerDataUpdate += OnUserDataUpdate;
            m_isListenToUserDataEvent = true;
        }
    }

    public override void OnPanelDisable()
    {
        base.OnPanelDisable();

        if (m_isListenToUserDataEvent)
        {
            UserManager.Instance.onServerDataUpdate -= OnUserDataUpdate;
            m_isListenToUserDataEvent = false;
        }
    }

    private void OnUserDataUpdate(int diamonds, int hearts)
    {
        userDiamondText.text = diamonds.ToString();
        userHeartSlider.value = hearts;
    }

    private void OnUserDiaomondIncreaseButtonClick()
    {
        ModifyDiamond(amount: 100);
    }

    private void OnUserDecreaseHeartButtonClick()
    {
        ModiflyUserHeart(amount: -10);
    }

    private void OnUserIncreaseHeartButtonClick()
    {
        ModiflyUserHeart(amount: 10);
    }

    public void ModifyDiamond(int amount)
    {
        if (amount == 0)
            return;

        UserManager.Instance.ModifyDiamonds(amount).Forget();
    }

    private void ModiflyUserHeart(int amount)
    {
        if (amount == 0)
            return;

        if (amount < 0 && userHeartSlider.value <= userHeartSlider.minValue)
            return;

        if (amount > 0 && userHeartSlider.value >= userHeartSlider.maxValue)
            return;

        UserManager.Instance.ModifyHearts(amount).Forget();
    }
}
