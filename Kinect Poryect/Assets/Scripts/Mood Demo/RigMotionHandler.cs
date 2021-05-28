using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigMotionHandler : MonoBehaviour
{
    public GameObject Hips;
    public GameObject LeftUpLeg;
    public GameObject LeftLeg;
    public GameObject LeftFoot;
    public GameObject LeftToeBase;
    public GameObject LeftToe_End;
    public GameObject RightUpLeg;
    public GameObject RightLeg;
    public GameObject RightFoot;
    public GameObject RightToeBase;
    public GameObject RightToe_End;

    public GameObject Spine;
    public GameObject Spine1;
    public GameObject Spine2;
    //LEFT
    public GameObject LeftShoulder;
    public GameObject LeftArm;
    public GameObject LeftForeArm;
    public GameObject LeftHand;
    public GameObject LeftHandIndex1;
    public GameObject LeftHandIndex2;
    public GameObject LeftHandIndex3;
    public GameObject LeftHandIndex4;
    public GameObject LeftHandMiddle1;
    public GameObject LeftHandMiddle2;
    public GameObject LeftHandMiddle3;
    public GameObject LeftHandMiddle4;
    public GameObject LeftHandPinky1;
    public GameObject LeftHandPinky2;
    public GameObject LeftHandPinky3;
    public GameObject LeftHandPinky4;
    public GameObject LeftHandRing1;
    public GameObject LeftHandRing2;
    public GameObject LeftHandRing3;
    public GameObject LeftHandRing4;
    public GameObject LeftHandThumb1;
    public GameObject LeftHandThumb2;
    public GameObject LeftHandThumb3;
    public GameObject LeftHandThumb4;
    //others
    public GameObject Neck;
    public GameObject Head;
    public GameObject HeadTop_End;
    //RIGHT
    public GameObject RightShoulder;
    public GameObject RightArm;
    public GameObject RightForeArm;
    public GameObject RightHand;
    public GameObject RightHandIndex1;
    public GameObject RightHandIndex2;
    public GameObject RightHandIndex3;
    public GameObject RightHandIndex4;
    public GameObject RightHandMiddle1;
    public GameObject RightHandMiddle2;
    public GameObject RightHandMiddle3;
    public GameObject RightHandMiddle4;
    public GameObject RightHandPinky1;
    public GameObject RightHandPinky2;
    public GameObject RightHandPinky3;
    public GameObject RightHandPinky4;
    public GameObject RightHandRing1;
    public GameObject RightHandRing2;
    public GameObject RightHandRing3;
    public GameObject RightHandRing4;
    public GameObject RightHandThumb1;
    public GameObject RightHandThumb2;
    public GameObject RightHandThumb3;
    public GameObject RightHandThumb4;
    //other params
    public int currentFrame = 0;

    public float animSpeed = 30f/1000f; 


    public FileManager m_FileManager;

    private List<GameObject> bones = new List<GameObject>(); //MAPA
    private Dictionary<string,List<Rotation>> rotations = new Dictionary<string,List<Rotation>>();
    public class Rotation{
        public Rotation(float x,float y,float z){
            rotationX = x;
            rotationY = y;
            rotationZ = z;
        }

        public Rotation(){
            rotationX = 0;
            rotationY = 0;
            rotationZ = 0;
        }
        public float rotationX { get; set; }
        public float rotationY { get; set; }
        public float rotationZ { get; set; }

        internal static Rotation FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            Rotation rotation = new Rotation();
            rotation.rotationX = float.Parse(values[2])/10000;
            rotation.rotationY = float.Parse(values[1])/10000;
            rotation.rotationZ = float.Parse(values[0])/10000;
            // rotation.rotationX = float.Parse(values[2]);
            // rotation.rotationY = float.Parse(values[1]);
            // rotation.rotationZ = float.Parse(values[0]);
            // rotation.rotationX = float.Parse(values[2]);
            // rotation.rotationY = float.Parse(values[1]);
            // rotation.rotationZ = float.Parse(values[0]);
            return rotation;
        }
    }

    private void addBonesToList(){
        bones.Add(Hips);
        bones.Add(LeftUpLeg);
        bones.Add(LeftLeg);
        bones.Add(LeftFoot);
        bones.Add(LeftToeBase);
        bones.Add(LeftToe_End);
        bones.Add(RightUpLeg);
        bones.Add(RightLeg);
        bones.Add(RightFoot);
        bones.Add(RightToeBase);
        bones.Add(RightToe_End);
        bones.Add(Spine);
        bones.Add(Spine1);
        bones.Add(Spine2);
        bones.Add(LeftShoulder);
        bones.Add(LeftArm);
        bones.Add(LeftForeArm);
        bones.Add(LeftHand);
        bones.Add(LeftHandIndex1);
        bones.Add(LeftHandIndex2);
        bones.Add(LeftHandIndex3);
        bones.Add(LeftHandIndex4);
        bones.Add(LeftHandMiddle1);
        bones.Add(LeftHandMiddle2);
        bones.Add(LeftHandMiddle3);
        bones.Add(LeftHandMiddle4);
        bones.Add(LeftHandPinky1);
        bones.Add(LeftHandPinky2);
        bones.Add(LeftHandPinky3);
        bones.Add(LeftHandPinky4);
        bones.Add(LeftHandRing1);
        bones.Add(LeftHandRing2);
        bones.Add(LeftHandRing3);
        bones.Add(LeftHandRing4);
        bones.Add(LeftHandThumb1);
        bones.Add(LeftHandThumb2);
        bones.Add(LeftHandThumb3);
        bones.Add(LeftHandThumb4);
        bones.Add(RightShoulder);
        bones.Add(RightArm);
        bones.Add(RightForeArm);
        bones.Add(RightHand);
        bones.Add(RightHandIndex1);
        bones.Add(RightHandIndex2);
        bones.Add(RightHandIndex3);
        bones.Add(RightHandIndex4);
        bones.Add(RightHandMiddle1);
        bones.Add(RightHandMiddle2);
        bones.Add(RightHandMiddle3);
        bones.Add(RightHandMiddle4);
        bones.Add(RightHandPinky1);
        bones.Add(RightHandPinky2);
        bones.Add(RightHandPinky3);
        bones.Add(RightHandPinky4);
        bones.Add(RightHandRing1);
        bones.Add(RightHandRing2);
        bones.Add(RightHandRing3);
        bones.Add(RightHandRing4);
        bones.Add(RightHandThumb1);
        bones.Add(RightHandThumb2);
        bones.Add(RightHandThumb3);
        bones.Add(RightHandThumb4);
        bones.Add(Neck);
        bones.Add(Head);
        bones.Add(HeadTop_End);
    }

    private void LoadRotationSequenceMap(){
        foreach(GameObject go in bones){
            //Todo --> sacar el mixamo:
            Debug.Log(go.name);
            if(go.name == "mixamorig:RightForeArm"){
                rotations.Add(go.name,getRotationList(go.name));
            }
        }
    }

    private List<Rotation> getRotationList(string name)
    {
        //A partir del nombre devolver la sublist correspondiente de la lista que se trae desde lo de facu
        return m_FileManager.LoadFile(); //Borrar cuando se implemente
        // return crearListaDeEjemplo(); //Borrar cuando se implemente
    }

    private List<Rotation> crearListaDeEjemplo() //Esta lista es para todos lo mismo, es para meter un ejemplo y probar el flujo
    {
        List<Rotation> rot = new List<Rotation>();
        rot.Add(new Rotation(2.0f,2.0f,1.0f));
        rot.Add(new Rotation(3.0f,4.0f,2.0f));
        rot.Add(new Rotation(2.0f,4.0f,2.0f));
        rot.Add(new Rotation(2.0f,6.0f,5.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        rot.Add(new Rotation(1.0f,2.0f,1.0f));
        return rot;
    }
    public void UpdateAnim() {
        // var currentRotation = rotations[currentFrame % rotations.Count];
        // foreach (GameObject go in bones){
        //     // Debug.Log(go.name + " en frame" + currentFrame);
        //     if(go.name == "mixamorig:RightForeArm"){
        //         Rotation currentRotation = rotations[go.name][currentFrame];
        //         // go.transform.Rotate(currentRotation.rotationX,currentRotation.rotationY,currentRotation.rotationZ);
        //         // go.transform.Rotate(currentRotation.rotationX,currentRotation.rotationY,currentRotation.rotationZ);
        //         go.transform.rotation = Quaternion.Euler(currentRotation.rotationX,currentRotation.rotationY,currentRotation.rotationZ);
        //     }
        // }
        Rotation currentRotation = rotations["mixamorig:RightForeArm"][currentFrame];
        Debug.Log("currentRotation.rotationX: " + currentRotation.rotationX);
        Debug.Log("currentRotation.rotationY: " + currentRotation.rotationY);
        Debug.Log("currentRotation.rotationZ: " + currentRotation.rotationZ);
        RightForeArm.transform.eulerAngles.Set(0f,0f,currentRotation.rotationZ);
        RightForeArm.transform.eulerAngles.Set(0f,currentRotation.rotationY, RightForeArm.transform.eulerAngles.z);
        RightForeArm.transform.eulerAngles.Set(currentRotation.rotationX,RightForeArm.transform.eulerAngles.y, RightForeArm.transform.eulerAngles.z);
        
        // RightForeArm.transform.Rotate(currentRotation.rotationX,currentRotation.rotationY,currentRotation.rotationZ,Space.World);
        currentFrame++;
    }

    // private void FixedUpdate() {
    //     UpdateAnim();
    // }

    // Start is called before the first frame update
    void Start()
    {
        addBonesToList();
        LoadRotationSequenceMap();
        //InvokeRepeating("UpdateAnim", 2, animSpeed);
    }
    
}
