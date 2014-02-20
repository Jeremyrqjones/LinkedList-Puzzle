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

            PersonLinkedList.Person Ted = new PersonLinkedList.Person("Ted", "Sully", 23, "12345", null);
            PersonLinkedList.Person Reta = new PersonLinkedList.Person("Reta", "Lopez", 44, "12345", null);
            PersonLinkedList.Person Sally = new PersonLinkedList.Person("Sally", "Hofsten", 36, "12345", null);
            PersonLinkedList.Person Mike = new PersonLinkedList.Person("Mike", "Dunken", 55, "12345", null);



            PersonList.Add(Ted);
            PersonList.Add(Reta);
            //PersonList.Add(Sally);
            PersonList.Add(Mike);
            

            //////////
            PersonList.ListNodes();
            PersonList.MergeIntoList(Ted, Sally);
            PersonList.ListNodes();
            Console.ReadLine();
        }
    }

    public class PersonLinkedList
    {
        public class Person
        {
            public Person(string firstname, string lastname, int age, string zip, Person next)
            {
                FirstName = firstname;
                LastName = lastname;
                Age = age;
                Zipcode = zip;
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
        }
        enum PersonField { FirstName, LastName, Age, Zipcode };

        private Person head;
        private Person headOfRightList;
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
        public bool SortList()
        {
           
            SplitList(this);
            Person tempNode = headOfRightList;
            //String.Compare(name1, index1, name2, index2, length, new CultureInfo("en-US"), CompareOptions.IgnoreCase) < 0)

            while (tempNode != null)
            {
                MergeIntoList(head, tempNode);
              tempNode = tempNode.Next;
            }

            return true;
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
        public void MergeIntoList(Person Left, Person Right)//TODO: Account for replacing head node, conditional for between left with right.next == null, setting last node.Next to null
        {
            if (Left.Age < Right.Age && Right.Age < Left.Next.Age)
            {
                Person temp = Left.Next;

                Left.Next = Right;
                Right.Next = temp;
            }
            else if(Left.Next != null)
            {
                Left = Left.Next;
                MergeIntoList(Left, Right);
            }
        }

        public List<Person> SeachByField(Person t, string searchString)
        {
            List<Person> PersonList = new List<Person>();
            Person tempNode = head;
           
            while (tempNode != null)
            {
                string FieldToCompare = tempNode.Age.ToString();
                if (FieldToCompare == searchString)
                {
                    PersonList.Add(tempNode);
                }
                tempNode = tempNode.Next;
            }

            return PersonList;
        }

        public void ListNodes()
        {
            Person tempNode = head;

            while (tempNode != null)
            {
                Console.WriteLine(tempNode.FirstName + ",  " +tempNode.Age.ToString());
                tempNode = tempNode.Next;
            }
        }
       
        
    }
}
