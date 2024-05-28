using System.Collections.Generic;
using UnityEngine;

namespace Status
{
    public class Stats : MonoBehaviour
    {
        [SerializeField] private int baseValue;
        
        public List<int> modifiers = new List<int>();
        
        public int GetValue()
        {
            int finalValue = baseValue;
            modifiers.ForEach(x => finalValue += x);
            return finalValue;
        }

        public void AddModifier(int modifier)
        {
            if(modifier != 0)
                modifiers.Add(modifier);
        }
        
        public void RemoveModifier(int modifier)
        {
            if(modifier != 0)
                modifiers.Remove(modifier);
        }
        
        public void SetBaseValue(int value)
        {
            baseValue = value;
        }
    }
}