public class Teacher
{
    public string Name { get; }
    public Cathedra Cathedra { get; private set; }

    public Teacher(string name)
    {
        Name = name;
    }

    public void SetCathedra(Cathedra cathedra)
    {
        Cathedra = cathedra;
    }
}

