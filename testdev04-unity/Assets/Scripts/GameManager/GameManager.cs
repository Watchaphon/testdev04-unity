using StateMachine;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private StateController<GameStateType, GameManager> m_stateController;

    private GameInitializeState m_InitializeState;
    private GameAuthenticationState m_AuthenticationState;
    private GameLobbyState m_LobbyState;

    private void Start()
    {
        m_stateController = new();

        m_InitializeState = new();
        m_AuthenticationState = new();
        m_LobbyState = new();

        m_stateController.SetContext(this);
        m_stateController.SetConten(m_InitializeState);
        m_stateController.SetConten(m_AuthenticationState);
        m_stateController.SetConten(m_LobbyState);

        m_stateController.Change(GameStateType.Initialize);
    }
}
