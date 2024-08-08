using System.Collections.Generic;
using UnityEngine;

namespace StateMachine
{
    /// <summary>
    /// This is state controller of state machine system.
    /// </summary>
    public class StateController<TType>
    {
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

        public State<TType> curretState { get; private set; }

        private Dictionary<TType, State<TType>> StateConten = new Dictionary<TType, State<TType>>();

        public void SetConten(State<TType> state)
        {
            if(StateConten.ContainsKey(state.type))
            {
                Debug.LogWarningFormat($"State type <{state.type}> aready set in the state conten of the controller");
                return;
            }

            StateConten.Add(state.type, state);
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
        public virtual void Change(State<TType> state)
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
