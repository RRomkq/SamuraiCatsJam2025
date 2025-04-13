using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.CoreScene
{
    [CreateAssetMenu(fileName = "Feedback", menuName = "Feedback", order = 0)]
    public class FeedbackSO: ScriptableObject
    {
        public List<Feedback> feedbacks;
    }

    [Serializable]
    public class Feedback
    {
        public string message;
        public int starCount;
    }
}