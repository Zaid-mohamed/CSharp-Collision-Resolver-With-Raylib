

using System.Linq.Expressions;
using System.Numerics;
using Raylib_cs;
using Key = Raylib_cs.KeyboardKey;


partial class Player : Node2D
{
    public Player(float PosX, float PosY, Shape p_shape, Node p_parent) : base(PosX, PosY, p_shape, p_parent)
    {
    }

    public float Speed = 1f;
    public Vector2 Velocity;

    public float Gravity = 0.0f;

    private Key LKey = Key.A;
    private Key RKey = Key.D;

    private CircleCollisionShape collision = new(new Vector2(0f, 0f), 60f);


    private void HandleMovement() {
        float InputDir = PlayerInputUtil.GetAxis(LKey, RKey);
        Velocity.X = Speed * InputDir;
        ApplyVelocity();

    }

    private void ApplyVelocity() {
        position += Velocity;
        collision.Position = position;
        ApplyGravity();
    }
    private void ApplyGravity() { Velocity.Y += Gravity; }


    public void _Process() {
        HandleMovement();
        Draw();
        // collision.DebugDraw();

    }

}




class PlayerInputUtil
{




    public static float GetKeyStrength(Key Key) => Raylib.IsKeyDown(Key) ? 1f : 0f;
    
    public static float GetAxis(Key LKey, Key RKey) => GetKeyStrength(RKey) - GetKeyStrength(LKey);

}