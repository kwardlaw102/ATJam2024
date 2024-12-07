using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vessel : MonoBehaviour {
   public Dictionary<string, int> pointsDict = new();

   void Start() {
   }

   void Update() {
   }

   public void AddPoints(string color, int points) {
      if (pointsDict.ContainsKey(color)) {
         pointsDict[color] += points;
      } else {
         pointsDict[color] = points;
      }
   }

   public int GetPoints(string color) {
      if (pointsDict.TryGetValue(color, out int points)) {
         return points;
      }
      // default to 0 if key is not in dictionary
      return 0;
   }

   public void Clear() {
      pointsDict.Clear();
   }

   public void OnGUI() {
      string str = "";
      foreach ((string color, int points) in pointsDict) {
         str += $"{color}: {points}\n";
      }

      //GUI.color = Color.yellow;
      GUI.Label(new Rect(10, 10, 800, 800), str);
   }
}
