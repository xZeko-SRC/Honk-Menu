using System.Collections.Generic;
using UnityEngine;
using MelonLoader;
using System.Linq;

namespace HonkMenu
{
    internal class RandoBellDrawer : MelonMod
    {
        private GameObject _bell;
        private GameObject _marker;
        private bool _markerEnabled = false;

        private List<Vector3[]> spawnAreas = new List<Vector3[]>
        {
            new Vector3[] { new Vector3(-52.1f, 1.2f, 0.7f), new Vector3(-46.4f, 1.2f, 12.3f) }, // Mini village new Vector3[] { new Vector3(-52.1f, 0.5f, 12.5f), new Vector3(-38.8f, 2.0f, -1.6f) }
            new Vector3[] { new Vector3(-54.3f, 1.2f, 5.5f), new Vector3(-52.8f, 1.2f, 1f) }, // Tower bell
            new Vector3[] { new Vector3(-37.8f, 1.2f, -4.3f), new Vector3(-35.6f, 1.4f, -6.4f) }, // mini bridge right
            new Vector3[] { new Vector3(-44.5f, 1.2f, 4.3f), new Vector3(-37.8f, 1.2f, -4f) }, // mini bridge right
            new Vector3[] { new Vector3(-37.7f, 1.2f, 1.9f), new Vector3(-28.5f, 3.2f, 12f) }, //pub left
            new Vector3[] { new Vector3(-28.7f, 1.9f, -3.4f), new Vector3(-7f, 2.2f, 10.8f) }, //pub Right
            new Vector3[] { new Vector3(-7f, 1.4f, 2.2f), new Vector3(0.1f, 1.5f, 8f) }, //fishing bridge
            new Vector3[] { new Vector3(-13.5f, 2.0f, 21.7f), new Vector3(22.8f, 3.0f, 10.8f) }, //pink house
            new Vector3[] { new Vector3(-1.9f, 1.2f, 0.9f), new Vector3(22.8f, 2.0f, 16.8f) }, // new Vector3(22.8f, 2.0f, 10.8f) },
            new Vector3[] { new Vector3(13.8f, 1.2f, -8.6f), new Vector3(37.2f, 2.0f, -14.2f) }, // high street
            new Vector3[] { new Vector3(-14.1f, 1.2f, 2.1f), new Vector3(2.5f, 2.0f, -21.1f) }, // Gardens new Vector3(1.5f, 2.0f, -18.3f)
            new Vector3[] { new Vector3(-1.9f, 1.2f, 3.1f), new Vector3(34f, 1.2f, 1.4f) }, // Gardens new Vector3(1.5f, 2.0f, -18.3f)
            new Vector3[] { new Vector3(-13.0f, 1.2f, -25.6f), new Vector3(-21.7f, 1.2f, -23.4f) }, // picnic new Vector3(1.5f, 2.0f, -18.3f)
            new Vector3[] { new Vector3(2.5f, 1.2f, -19.6f), new Vector3(36.9f, 1.2f, -10.8f) }, // walkway to highstreet
            new Vector3[] { new Vector3(29.1f, 1.2f, -7.3f), new Vector3(32.6f, 1.2f, -3.4f) }, // garage
            new Vector3[] { new Vector3(16f, 1.5f, -7.8f), new Vector3(18.1f, 1.2f, -5.2f) }, // tv shop
            new Vector3[] { new Vector3(2.4f, 1.5f, -1.2f), new Vector3(10.6f, 1.2f, 0.0f) } // right side of well
        };

        public void RandoBell()
        {
            MelonLogger.Msg("[Honk_Menu] RandoBell called");

            _bell = GameObject.Find("goldenBell");
            if (_bell == null)
            {
                MelonLogger.Error("[Honk_Menu] Bell object not found in the scene.");
                return;
            }

            GameObject parkTerrain = GameObject.Find("ParkTerrain 1");
            if (parkTerrain == null)
            {
                MelonLogger.Error("[Honk_Menu] GameObject 'ParkTerrain 1' not found in the scene.");
                return;
            }

            Collider parkTerrainCollider = parkTerrain.GetComponent<Collider>();
            if (parkTerrainCollider == null)
            {
                MelonLogger.Error("[Honk_Menu] Collider not found on 'ParkTerrain 1'.");
                return;
            }

            Vector3[] randomArea = spawnAreas[Random.Range(0, spawnAreas.Count)];
            Vector3 spawnPosition;

            int maxRetries = 10;
            bool validPositionFound = false;

            for (int i = 0; i < maxRetries; i++)
            {
                spawnPosition = new Vector3(
                    Random.Range(randomArea[0].x, randomArea[1].x),
                    Random.Range(randomArea[0].y, randomArea[1].y),
                    Random.Range(randomArea[0].z, randomArea[1].z)
                );

                _bell.transform.position = spawnPosition;

                if (!parkTerrainCollider.bounds.Contains(_bell.transform.position))
                {
                    MelonLogger.Warning($"[Honk_Menu] Bell is outside ParkTerrain 1 on attempt {i + 1}. Retrying...");
                    continue;
                }

                Collider[] colliders = Physics.OverlapSphere(_bell.transform.position, 0.5f);
                bool hasProblematicColliders = false;

                foreach (var collider in colliders)
                {
                    string colliderName = collider.gameObject.name;
                    MelonLogger.Msg($"[Honk_Menu] Bell is touching: {colliderName}");

                    if (colliderName.Equals("HALL", System.StringComparison.OrdinalIgnoreCase) ||
                        colliderName.ToLowerInvariant().Contains("plantsphere") ||
                        colliderName.ToLowerInvariant().Contains("tilefloor") ||
                        colliderName.ToLowerInvariant().Contains("pubDeckingFloor") ||
                        colliderName.ToLowerInvariant().Contains("newHedge"))
                    {
                        MelonLogger.Warning($"[Honk_Menu] Bell is inside problematic collider: {colliderName}");
                        hasProblematicColliders = true;
                        break;
                    }
                }

                if (hasProblematicColliders)
                {
                    MelonLogger.Warning($"[Honk_Menu] Attempt {i + 1}: Bell placement failed due to collision. Retrying...");
                    continue;
                }

                MelonLogger.Msg($"[Honk_Menu] Bell successfully placed at {spawnPosition} on attempt {i + 1}.");
                validPositionFound = true;
                break;
            }

            if (!validPositionFound)
            {
                MelonLogger.Error("[Honk_Menu] Failed to place the bell within a valid area after retries.");
            }
        }

        public void ToggleMarker()
        {
            if (_bell == null)
            {
                MelonLogger.Error("[Honk_Menu] Cannot toggle marker - Bell not found.");
                return;
            }

            if (_markerEnabled)
            {
                if (_marker != null)
                {
                    GameObject.Destroy(_marker);
                    MelonLogger.Msg("[Honk_Menu] Marker disabled.");
                }
                _markerEnabled = false;
            }
            else
            {
                _marker = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
                _marker.transform.position = _bell.transform.position + new Vector3(0, 10, 0);
                _marker.transform.localScale = new Vector3(0.15f, 20f, 0.15f);

                Renderer renderer = _marker.GetComponent<Renderer>();
                if (renderer != null)
                {
                    Material solidColorMaterial = new Material(Shader.Find("Unlit/Color"));
                    solidColorMaterial.color = Color.yellow;
                    renderer.material = solidColorMaterial;
                }

                _marker.GetComponent<Collider>().enabled = false;

                MelonLogger.Msg($"[Honk_Menu] Marker created at {_marker.transform.position}.");
                _markerEnabled = true;
            }
        }
    }
}
