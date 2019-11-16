/// <summary>
/// A base class for event callbacks. Specific events must derive from this class.
/// </summary>
/// <typeparam name="T">The class inherited from Event</typeparam>
public abstract class Event<T> where T : Event<T>
{
    private static event EventListener listeners;

    /// <summary>
    /// The delegate (blueprint) for creating a function to be triggered upon an event
    /// </summary>
    public delegate void EventListener(T info);
    public string description { get; protected set; }
    private bool hasFired;

    /// <summary>
    /// Adds an EventListener.
    /// </summary>
    /// <param name="listener">The EventListener to be added</param>
    public static void RegisterListener(EventListener listener)
    {
        listeners += listener;
    }

    /// <summary>
    /// Removes an EventListener.
    /// </summary>
    /// <param name="listener">The EventListener to be removed</param>
    public static void UnregisterListener(EventListener listener)
    {
        listeners -= listener;
    }

    /// <summary>
    /// Trigger the event.
    /// </summary>
    public void TriggerEvent()
    {
        if (hasFired)
        {
            throw new System.Exception("This event has already triggered, to prevent infinite loops make sure " +
                "events aren't re-triggered");
        }
        hasFired = true;
        if (listeners != null)
        {
            listeners(this as T);
        }
    }
} 