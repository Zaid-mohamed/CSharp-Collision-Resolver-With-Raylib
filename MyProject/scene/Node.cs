using System;
using System.Numerics;
using Raylib_cs;


class Node {
    public string Name = "";
    public List<Node> Children = [];
    
    protected Node parent;

    
    public Node GetParent() => parent;
    public void AddChild(Node p_node) {
        Children.Append(p_node);
    }
}



class Node2D : Node {
    public Vector2 position = new(0f, 0f);

    RectangleShape RectShape;
    CircleShape CirShape;


    public Node2D(float PosX, float PosY, Shape p_shape, Node p_parent) {
        position.X = PosX;
        position.Y = PosY;
        shape = p_shape;
        parent = p_parent;
        if (parent != null) {
            parent.AddChild(this);
        }

    }

    public enum Shape {
        CIRCLE,
        RECTANGLE,
    }

    public Shape shape;

    public virtual void Draw() {
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


    public void GiveRectShape(RectangleShape p_shape) {
        if (shape == Shape.RECTANGLE) {
            RectShape = p_shape;
        }
        else {
            Console.WriteLine(string.Format("Node with name: {0} failed to get rectange shape"));
        }

    }

    
    public void GiveCircleShape(CircleShape p_shape) {
        if (shape == Shape.CIRCLE) {
            CirShape = p_shape;
        }
        else {
            Console.WriteLine(string.Format("Node with name: {0} failed to get circle shape"));
        }

    }


    private Vector2 GetRealPosition() {
        if (parent == null || !(parent is Node2D)) {
            return position;
        }
        else {
            return (parent as Node2D).position + position;
        }
    }

}



struct CircleShape{
    public float radius;

    public Color color;

   public CircleShape(float p_radius, Color p_color) {
        radius = p_radius;
        color = p_color;
    }
}



struct RectangleShape {
    public Vector2 Size;
    public Color color;

    public RectangleShape(Vector2 p_size, Color p_color) {
        Size = p_size;
        color = p_color;

    }
}