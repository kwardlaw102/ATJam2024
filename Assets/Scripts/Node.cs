using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour {
   [Header("Vars")]
   public string color;

   [Header("Stats")]
   public int pointValue;
   public List<Node> nextNodes = new();
   public List<float> weights = new();

   // TEMP
   public Vessel vessel;
   public bool entryNode = false; // TODO: make "official" way to trigger entry node

   void Start() {
      if (nextNodes.Count != weights.Count) {
         Debug.LogError("Length of list \"nextNodes\" should match length of list \"weights\"", this);
      }

      float sum = 0;
      foreach (float w in weights) {
         sum += w;
      }
      if (nextNodes.Count > 0 && !Mathf.Approximately(sum, 1f)) {
         Debug.LogError($"Weights do not add up to 1: {sum}", this);
      }
   }

   void Update() {
      if (entryNode && Input.GetKeyDown("z")) {
         ModifyVessel(vessel);
      }

      if (entryNode && Input.GetKeyDown("x")) {
         vessel.Clear();
      }
   }

   public Node SampleNextNode() {
      if (nextNodes.Count == 0) return null; // leaf/output node

      // sample from next node list based on weights
      float fac = Random.Range(0f, 1f);
      Debug.Log($"fac: {fac}");
      int i;
      Node nextNode = null;
      for (i = 0; i < weights.Count - 1; i++) {
         fac -= weights[i];
         if (fac <= 0) {
            
            return nextNodes[i];
         }
      }
      // if we didn't find a node yet, RNG picked the last node
      return nextNodes[nextNodes.Count - 1];
   }

   public void ModifyVessel(Vessel vessel) {
      // perform action
      if (color != "") {
         vessel.AddPoints(color, pointValue);
      }

      // go to next node
      // TODO: a higher level class should be responsible for organizing this
      Node nextNode = SampleNextNode();
      if (nextNode != null) {
         nextNode.ModifyVessel(vessel); 
      }
   }
}
