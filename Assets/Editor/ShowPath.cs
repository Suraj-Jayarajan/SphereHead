using UnityEngine;
using UnityEditor;


[ExecuteInEditMode]
public class ShowPath : MonoBehaviour {

    Transform[] nodes;
    FollowPath fp;
	
	void Start () {
        fp = GetComponent<FollowPath>();
        nodes = fp.nodes;
	}
	
	void OnDrawGizmos() {
        if(nodes.Length == 0) {
            return;
        }
        int j = 0;
        for(int i=0;i<nodes.Length;i++) {
            j = i + 1;
            if(i==nodes.Length-1) {
                j = 0;
            }
            Handles.DrawDottedLine(nodes[i].position, nodes[j].position, 3f);

        }
    }
}
