using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FileManager : MonoBehaviour
{
    public List<RigMotionHandler.Rotation> LoadFile() {
        List<RigMotionHandler.Rotation> values = File.ReadAllLines("Assets\\BVH - SAMPLE - KINECT\\rotationsJere.csv")
                                           .Select(v => RigMotionHandler.Rotation.FromCsv(v))
                                           .ToList();
        return values;
    }
}
