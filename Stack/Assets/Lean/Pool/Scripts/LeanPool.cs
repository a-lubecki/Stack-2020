using UnityEngine;
using System.Collections.Generic;
using Lean.Common;

namespace Lean.Pool
{
	/// <summary>This class handles the association between a spawned prefab, and the LeanGameObjectPool component that manages it.</summary>
	public static class LeanPool
	{
		public const string HelpUrlPrefix = LeanHelper.HelpUrlPrefix + "LeanPool#";

		public const string ComponentPathPrefix = LeanHelper.ComponentPathPrefix + "Pool/Lean ";

		/// <summary>This stores all references between a spawned GameObject and its pool.</summary>
		public static Dictionary<GameObject, LeanGameObjectPool> Links = new Dictionary<GameObject, LeanGameObjectPool>();

		/// <summary>This allows you to spawn a prefab via Component.</summary>
		public static T Spawn<T>(T prefab, Transform parent = null, bool worldPositionStays = false)
			where T : Component
		{
			// Clone this component's GameObject
			var gameObject = prefab != null ? prefab.gameObject : null;
			var clone      = Spawn(gameObject, parent, worldPositionStays);

			// Return the same component from the clone
			return clone != null ? clone.GetComponent<T>() : null;
		}

		/// <summary>This allows you to spawn a prefab via Component.</summary>
		public static T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null, bool worldPositionStays = true)
			where T : Component
		{
			// Clone this component's GameObject
			var gameObject = prefab != null ? prefab.gameObject : null;
			var clone      = Spawn(gameObject, position, rotation, parent, worldPositionStays);

			// Return the same component from the clone
			return clone != null ? clone.GetComponent<T>() : null;
		}

		/// <summary>This allows you to spawn a prefab via GameObject.</summary>
		public static GameObject Spawn(GameObject prefab, Transform parent = null, bool worldPositionStays = false)
		{
			if (prefab != null)
			{
				return Spawn(prefab, prefab.transform.position, prefab.transform.rotation, parent, worldPositionStays);
			}
			else
			{
				Debug.LogError("Attempting to spawn a null prefab.");
			}

			return null;
		}

		/// <summary>This allows you to spawn a prefab via GameObject.</summary>
		public static GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null, bool worldPositionStays = true)
		{
			if (prefab != null)
			{
				// Find the pool that handles this prefab
				var pool = default(LeanGameObjectPool);

				// Create a new pool for this prefab?
				if (LeanGameObjectPool.TryFindPoolByPrefab(prefab, ref pool) == false)
				{
					pool = new GameObject("LeanPool (" + prefab.name + ")").AddComponent<LeanGameObjectPool>();

					pool.Prefab = prefab;
				}

				// Try and spawn a clone from this pool
				var clone = default(GameObject);

				if (pool.TrySpawn(position, rotation, parent, worldPositionStays, ref clone) == true)
				{
					// Clone already registered?
					if (Links.Remove(clone) == true)
					{
						// If this pool recycles clones, then this can be expected
						if (pool.Recycle == true)
						{
							
						}
						// This shouldn't happen
						else
						{
							Debug.LogWarning("You're attempting to spawn a clone that hasn't been despawned. Make sure all your Spawn and Despawn calls match, you shouldn't be manually destroying them!", clone);
						}
					}

					// Associate this clone with this pool
					Links.Add(clone, pool);

					return clone;
				}
			}
			else
			{
				Debug.LogError("Attempting to spawn a null prefab.");
			}

			return null;
		}

		/// <summary>This will despawn all pool clones.</summary>
		public static void DespawnAll()
		{
			foreach (var instance in LeanGameObjectPool.Instances)
			{
				instance.DespawnAll();
			}

			Links.Clear();
		}

		/// <summary>This allows you to despawn a clone via Component, with optional delay.</summary>
		public static void Despawn(Component clone, float delay = 0.0f)
		{
			if (clone != null) Despawn(clone.gameObject, delay);
		}

		/// <summary>This allows you to despawn a clone via GameObject, with optional delay.</summary>
		public static void Despawn(GameObject clone, float delay = 0.0f)
		{
			if (clone != null)
			{
				var pool = default(LeanGameObjectPool);

				// Try and find the pool associated with this clone
				if (Links.TryGetValue(clone, out pool) == true)
				{
					// Remove the association
					Links.Remove(clone);

					pool.Despawn(clone, delay);
				}
				else
				{
					if (LeanGameObjectPool.TryFindPoolByClone(clone, ref pool) == true)
					{
						pool.Despawn(clone, delay);
					}
					else
					{
						Debug.LogWarning("You're attempting to despawn a gameObject that wasn't spawned from this pool", clone);

						// Fall back to normal destroying
						Object.Destroy(clone);
					}
				}
			}
			else
			{
				Debug.LogWarning("You're attempting to despawn a null gameObject", clone);
			}
		}
	}
}