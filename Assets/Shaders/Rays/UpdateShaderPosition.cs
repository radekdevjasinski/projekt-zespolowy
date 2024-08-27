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
        characterTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        if (material != null && characterTransform != null)
        {
            Vector3 characterPosition = characterTransform.position;
            material.SetVector("_CharacterMovement", new Vector4(characterPosition.x, characterPosition.y, characterPosition.z, 0));
        }
    }
}
