using System;

public static class GlobalEvents
{
    public static event Action<ResourceType, int> OnResourceCollected;

    public static void InvokeOnResourceCollected(ResourceType resourceType, int value) => OnResourceCollected?.Invoke(resourceType, value);
}
