using System;
using System.Collections.Generic;
using System.Linq;

namespace MokomoGames.Event
{
    public static class EventMessageRunner
    {
        public static void Run(List<EventMessage> messages)
        {
            foreach (var eventMessage in 
                     messages.OrderBy(x => x.Priority))
            {
                eventMessage.ExecuteAction();
            }
            messages.Clear();
        }   
    }
    
    public class EventMessage
    {
        public int Priority { get; }
        public readonly Action ExecuteAction;

        public EventMessage(int priority, Action executeAction)
        {
            Priority = priority;
            ExecuteAction = executeAction;
        }
    }
}