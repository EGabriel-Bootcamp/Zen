using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace Banking_Software
{
    class Bank

    {
        //Initlizing Bank class properties and fields

        String username { get; set; }
        string email { get; set; }
        string password { get; set; }
        int age { get; set; }
        int phone { get; set; }
        const int attempts = 3;//remember to remove 'const' if it messes with the login recursion
        const string exitMessage = "GOOD BYE";
        int accountBalance { get; set; }
       
        
        

        public void SignUp()
        {
            Console.WriteLine("PLEASE PROVIDE YOUR" +
                " PERSONAL INFORMATION TO SIGN UP \n"  +
                "ENTER THE CORRESPONDING " +
                "DETAILS AS YOU ARE BEING PROMPTED");

            //prompting, collecting and parsing user details
            Console.WriteLine("Name:");
            username = Console.ReadLine();

            Console.WriteLine("Email: (xxxxx@xx.xxx)");
            email = Console.ReadLine();

            Console.WriteLine("Age:");
            string Age = Console.ReadLine();
            age = int.Parse(Age);

            Console.WriteLine("Phone number:");
            string Phone = Console.ReadLine();
            phone = int.Parse(Phone);

            Console.WriteLine("Set a password:");
            password = Console.ReadLine();



            //creating a validator object "checker"
            Validator checker = new Validator();

            //using the field_validator method of the checker object to validate all the fields with && operator

            bool allValid =
               checker.field_validator(username)
              && checker.field_validator(email)
              && checker.field_validator(Age)
              && checker.field_validator(phone)
              && checker.field_validator(password);



            //recursion conditional
            if ( allValid == true)
            {
                //implement the saving to text file here

                //implementing the Login method to take the user to the login page
                Login(attempts);
            }
            else
            {
                Console.WriteLine("SEEMS THERE WAS AN EMPTY FIELD \n" +
                    "Please try again.");
                SignUp();
            }
             


        }

        public void Login( int attempts )
        {
            //n should define the number of attempts so far
            //think of using recursion for the limited nummber of attempts implementation
            // ?do this by settin a 

            Console.WriteLine("LOGIN \n" +
                $"PLEASE NOTE THAT YOU HAVE {attempts} ATTEMPTS LEFT!");

            Console.WriteLine("Input your Username");
            string customer_username = Console.ReadLine();

            Console.WriteLine("input your password");
            string customer_password = Console.ReadLine();

            if (attempts != 0 && true /*read and validate credential*/)
                {
                Console.WriteLine("LOGIN SUCCESSFUL!");
                Console.WriteLine( "WELCOME" + username + " How can we help you today? \n" +
                    "Reply:\n" +
                    " 1 for Deposit \n" +
                    "2 for withdrawal \n" +
                    "3 to view balance \n" +
                    "4 to view account summary");
                }



            else if (attempts == 0)
            {


                Console.WriteLine("Maximum number of attempts exceeded!");

                if (customer_username == "" && customer_password == "")
                {

                }
            }
            else
            {
                Login(attempts - 1);
               
                
            }
         
        }



        public void Deposit()
        {
            //collecting deposit input
            Console.WriteLine("How much do you wish to deposit");
            string depositInput = Console.ReadLine();

            //Validating user input
            bool isDepositValid = int.TryParse( depositInput, out int depositAmount );

            if (isDepositValid == true)
            {

                accountBalance = accountBalance + depositAmount;
                Console.WriteLine($"Your deposit of: {depositAmount} was successful");
                Console.WriteLine("Repy \n" +
                    " 1 to deposit again.\n" +
                    "2 to withdraw \n" +
                    "press any key to exit");
                string depositReply = Console.ReadLine();
                
                //conditional to deposit again, withdraw or exit
                if (depositReply == "1")
                {
                    Deposit();
                }
                else if (depositReply == "2")
                {
                    //implement withdrawal
                }
                else
                {
                    Console.WriteLine(exitMessage);
                }
            }
            else
            {
                //if the input amount is not valid the user would be prompted
                //And the deposit method would be called again

                Console.WriteLine("Please input a valid non-negative amount");
                Deposit();
            }

        }
    }

   

         
}
