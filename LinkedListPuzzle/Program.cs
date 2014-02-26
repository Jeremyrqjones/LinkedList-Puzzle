using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LinkedListPuzzle
{
    class Program
    {
        static void Main(string[] args)
        {
            PersonLinkedList PersonList = new PersonLinkedList();

            PersonLinkedList.Person Ted = new PersonLinkedList.Person("Ted", "Sully", 23, "12345");
            PersonLinkedList.Person Reta = new PersonLinkedList.Person("Reta", "Lopez", 44, "12345");
            PersonLinkedList.Person Sally = new PersonLinkedList.Person("Sally", "Hofsten", 36, "12345");
            PersonLinkedList.Person Mike = new PersonLinkedList.Person("Mike", "Dunken", 55, "12345");


            ////ADD
            PersonList.Add(Ted);
            PersonList.Add(Reta);
            PersonList.Add(Sally);
            PersonList.Add(Mike);
            PersonList.ListNodes(PersonLinkedList.PersonField.LastName);

            ////TEST FOR SORT
            Console.WriteLine("***TEST FOR SORT***");
            PersonList.SortList(PersonLinkedList.PersonField.LastName);
            PersonList.ListNodes(PersonLinkedList.PersonField.LastName);

            ////TEST FOR SEARCH BY FIELD
            Console.WriteLine("***TEST FOR SEARCH BY FIELD***");
            List<PersonLinkedList.Person> Persons = new List<PersonLinkedList.Person>();
            Persons = PersonList.SeachByField(PersonLinkedList.PersonField.LastName, "Lopez");
            foreach (PersonLinkedList.Person p in Persons)
            {
                Console.WriteLine(p.LastName + ", " + p.FirstName);
            }
            Console.WriteLine(" ");

            ////TEST FOR DELETE
            Console.WriteLine("***TEST FOR DELETE***");
            PersonList.Delete(Sally);
            PersonList.ListNodes(PersonLinkedList.PersonField.LastName);


            Console.ReadLine();
        }
    }

    public class PersonLinkedList
    {
        public class Person
        {
            public Person(string firstname, string lastname, int age, string zip)
            {
                FirstName = firstname;
                LastName = lastname;
                Age = age;
                Zipcode = zip;
                Previous = null;
                Next = null;
            }
            public string FirstName
            {
                get;
                set;
            }
            public string LastName
            {
                get;
                set;
            }
            public int Age
            {
                get;
                set;
            }
            public string Zipcode
            {
                get;
                set;
            }
            public Person Next
            {
                get;
                set;
            }
            public Person Previous
            {
                get;
                set;
            } 

            public string GetFieldValue (PersonField f)
            { 
                switch (f)
                {
                    case PersonField.Age:
                        return this.Age.ToString();
                    case PersonField.FirstName:
                        return this.FirstName;
                    case PersonField.LastName:
                        return this.LastName;
                    case PersonField.Zipcode:
                        return this.Zipcode;
                    default:
                        return string.Empty;
                }
                        
            }
        }
        public enum PersonField { FirstName, LastName, Age, Zipcode };
        private Person head;
        private Person current;
        private int size;
        public int Count
        {
            get
            {
                return size;
            }
        }
        public PersonLinkedList()
        {
            size = 0;
            head = null;
        }

        public void Add(Person person)
        {
            size++;
            var node = person;
            
            if (head == null)
            {
                // This is the first node. Make it the head
                head = node;
            }
            else
            {
                // This is not the head. Make it current's next node.
                current.Next = node;
                node.Previous = current;
            }

            // Makes newly added node the current node
            current = node;
        }
        public void Delete(Person person)
        {
            if (person.Equals(head))
            {
                head = person.Next;
                person.Next = null;
                head.Previous = null;
                size--;
                return;
            }
            if (person.Next == null)//This is the last node
            {
                person.Previous.Next = null;
                person.Previous = null;
                size--;
            }
            else
            {
                person.Next.Previous = person.Previous;
                person.Previous.Next = person.Next;
                person.Next = null;
                person.Previous = null;
                size--;
            }
        }
        public void SortList(PersonField field)
        {
            Person tempNode = head.Next;
            int counter = 0;
            while (counter <= Count)
            {
                MergeIntoList(head, tempNode, field);
                if (tempNode.Next != null)
                {
                    tempNode = tempNode.Next;
                }
                else
                {
                    tempNode = head;
                }
                counter++;
            }

        }
        
        public void MergeIntoList(Person Left, Person Right, PersonField field)
        {
            int compInt = String.Compare(Left.GetFieldValue(field), Right.GetFieldValue(field), false);
            if (compInt > 0 && Left.Equals(head))//Right is less than current head, move to head
            {
                if (Right.Next == null)//if Right.Next = null it is the last node, make last node.Next == null
                {
                    Right.Previous.Next = null;
                }
                else
                {
                    Right.Next.Previous = Left;
                }
                Right.Previous = null;
                Left.Previous = Right;
                Left.Next = Right.Next;
                Right.Next = Left;
                head = Right;
                //this.ListNodes(PersonField.LastName);
                return;
            }
            if (compInt < 0 && Left.Next == null)//Right is greater than last node, make Right the last node
            {
                if (Right.Previous == null)//it is the current head
                {
                    head = Right.Next;
                }
                else
                {
                    Right.Previous.Next = Right.Next;  //rejoin list, if taken out of the middle, list will split 
                }
                Left.Next = Right;
                Right.Previous = Left;
                Right.Next = null;
                //this.ListNodes(PersonField.Age);
                return;
            }
            if (Left.Next != null)
            {
                int nextCompInt = String.Compare(Right.GetFieldValue(field), Left.Next.GetFieldValue(field), false);
                if (compInt < 0 && nextCompInt < 0)
                {
                    Person temp = Left.Next;
                    if (Right.Equals(head))
                    {
                        head = Right.Next;
                        head.Previous = null;
                    }

                    Left.Next.Previous = Right;
                    Left.Next = Right;
                    Right.Previous = Left;
                    Right.Next = temp;
                    //this.ListNodes(PersonField.Zipcode);
                    return;
                }
                else
                {
                    Left = Left.Next;
                    MergeIntoList(Left, Right, field);
                }   
            }
        }

        public List<Person> SeachByField(PersonField field,  string searchString)
        {
            List<Person> PersonList = new List<Person>();
            Person tempNode = head;

            while (tempNode != null)
            {
                string FieldToCompare = tempNode.GetFieldValue(field);
                if (FieldToCompare == searchString)
                {
                    PersonList.Add(tempNode);
                }
                tempNode = tempNode.Next;
            }

            return PersonList;
        }

        public void ListNodes(PersonField field)
        {
            Person tempNode = head;
            string previous = string.Empty;
            while (tempNode != null)
            {
                if (tempNode.Previous != null)
                {
                    previous = tempNode.Previous.FirstName;
                }
                else
                {
                    previous = "NULL";
                }
                Console.WriteLine(tempNode.FirstName + ",  " + tempNode.GetFieldValue(field) + " Previous: " + previous);
                tempNode = tempNode.Next;
            }
            Console.WriteLine(" ");
        }
       
        
    }
}
