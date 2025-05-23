using UnityEngine;

namespace Player
{
    public class PlayerPoints : MonoBehaviour
    {
        public int points = 0;

        public void AddPoints(int amount)
        {
            points += amount;
        }

        public bool SpendPoints(int amount)
        {
            if (points >= amount)
            {
                points -= amount;
                return true;
            }
            return false;
        }
    }
}