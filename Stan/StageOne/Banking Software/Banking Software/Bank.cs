using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
 

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
            FieldValidator checker = new FieldValidator();

            //using the field_validator method of the checker object to validate all the fields with && operator

            bool allValid =
               checker.field_validator(username)
              && checker.field_validator(email)
              && checker.field_validator(Age)
              && checker.field_validator(phone)
              && checker.field_validator(password);



            //Conditional to take user to login page if all fields are valid or perform recursion if any field is invalid
            if ( allValid == true)
            {
                //saving credentials to text file
               string passwordPath = @"savedPassword.txt";
               File.WriteAllText(passwordPath, password);

                string usernamePath = @"savedUsername.txt";
                File.WriteAllText(usernamePath, password);


                //Calling the Login method to take the user to the login page
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
             
            Console.WriteLine("LOGIN \n" +
                $"PLEASE NOTE THAT YOU HAVE {attempts} ATTEMPTS LEFT!");

            //collecting visitor credentials
            Console.WriteLine("Input your Username");
            string visitorUsername = Console.ReadLine();

            Console.WriteLine("input your password");
            string visitorPassword = Console.ReadLine();

            // Reading saved credentials from txt file
            string savedPassword = System.IO.File.ReadAllText(@"savedPassword.txt");
            string savedUsername = System.IO.File.ReadAllText(@"savedUsername.txt");

            //comparing credentials
            bool cleared;

            if (visitorUsername == savedUsername && visitorPassword == savedPassword)
            {
                cleared = true;
            }
            else
            {
                Console.WriteLine("Invalid Username or Passwor \n" +
                    "Try again");
                cleared = false;
                Login (attempts);
            }
            //Final login Verification and intended operation operation selection
            if (attempts != 0 && cleared == true)
            {
                Console.WriteLine("LOGIN SUCCESSFUL!");
                Console.WriteLine("WELCOME " + username + " How can we help you today? \n" +
                    "Reply:\n" +
                    " 1 for Deposit \n" +
                    "2 for withdrawal \n" +
                    "3 to view balance \n" +
                    "4 to view account summary");
                string loginRequest = Console.ReadLine();



                //Defining the method for login operation that would recall itself when a user makes an invalid input
                void LoginOperation()
                {
                    if (loginRequest == "1")
                    {
                        Deposit();
                    }
                    else if (loginRequest == "2")
                    {
                        //implement withdrawal
                        Withdraw();
                    }
                    else if (loginRequest == "3")
                    {
                        ViewBalance();
                    }
                    else if (loginRequest == "4")
                    {
                        ViewAccountSummary();
                    }

                    else
                    {
                        Console.WriteLine("You did not select a valid input");
                        LoginOperation();

                    }
                }



                //calling the method
                LoginOperation();
                 
            }



            else if (attempts == 0)
            {


                Console.WriteLine("Maximum number of attempts exceeded!");

                Console.WriteLine(exitMessage);
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

            if (isDepositValid == true && depositAmount >= 0 && depositAmount != 0)
            {

                accountBalance = accountBalance + depositAmount;
                Console.WriteLine($"Your deposit of: {depositAmount} was successful");
                Console.WriteLine("Repy \n" +
                    " 1 to deposit again.\n" +
                    "2 to withdraw \n" +
                    "3 to view your balance" +
                    "press any key to exit");
                string depositReply = Console.ReadLine();
                
                //conditional to deposit again, withdraw or exit
                if (depositReply == "1")
                {
                    Deposit();
                }
                else if (depositReply == "2")
                {
                    Withdraw();
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

                Console.WriteLine("Please input a valid non-negative integer for: Deposit amount");
                Deposit();
            }

        }

        public void Withdraw()
        {

            //collecting withdrawal input
            Console.WriteLine("How much do you wish to withdraw");
            string withdrawalInput = Console.ReadLine();

            //Validating user input
            bool isWithdrawalValid = int.TryParse(withdrawalInput, out int withdrawnAmount);

            if (isWithdrawalValid == true && withdrawnAmount >= 0 && withdrawnAmount != 0)
            {

                accountBalance = accountBalance - withdrawnAmount;
                Console.WriteLine($"Your withdrawal of: {withdrawnAmount} was successful");
                Console.WriteLine("Repy \n" +
                    " 1 to withdraw again.\n" +
                    "2 to deposit \n" +
                    "3 to view your balance" +
                    "press any key to exit");
                string withdrawReply = Console.ReadLine();

                //conditional to withdraw again, deposit or exit
                if (withdrawReply == "1")
                {
                    Withdraw();
                }
                else if (withdrawReply == "2")
                {
                    Deposit();
                }

                else
                {
                    Console.WriteLine(exitMessage);
                }

            }
            else
            {

                //if the input amount is not valid the user would be prompted
                //And the withdraw method would be called again

                Console.WriteLine("Please input a valid non-negative Integer for :Withdrawal amount");
                Withdraw();
            }
        }
        public void ViewBalance()
        {
            //creating the balance check time
            DateTime timeObject = DateTime.Now;
            string time = timeObject.ToString();


            Console.WriteLine($"your account balance as of {time} is:");
            Console.WriteLine(accountBalance);
        }


        public void ViewAccountSummary()
        {

        }
   

    }

   

         
}
