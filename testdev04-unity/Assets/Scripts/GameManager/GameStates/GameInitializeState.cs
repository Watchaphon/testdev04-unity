using StateMachine;

public class GameInitializeState : State<GameStateType>
{
    public override GameStateType type => GameStateType.Initialize;

    public override void Enter()
    {
        UIManager.Instance.Initialize();

        controller.Change(GameStateType.Authentication);
    }

    public override void Exit()
    {
        //Do Nothing.
    }

    public override void Update(float deltaTime)
    {
        //Do Nothing.
    }
}
