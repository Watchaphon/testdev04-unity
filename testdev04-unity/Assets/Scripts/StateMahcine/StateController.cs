using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// This is state controller of state machine system.
    /// </summary>
    public class StateController<TType, TContext>
    {
        /// <summary>
        /// The property that your can custom to very state you create that accessable for the property.
        /// </summary>
        public TContext context { get; private set; }

        public TType priviousStateType { get; private set; }

        public TType currentStateType
        {
            get
            {
                if(curretState ==  null)
                {
                    return default(TType);
                }

                return curretState.type;
            }
        }

        public State<TType, TContext> curretState { get; private set; }

        private Dictionary<TType, State<TType, TContext>> StateConten = new Dictionary<TType, State<TType, TContext>>();

        public void SetConten(State<TType, TContext> state)
        {
            if(StateConten.ContainsKey(state.type))
            {
                Debug.LogWarningFormat($"State type <{state.type}> aready set in the state conten of the controller");
                return;
            }

            StateConten.Add(state.type, state);
        }

        public void SetContext(TContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Call for change state to target state with state type.
        /// </summary>
        /// <param name="stateType"></param>
        public virtual void Change(TType stateType)
        {
            if (!StateConten.ContainsKey(stateType))
            {
                Debug.LogErrorFormat($"State type <{stateType}> not extis or set conten for the controller");
                return;
            }

            Change(StateConten[stateType]);
        }

        /// <summary>
        /// Call for change state to target state.
        /// </summary>
        /// <param name="state"></param>
        public virtual void Change(State<TType, TContext> state)
        {
            if (curretState != null)
            {
                priviousStateType = curretState.type;
                curretState.Exit();
            }

            curretState = state;

            if (state == null)
            {
                Debug.LogErrorFormat($"The state can't be null");
                return;
            }

            curretState.Init(this);
            curretState.Enter();
        }

        public virtual void Update(float deltaTime)
        {
            if(curretState == null)
                return;

            curretState.Update(deltaTime);
        }
    }
}
