using System.Numerics;
using Raylib_cs;


class Node {
    public string Name = "";
    public List<Node> Children = [];
    
    protected Node? Parent;


    public Node? GetParent() => Parent;

    public void AddChild(Node PNode) {
        Children.Add(PNode);
    }
}



class Node2D : Node {
    public Vector2 Position = new(0f, 0f);

    RectangleShape RectShape;
    CircleShape CirShape;


    public Node2D(float PosX, float PosY, DrawShape PShape, Node? PParent) {
        Position.X = PosX;
        Position.Y = PosY;
        Shape = PShape;
        Parent = PParent;
        Parent?.AddChild(this);

    }

    public enum DrawShape {
        CIRCLE,
        RECTANGLE,
    }

    public DrawShape Shape;

    public virtual void Draw() {
        Vector2 FinalPos = GetRealPosition();

        switch (Shape) {
            case DrawShape.CIRCLE:
                Raylib.DrawCircle((int)FinalPos.X, (int)FinalPos.Y, CirShape.Radius, CirShape.Color);
                break;
            case DrawShape.RECTANGLE:
                Raylib.DrawRectangle((int)FinalPos.X, (int)FinalPos.Y, (int)RectShape.Size.X, (int)RectShape.Size.Y, RectShape.color);
                break;
        }
    

    }


    public void GiveRectShape(RectangleShape PShape) {
        if (Shape == DrawShape.RECTANGLE) {
            RectShape = PShape;
        }
        else {
            Console.WriteLine(string.Format("Node with name: {0} failed to get rectangle shape"));
        }

    }

    
    public void GiveCircleShape(CircleShape PShape) {
        if (Shape == DrawShape.CIRCLE) {
            CirShape = PShape;
        }
        else {
            Console.WriteLine(string.Format("Node with name: {0} failed to get circle shape"));
        }

    }


    private Vector2 GetRealPosition() {
        if ((Parent is null) || (Parent is not Node2D Parent2D)) {
            return Position;
        }
        else {
            return Parent2D.Position + Position;
        }
    }

}



struct CircleShape{
    public float Radius;

    public Color Color;

   public CircleShape(float PRadius, Color PColor) {
        Radius = PRadius;
        Color = PColor;
    }
}



struct RectangleShape {
    public Vector2 Size;
    public Color color;

    public RectangleShape(Vector2 PSize, Color PColor) {
        Size = PSize;
        color = PColor;

    }
}