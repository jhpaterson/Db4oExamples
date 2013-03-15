
namespace MappingExample
{
    public class SalariedEmployee : Employee
    {
        private int payGrade;

        public int PayGrade
        {
            get { return payGrade; }
            set { payGrade = value; }
        }

        public SalariedEmployee()
        {
        }

        public SalariedEmployee(string name,
                        string username,
                        string phoneNumber,
                        int payGrade) : 
                        base(name, username,phoneNumber)
        {
            this.payGrade = payGrade;
        }
    }
}
