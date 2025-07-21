
using Raylib_cs;
using Key = Raylib_cs.KeyboardKey;


partial class Player(float PosX, float PosY, Node2D.Shape p_shape, Node? p_parent) : KinematicObject(PosX, PosY, p_shape, p_parent)
{
    public float Speed = 1f;
    public float JumpForce = 2f;

    public float Gravity = 0.01f;

    private readonly Key LKey = Key.A;
    private readonly Key RKey = Key.D;
    private readonly Key JumpKey = Key.Space;



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

    }



}




class PlayerInputUtil
{

    public static float GetKeyStrength(Key Key) => Raylib.IsKeyDown(Key) ? 1f : 0f;
    
    public static float GetAxis(Key LKey, Key RKey) => GetKeyStrength(RKey) - GetKeyStrength(LKey);

}