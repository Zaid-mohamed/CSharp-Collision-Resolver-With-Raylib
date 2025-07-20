

using System.Linq.Expressions;
using System.Numerics;
using System.Runtime.CompilerServices;
using Raylib_cs;
using Key = Raylib_cs.KeyboardKey;


partial class Player : KinematicObject
{
    public Player(float PosX, float PosY, Shape p_shape, Node p_parent) : base(PosX, PosY, p_shape, p_parent)
    {
    }

    public float Speed = 1f;
    public Vector2 Velocity;

    public float Gravity = 0.0f;

    private Key LKey = Key.A;
    private Key RKey = Key.D;



    private void HandleMovement() {
        float InputDir = PlayerInputUtil.GetAxis(LKey, RKey);
        float InputDirHor = PlayerInputUtil.GetAxis(Key.W, Key.S);
        Velocity.X = Speed * InputDir;
        Velocity.Y = Speed * InputDirHor;
        ApplyVelocity();

    }

    private void ApplyVelocity() {
        position += Velocity;
        CollShape.Position = position;
        ApplyGravity();
    }
    private void ApplyGravity() { Velocity.Y += Gravity; }


    public void _Process() {
        HandleMovement();
        Draw();
        // CollShape.DebugDraw();

    }



}




class PlayerInputUtil
{




    public static float GetKeyStrength(Key Key) => Raylib.IsKeyDown(Key) ? 1f : 0f;
    
    public static float GetAxis(Key LKey, Key RKey) => GetKeyStrength(RKey) - GetKeyStrength(LKey);

}