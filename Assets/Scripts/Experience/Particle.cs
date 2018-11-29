using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Particle", menuName = "PhysicsParticle/NewParticle", order = 1)]
public class Particle : ScriptableObject {

    public string particleName;

    public string symbol;

    public bool negative;

    public bool line;

    public int destination;

    public bool secondLine;

    public int secondDestination;

    public bool head;



}
