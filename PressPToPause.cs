using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using UnityEngine;

namespace GraphicsPluginTest
{
    class PressPToPause : MonoBehaviour
    {
        bool paused = false;
        float timescale = 1;

        void Start()
        {
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            //Console.WriteLine(Time.time);
            if (Input.GetKeyDown(KeyCode.P))
            {
                if (!paused)
                {
                    timescale = Time.timeScale;
                    Time.timeScale = 0.001f;
                    paused = true;
                    Console.WriteLine("pauserinoed");
                }
                else
                {
                    Time.timeScale = timescale;
                    paused = false;
                    Console.WriteLine("unpaused");
                }
            }
            if (Input.GetKeyDown(KeyCode.X))
            {
                Plugin.Log("searching through heirarchy");
                GameObject[] gos = FindObjectsOfType<GameObject>();
                StringBuilder sb = new StringBuilder();

                foreach (GameObject go in gos)
                {
                    if (go.transform.parent == null)
                    {
                        Map(go, sb, 0);
                    }
                }


                System.IO.File.WriteAllText("Heirarchy Dump.txt", sb.ToString());
                Plugin.Log("Heirarchy dumped to Heirarchy Dump.txt");
            }
            if (Input.GetKeyDown(KeyCode.Z))
            {
                Plugin.Log("searching through loaded assemblies");
                StringBuilder sb = new StringBuilder();
                Plugin.Log("there are " + AppDomain.CurrentDomain.GetAssemblies().Length + " asses");
                foreach (Assembly ass in AppDomain.CurrentDomain.GetAssemblies())
                {
                    sb.Append(ass.GetName()).Append("{").Append(Environment.NewLine);
                    if (ass.GetName().Name == "System.Runtime.Serialization") continue;
                    foreach (Type type in ass.GetTypes())
                    {
                        sb.Append("\t").Append(type.Name).Append(Environment.NewLine);
                    }
                    sb.Append("}").Append(Environment.NewLine);

                }
                System.IO.File.WriteAllText("Assembly Dump.txt", sb.ToString());
                Plugin.Log("Assemblies dumped to Assembly Dump.txt");
            }
            if (Input.GetKeyDown(KeyCode.C))
            {
                GameObject.Find("Enemy");

            }
            if (Input.GetKeyDown(KeyCode.V))
            {
                TrySpawnDude();
            }
        }

        void TrySpawnDude()
        {

            //Resources.FindObjectsOfTypeAll
            //Plugin.Log("trying to spawn dude");
        }

        string ArrayToString(float[] array)
        {
            StringBuilder sb = new StringBuilder("[");
            foreach (float f in array)
            {
                sb.Append(f).Append(", ");
            }
            sb.Remove(sb.Length - 2, 2).Append("]");
            return sb.ToString();
        }

        void Map(GameObject go, StringBuilder sb, int tier)
        {
            try
            {
                sb.Append(' ', tier * 4).Append(go.name);
                if (!go.activeSelf) sb.Append("<<<<disabled>>>>");
                sb.Append(System.Environment.NewLine);

                if (go == Camera.main?.gameObject)
                {
                    sb.Append("<<<<Camera.main>>>> (");
                    foreach (Behaviour b in Camera.main.gameObject.GetComponents<Behaviour>())
                    {
                        sb.Append(b.GetType().Name).Append(", ");
                    }
                    if (Camera.main.gameObject.GetComponents<Behaviour>().Length != 0) sb.Remove(sb.Length - 2, 2);
                    sb.Append(")");
                    sb.Append(System.Environment.NewLine);
                }

                MeshFilter mf = go.GetComponent<MeshFilter>();
                if (mf != null)
                {
                    sb.Append(' ', tier * 4).Append("<<<<MeshFilter>>>> (mesh: ").Append(mf.mesh.name).Append(")");
                    sb.Append(System.Environment.NewLine);
                }

                SkinnedMeshRenderer smr = go.GetComponent<SkinnedMeshRenderer>();
                if (smr != null)
                {
                    sb.Append(' ', tier * 4).Append("<<<<SkinnedMeshRenderer>>>> (mesh: ").Append(smr.sharedMesh.name).Append(", material: ").Append(smr.material.name).Append(")");
                    sb.Append(System.Environment.NewLine);
                    //Mesh m = smr.sharedMesh;
                    //if (m != null)
                    //{
                    //    Vector3[] verts = m.vertices;
                    //    Vector3[] normals = m.normals;
                    //    int[] tris = m.triangles;

                    //    Transform[] bones = smr.bones;
                    //    Transform root = smr.rootBone;
                    //    BoneWeight[] bw = m.boneWeights;

                    //    StringBuilder sb2 = new StringBuilder();

                    //    sb2.Append("#verts");
                    //    foreach (var vert in verts)
                    //    {
                    //        sb2.Append("v ").Append(vert[0]).Append(" ").Append(vert[1]).Append(" ").Append(vert[2]).Append(Environment.NewLine);
                    //    }

                    //    sb2.Append("#normals");
                    //    foreach (var normal in normals)
                    //    {
                    //        sb2.Append("vn ").Append(normal[0]).Append(" ").Append(normal[1]).Append(" ").Append(normal[2]).Append(Environment.NewLine);
                    //    }

                    //    sb2.Append("#triangles");
                    //    for (int i = 0; i < tris.Length; i += 3)
                    //    {
                    //        sb2.Append("f ").Append(tris[i]).Append(" ").Append(tris[i + 1]).Append(" ").Append(tris[i + 2]).Append(Environment.NewLine);
                    //    }

                    //    sb2.Append("#bone transforms");
                    //    foreach(var t in bones)
                    //    {
                    //        sb2.Append(t.gameObject.name).Append(" (");
                    //        sb2.Append(t.position[0]).Append(" ").Append(t.position[1]).Append(" ").Append(t.position[2]).Append(") (");
                    //        sb2.Append(t.rotation.eulerAngles[0]).Append(" ").Append(t.rotation.eulerAngles[1]).Append(" ").Append(t.rotation.eulerAngles[2]).Append(")").Append(Environment.NewLine);
                    //    }

                    //    sb2.Append("#bone weights");
                    //}
                    //else
                    //{
                    //    sb.Append("shared mesh was null");
                    //}
                }

                MeshRenderer mr = go.GetComponent<MeshRenderer>();
                Material mat = null;
                if (mr != null)
                {

                    mat = mr.material;
                    sb.Append(' ', tier * 4).Append("<<<<MeshRenderer>>>> (material: ").Append(mat.name).Append(", ")
                        .Append("shader: ").Append(mat.shader.name).Append(", ")
                        .Append("_MainTex : ").Append(mat.mainTexture).Append(", ")
                        .Append("_EmissionColor : ").Append(mat.GetColor("_EmissionColor")).Append(", ")
                        .Append("_Color : ").Append(mat.GetColor("_Color"))
                        .Append(")");
                    sb.Append(System.Environment.NewLine);
                }

                for (int i = 0; i < go.transform.childCount; i++)
                {
                    Transform child = go.transform.GetChild(i);
                    Map(child.gameObject, sb, tier + 1);
                }
            }
            catch (Exception e)
            {
                Plugin.Log("Exception while searching hierarchy");
                Plugin.Log(e.ToString());
            }
        }
    }
}
