using UnityEngine;
using MelonLoader;
using System.Collections.Generic;

namespace HonkMenu
{
    class CreateItems : MelonMod
    {
        private static bool _wireframe;
        public static GameObject _goose;

        private static List<GameObject> capsules = new List<GameObject>();
        private static List<GameObject> cubes = new List<GameObject>();
        private static List<GameObject> spheres = new List<GameObject>();
        private static List<GameObject> cylinders = new List<GameObject>();

        public static void WireFrame()
        {
            _wireframe = !_wireframe;
            GL.wireframe = _wireframe;
        }

        public static void CreateCapsule()
        {
            _goose = GameObject.Find("Goose");
            GameObject capsule = GameObject.CreatePrimitive(PrimitiveType.Capsule);
            capsule.transform.position = _goose.transform.position;
            capsules.Add(capsule);
        }

        public static void CreateCube()
        {
            _goose = GameObject.Find("Goose");
            GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.transform.position = _goose.transform.position;
            cubes.Add(cube);
        }

        public static void CreateSphere()
        {
            _goose = GameObject.Find("Goose");
            GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            sphere.transform.position = _goose.transform.position;
            spheres.Add(sphere);
        }

        public static void CreateCylinder()
        {
            _goose = GameObject.Find("Goose");
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.position = _goose.transform.position;
            cylinders.Add(cylinder);
        }

        public static void DestroyCapsule()
        {
            if (capsules.Count > 0)
            {
                GameObject capsule = capsules[capsules.Count - 1];
                GameObject.Destroy(capsule);
                capsules.RemoveAt(capsules.Count - 1);
            }
        }

        public static void DestroyCube()
        {
            if (cubes.Count > 0)
            {
                GameObject cube = cubes[cubes.Count - 1];
                GameObject.Destroy(cube);
                cubes.RemoveAt(cubes.Count - 1);
            }
        }

        public static void DestroySphere()
        {
            if (spheres.Count > 0)
            {
                GameObject sphere = spheres[spheres.Count - 1];
                GameObject.Destroy(sphere);
                spheres.RemoveAt(spheres.Count - 1);
            }
        }

        public static void DestroyCylinder()
        {
            if (cylinders.Count > 0)
            {
                GameObject cylinder = cylinders[cylinders.Count - 1];
                GameObject.Destroy(cylinder);
                cylinders.RemoveAt(cylinders.Count - 1);
            }
        }
    }
}
