using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Net.Mail;
using System.Net;

namespace BanhSystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Login();
        }

        //login
        public static void Login()
        {
            string username = "";
            string password = "";
            //verify
            bool loginIsSuccess = false;
            
            //clear page
            Console.Clear();

            //display login info
            Console.WriteLine("===================================");
            Console.WriteLine("|                                 |");
            Console.WriteLine("|WELCOME TO SIMPLE BANKING SYSTEM |");
            Console.WriteLine("===================================");
            Console.WriteLine("|        LOGIN TO START           |");
            Console.WriteLine("| USER NAME:                      |");
            Console.WriteLine("| PASSWORD:                       |");
            Console.WriteLine("===================================");
            Console.WriteLine("                                   ");
            //get input,
            //get username, method below
            username = GetCustomerInput(13,5);
            //get user password(get single character, after input now password show, pass single character to password, then show *)(while ture used when need get useriput and if)
            Console.SetCursorPosition(13, 6);
            while (true)
            {
                //get a key after user enter every single key 
                ConsoleKeyInfo singleKey = Console.ReadKey(true);
                //check enter,delete key, if so, do not consider them
                //enter key
                if(singleKey.Key == ConsoleKey.Enter)
                {
                    //\n means Enter
                    Console.Write("\n"); //Console.Writline;
                    //jump out whole loop
                    break;
                }
                //delete key
                if(singleKey.Key == ConsoleKey.Backspace)
                {
                    if(password.Length > 0)
                    {
                        //get password except the last number
                        password = password.Substring(0, password.Length - 1);
                        //get rid of the last*, \b means delete key
                        Console.Write("\b \b");
                    }
                    //jump out current loop
                    continue;
                }
                //other key, which can be entered
                //save to variable
                password += singleKey.KeyChar;
                //show *
                Console.Write("*");

            }

            //verify username and password, login.txt, when username=text's username, success
            //read txt
            string[] items = File.ReadAllLines(@"../login.txt");
            foreach (string item in items)
            {
                //use| to seperate username and password
                string[] _item = item.Split('|');
                if(_item[0] == username && _item[1] == password)
                {
                    loginIsSuccess = true;
                }
            }
            //if login success, jump to Menu
            if (loginIsSuccess)
            {
                //Console.WriteLine("Valid credentials!  PLEASE ENTER ANY BUTTON TO CONTINUE");
                PrintWordsLine("Valid credentials!  PLEASE ENTER ANY BUTTON TO CONTINUE", 0, 11);
                Console.ReadKey();
                Menu();
            }
            else
            {
                //Console.WriteLine("THE USERNAME or PASSWORD ARE NOT COMPARED, PRESS ANY BUTTON TO CONTINUE");
                PrintWordsLine("THE USERNAME or PASSWORD ARE NOT COMPARED, PRESS ANY BUTTON TO CONTINUE", 0, 11);
                Console.ReadKey();
                Login();
            }
        }
        //menu
        public static void Menu()
        {
            Console.Clear();
            //display content
            Console.WriteLine("WELCOME TO SIMPLE BANKING SYSTEM");
            Console.WriteLine("==================================");
            Console.WriteLine("1. CREATE A NEW ACCOUNT");
            Console.WriteLine("2. SEARCH FOR A NEW ACCOUNT");
            Console.WriteLine("3. DEPOSIT");
            Console.WriteLine("4. WITHDRAW");
            Console.WriteLine("5. A//C STATEMENT");
            Console.WriteLine("6. DELETE ACCOUNT");
            Console.WriteLine("7. EXIT");
            Console.WriteLine("==================================");
            Console.Write("ENTER YOUR CHOISE(1-7):");

            //through enter, jump to createNewAccount
            ConsoleKeyInfo input = Console.ReadKey(true);

            switch (input.Key)
            {
                case ConsoleKey.D1:
                    CreateANewAccount();
                    break;
                case ConsoleKey.D2:
                    SearchAccount();
                    break;
                case ConsoleKey.D3:
                    Deposit();
                    break;
                case ConsoleKey.D4:
                    Withdraw();
                    break;
                case ConsoleKey.D5:
                    Statement();
                    break;
                case ConsoleKey.D6:
                    Delete();
                    break;
                case ConsoleKey.D7:
                    Exit();
                    break;
                default:
                    Menu();
                    break;
            }
        }
        //creatNewAccount
        public static void CreateANewAccount()
        {
            //userID
            string userID = "0000000";
            //get user input, form user id, store user id into folder
            //get user input
            //set initial variable
            string email = "", firstName = "", lastName = "", address = "", phoneNumber = "";
            //display
            Console.Clear();
            Console.WriteLine("CEATE A NEW ACCOUNT");
            Console.WriteLine("================================");
            Console.WriteLine("ENTER THE DETAILS");
            Console.WriteLine("FIRST NAME:");
            Console.WriteLine("LAST NAME:");
            Console.WriteLine("ADDRESS:");
            Console.WriteLine("PHONE:");
            Console.WriteLine("EMAIL:");
          
            Console.WriteLine("=================================");
            //get user input
            firstName = GetCustomerInput(15, 3);
            lastName = GetCustomerInput(15, 4);
            address = GetCustomerInput(15, 5);
            
            //get phone number, limited within 10 numbers
            //after user enter phone number, if user input can be accept, if not, user should try again
            while (true)
            {
                phoneNumber = GetCustomerInput(15, 6);
                //check if there is 10 numbers, method
                if(Check10Character(phoneNumber))
                    {
                    //clean former information, method
                    CleanLine(10);
                    break;
                    }
                //IF PNONE NUMBER IS NOT RIGHT, clear user input
                PrintWordsLine("PHONE SHOULD not MORE THAN 10 CHARACTERS", 0, 10);
                CleanLinePart(15, 6);
                //CleanLine(6);
                //Console.SetCursorPosition(0, 6);
                //Console.WriteLine("PHONE:");
            }
            //get email
            while (true)
            {
                email = GetCustomerInput(15, 7);
                if (CheckEmail(email))
                {
                    CleanLine(10);
                    break;
                }
                //IF EMAIL IS NOT RIGHT, clear email input
                PrintWordsLine("EMAIL SHOULD CONTAIN @, DOMAIN SHOULD ONE OF 'uts.edu.au', 'gmail.com', 'outlook.com' ", 0, 10);
                CleanLinePart(15, 7);

            }

                //form userid
                //let user to double check their information, Y/N,uses while(true) because 输入判断
                while (true)
                {
                    PrintWordsLine("Is the information correct (Y/N) ?", 0, 12);
                    ConsoleKeyInfo singleCharacter = Console.ReadKey(true);
                    switch (singleCharacter.Key)
                    {
                        case ConsoleKey.Y:
                            //form new user id
                            //all user form as txt( users-> <id>.txt
                            //all txt in folder are existing user, files from 0 to 0+1,1+1, 2+1, 3+1)
                            string path = @"../accounts/";
                            string[] files = Directory.GetFiles(path, "*.txt");//create array called files, which include all txt
                        string balance = "0";
                            //order can use list
                            if (files.Length !=0)
                            {
                                //change array to sortable list
                                var userNameList = new List<string>();//create a list called userNameList//add using System.Collections.Generic; in the top 
                                userNameList.AddRange(files);//so fils be added in the usernamelist
                               //sort list
                                userNameList.Sort();
                                //get the largest id in list
                                userID = userNameList[userNameList.Count - 1];// now 00000000.txt is get
                                //using System.Text.RegularExpressions; added in the top because there is Match type
                                Match match = Regex.Match(userID, @"\d+(?=\.)"); // \d+ means multiple number 结尾 \.
                                userID = match.Value ;//get "00000000"
                                //00000000+1
                                //first change to intenger, +1, then change to string
                                userID = (int.Parse(userID) + 1).ToString().PadLeft(8, '0');//0->00000000

                            }
                            
                            //create file, named as id.txt
                            string filePath = Path.Combine(path, $"{userID}.txt");
                            FileStream _fileStream = File.Create(filePath);
                            _fileStream.Close();
                            //write content in file
                            StreamWriter _StreamWriter = new StreamWriter(filePath, true);
                            _StreamWriter.WriteLine($"First Name| {firstName}");
                            _StreamWriter.WriteLine($"Last Name|{lastName}");
                            _StreamWriter.WriteLine($"Address|{address}");
                            _StreamWriter.WriteLine($"Phone|{phoneNumber}");
                            _StreamWriter.WriteLine($"{email}");
                            _StreamWriter.WriteLine($"AccountNo|{userID}");
                        //initial balance 0
                            _StreamWriter.WriteLine($"{balance}");
                            _StreamWriter.Close();
                            PrintWordsLine("ACCOUNT CREATED! DETAILS WILL BE PROVIDED VIA EMAIL",0, 14);
                            PrintWordsLine($"ACOOUNT NUMBER IS: {userID}", 0, 15);
                        break;
                        case ConsoleKey.N:
                            //re enter
                            CreateANewAccount();
                            break;
                        default:Console.Write("\b");
                            Menu();
                            break;
                }

                //sentemail
                string newLine = "<br/>";
                string contents = $"Account number : {userID} {newLine}"
                                    + $"First Name :  {firstName} {newLine}"
                                     + $"Last Name : {lastName} {newLine}"
                                      + $"Adress : {address} {newLine}"
                                       + $"Phone : {phoneNumber} {newLine}"
                                        + $"Email : {email} {newLine}";
                                        
                
                SentEmail("your account info", email, contents);


            }


                //CheckEmail
            
        }
        //searchAccount
        public static void SearchAccount()
        {
            //enter account
            //if not exist, warning
            //if exist , output user information
            string account = "";
            //clear and output content
            Console.Clear();
            Console.WriteLine("SEARCH FOR AN ACCOUNT");
            Console.WriteLine("========================");
            Console.WriteLine("ENTER THE DETAILS");
            Console.WriteLine("ACCOUNT NUMBER");
            Console.WriteLine("=========================");
            
            //verify user input
            while (true)
            {
                //get user input
                account = GetCustomerInput(16, 3);
                if (!Check10Character(account))
                { //if not, enter again
                    //show error massage
                    PrintWordsLine("SHOULD NOT MORE THAN 10 CHARACTERS", 0, 6);
                    //clear user input
                    CleanLinePart(16, 3);
                    continue;
                }
                if (CheckAccount(account))
                {
                    //print
                    //if customer exist(function below)


                    //get file content
                    string path = $@"../accounts/{account}.txt";
                    string[] lines = File.ReadAllLines(path);
                    //show details(function below)
                    ShowDetails(7, account, lines[6], lines[0], lines[1], lines[2], lines[3], lines[4]);
                    PrintWordsLine("CHECK ANOTHER ACCOUNT (Y/N)?", 0, 17);

                }
                else
                {
                    //show account number enter is not correct
                    PrintWordsLine("ACCOUNT NOT FOUND", 0, 6);
                    PrintWordsLine("CHECK ANOTHER ACCOUNT (Y/N)?", 0, 7);
                }
                //check Y/N jump to different page
                while(true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.Y:
                            SearchAccount();
                            break;
                        case ConsoleKey.N:
                            Menu();
                            break;
                        default:
                            Console.Write("\b");
                            break;
                    }
                }


        
            }
        }
        //deposit
        public static void Deposit()
        {
            string account = "";
            string amount = "";

            //display information
            Console.Clear();
            Console.WriteLine("DEPOSIT");
            Console.WriteLine("=============================");
            Console.WriteLine("ENTER THE DETAILS");
            Console.WriteLine("ACCOUNT NUMBER:");
            Console.WriteLine("AMOUNT        :");
            Console.WriteLine("=============================");
            
            while(true)
            {
                //get account number
                account = GetCustomerInput(16, 3);
                //check account number 10 character
                if (!Check10Character(account))
                {
                    //IF ACCOUNT NUMBER IS NOT RIGHT, clear user input
                    PrintWordsLine("PHONE SHOULD NOT MORE THAN 10 CHARACTERS", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }
                //check if account number is exist
                if(!CheckAccount(account))
                {
                    //show error
                    PrintWordsLine("ACCOUNT IS NOT FOUND, PLEASE TRY AGAIN", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }

                break;
            }

            //get amount to deposit
            amount = GetCustomerInput(16, 4);
            
            //proceed deposite
            CleanLine(7);
            TransactionForDeposit(account, amount);
            PrintWordsLine("DEPOSIT SUCCESS", 0, 7);
            PrintWordsLine("PRESS ANY KEY BACK TO MAIN MENU", 0, 8);
            Console.ReadKey(true);
            Menu();

        }
        //withdraw
        public static void Withdraw()
        {
            string account = "";
           

            //display information
            Console.Clear();
            Console.WriteLine("Withdraw");
            Console.WriteLine("=============================");
            Console.WriteLine("ENTER THE DETAILS");
            Console.WriteLine("ACCOUNT NUMBER:");
            Console.WriteLine("AMOUNT        :");
            Console.WriteLine("=============================");
            //get account number
            while (true)
            {
                //get account number
                account = GetCustomerInput(16, 3);
                //check account number 10 character
                if (!Check10Character(account))
                {
                    //IF ACCOUNT NUMBER IS NOT RIGHT, clear user input
                    PrintWordsLine("PHONE SHOULD NOT MORE THAN 10 CHARACTERS", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }
                //check if account number is exist
                if (!CheckAccount(account))
                {
                    //show error
                    PrintWordsLine("ACCOUNT IS NOT FOUND, PLEASE ENTER THE AMOUNT", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }

                break;
            }

            
            //get amount
             PrintWordsLine("ACCOUNT FOUND, PLEASE ENTER THE AMOUNT", 0, 7);

            //proceed withdraw
            TransactionForWithdraw(account);

            CleanLine(7);
            PrintWordsLine("WITHDRAW SUCCESS", 0, 7);
            PrintWordsLine("PRESS ANY KEY BACK TO MAIN MENU", 0, 9);
            Console.ReadKey(true);
            Menu();
        }
        //statement
        public static void Statement()
        {
            string account = "";
            Console.Clear();
            Console.WriteLine("STATEMENT");
            Console.WriteLine("=========================");
            Console.WriteLine("ENTER THE DETAILS");
            Console.WriteLine("ACCOUNT NUMBER");
            Console.WriteLine("=========================");

            //get account number
            while (true)
            {
                //get account number
                account = GetCustomerInput(16, 3);
                //check account number 10 character
                if (!Check10Character(account))
                {
                    //IF ACCOUNT NUMBER IS NOT RIGHT, clear user input
                    PrintWordsLine("PHONE SHOULD NOT MORE THAN 10 CHARACTERS", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }
                //check if account number is exist
                if (!CheckAccount(account))
                {
                    //show error
                    PrintWordsLine("ACCOUNT IS NOT FOUND, PLEASE ENTER THE AMOUNT", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }
                CleanLine(7);
                break;
            }

            //show details
            //get file content
            string path = $@"../accounts/{account}.txt";
            string[] lines = File.ReadAllLines(path);
            //show details(function below)
            ShowDetails(7, account, lines[6], lines[0], lines[1], lines[2], lines[3], lines[4]);

            //show transaction history record
            PrintWordsLine("TRANSACTION", 0, 17);
            //only show 5 records, if<5, all record shows, if >5, show the lateset 5 records

            //get transaction length
            int transactionLength = lines.Length - 6;
            //get arryay copy start position
            int startPosition = transactionLength >= 5 ? lines.Length - 5 : 6;
            int actualTransactionlength = transactionLength >= 5 ? 5 : transactionLength;
            string[] transactions = new string[actualTransactionlength];
            //check the amount of transaction, get transaction
            Array.Copy(lines, startPosition, transactions, 0, actualTransactionlength);
            


            // if there is transaction
            if (lines.Length > 6)
            {
                //display
                foreach (string transaction in transactions)
                {
                    Console.WriteLine(transaction);
                }
                PrintWordsLine("Processing the email sending...", 0, 18+5);
            }
            else
            {
                PrintWordsLine("Processing the email sending...", 0, 18 + 5);
            }

            //send email
            //email content
            string newLine = "<br/>";
            string contents = $"Account number : {account} {newLine}"
                                + $"First Name : {lines[0]} {newLine}"
                                 + $"Last Name : {lines[1]} {newLine}"
                                  + $"Adress : {lines[2]} {newLine}"
                                   + $"Phone : {lines[3]} {newLine}"
                                    + $"Email : {lines[4]} {newLine}"
                                     + $"Balance : {lines[5]} {newLine}"
                                     + $"Transaction:{newLine}";
            foreach (string t in transactions)
            {
                contents += $"{t}{newLine}";
            }
            SentEmail("your account info",lines[4], contents);



            PrintWordsLine("Return to main menu (Y/N)?", 0, 25);
        
                //check Y/N jump to different page
                while(true)
                {
                    ConsoleKeyInfo cki = Console.ReadKey(true);
                    switch (cki.Key)
                    {
                        case ConsoleKey.Y:
                             Menu();
                        break;
                        case ConsoleKey.N:
                            Statement();
                            break;
                        default:
                            Console.Write("\b");
                            break;
                    }
}
        }

        
        //delete
        public static void Delete()
        {
            //get user account
            //check if the account is exist
            //if exist, delete fiile
            //if not exist, show error message

            string account = "";
            Console.Clear();
            Console.WriteLine("DELETE ACCOUNT");
            Console.WriteLine("=========================");
            Console.WriteLine("ENTER THE DETAILS");
            Console.WriteLine("ACCOUNT NUMBER");
            Console.WriteLine("=========================");

            while (true)
            {
                //get account number
                account = GetCustomerInput(16, 3);
                //check account number 10 character
                if (!Check10Character(account))
                {
                    //IF ACCOUNT NUMBER IS NOT RIGHT, clear user input
                    PrintWordsLine(" SHOULD NOT MORE THAN 10 CHARACTERS", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }
                //check if account number is exist
                if (!CheckAccount(account))
                {
                    //show error
                    PrintWordsLine("ACCOUNT IS NOT FOUND, PLEASE ENTER AGAIN", 0, 7);
                    CleanLinePart(16, 3);
                    continue;
                }

                break;
            }
            //look for file and delete
            PrintWordsLine("ACCOUNT FOUND, DETAILS DISPLAY BELOW", 0, 7);
            string path = $@"../accounts/{account}.txt";
            string[] lines = File.ReadAllLines(path);
            //show details(function below)
            ShowDetails(8, account, lines[6], lines[0], lines[1], lines[2], lines[3], lines[4]);

            Console.Write("DO YOU WANT TO DELETE?(Y/N):");
            while (true)
            {
                ConsoleKeyInfo cki = Console.ReadKey(true);
                switch (cki.Key)
                {
                    case ConsoleKey.Y:
                        //Console.Write("\n");//add a enter row
                        try
                        {
                            //delete file
                            File.Delete(path);
                            Console.WriteLine("YOUR DELETE IS SUCCESS");
                        }
                        catch(IOException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                        Console.WriteLine("PLEASE PRESS ANY KEY TO CONTINUE");
                        Menu();
                        break;
                    case ConsoleKey.N:
                        Menu();
                        break;
                    default:
                        Console.Write("\b");
                        break;
                }
            }
        }
        //exit
        public static void Exit()
        {
            Environment.Exit(0);
        }

        public static string GetCustomerInput (int left,  int top)
        {
            //set Cursor position
            Console.SetCursorPosition(left, top);
            //get user input
            return Console.ReadLine();

        }
        //method in create account-check phone number
        public static bool Check10Character(string input)
        {
            // /d means number, range 0~10, ^or $ means boundary
            return System.Text.RegularExpressions.Regex.IsMatch(input, @"^\d{0,10}$");

        }
        //method in create account-check phone number
        public static void PrintWordsLine(string word, int left, int top)
        {
            //set clusor
            Console.SetCursorPosition(left, top);
            // output letter
            Console.WriteLine(word);

        }
        //method in create account-check phone number
        public static void CleanLine(int lineNumber)
        {
            //testc clusor into relate X(top), clear content(use space)

            //accourding to window width, create space
            PrintWordsLine(new string(' ', Console.WindowWidth), 0, lineNumber);

        }
        //method in create account-check phone number
        public static void CleanLinePart (int left, int top)
        {
            Console.SetCursorPosition(left, top);
            PrintWordsLine(new string(' ', Console.WindowWidth-left), left, top);
        }
        //method in create account-check email
        public static bool CheckEmail(string email)
        {
            //.*(.com/.au)
            return System.Text.RegularExpressions.Regex.IsMatch(email, @"@.*(gmail.com|outlook.com|uts.edu.au)");
        }

        public static bool CheckAccount(string accountNumber)
        {
            //check if file is exist
            //string path = @"../accounts/";
            //path = Path.Combine(path, $"{accountNumber}.txt");
            //return File.Exists(path);
            return File.Exists($"../accounts/{accountNumber}.txt");
        }

        public static void ShowDetails(int startWhichRow ,string account, string balance, string firstName, string lastName, string address , string phoneNumber, string emailAddress)
        {
            PrintWordsLine("ACCOUNT DETAILS", 0, startWhichRow);
            PrintWordsLine("=========================", 0, startWhichRow+1);

            PrintWordsLine($"FIRST NAME : {firstName}", 0, startWhichRow+2);
            PrintWordsLine($"LAST NAME : {lastName}", 0, startWhichRow+3);
            PrintWordsLine($"ADDRESS : {address}", 0, startWhichRow+4);
            PrintWordsLine($"PHONE NUMBER : {phoneNumber}", 0, startWhichRow+5);
            PrintWordsLine($"EMAIL ADDRESS : {emailAddress}", 0, startWhichRow+6);
            PrintWordsLine($"ACCOUNT NUMBER :{account}", 0, startWhichRow + 7);
            PrintWordsLine($"ACCOUNT BALANCE :{balance}", 0, startWhichRow + 8);
        }
        public static void TransactionForDeposit(string accountNumber, string amount)
        {
            
            //read file, find line of balance, use +, print out

            //1.read file
            string path = $@"../accounts/{accountNumber}.txt";
            string[] lines = File.ReadAllLines(path);
            //deposite time
            string date = DateTime.Now.ToString();

            //2.balance is the 7th line, which is string, so need to change to int
            string bl = lines[6];
            int balance = Convert.ToInt32(bl);
            //change amount to int
            
            int aamount = Convert.ToInt32(amount);
            lines[6] = $"{balance + aamount}";
            //put new amount into file

            //create a new array, length is former length+1
            string[] newlines = new string[lines.Length + 1];
            //copy former lines to new lines
            lines.CopyTo(newlines, 0);
            //in new lines, the last element put new added element
            newlines[lines.Length] = $"{date}|DEPOSIT| ${amount} | {lines[6]}";
            File.WriteAllLines(path, newlines);
        }

        public static void TransactionForWithdraw(string accountNumber)
        {
            string amount = "";
            int aamount = 0;
            //read file, find line of balance, use -, print out

            //1.read file
            string path = $@"../accounts/{accountNumber}.txt";
            string[] lines = File.ReadAllLines(path);
            //deposite time
            string date = DateTime.Now.ToString();

            //2.balance is the 6th line, which is string, so need to change to int
            int balance = int.Parse(lines[6]);
           

            //get amount
            while (true)
            {
                amount = GetCustomerInput(16, 4);
                aamount = int.Parse(amount);
                //amount = int.Parse(GetCustomerInput(16,4));

                if (aamount > balance)
                {
                    PrintWordsLine("MONEY NOT ENOUGH", 0, 8);
                    CleanLinePart(16,4);
                    continue;
                }
                break;//if amount < banlance, out of whole loop
            }

            CleanLine(8);
            lines[6] = $"{balance - aamount}";
            //put new amount into file

            //create a new array, length is former length+1
            string[] newlines = new string[lines.Length + 1];
            //copy former lines to new lines
            lines.CopyTo(newlines, 0);
            //in new lines, the last element put new added element
            newlines[lines.Length] = $"{date} |WITHDRAW| ${amount} | {lines[6]}";
            File.WriteAllLines(path, newlines);
        }

        public static void SentEmail(string title, string toEmail,string content)
        {
            MailMessage mail = new MailMessage();
            mail.From = new MailAddress("daisyqin1111@gmail.com");
            mail.To.Add(new MailAddress(toEmail));
            mail.Subject = title;

            SmtpClient smtp = new SmtpClient();
            smtp.Port = 587;
            smtp.EnableSsl = true;
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new NetworkCredential(mail.From.Address, "yzcwfuavmzsonopq");
            smtp.Host = "smtp.gmail.com";

            mail.IsBodyHtml = true;
            mail.Body = content;
            smtp.Send(mail);
            Console.WriteLine("EMAIL SEND SUCCESSFULLY");
        }
    }
}
