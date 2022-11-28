namespace MokomoGames.StateMachine
{
    public class StateHistoryItem
    {
        public System.Type StateType { get; }
        public StateChangeRequest ChangeRequest { get; }
        
        public StateHistoryItem(System.Type stateType, StateChangeRequest changeRequest)
        {
            StateType = stateType;
            ChangeRequest = changeRequest;
        }
    }
}