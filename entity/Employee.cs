using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssetManagementSystem.entity
{
    public class Employee
    {
        private int employeeId;
        private string name;
        private string department;
        private string email;
        private string password;

        public Employee() { }

        public Employee(int employeeId, string name, string department, string email, string password)
        {
            this.employeeId = employeeId;
            this.name = name;
            this.department = department;
            this.email = email;
            this.password = password;
        }

        public int GetEmployeeId() { return employeeId; }
        public void SetEmployeeId(int value) { employeeId = value; }

        public string GetName() { return name; }
        public void SetName(string value) { name = value; }

        public string GetDepartment() { return department; }
        public void SetDepartment(string value) { department = value; }

        public string GetEmail() { return email; }
        public void SetEmail(string value) { email = value; }

        public string GetPassword() { return password; }
        public void SetPassword(string value) { password = value; }

        public override string ToString()
        {
            return $"EmployeeId: {employeeId}, Name: {name}, Department: {department}, Email: {email}";
        }
    }
}
