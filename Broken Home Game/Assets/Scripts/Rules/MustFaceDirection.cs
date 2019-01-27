using UnityEngine;

[CreateAssetMenu(menuName = "Furniture Rules/MustFaceDirection")]
public class MustFaceDirection : FurnitureRule
{
    [SerializeField] Rotation _direction = Rotation.SOUTH;

    public override bool Passes(Furniture checkingObject)
    {
        return (checkingObject.TileObject.GetRotation() == _direction);
    }
}
