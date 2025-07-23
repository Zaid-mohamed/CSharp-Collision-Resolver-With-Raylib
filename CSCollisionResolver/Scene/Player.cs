using Raylib_cs;
using Key = Raylib_cs.KeyboardKey;


partial class Player(float PosX, float PosY, Node2D.DrawShape PShape, Node? p_parent) : KinematicObject(PosX, PosY, PShape, p_parent)
{
    /// <summary>
    /// Speed of the player
    /// </summary>
    public float Speed = 1f;

    /// <summary>
    /// Jumpforce of the player,
    /// When used it must be negative.
    /// </summary>
    public float JumpForce = 2f;

    /// <summary>
    /// Gravity of the player.
    /// </summary>
    public float Gravity = 0.01f;

    /// <summary>
    /// Left Key
    /// </summary>
    private readonly Key LKey = Key.A;
    /// <summary>
    /// Right Key
    /// </summary>
    private readonly Key RKey = Key.D;
    /// <summary>
    /// Jump Key
    /// </summary>
    private readonly Key JumpKey = Key.Space;


    /// <summary>
    /// Handles the movement of the player, Input, Changing Velocity, Gravity
    /// And Calls MoveAndCollide() to Apply The Velocity
    /// </summary>
    private void HandleMovement()
    {
        float InputDir = PlayerInputUtil.GetAxis(LKey, RKey);
        Velocity.X = Speed * InputDir;
        HandleJumping();
        ApplyGravity();
        MoveAndCollide();

    }

    /// <summary>
    /// Pulls the Player downwards
    /// </summary>
    private void ApplyGravity() { Velocity.Y += Gravity; }

    /// <summary>
    /// Handles Jumping Input and Changing Velocity
    /// </summary>
    private void HandleJumping()
    {
        if (Raylib.IsKeyPressed(JumpKey)) Velocity.Y = -JumpForce;
    }

    /// <summary>
    /// This is where all methods meet
    /// Called once every loop iterate in the main funciton
    /// </summary>
    public void _Process()
    {
        HandleMovement();
        Draw();

    }


    /// <summary>
    /// Utiliy Class for the Player containing some usful methods for input.
    /// </summary>
    class PlayerInputUtil
    {

        /// <summary>
        /// Returns 1f if the given Key is pressed
        /// </summary>
        /// <param name="PKey"></param>
        /// <returns></returns>
        public static float GetKeyStrength(Key PKey) => Raylib.IsKeyDown(PKey) ? 1f : 0f;

        /// <summary>
        /// Returns 1f if the Rkey is being pressed and the Lkey is not, and -1f for the opposite
        /// Also returns 0f if both of them are pressed or released.
        /// </summary>
        /// <param name="LKey"></param>
        /// <param name="RKey"></param>
        /// <returns></returns>
        public static float GetAxis(Key LKey, Key RKey) => GetKeyStrength(RKey) - GetKeyStrength(LKey);

    }
}