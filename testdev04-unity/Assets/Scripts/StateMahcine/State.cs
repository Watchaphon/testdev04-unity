namespace StateMachine
{
    /// <summary>
    /// Base state action class will be reused on the state controller it will nerver create new when change to next state 
    /// that why you need to clear the property everytime state exited for look like create new.
    /// </summary>
    public abstract class State<StateType, Property>
    {
        /// <summary>
        /// Type of state action.
        /// </summary>
        public abstract StateType type { get; }

        protected StateController<StateType, Property> controller { get; private set; }

        /// <summary>
        /// Invoke when this state was changed with state controller.
        /// </summary>
        /// <param name="controller"></param>
        public virtual void Init(StateController<StateType, Property> controller) => this.controller = controller;

        /// <summary>
        /// Invoke when state controller changed to this state.
        /// </summary>
        public abstract void Enter();

        /// <summary>
        /// Invoke every frame after enter this state.
        /// </summary>
        public abstract void Update(float deltaTime);

        /// <summary>
        /// Invoke when state controller changed this to other state.
        /// </summary>
        public abstract void Exit();
    }
}
