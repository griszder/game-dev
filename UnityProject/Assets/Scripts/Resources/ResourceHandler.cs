using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class ResourceHandler {
	public static Dictionary<ResourceType, Resource> Resources = new();

	static ResourceHandler() {
		Resources.Add(ResourceType.Wood, new Resource(ResourceType.Wood));
		Resources.Add(ResourceType.Stone, new Resource(ResourceType.Stone));
		Resources.Add(ResourceType.Iron, new Resource(ResourceType.Iron));
	}

	// ReSharper disable Unity.PerformanceAnalysis
	public static bool AddResources(ResourceType type, int amount) {
		var complete =  Resources[type].Add(amount);

		OnUpdate(type);
		return complete;
	}
	
	// ReSharper disable Unity.PerformanceAnalysis
	public static bool UseResources(ResourceType type, int amount) {
		var complete = Resources[type].Use(amount);

		OnUpdate(type);
		return complete;
	}

	// ReSharper disable Unity.PerformanceAnalysis
	private static void OnUpdate(ResourceType type) {
		ResourceBar.Instance.SetResourceAmount(type, Resources[type].Amount);
	}
	
	public static void Update() {
		OnUpdate(ResourceType.Wood);
		OnUpdate(ResourceType.Stone);
		OnUpdate(ResourceType.Iron);
	}
}