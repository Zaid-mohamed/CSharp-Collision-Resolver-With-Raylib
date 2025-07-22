using System;
using System.Numerics;
using Raylib_cs;

/// <summary>
/// The base class for all nodes.
/// Nodes are the building blocks of the project.
/// </summary>
class Node {
    /// <summary>
    /// Name of this Node, Currenlty not used for anything.
    /// </summary> 
    public string Name = "";

    /// <summary>
    /// All Children Nodes
    /// </summary>
    public List<Node> Children = [];
    
    /// <summary>
    /// The Parent of this Node if there is one.
    /// Note: Nodes can be without parent, But it is recommended to have only one Node with no parent.
    /// </summary>
    protected Node? parent;


    /// <summary>
    /// Returns the parent of this Node, if there is not, It will return null
    /// </summary>
    /// <returns>parent</returns>
    public Node? GetParent() => parent;


    /// <summary>
    /// Adds a child to this Node children
    /// </summary>
    /// <param name="p_node"></param>
    public void AddChild(Node p_node) {
        Children.Add(p_node);
    }
}



/// <summary>
/// A Node that has position and 2D rendering as Circle or Rectangle
/// </summary>
class Node2D : Node {

    /// <summary>
    /// The position of this Node2D in the world
    /// </summary>
    public Vector2 position = new(0f, 0f);

    /// <summary>
    /// The Rectangle Shape this node has if found.
    /// Note: Node2D can have either Rectangle Shape or Circle Shape, and not both at a time.
    /// </summary>
    RectangleShape RectShape;

    
    /// <summary>
    /// The Circle Shape this node has if found.
    /// Note: Node2D can have either Rectangle Shape or Circle Shape, and not both at a time.
    /// </summary>
    CircleShape CirShape;


    /// <summary>
    /// Basic Construcotr. p_parent can be null.
    /// </summary>
    /// <param name="PosX"></param>
    /// <param name="PosY"></param>
    /// <param name="p_shape"></param>
    /// <param name="p_parent"></param>
    public Node2D(float PosX, float PosY, Shape p_shape, Node? p_parent) {
        position.X = PosX;
        position.Y = PosY;
        shape = p_shape;
        parent = p_parent;
        parent?.AddChild(this);

    }

    /// <summary>
    /// Enum storing possible shapes the Node2D can has.
    /// </summary>
    public enum Shape {
        /// <summary>
        /// Circle Drawing
        /// </summary>
        CIRCLE,
        /// <summary>
        /// Rectangle Drawing
        /// </summary>
        RECTANGLE,
    }


    /// <summary>
    /// The shape of the Node2D
    /// </summary>
    public Shape shape;

    /// <summary>
    /// Draws the shape of the Node2D according to position and parent position if there is a Node2D parent. 
    /// </summary>
    public virtual void Draw() {
        // the final position the Node2D will be drawn at
        // according to parent if found and if it is Node2D
        Vector2 FinalPos = GetRealPosition();

        switch (shape) {
            case Shape.CIRCLE:
                Raylib.DrawCircle((int)FinalPos.X, (int)FinalPos.Y, CirShape.radius, CirShape.color);
                break;
            case Shape.RECTANGLE:
                Raylib.DrawRectangle((int)FinalPos.X, (int)FinalPos.Y, (int)RectShape.Size.X, (int)RectShape.Size.Y, RectShape.color);
                break;
        }
    

    }

    /// <summary>
    /// Sets the RectShape variable to the given p_shape
    /// </summary>
    /// <param name="p_shape"></param>
    public void GiveRectShape(RectangleShape p_shape) {
        if (shape == Shape.RECTANGLE) {
            RectShape = p_shape;
        }
        else {
            Console.WriteLine(string.Format("Node with name: {0} failed to get rectange shape"));
        }

    }

    /// <summary>
    /// Sets the CircShape variable to the given p_shape
    /// </summary>
    /// <param name="p_shape"></param>
    public void GiveCircleShape(CircleShape p_shape) {
        if (shape == Shape.CIRCLE) {
            CirShape = p_shape;
        }
        else {
            Console.WriteLine(string.Format("Node with name: {0} failed to get circle shape"));
        }

    }

    /// <summary>
    /// returns the world position the this Node2D should be drawn at.
    /// according to parent if found and if it was Node2D
    /// </summary>
    /// <returns></returns>
    private Vector2 GetRealPosition() {
        if ((parent is null) || (parent is not Node2D parent2D)) {
            return position;
        }
        else {
            return parent2D.position + position;
        }
    }

}


/// <summary>
/// Struct representing the Circle shape of a Node2D
/// </summary>
struct CircleShape{
    /// <summary>
    /// Radius of the circle
    /// </summary>
    public float radius;
    /// <summary>
    /// Color of the circle
    /// </summary>
    public Color color;

   public CircleShape(float p_radius, Color p_color) {
        radius = p_radius;
        color = p_color;
    }
}

/// <summary>
/// Struct representing the Rectangle shape of a Node2D
/// </summary>
struct RectangleShape {
    /// <summary>
    /// Size in X and Y
    /// X: Width
    /// Y: Height
    /// </summary>
    public Vector2 Size;
    /// <summary>
    /// Color of the Rectangle
    /// </summary>
    public Color color;

    public RectangleShape(Vector2 p_size, Color p_color) {
        Size = p_size;
        color = p_color;

    }
}