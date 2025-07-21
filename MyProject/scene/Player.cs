

using System.Linq.Expressions;
using System.Numerics;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using Raylib_cs;
using Key = Raylib_cs.KeyboardKey;


partial class Player : KinematicObject
{
    public Player(float PosX, float PosY, Shape p_shape, Node p_parent) : base(PosX, PosY, p_shape, p_parent)
    {
    }

    public float Speed = 1f;
    public float JumpForce = 2f;

    public float Gravity = 0.01f;

    private Key LKey = Key.A;
    private Key RKey = Key.D;
    private Key JumpKey = Key.Space;



    private void HandleMovement() {
        float InputDir = PlayerInputUtil.GetAxis(LKey, RKey);
        float InputDirHor = PlayerInputUtil.GetAxis(Key.W, Key.S);
        Velocity.X = Speed * InputDir;
        // Velocity.Y = Speed * InputDirHor;
        HandleJumping();
        ApplyGravity();
        MoveAndCollide();

    }

    private void ApplyGravity() { Velocity.Y += Gravity; }

    private void HandleJumping() {
        if (Raylib.IsKeyPressed(JumpKey)) Velocity.Y = -JumpForce;
    }
    public void _Process() {
        HandleMovement();
        Draw();
        // Console.WriteLine(Velocity);
        // CollShape.DebugDraw();

    }



}




class PlayerInputUtil
{




    public static float GetKeyStrength(Key Key) => Raylib.IsKeyDown(Key) ? 1f : 0f;
    
    public static float GetAxis(Key LKey, Key RKey) => GetKeyStrength(RKey) - GetKeyStrength(LKey);

}