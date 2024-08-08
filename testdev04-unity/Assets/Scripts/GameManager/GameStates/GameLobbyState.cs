using Cysharp.Threading.Tasks;
using StateMachine;

public class GameLobbyState : State<GameStateType>
{
    public override GameStateType type => GameStateType.Lobby;

    public override void Enter()
    {
        UIManager.Instance.CreateThenOpenPanel<LobbyPanel>();

        UserManager.Instance.UpdateUserData().Forget();
    }

    public override void Exit()
    {
        UIManager.Instance.CloseThenDestoryPanel<LobbyPanel>();
    }

    public override void Update(float deltaTime)
    {
        //Do nothing.
    }
}
