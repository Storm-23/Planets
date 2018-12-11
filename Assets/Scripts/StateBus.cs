using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using PlanetsCore;
using UnityEngine;

/// <summary>
/// State bus
/// </summary>
class StateBus
{
    #region Custom states

    //current mouse pos
    public static Vector2 MousePos;
    //user click mouse in this frame
    public static bool MouseIsDown;
    //is mouse down at the moment?
    public static bool MouseDown;
    //mouse scroll
    public static Vector2 MouseScroll;
    //target Transform for camera
    public static Transform CameraTarget;
    //target body for camera
    public static Body CameraTargetBody;
    //target OrbitalBodyDescription for camera
    public static OrbitalBodyDescription CameraTargetDesc;
    //CameraTargetChanged event
    public static StateQueue<bool> CameraTargetChanged;
    //PlayerPrefsChanged event
    public static StateQueue<bool> PlayerPrefsChanged;
    //current model
    public static Model Model;
    //scale for distances
    public static float DistanceScale{ get { return DistanceScaleRelativeToSize * SizeScale; } }
    public static float DistanceScaleRelativeToSize = 0.1f;
    //scale for sizes of objects
    public static float SizeScale = (float)(1 / Constants.EARTH_RADIUS);
    //use logarithmic scale for object's size
    public static bool LogarithmicSize = true;
    //current model time (in seconds)
    public static double ModelTime = 0;
    //model time multiplier
    public static int TimeMultiplier = 1;
    //coeff to translate real time to model time
    public static int RealTimeToModelTime = 1 * 60 * 60;//1 hour per second
    //stop model time
    public static bool StopTime = false;
    //model loaded event
    public static StateQueue<bool> ModelLoaded;
    //map scale coeff
    public static float MapScale = 1.5f;

    #endregion

    #region Standard events

    public static event Action OnAwake = delegate { };
    public static event Action OnStart = delegate { };
    public static event Action OnUpdate = delegate { };
    public static event Action OnLateUpdate = delegate { };

    #endregion

    #region Delayed Actions

    private static SortedDictionary<float, Queue<Action>> delayedActions = new SortedDictionary<float, Queue<Action>>();

    public static void EnqueueDelayed(Action act, float delayTime)
    {
        if (delayTime < 0)
            delayTime = 0;
        var time = Time.time + delayTime;

        Queue<Action> queue;
        if (!delayedActions.TryGetValue(time, out queue))
        {
            queue = new Queue<Action>(1);
            delayedActions.Add(time, queue);
        }

        queue.Enqueue(act);
    }

    static bool ExecuteDelayedQueue()
    {
        if (delayedActions.Count == 0)
            return false;
        var time = Time.time;
        foreach (var pair in delayedActions)
        {
            if (pair.Key > time) return false;
            foreach (var act in pair.Value)
                try
                {
                    act();
                }
                catch { }

            delayedActions.Remove(pair.Key);
            return true;
        }

        return false;
    }

    #endregion

    #region Utils

    private static readonly List<IStateQueue> stateQueues = new List<IStateQueue>();

    class Updater : MonoBehaviour
    {
        private void Awake() { StateBus.Awake(); }
        private void Start() { StateBus.Start(); }
        private void Update() { StateBus.Update(); }
        private void LateUpdate() { StateBus.LateUpdate(); }
    }

    [RuntimeInitializeOnLoadMethod]
    static void Init()
    {
        var updater = new GameObject() { name = "StateBusUpdater" };
        updater.AddComponent<Updater>();
        GameObject.DontDestroyOnLoad(updater);
    }

    static void Awake()
    {
        stateQueues.Clear();

        foreach (var fi in typeof(StateBus).GetFields())
            if (typeof(IStateQueue).IsAssignableFrom(fi.FieldType))
            {
                IStateQueue stateQueue = fi.GetValue(null) as IStateQueue;
                //create StateQueue object, if not created
                if (stateQueue == null)
                {
                    stateQueue = Activator.CreateInstance(fi.FieldType) as IStateQueue;
                    fi.SetValue(null, stateQueue);
                }
                //save to queues list
                stateQueues.Add(stateQueue);
            }
        OnAwake();
    }

    static void Start()
    {
        OnStart();
    }

    static void Update()
    {
        OnUpdate();
        while (ExecuteDelayedQueue()) ;
    }

    static void LateUpdate()
    {
        OnLateUpdate();

        foreach (var queue in stateQueues)
            queue.Dequeue();
    }

    struct QueueItem<T>
    {
        public T Value;
        public float TimeToFire;
    }

    interface IStateQueue
    {
        void Dequeue();
    }

    /// <summary>
    /// Queue of states
    /// </summary>
    public class StateQueue<T> : IStateQueue
    {
        // Queue of events
        private Queue<QueueItem<T>> queue = new Queue<QueueItem<T>>();

        /// <summary>
        /// Current value of state (in current frame)
        /// </summary>
        public T Value { get; private set; }

        /// <summary>
        /// Put event to queue
        /// </summary>
        public void Enqueue(T value, float deltaTime = 0)
        {
            queue.Enqueue(new QueueItem<T> { Value = value, TimeToFire = Time.time + deltaTime });
        }

        /// <summary>
        /// Implicit conversion to T
        /// </summary>
        public static implicit operator T(StateQueue<T> val)
        {
            return val.Value;
        }

        /// <summary>
        /// Conversion to true/false
        /// </summary>
        public static bool operator true(StateQueue<T> val)
        {
            return !System.Object.Equals(val.Value, default(T));
        }

        /// <summary>
        /// Conversion to true/false
        /// </summary>
        public static bool operator false(StateQueue<T> val)
        {
            return System.Object.Equals(val.Value, default(T));
        }

        /// <summary>
        /// Put event to queue via operator +
        /// </summary>
        public static StateQueue<T> operator +(StateQueue<T> vq, T val)
        {
            vq.Enqueue(val);
            return vq;
        }

        /// <summary>
        /// Clear queue, set default value
        /// </summary>
        public void Reset()
        {
            queue.Clear();
            Value = default(T);
        }

        void IStateQueue.Dequeue()
        {
            var count = queue.Count;
            for (int i = 0; i < count; i++)
            {
                //get next event
                var item = queue.Dequeue();

                //time elapsed?
                if (item.TimeToFire <= Time.time)
                {
                    //set event to current value
                    Value = item.Value;
                    return;
                }

                //time is not elapsed => enqueue again
                queue.Enqueue(item);
            }

            //set default value
            Value = default(T);
        }
    }

    #endregion
}