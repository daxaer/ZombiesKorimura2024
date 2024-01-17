using UnityEngine;

public class TargetFinder : ITargetFinder
{
    public static TargetFinder Instance => _instance ?? (_instance = new TargetFinder());
    private static TargetFinder _instance;

    public Personaje[] FindTargets()
    {
        return Object.FindObjectsOfType<Personaje>();
    }
}