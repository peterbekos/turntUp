using UnityEngine;
using System.Collections;

public class LinearNote : NoteObject {
    new public void Update()
    {
        base.Update();
        transform.Translate(Vector3.left * speed * Time.deltaTime);
    }
}
