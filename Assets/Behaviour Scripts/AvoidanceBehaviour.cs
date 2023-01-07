using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behavior/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehavior
{
    public override Vector2 CalculateMove(FlockAgent flockAgent, List<Transform> context, Flock flock)
    {
        // if no neighbours return no adjustment
        if (context.Count == 0)
            return Vector2.zero;

        // add all points together and average
        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(flockAgent, context);
        foreach (Transform item in filteredContext)
        {
            if (Vector2.SqrMagnitude(item.position - flockAgent.transform.position) < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2)(flockAgent.transform.position - item.position);
            }
        }
        if (nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
