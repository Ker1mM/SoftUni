using System;
using System.Collections.Generic;
using System.Linq;

public class Person
{
    private string name;
    private string birthday;
    private List<Person> parents;
    private List<Person> children;

    public Person()
    {
        this.Children = new List<Person>();
        this.Parents = new List<Person>();
    }

    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Birthday
    {
        get { return birthday; }
        set { birthday = value; }
    }

    public List<Person> Parents
    {
        get { return parents; }
        set { parents = value; }
    }

    public List<Person> Children
    {
        get { return children; }
        set { children = value; }
    }

    public override string ToString()
    {
        return $"{this.Name} {this.Birthday}";
    }

    public static Person Parse(string info)
    {
        Person current = new Person();
        if (IsBirthday(info))
        {
            current.Birthday = info;
        }
        else
        {
            current.Name = info;
        }

        return current;
    }

    public static bool IsBirthday(string input)
    {
        return Char.IsDigit(input[0]);
    }

    private static void SetChild(List<Person> familyTree, Person parentPerson, string child)
    {
        var childPerson = new Person();

        if (IsBirthday(child))
        {
            if (!familyTree.Any(p => p.Birthday == child))
            {
                childPerson.Birthday = child;
            }
            else
            {
                childPerson = familyTree.First(p => p.Birthday == child);
            }
        }
        else
        {
            if (!familyTree.Any(p => p.Name == child))
            {
                childPerson.Name = child;
            }
            else
            {
                childPerson = familyTree.First(p => p.Name == child);
            }
        }

        parentPerson.Children.Add(childPerson);
        childPerson.Parents.Add(parentPerson);
        familyTree.Add(childPerson);
    }

    public static void AddPersonInfo(string firstPerson, string secondPerson, List<Person> familyTree)
    {
        Person currentPerson;

        if (IsBirthday(firstPerson))
        {
            currentPerson = familyTree.FirstOrDefault(p => p.Birthday == firstPerson);

            if (currentPerson == null)
            {
                currentPerson = new Person();
                currentPerson.Birthday = firstPerson;
                familyTree.Add(currentPerson);
            }

            SetChild(familyTree, currentPerson, secondPerson);
        }
        else
        {
            currentPerson = familyTree.FirstOrDefault(p => p.Name == firstPerson);

            if (currentPerson == null)
            {
                currentPerson = new Person();
                currentPerson.Name = firstPerson;
                familyTree.Add(currentPerson);
            }

            SetChild(familyTree, currentPerson, secondPerson);
        }
    }

    public static void CreatePerson(string[] tokens, List<Person> familyTree)
    {
        tokens = tokens[0].Split();
        string name = $"{tokens[0]} {tokens[1]}";
        string birthday = tokens[2];

        var person = familyTree.FirstOrDefault(p => p.Name == name || p.Birthday == birthday);
        if (person == null)
        {
            person = new Person();
            familyTree.Add(person);
        }
        person.Name = name;
        person.Birthday = birthday;

        int index = familyTree.IndexOf(person) + 1;
        int count = familyTree.Count - index;

        Person[] copy = new Person[count];
        familyTree.CopyTo(index, copy, 0, count);

        Person copyPerson = copy.FirstOrDefault(p => p.Name == name || p.Birthday == birthday);

        if (copyPerson != null)
        {
            familyTree.Remove(copyPerson);
            person.Parents.AddRange(copyPerson.Parents);
            person.Parents = person.Parents.Distinct().ToList();

            person.Children.AddRange(copyPerson.Children);
            person.Children = person.Children.Distinct().ToList();
        }

        for (int i = 0; i < familyTree.Count; i++)
        {
            Person current = familyTree[i];
            int indexOfCurrentPerson = current.Children.FindIndex(x => x.Name == name || x.Birthday == birthday);
            if (indexOfCurrentPerson >= 0)
            {
                familyTree[i].Children[indexOfCurrentPerson].Name = name;
                familyTree[i].Children[indexOfCurrentPerson].Birthday = birthday;
            }
            indexOfCurrentPerson = current.Parents.FindIndex(x => x.Name == name || x.Birthday == birthday);
            if (indexOfCurrentPerson >= 0)
            {
                familyTree[i].Parents[indexOfCurrentPerson].Name = name;
                familyTree[i].Parents[indexOfCurrentPerson].Birthday = birthday;
            }
        }
    }
}
