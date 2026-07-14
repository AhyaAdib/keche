// using System.Collections.Generic;
// using UnityEngine;
// using Pathfinding;

// public class gizmosDrawer : MonoBehaviour
// {
//     private List<Seeker> seekers = new List<Seeker>();
//     private Dictionary<Seeker, Path> paths = new Dictionary<Seeker, Path>();

//     void Start()
//     {
//         // Find all Seeker components in the scene and subscribe to their path callback
//         Seeker[] foundSeekers = FindObjectsOfType<Seeker>();
//         foreach (Seeker seeker in foundSeekers)
//         {
//             seekers.Add(seeker);
//             seeker.pathCallback += (Path p) => OnPathComplete(p, seeker);
//         }
//     }

//     void OnPathComplete(Path p, Seeker seeker)
//     {
//         if (!p.error)
//         {
//             if (paths.ContainsKey(seeker))
//             {
//                 paths[seeker] = p;
//             }
//             else
//             {
//                 paths.Add(seeker, p);
//             }
//         }
//     }

//     void OnRenderObject()
//     {
//         if (Camera.current == null)
//         {
//             return;
//         }

//         Material mat = new Material(Shader.Find("Hidden/Internal-Colored"));
//         mat.SetPass(0);

//         GL.PushMatrix();
//         GL.LoadProjectionMatrix(Camera.current.projectionMatrix);
//         GL.modelview = Camera.current.worldToCameraMatrix;

//         GL.Begin(GL.LINES);
//         GL.Color(Color.green);

//         foreach (KeyValuePair<Seeker, Path> entry in paths)
//         {
//             Path path = entry.Value;
//             if (path == null) continue;

//             for (int i = 0; i < path.vectorPath.Count - 1; i++)
//             {
//                 GL.Vertex(path.vectorPath[i]);
//                 GL.Vertex(path.vectorPath[i + 1]);
//             }
//         }

//         GL.End();
//         GL.PopMatrix();
//     }

// }
