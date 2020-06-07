using UnityEngine;
using UnityEditor;

public struct Vec2i
{
    public int x, y;
    public Vec2i(int x, int y)
    {
        this.x = x;
        this.y = y;
    }
    public override int GetHashCode()
    {
        return x << 16 + y;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Vec2i)) return false;
        Vec2i v = (Vec2i)obj;
        return v.x == x && v.y == y;
    }
    public override string ToString()
    {
        return "Vec2i:[" + x + "," + y + "]";
    }

    public static bool operator ==(Vec2i a, Vec2i b)
    {
        return a.x == b.x && a.y == b.y;
    }
    public static bool operator !=(Vec2i a, Vec2i b)
    {
        return !(a == b);
    }
    public static Vec2i operator +(Vec2i a, Vec2i b)
    {
        return new Vec2i(a.x + b.x, a.y + b.y);
    }
    public static Vec2i operator -(Vec2i a, Vec2i b)
    {
        return new Vec2i(a.x - b.x, a.y - b.y);
    }
    public static Vec2i operator *(Vec2i a, Vec2i b)
    {
        return new Vec2i(a.x * b.x, a.y * b.y);
    }
    public static Vec2i operator *(Vec2i a, int b)
    {
        return new Vec2i(a.x * b, a.y * b);
    }
    public static Vec2i operator /(Vec2i a, Vec2i b)
    {
        return new Vec2i(a.x / b.x, a.y / b.y);
    }
    public static Vec2i operator /(Vec2i a, int b)
    {
        return new Vec2i(a.x / b, a.y / b);
    }
}
public struct Vec3i
{
    public int x, y, z;
    public Vec3i(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    public override int GetHashCode()
    {
        return x << 22 + y<<11 + z;
    }

    public override bool Equals(object obj)
    {
        if (obj == null || !(obj is Vec3i)) return false;
        Vec3i v = (Vec3i)obj;
        return v.x == x && v.y == y && v.z==z;
    }
    public override string ToString()
    {
        return "Vec3i:[" + x + "," + y + "," + z + "]";
    }

    public static bool operator ==(Vec3i a, Vec3i b)
    {
        return a.x == b.x && a.y == b.y && a.z==b.z;
    }
    public static bool operator !=(Vec3i a, Vec3i b)
    {
        return !(a == b);
    }
    public static Vec3i operator +(Vec3i a, Vec3i b)
    {
        return new Vec3i(a.x + b.x, a.y + b.y, a.z+b.z);
    }
    public static Vec3i operator -(Vec3i a, Vec3i b)
    {
        return new Vec3i(a.x - b.x, a.y - b.y, a.z-b.z);
    }
    public static Vec3i operator *(Vec3i a, Vec3i b)
    {
        return new Vec3i(a.x * b.x, a.y * b.y, a.z*b.z);
    }
    public static Vec3i operator *(Vec3i a, int b)
    {
        return new Vec3i(a.x * b, a.y * b, a.z*b);
    }
    public static Vec3i operator /(Vec3i a, Vec3i b)
    {
        return new Vec3i(a.x / b.x, a.y / b.y);
    }
    public static Vec3i operator /(Vec3i a, int b)
    {
        return new Vec3i(a.x / b, a.y / b);
    }

}