using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class TTTest : MonoBehaviour
{
    public List<Student> stuClass = new List<Student>();

    public void Start()
    {
        stuClass.Add(new Student(Grade.One, 8, true, "张一"));
        stuClass.Add(new Student(Grade.One, 9, true, "张二"));
        stuClass.Add(new Student(Grade.One, 7, true, "张三"));

        stuClass.Add(new Student(Grade.Two, 12, true, "李一"));
        stuClass.Add(new Student(Grade.Two, 14, true, "李二"));
        stuClass.Add(new Student(Grade.Two, 16, true, "李三"));

        stuClass.Add(new Student(Grade.Three, 22, true, "王一"));
        stuClass.Add(new Student(Grade.Three, 25, true, "王二"));
        stuClass.Add(new Student(Grade.Three, 28, true, "王三"));

        Debug.Log("Stu Class init over.");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("############## ");
            IEnumerable<string> nameList;
            nameList = stuClass.Where((o) => o.gradle == Grade.Two && o.age > 12).Select(o => o.name);
            foreach (string item in nameList)
            {
                Debug.Log("name: " + item);
            }
        }
    }
}


public enum Grade
{
    One,
    Two,
    Three
}

public struct Student
{
    public Grade gradle;
    public int age;
    public bool sex;      // yes = 男
    public string name;


    public Student(Grade gradle, int age, bool sex, string name)
    {
        this.gradle = gradle;
        this.age = age;
        this.sex = sex;
        this.name = name;
    }

}