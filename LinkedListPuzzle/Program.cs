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

            PersonLinkedList.Person Ted = new PersonLinkedList.Person("Ted", "Sully", 23, "12345", null, null);
            PersonLinkedList.Person Reta = new PersonLinkedList.Person("Reta", "Lopez", 44, "12345", null, null);
            PersonLinkedList.Person Sally = new PersonLinkedList.Person("Sally", "Hofsten", 36, "12345", null, null);
            PersonLinkedList.Person Mike = new PersonLinkedList.Person("Mike", "Dunken", 55, "12345", null, null);



            PersonList.Add(Ted);
            PersonList.Add(Reta);
            PersonList.Add(Sally);
            PersonList.Add(Mike);
            
            

            //////////
            PersonList.ListNodes(PersonLinkedList.PersonField.LastName);
            //Console.WriteLine("/br");
            //PersonList.MergeIntoList(Ted, Sally, PersonLinkedList.PersonField.LastName);
            PersonList.SortList(PersonLinkedList.PersonField.LastName);
            PersonList.ListNodes(PersonLinkedList.PersonField.LastName);
            Console.ReadLine();
        }
    }

    public class PersonLinkedList
    {
        public class Person
        {
            public Person(string firstname, string lastname, int age, string zip,Person previous, Person next)
            {
                FirstName = firstname;
                LastName = lastname;
                Age = age;
                Zipcode = zip;
                Previous = previous;
                Next = next;
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
        private Person headOfRightList;
        private bool hadAnInsert = true;
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


        /// <summary>
        /// Add a new Node to the list.
        /// </summary>
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
        public bool Delete(int Position)
        {
            if (Position == 1)
            {
                head = null;
                current = null;
                return true;
            }

            if (Position > 1 && Position <= size)
            {
                Person tempNode = head;

                Person lastNode = null;
                int count = 0;

                while (tempNode != null)
                {
                    if (count == Position - 1)
                    {
                        lastNode.Next = tempNode.Next;
                        return true;
                    }
                    count++;

                    lastNode = tempNode;
                    tempNode = tempNode.Next;
                }
            }

            return false;
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
        public void SplitList(PersonLinkedList ThisList)
        {
            //
            //split list into L & R
            //take the count of the list and /2.
            int LeftCount = size / 2;
            //if % != 0 take L_list + 1.
            //assign new head to begining of R_list.//headOfRightList
            //assign Next pointer of last node in L_list to null.
	        //
        }
        public void MergeIntoList(Person Left, Person Right, PersonField field)
        {
            hadAnInsert = false;
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
                this.ListNodes(PersonField.LastName);
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
                this.ListNodes(PersonField.LastName);
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
                    }

                    Left.Next.Previous = Right;
                    Left.Next = Right;
                    Right.Previous = Left;
                    Right.Next = temp;
                    this.ListNodes(PersonField.LastName);
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

            while (tempNode != null)
            {
                Console.WriteLine(tempNode.FirstName + ",  " +tempNode.GetFieldValue(field));
                tempNode = tempNode.Next;
            }
            Console.WriteLine(" ");
        }
       
        
    }
}
