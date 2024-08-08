using StateMachine;

public class GameAuthenticationState : State<GameStateType>
{
    public override GameStateType type => GameStateType.Authentication;

    public override void Enter()
    {
        var loginPanel = UIManager.Instance.CreateThenOpenPanel<LoginPanel>();
        loginPanel.SetEventListener(OnLoginCompleted);
    }

    public override void Exit()
    {
        UIManager.Instance.CreateThenOpenPanel<LoginPanel>();
    }

    public override void Update(float deltaTime)
    {
        
    }

    private void OnLoginCompleted()
    {
        controller.Change(GameStateType.Lobby);
    }
}
