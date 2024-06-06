using UnityEngine;

public class UpdateShaderPosition : MonoBehaviour
{
    public Material material;
    public Transform characterTransform;
    Renderer renderer;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        material = renderer.material;
    }

    void Update()
    {
        characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
        if (material != null && characterTransform != null)
        {
            Vector3 characterPosition = characterTransform.position;
            material.SetVector("_CharacterMovement", new Vector4(characterPosition.x, characterPosition.y, characterPosition.z, 0));
            Debug.Log("Testy movementu: "+material.GetVector("_CharacterMovement"));
        }
    }
}
