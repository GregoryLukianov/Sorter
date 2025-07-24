using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Events
{
    public class EventBus
    {
        private Dictionary<Type, SubscribersList<IGlobalSubscriber>> s_Subscribers
            = new Dictionary<Type, SubscribersList<IGlobalSubscriber>>();

        private Dictionary<Type, List<Type>> s_CachedSubscriberTypes
            = new Dictionary<Type, List<Type>>();

        public void Subscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetSubscriberTypes(subscriber.GetType());
            foreach (Type t in subscriberTypes)
            {
                if (!s_Subscribers.ContainsKey(t))
                    s_Subscribers[t] = new SubscribersList<IGlobalSubscriber>();
                s_Subscribers[t].Add(subscriber);
            }
        }

        public void Unsubscribe(IGlobalSubscriber subscriber)
        {
            List<Type> subscriberTypes = GetSubscriberTypes(subscriber.GetType());
            foreach (Type t in subscriberTypes)
            {
                if (s_Subscribers.ContainsKey(t))
                    s_Subscribers[t].Remove(subscriber);
            }
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void RaiseEvent<TSubscriber>(Action<TSubscriber> action) 
            where TSubscriber : class, IGlobalSubscriber
        {
            if (!s_Subscribers.ContainsKey(typeof(TSubscriber)))
                return;

            SubscribersList<IGlobalSubscriber> subscribers = s_Subscribers[typeof(TSubscriber)];

            var subscribersCopy = new List<IGlobalSubscriber>(subscribers.List);

            subscribers.Executing = true;
            foreach (IGlobalSubscriber subscriber in subscribersCopy)
            {
                if (subscriber == null) continue;
                try
                {
                    action.Invoke(subscriber as TSubscriber);
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                }
            }
            subscribers.Executing = false;
            subscribers.Cleanup();
        }

        private List<Type> GetSubscriberTypes(Type type)
        {
            if (s_CachedSubscriberTypes.ContainsKey(type))
                return s_CachedSubscriberTypes[type];

            List<Type> subscriberTypes = type
                .GetInterfaces()
                .Where(it =>
                    typeof(IGlobalSubscriber).IsAssignableFrom(it) &&
                    it != typeof(IGlobalSubscriber))
                .ToList();

            s_CachedSubscriberTypes[type] = subscriberTypes;
            return subscriberTypes;
        }
    }
}