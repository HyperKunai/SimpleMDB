namespace Smdb.Core.Actors;

public class Actor
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Bio { get; set; }

    public Actor(int id, string name, int age, string bio)
    {
        Id = id;
        Name = name;
        Age = age;
        Bio = bio;
    }

    public override string ToString()
    {
        return $"Actor[Id={Id}, Name={Name}, Age={Age}, Bio={Bio}]";
    }
}