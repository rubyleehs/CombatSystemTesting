using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Element : ScriptableObject {

}

[CreateAssetMenu(menuName = "Elements/Int")]
public class ElementInt : Element
{

}

[CreateAssetMenu(menuName = "Elements/Float")]
public class ElementFloat : Element
{

}

[CreateAssetMenu(menuName = "Elements/String")]
public class ElementString : Element
{

}
