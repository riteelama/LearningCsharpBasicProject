using UserManagement;

Employee obj = new Employee();
Employee obj2 = new Employee("Ritee", "Lama");
Console.WriteLine(obj2.FirstName + " " + obj2.LastName);

Console.WriteLine("No. of User = " + UserSetting.noOfUser);
Console.WriteLine("Name of Apllication = " + UserSetting.applicationName);