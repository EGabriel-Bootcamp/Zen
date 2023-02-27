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
        const string exitMessage = "GOOD BYE";
        int accountBalance { get; set; }
        private int attempt = 3;


        // creating a Datetime.now returning method
        public string TimeNow()
        {
            DateTime timeObject = DateTime.Now;
            string time = timeObject.ToString();
            return time;
        }
        
        
       //creating a field validator class
        public class FieldValidator
        {
            public bool field_validator(string x)
            {
                // Validate string input
                return !string.IsNullOrEmpty(x);
            }

            public bool field_validator(int x)
            {
                // Validate integer input
                return x > 0 && x < 100;
            }
        }



        //Signup method
        public void SignUp()
        {

            Console.WriteLine("PLEASE PROVIDE YOUR" +
                " PERSONAL INFORMATION TO SIGN UP \n" +
                "ENTER THE CORRESPONDING " +
                "DETAILS AS YOU ARE BEING PROMPTED\n\n");

            //prompting, collecting and parsing user details
            Console.WriteLine("Selct a Username:");
            username = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Set a password:");
            password = Console.ReadLine();
            Console.WriteLine("\n");

            Console.WriteLine("Email:");
            email = Console.ReadLine();
            Console.WriteLine("\n");

            try
            {
                Console.WriteLine("Age:");
                string Age = Console.ReadLine();
                age = int.Parse(Age);
                Console.WriteLine("\n");

                Console.WriteLine("Phone number:");
                string Phone = Console.ReadLine();
                phone = int.Parse(Phone);
                Console.WriteLine("\n");
            }
            catch (Exception e)
            {
                // Console.WriteLine(e.Message);
                Console.WriteLine("\nEnter valid Integer type for Age and/or Phone number \n");

            }

            //creating an object "checker" from the FieldValidator class, this would be used to validate all fields of signup input
            FieldValidator checker = new FieldValidator();


            //using the field_validator method of the checker object from FieldValidator class to validate all the fields with && operator
            bool allValid =
               checker.field_validator(username)
              && checker.field_validator(email)
              && checker.field_validator(age)
              && checker.field_validator(phone)
              && checker.field_validator(password);



            //Conditional to take user to login page if all fields are valid or perform recursion if any field is invalid
            if (allValid == true)
            {
                //saving credentials to text file
                string passwordPath = @"savedPassword.txt";
                File.WriteAllText(passwordPath, password);

                string usernamePath = @"savedUsername.txt";
                File.WriteAllText(usernamePath, password);


                //Calling the Login method to take the user to the login page
                Login(ref attempt);
            }
            else
            {
                Console.WriteLine("\n" +
                    "SEEMS THERE WAS AN EMPTY FIELD OR A fORMAT ISSUE  \n" +
                    "Please try again." +
                    "\n");
                SignUp();
            }



        }
        //Login method  
        public void Login(ref int attempt)
        {

            Console.WriteLine("\n\n\n");
            Console.WriteLine("LOGIN \n" +
                $"PLEASE NOTE THAT YOU HAVE {attempt} ATTEMPT(S) LEFT!");


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
                Console.WriteLine("XXXX Invalid Username or Password  XXXX\n" +
                    "Try again");
                cleared = false;

            }


            //Final login Verification 
            /*this conditional crosschecks the details with those on the database if they match, the user is logged in 
                and gets to perform the intended operation but if the credentials donot match it continously 
             * reduces the number of attempts from 3 till it gets to 0 */
            if (cleared == true && attempt > 1)
            {
                Console.WriteLine("\n\n\n");
                Console.WriteLine("LOGIN SUCCESSFUL!");
                Console.WriteLine("WELCOME " + visitorUsername + " How can we help you today? \n" +
                    "Reply:\n" +
                    "1 for Deposit \n" +
                    "2 for withdrawal \n" +
                    "3 to view balance \n" +
                    "4 to view account summary");
                string endOfLoginRequest = Console.ReadLine();


                //switch to process end of Login prompt reply
                switch (endOfLoginRequest)
                {
                    case "1":
                        Deposit();
                        break;
                    case "2":
                        Withdraw();
                        break;
                    case "3":
                        ViewBalance();
                        break;
                    case "4":
                        ViewAccountSummary();
                        break;

                }



            }

            else if (cleared == false && attempt > 1)
            {
                attempt = attempt - 1;
                Login(ref attempt);
            }

            else if (cleared == false && attempt == 1)
            {
                Console.WriteLine("\nNUMBER OF ATTEMPTS EXCEEEDED!!!!");
                Console.WriteLine(exitMessage);
                Console.ReadLine();
            }


        }



        public void Deposit()

        {
            const string transactionType = "DEPOSIT (+)";


            //creating the time of operation by calling the TimeNow method
            string time = TimeNow();


            //collecting deposit input
            Console.WriteLine("\n\n\n");
            Console.WriteLine("How much do you wish to deposit");
            string depositInput = Console.ReadLine();


            //Validating user input
            bool isDepositValid = int.TryParse(depositInput, out int depositAmount);

            //Making the deposit
            if (isDepositValid == true && depositAmount >= 0 && depositAmount != 0)
            {

                accountBalance = accountBalance + depositAmount;

                //creating transaction details
                string transactionDetails = $"Transaction type:{transactionType}\n" +
                    $"Deposit Date and Time: {time} \n" +
                    $"=== Deposited Amount: {depositAmount} ===\n" +
                    $"Current Balance In account: {accountBalance}" +
                    $"\n\n";
                Console.WriteLine("SUCCESSFUL!");
                Console.WriteLine(transactionDetails);


                //Writing transaction details
                string databasePath = @"database.txt";
                using (StreamWriter sw = new StreamWriter(databasePath, true))
                {
                    sw.WriteLine(transactionDetails);
                }


                //propmting to withdraw again or exit
                Console.WriteLine("Reply \n" +
                    "1 to deposit again.\n" +
                    "2 to withdraw \n" +
                    "3 to view your balance\n" +
                    "4 to view your account summary\n" +
                    "press any key to exit");
                string endOfDepositReply = Console.ReadLine();

                //SWITCH to process end of deposit prompt reply
                switch (endOfDepositReply)
                {
                    case "1":
                        Deposit();
                        break;
                    case "2":
                        Withdraw();
                        break;
                    case "3":
                        ViewBalance();
                        break;
                    case "4":
                        ViewAccountSummary();
                        break;
                    default:
                        Console.WriteLine(exitMessage);
                        break;

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
            const string transactionType = "WITHDRAWAL (-)";


            //creating the time of operation by calling the TimeNow method
            string time = TimeNow();


            //collecting withdrawal input
            Console.WriteLine("\n\n\n");
            Console.WriteLine("How much do you wish to withdraw");
            string withdrawalInput = Console.ReadLine();


            //Validating user input
            bool isWithdrawalValid = int.TryParse(withdrawalInput, out int withdrawnAmount);

            if (isWithdrawalValid == true && withdrawnAmount >= 0 && withdrawnAmount != 0)
            {

                accountBalance = accountBalance - withdrawnAmount;
                Console.WriteLine("SUCCESSFUL!");



                //creating transaction details
                string transactionDetails = $"Transaction type:{transactionType}\n" +
                    $"Withdrawal Date and Time: {time} \n" +
                    $"=== Withdrawn Amount: {withdrawnAmount} ===\n" +
                    $"Current Balance In account: {accountBalance}" +
                    $"\n\n";
                Console.WriteLine(transactionDetails);


                //Writing transaction details
                string databasePath = @"database.txt";
                using (StreamWriter sw = new StreamWriter(databasePath, true))
                {
                    sw.WriteLine(transactionDetails);
                }


                // prompting the user to withdraw again, exit or deposit
                Console.WriteLine("Reply \n" +
                    "1 to withdraw again.\n" +
                    "2 to deposit \n" +
                    "3 to view your balance\n" +
                    "4 to view account Summary \n" +
                    "press any key to exit");
                string endOfWithdrawalReply = Console.ReadLine();


                //switch to process end of withdrawal reply
                switch (endOfWithdrawalReply)
                {
                    case "1":
                        Withdraw();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        ViewBalance();
                        break;
                    case "4":
                        ViewAccountSummary();
                        break;
                    default:
                        Console.WriteLine(exitMessage);
                        break;
                }
            }
            else
            {

                //if the input amount is not valid the user would be prompted
                //And the withdraw method would be called again

                Console.WriteLine("Please input a valid non-negative Integer for Withdrawal amount");
                Withdraw();
            }
        }
        public void ViewBalance()
        {

            //creating the time of operation by calling the TimeNow method
            Console.WriteLine("\n\n\n\n\n");
            string time = TimeNow();
            Console.WriteLine($"your account balance as of {time} is:");
            Console.WriteLine(accountBalance);
            Console.ReadLine();
            Console.WriteLine("\n\n\n\n");


            Console.WriteLine("Repy \n" +
                   " 1 to withdraw \n" +
                   "2 to deposit \n" +
                   "3 to view your balance again \n" +
                   "4 to view account summary/n" +
                   "press any key to exit");
            string endOfViewBalanceReply = Console.ReadLine();

            switch (endOfViewBalanceReply)
            {
                case "1":
                    Withdraw();
                    break;
                case "2":
                    Deposit();
                    break;
                case "3":
                    ViewBalance();
                    break;
                case "4":
                    ViewAccountSummary();
                    break;
                default:
                    Console.WriteLine(exitMessage);
                    break;

            }

        }


        public void ViewAccountSummary()
        {
            Console.WriteLine("\n\n\n");
            Console.WriteLine("BELOW IS YOUR ACCOUNT SUMMARY");
            string databasePath = @"database.txt";

            using (StreamReader reader = new StreamReader(databasePath))
            {
                string fileContent = reader.ReadToEnd();
                Console.WriteLine(fileContent);


                Console.WriteLine("Repy \n" +
                       "1 to withdraw \n" +
                       "2 to deposit \n" +
                       "3 to view your balance\n " +
                       "press any key to exit");
                string endOfViewAccountSummaryReply = Console.ReadLine();


                //switch to process end of view account summary
                switch (endOfViewAccountSummaryReply)
                {
                    case "1":
                        Withdraw();
                        break;
                    case "2":
                        Deposit();
                        break;
                    case "3":
                        ViewBalance();
                        break;
                    default:
                        Console.WriteLine(exitMessage);
                        break;


                }

            }


        }




    }
}
