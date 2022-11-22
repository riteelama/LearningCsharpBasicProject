namespace UserManagement
{
    public class Employee
        {
            public Employee()
            {

            }

            public Employee(string firstName, string lastName)
            {
                FirstName = firstName;
                LastName = lastName;
            }

            public Employee(int userId, string firstName, string lastName)
            {
                UserId = userId;
                FirstName = firstName;
                LastName = lastName;
            }

            private int _userid;
            private string _fName;
            private string _lName;

            public string FirstName
            {
                get { return _fName; }
                set { _fName = value; }
            }

            public string LastName
            {
                get { return _lName; }
                set { _lName = value; }
            }

            public int UserId
            {
                get { return _userid; }
                set { _userid = value; }
            }
        }
}