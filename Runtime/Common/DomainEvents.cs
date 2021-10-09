using System.Collections.Generic;
using System.Linq;
using System;

namespace Scream.UniMO.Common
{
    /// <summary>
    /// <para>This class handles all events.</para>
    /// You can raise event register and unregister event use this class
    /// </summary>
    public static class DomainEvents
    {
        static Dictionary<Type, List<Delegate>> actionsByType;

        /// <summary>
        /// Call this method to subscribe an event
        /// </summary>
        /// <param name="callback">the callback function to subscribe</param>
        /// <typeparam name="T">which event you want to subscribe</typeparam>
        public static void Register<T>(Action<T> callback) where T : IDomainEvent
        {
            var eventType = typeof(T);
            if (actionsByType == null)
                actionsByType = new Dictionary<Type, List<Delegate>>();
            if (!actionsByType.ContainsKey(eventType))
            {
                actionsByType.Add(eventType, new List<Delegate>());
            }
            var actions = actionsByType[eventType];
            if (!actions.Contains(callback))
                actions.Add(callback);
        }

        /// <summary>
        /// Call this method to unsubscribe
        /// </summary>
        /// <param name="callback">the callback function to unsubscribe</param>
        /// <typeparam name="T">which event you want to unsubscribe</typeparam>
        public static void UnRegister<T>(Action<T> callback) where T : IDomainEvent
        {
            if (actionsByType == null)
                return;
            var eventType = typeof(T);
            if (actionsByType.ContainsKey(eventType))
            {
                var actions = actionsByType[eventType];
                actions.Remove(callback);
            }
        }

        /// <summary>
        /// Clear all event have subscribed
        /// </summary>
        public static void Clear()
        {
            if (actionsByType != null)
            {
                actionsByType.Clear();
                actionsByType = null;
            }
        }

        /// <summary>
        /// Call this method to raise the event
        /// </summary>
        /// <param name="args">argument for this event</param>
        /// <typeparam name="T">the event type</typeparam>
        public static void Raise<T>(T args) where T : IDomainEvent
        {
            if (actionsByType != null)
            {
                Type eventType = typeof(T);
                if (actionsByType.ContainsKey(eventType))
                {
                    List<Delegate> actions = actionsByType[eventType];
                    foreach (var action in actions.Cast<Action<T>>().ToList())
                        action(args);
                }
            }
        }
    }

    /// <summary>
    /// base type for event argument
    /// </summary>
    public abstract class IDomainEvent { }

}
