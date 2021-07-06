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
        stuClass.Add(new Student(Grade.One, 8, true, "��һ"));
        stuClass.Add(new Student(Grade.One, 9, true, "�Ŷ�"));
        stuClass.Add(new Student(Grade.One, 7, true, "����"));

        stuClass.Add(new Student(Grade.Two, 12, true, "��һ"));
        stuClass.Add(new Student(Grade.Two, 14, true, "���"));
        stuClass.Add(new Student(Grade.Two, 16, true, "����"));

        stuClass.Add(new Student(Grade.Three, 22, true, "��һ"));
        stuClass.Add(new Student(Grade.Three, 25, true, "����"));
        stuClass.Add(new Student(Grade.Three, 28, true, "����"));

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
    public bool sex;      // yes = ��
    public string name;


    public Student(Grade gradle, int age, bool sex, string name)
    {
        this.gradle = gradle;
        this.age = age;
        this.sex = sex;
        this.name = name;
    }

}