using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text;

namespace HotelSystem
{
    class Account
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }

        public bool IsActive { get; set; } = true;

        public Account() { }

        public Account(string username, string password, string role)
        {
            Username = username;
            Password = password;
            Role = role;
            IsActive = true;
        }

        public void DisplayAccountInfo()
        {
            ConsoleUtils.CenterTextLine($"{Username} - {Role}");
        }
    }

    class Room
    {
        public string RoomType { get; set; }
        public double RoomRate { get; set; }
        public int RoomNumber { get; set; }

        public Room() { }

        public Room(string roomType, double roomRate, int roomNumber)
        {
            RoomType = roomType;
            RoomRate = roomRate;
            RoomNumber = roomNumber;
        }

        public void DisplayRoomInfo()
        {
            Console.WriteLine($"Room {RoomNumber} - {RoomType} (₱{RoomRate:N2}/night)");
        }
    }

    class Booking
    {
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public Room RoomType { get; set; }
        public int RoomNumber { get; set; }
        public bool IsCheckedIn { get; set; }
        public bool IsCheckedOut { get; set; }
        public string BookedBy { get; set; }
        public string CheckedInBy { get; set; }
        public string CheckedOutBy { get; set; }

        public Booking() { }

        public Booking(string name, string phoneNumber, DateTime checkInDate, DateTime checkOutDate, Room room, string bookedBy)
        {
            Name = name;
            PhoneNumber = phoneNumber;
            CheckInDate = checkInDate;
            CheckOutDate = checkOutDate;
            RoomType = room;
            RoomNumber = room.RoomNumber;
            BookedBy = bookedBy;
            CheckedInBy = "";
            CheckedOutBy = "";
            IsCheckedIn = false;
            IsCheckedOut = false;
        }

        public int GetStayDuration()
        {
            int days = (CheckOutDate - CheckInDate).Days;
            return days > 0 ? days : 1;
        }

        public double CalculateTotalCost()
        {
            return GetStayDuration() * RoomType.RoomRate;
        }

        public void DisplayInfo()
        {
            ConsoleUtils.CenterTextLine("===== BOOKING DETAILS =====\n");
            ConsoleUtils.CenterTextLine($"Name: {Name}");
            ConsoleUtils.CenterTextLine($"Phone Number: {PhoneNumber}");
            ConsoleUtils.CenterTextLine($"Check-In Date: {CheckInDate:yyyy-MM-dd}");
            ConsoleUtils.CenterTextLine($"Check-Out Date: {CheckOutDate:yyyy-MM-dd}");
            ConsoleUtils.CenterTextLine($"Room Number: {RoomNumber}");
            ConsoleUtils.CenterTextLine($"Room Type: {RoomType.RoomType}");
            ConsoleUtils.CenterTextLine($"Rate per Night: ₱{RoomType.RoomRate:N2}");
            ConsoleUtils.CenterTextLine($"Stay Duration: {GetStayDuration()} nights");
            ConsoleUtils.CenterTextLine($"Total Cost: ₱{CalculateTotalCost():N2}");
            ConsoleUtils.CenterTextLine($"Booked By: {BookedBy}");
            if (IsCheckedIn)
                ConsoleUtils.CenterTextLine($"Checked In By: {CheckedInBy}");
            if (IsCheckedOut)
                ConsoleUtils.CenterTextLine($"Checked Out By: {CheckedOutBy}");
            ConsoleUtils.CenterTextLine("===========================\n");
        }

        public string GetSummary()
        {
            string status = IsCheckedOut ? "Checked-Out" : (IsCheckedIn ? "Checked-In" : "Booked");
            string roomInfo = $"Room {RoomNumber} ({RoomType.RoomType})";
            return $"{Name.PadRight(20)}  {roomInfo.PadRight(25)}  {status.PadRight(15)}";
        }

    }

    class SalesReport
    {
        public string GuestName { get; set; }
        public int RoomNumber { get; set; }
        public string RoomType { get; set; }
        public DateTime CheckOutDate { get; set; }
        public double AmountPaid { get; set; }

        public SalesReport() { }

        public SalesReport(string guestName, int roomNumber, string roomType, DateTime checkOutDate, double amountPaid)
        {
            GuestName = guestName;
            RoomNumber = roomNumber;
            RoomType = roomType;
            CheckOutDate = checkOutDate;
            AmountPaid = amountPaid;
        }

        public void DisplayReport()
        {
            ConsoleUtils.CenterTextLine($"Guest: {GuestName}");
            ConsoleUtils.CenterTextLine($"Room Number: {RoomType} {RoomNumber}");
            ConsoleUtils.CenterTextLine($"Check-Out Date: {CheckOutDate:yyyy-MM-dd}");
            ConsoleUtils.CenterTextLine($"Amount Paid: ₱{AmountPaid:N2}");
            ConsoleUtils.CenterTextLine("---------------------------");
        }

        public string GetTableRow()
        {
            string amountString = $"₱{AmountPaid:N2}";
            string dateString = CheckOutDate.ToString("yyyy-MM-dd");

            return $"{GuestName.PadRight(20)} {RoomNumber.ToString().PadRight(10)} {RoomType.PadRight(15)} {dateString.PadRight(15)} {amountString.PadLeft(15)}";
        }
    }
    public static class ConsoleUtils
    {
        public static void Coloring(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        public static void CenterText(string text)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = text.Length;
            int totalPadding = (screenWidth / 2) + (stringWidth / 2);
            Console.Write($"{text.PadLeft(totalPadding)}: ");
        }

        public static void CenterTextLine(string text)
        {
            int screenWidth = Console.WindowWidth;
            int stringWidth = text.Length;
            int totalPadding = (screenWidth / 2) + (stringWidth / 2);
            Console.WriteLine(text.PadLeft(totalPadding));
        }

        public static int MenuSelector(string title, string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                ConsoleUtils.CenterTextLine($"=========={title}==========");
                Console.WriteLine();
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterTextLine($"  {options[i]}");
                    }
                }
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }

        public static int BookingMenuSelector(string title, string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                ConsoleUtils.CenterTextLine("██████╗  ██████╗  ██████╗ ██╗  ██╗██╗███╗   ██╗ ██████╗ ███████╗");
                ConsoleUtils.CenterTextLine("██╔══██╗██╔═══██╗██╔═══██╗██║ ██╔╝██║████╗  ██║██╔════╝ ██╔════╝");
                ConsoleUtils.CenterTextLine("██████╔╝██║   ██║██║   ██║█████╔╝ ██║██╔██╗ ██║██║  ███╗███████╗");
                ConsoleUtils.CenterTextLine("██╔══██╗██║   ██║██║   ██║██╔═██╗ ██║██║╚██╗██║██║   ██║╚════██║");
                ConsoleUtils.CenterTextLine("██████╔╝╚██████╔╝╚██████╔╝██║  ██╗██║██║ ╚████║╚██████╔╝███████║");
                ConsoleUtils.CenterTextLine("╚═════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝ ╚══════╝");

                Console.WriteLine();

                string headerLine = $"  {"Guest Name".PadRight(20)} {"Room Info".PadRight(25)} {"Status".PadRight(15)}";
                ConsoleUtils.CenterTextLine(headerLine);
                ConsoleUtils.CenterTextLine(new string('-', 60));
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterTextLine($"  {options[i]}");
                    }
                }
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }

        public static int OutMenuSelector(string title, string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();

                ConsoleUtils.CenterTextLine("  ██████╗██╗  ██╗███████╗ ██████╗██╗  ██╗      ██████╗ ██╗   ██╗████████╗");
                ConsoleUtils.CenterTextLine("██╔════╝██║  ██║██╔════╝██╔════╝██║ ██╔╝     ██╔═══██╗██║   ██║╚══██╔══╝");
                ConsoleUtils.CenterTextLine("██║     ███████║█████╗  ██║     █████╔╝█████╗██║   ██║██║   ██║   ██║  ");
                ConsoleUtils.CenterTextLine("██║     ██╔══██║██╔══╝  ██║     ██╔═██╗╚════╝██║   ██║██║   ██║   ██║  ");
                ConsoleUtils.CenterTextLine("╚██████╗██║  ██║███████╗╚██████╗██║  ██╗     ╚██████╔╝╚██████╔╝   ██║   ");
                ConsoleUtils.CenterTextLine("╚═════╝╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝  ╚═╝      ╚═════╝  ╚═════╝    ╚═╝ ");

                Console.WriteLine();
                string headerLine = $"  {"Guest Name".PadRight(20)} {"Room Info".PadRight(25)} {"Status".PadRight(15)}";
                ConsoleUtils.CenterTextLine(headerLine);
                ConsoleUtils.CenterTextLine(new string('-', 60));
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterTextLine($"  {options[i]}");
                    }
                }
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }

        public static int InMenuSelector(string title, string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();

                ConsoleUtils.CenterTextLine(" ██████╗██╗  ██╗███████╗ ██████╗██╗  ██╗     ██╗███╗   ██╗");
                ConsoleUtils.CenterTextLine("██╔════╝██║  ██║██╔════╝██╔════╝██║ ██╔╝     ██║████╗  ██║");
                ConsoleUtils.CenterTextLine("██║     ███████║█████╗  ██║     █████╔╝█████╗██║██╔██╗ ██║");
                ConsoleUtils.CenterTextLine("██║     ██╔══██║██╔══╝  ██║     ██╔═██╗╚════╝██║██║╚██╗██║");
                ConsoleUtils.CenterTextLine("╚██████╗██║  ██║███████╗╚██████╗██║  ██╗     ██║██║ ╚████║");
                ConsoleUtils.CenterTextLine(" ╚═════╝╚═╝  ╚═╝╚══════╝ ╚═════╝╚═╝  ╚═╝     ╚═╝╚═╝  ╚═══╝");

                Console.WriteLine();
                string headerLine = $"  {"Guest Name".PadRight(20)} {"Room Info".PadRight(25)} {"Status".PadRight(15)}";
                ConsoleUtils.CenterTextLine(headerLine);
                ConsoleUtils.CenterTextLine(new string('-', 60));
                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterTextLine($"  {options[i]}");
                    }
                }
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }

        public static int StaffMenuSelector(string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                ConsoleUtils.CenterTextLine("██████╗  ██████╗  ██████╗ ██╗  ██╗██╗███╗   ██╗ ██████╗     ███╗   ███╗███████╗███╗   ██╗██╗   ██╗");
                ConsoleUtils.CenterTextLine("██╔══██╗██╔═══██╗██╔═══██╗██║ ██╔╝██║████╗  ██║██╔════╝     ████╗ ████║██╔════╝████╗  ██║██║   ██║");
                ConsoleUtils.CenterTextLine("██████╔╝██║   ██║██║   ██║█████╔╝ ██║██╔██╗ ██║██║  ███╗    ██╔████╔██║█████╗  ██╔██╗ ██║██║   ██║");
                ConsoleUtils.CenterTextLine("██╔══██╗██║   ██║██║   ██║██╔═██╗ ██║██║╚██╗██║██║   ██║    ██║╚██╔╝██║██╔══╝  ██║╚██╗██║██║   ██║");
                ConsoleUtils.CenterTextLine("██████╔╝╚██████╔╝╚██████╔╝██║  ██╗██║██║ ╚████║╚██████╔╝    ██║ ╚═╝ ██║███████╗██║ ╚████║╚██████╔╝");
                ConsoleUtils.CenterTextLine("╚═════╝  ╚═════╝  ╚═════╝ ╚═╝  ╚═╝╚═╝╚═╝  ╚═══╝ ╚═════╝     ╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝ ");


                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterTextLine($"  {options[i]}");
                    }
                }
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }

        public static int OwnerMenuSelector(string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();

                ConsoleUtils.CenterTextLine(" ██████╗ ██╗    ██╗███╗   ██╗███████╗██████╗     ███╗   ███╗███████╗███╗   ██╗██╗   ██╗");
                ConsoleUtils.CenterTextLine("██╔═══██╗██║    ██║████╗  ██║██╔════╝██╔══██╗    ████╗ ████║██╔════╝████╗  ██║██║   ██║");
                ConsoleUtils.CenterTextLine("██║   ██║██║ █╗ ██║██╔██╗ ██║█████╗  ██████╔╝    ██╔████╔██║█████╗  ██╔██╗ ██║██║   ██║");
                ConsoleUtils.CenterTextLine("██║   ██║██║███╗██║██║╚██╗██║██╔══╝  ██╔══██╗    ██║╚██╔╝██║██╔══╝  ██║╚██╗██║██║   ██║");
                ConsoleUtils.CenterTextLine("╚██████╔╝╚███╔███╔╝██║ ╚████║███████╗██║  ██║    ██║ ╚═╝ ██║███████╗██║ ╚████║╚██████╔╝");
                ConsoleUtils.CenterTextLine(" ╚═════╝  ╚══╝╚══╝ ╚═╝  ╚═══╝╚══════╝╚═╝  ╚═╝    ╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝ ╚═════╝ ");

                for (int i = 0; i < options.Length; i++)
                {
                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        CenterTextLine($"  {options[i]}");
                    }
                }
                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }

        private static void DrawField(int x, int y, string label, string value, bool isActive)
        {
            Console.SetCursorPosition(x, y);
            Console.Write($"{label}: ");

            Console.SetCursorPosition(x + label.Length + 2, y);

            // Draw the box
            int boxWidth = 30;
            Console.BackgroundColor = isActive ? ConsoleColor.DarkBlue : ConsoleColor.DarkGray;
            Console.ForegroundColor = ConsoleColor.White;
            // Pad the value to ensure the box is drawn cleanly
            Console.Write(value.PadRight(boxWidth));
            Console.ResetColor();
        }

        // Handles interactive text input with up/down navigation
        public static bool InteractiveTextInput(string title, List<(string label, string value)> fields, out List<string> outputValues)
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;
            outputValues = fields.Select(f => f.value).ToList();

            int startY = 4;
            int startX = (Console.WindowWidth - 50) / 2;

            do
            {
                Console.Clear();
                ConsoleUtils.CenterTextLine($"=== {title} ===\n");


                for (int i = 0; i < fields.Count; i++)
                {
                    DrawField(startX, startY + i * 2, fields[i].label, outputValues[i], i == selectedIndex);
                }


                Console.SetCursorPosition(startX, startY + fields.Count * 2 + 2);
                Coloring("Use ↑/↓/Tab to navigate, Enter to submit, ESC to cancel.", ConsoleColor.Yellow);


                Console.SetCursorPosition(startX + fields[selectedIndex].label.Length + 2, startY + selectedIndex * 2);

                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : fields.Count - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.Tab)
                {
                    selectedIndex = (selectedIndex < fields.Count - 1) ? selectedIndex + 1 : 0;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return false; // User canceled
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // Check for empty field before moving on/submitting
                    if (string.IsNullOrWhiteSpace(outputValues[selectedIndex]))
                    {
                        Console.SetCursorPosition(startX, startY + fields.Count * 2 + 4);
                        Coloring("❌ Field cannot be empty!", ConsoleColor.Red);
                        System.Threading.Thread.Sleep(500);
                        continue;
                    }

                    // Move to the next field if not the last one
                    if (selectedIndex < fields.Count - 1)
                    {
                        selectedIndex++;
                    }
                    else
                    {
                        // If it's the last field and Enter is pressed, submit the form
                        return true;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (outputValues[selectedIndex].Length > 0)
                    {
                        outputValues[selectedIndex] = outputValues[selectedIndex].Substring(0, outputValues[selectedIndex].Length - 1);
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar) && outputValues[selectedIndex].Length < 30)
                {
                    // Basic text input handling
                    outputValues[selectedIndex] += keyInfo.KeyChar;
                }

            } while (true);
        }

        public static bool InteractiveTextInputLogIn(string title, List<(string label, string value)> fields, out List<string> outputValues)
        {
            int selectedIndex = 0;
            ConsoleKeyInfo keyInfo;
            outputValues = fields.Select(f => f.value).ToList();

            int startY = 13;
            int startX = (Console.WindowWidth - 50) / 2; // Center the form


            do
            {

                for (int i = 0; i < fields.Count; i++)
                {

                    DrawField(startX, startY + i * 2, fields[i].label, outputValues[i], i == selectedIndex);
                }

                Console.SetCursorPosition(startX, startY + fields.Count * 2 + 2);
                Coloring("Use ↑/↓/Tab to navigate, Enter to submit, ESC to cancel.", ConsoleColor.Yellow);

                // Set cursor to the active field for typing
                Console.SetCursorPosition(startX + fields[selectedIndex].label.Length + 2, startY + selectedIndex * 2);

                keyInfo = Console.ReadKey(true);

                if (keyInfo.Key == ConsoleKey.UpArrow)
                {
                    selectedIndex = (selectedIndex > 0) ? selectedIndex - 1 : fields.Count - 1;
                }
                else if (keyInfo.Key == ConsoleKey.DownArrow || keyInfo.Key == ConsoleKey.Tab)
                {
                    selectedIndex = (selectedIndex < fields.Count - 1) ? selectedIndex + 1 : 0;
                }
                else if (keyInfo.Key == ConsoleKey.Escape)
                {
                    return false; // User canceled
                }
                else if (keyInfo.Key == ConsoleKey.Enter)
                {
                    // Check for empty field before moving on/submitting
                    if (string.IsNullOrWhiteSpace(outputValues[selectedIndex]))
                    {
                        Console.SetCursorPosition(startX, startY + fields.Count * 2 + 4);
                        Coloring("❌ Field cannot be empty!", ConsoleColor.Red);
                        System.Threading.Thread.Sleep(500);
                        continue;
                    }

                    // Move to the next field if not the last one
                    if (selectedIndex < fields.Count - 1)
                    {
                        selectedIndex++;
                    }
                    else
                    {
                        // If it's the last field and Enter is pressed, submit the form
                        return true;
                    }
                }
                else if (keyInfo.Key == ConsoleKey.Backspace)
                {
                    if (outputValues[selectedIndex].Length > 0)
                    {
                        outputValues[selectedIndex] = outputValues[selectedIndex].Substring(0, outputValues[selectedIndex].Length - 1);
                    }
                }
                else if (!char.IsControl(keyInfo.KeyChar) && outputValues[selectedIndex].Length < 30) // Limit input length
                {
                    // Basic text input handling
                    outputValues[selectedIndex] += keyInfo.KeyChar;
                }

            } while (true);
        }

        public static int MenuSelectorWithHeaders(string title, string[] headers, string[] options)
        {
            int index = 0;
            ConsoleKey key;

            do
            {
                Console.Clear();
                ConsoleUtils.CenterTextLine($"=========={title}==========");
                Console.WriteLine();

                string headerLine = $"  {"Guest Name".PadRight(20)} {"Room Info".PadRight(25)} {"Status".PadRight(15)}";
                ConsoleUtils.CenterTextLine(headerLine);
                ConsoleUtils.CenterTextLine("  " + new string('-', 60)); // Separator

                // 2. Print Options
                for (int i = 0; i < options.Length; i++)
                {
                    // Check if it's the "Cancel" option, which should not be selected
                    bool isCancel = i == options.Length - 1 && options[i] == "Cancel";

                    if (i == index)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        // Use the padded options from GetSummary()
                        CenterTextLine($"> {options[i]}");
                        Console.ResetColor();
                    }
                    else
                    {
                        // Use the padded options from GetSummary()
                        CenterTextLine($"  {options[i]}");
                    }
                }

                key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow && index > 0)
                    index--;
                else if (key == ConsoleKey.DownArrow && index < options.Length - 1)
                    index++;

            } while (key != ConsoleKey.Enter);
            return index;
        }
    }
    class Program
    {
        static string accountsFile = "accounts.json";
        static string bookingsFile = "bookings.json";
        static string salesReportFile = "salesreport.json";
        static List<Account> accounts = new List<Account>();
        static List<Booking> bookings = new List<Booking>();
        static List<SalesReport> salesReports = new List<SalesReport>();
        static string loggedInUser = "";

        static List<Room> allRooms = new List<Room>()
        {
            new Room("Single", 2500.0, 101),
            new Room("Single", 2500.0, 102),
            new Room("Single", 2500.0, 103),
            new Room("Double", 5000.0, 201),
            new Room("Double", 5000.0, 202),
            new Room("Double", 5000.0, 203),
            new Room("Suite", 8000.0, 301),
            new Room("Suite", 8000.0, 302),
            new Room("Suite", 8000.0, 303),
            new Room("Deluxe", 12000.0, 401),
            new Room("Deluxe", 12000.0, 402),
            new Room("Deluxe", 12000.0, 403)
        };

        static void Main(string[] args)
        {
            accounts = LoadAccounts();
            bookings = LoadBookings();
            salesReports = LoadSalesReports();

            if (!accounts.Any(a => a.Role == "Owner"))
            {
                Console.WriteLine("No owner account found. Creating default owner...");
                accounts.Add(new Account("owner", "119964", "Owner"));
                accounts.Add(new Account("ownerbackup", "119964", "Owner"));
                SaveAccounts(accounts);
            }

            while (true)
            {
                Console.Clear();
                ConsoleUtils.CenterTextLine("██╗  ██╗ ██████╗ ████████╗███████╗██╗         ███╗   ███╗ █████╗ ███╗   ██╗ █████╗  ██████╗ ███████╗███╗   ███╗███████╗███╗   ██╗████████╗    ███████╗██╗   ██╗███████╗████████╗███████╗███╗   ███╗");
                ConsoleUtils.CenterTextLine("██║  ██║██╔═══██╗╚══██╔══╝██╔════╝██║         ████╗ ████║██╔══██╗████╗  ██║██╔══██╗██╔════╝ ██╔════╝████╗ ████║██╔════╝████╗  ██║╚══██╔══╝    ██╔════╝╚██╗ ██╔╝██╔════╝╚══██╔══╝██╔════╝████╗ ████║");
                ConsoleUtils.CenterTextLine("███████║██║   ██║   ██║   █████╗  ██║         ██╔████╔██║███████║██╔██╗ ██║███████║██║  ███╗█████╗  ██╔████╔██║█████╗  ██╔██╗ ██║   ██║       ███████╗ ╚████╔╝ ███████╗   ██║   █████╗  ██╔████╔██║");
                ConsoleUtils.CenterTextLine("██╔══██║██║   ██║   ██║   ██╔══╝  ██║         ██║╚██╔╝██║██╔══██║██║╚██╗██║██╔══██║██║   ██║██╔══╝  ██║╚██╔╝██║██╔══╝  ██║╚██╗██║   ██║       ╚════██║  ╚██╔╝  ╚════██║   ██║   ██╔══╝  ██║╚██╔╝██║");
                ConsoleUtils.CenterTextLine("██║  ██║╚██████╔╝   ██║   ███████╗███████╗    ██║ ╚═╝ ██║██║  ██║██║ ╚████║██║  ██║╚██████╔╝███████╗██║ ╚═╝ ██║███████╗██║ ╚████║   ██║       ███████║   ██║   ███████║   ██║   ███████╗██║ ╚═╝ ██║");
                ConsoleUtils.CenterTextLine("╚═╝  ╚═╝ ╚═════╝    ╚═╝   ╚══════╝╚══════╝    ╚═╝     ╚═╝╚═╝  ╚═╝╚═╝  ╚═══╝╚═╝  ╚═╝ ╚═════╝ ╚══════╝╚═╝     ╚═╝╚══════╝╚═╝  ╚═══╝   ╚═╝       ╚══════╝   ╚═╝   ╚══════╝   ╚═╝   ╚══════╝╚═╝     ╚═╝");
                Console.WriteLine("\n");

                Account? loggedIn = Login();
                loggedInUser = loggedIn?.Username ?? "";

                if (loggedIn == null)
                {
                    continue;
                }

                if (loggedIn.Role == "Owner")
                    OwnerMenu();
                else
                    StaffMenu();

            }
        }

        static Account? Login()
        {
            List<(string label, string value)> loginFields = new List<(string label, string value)>
            {
                ("Username", ""),
                ("Password", "")
            };

            List<string> inputs;
            if (!ConsoleUtils.InteractiveTextInputLogIn("", loginFields, out inputs))
            {
                return null; // Login cancelled by user
            }

            string user = inputs[0];
            string pass = inputs[1];


            Account? account = accounts.FirstOrDefault(a => a.Username == user && a.Password == pass);

            if (account != null && !account.IsActive)
            {
                ConsoleUtils.Coloring("Account is deactivated. Please contact the owner.", ConsoleColor.Red);
                Console.ReadKey();
                return null;
            }

            return account;


        }

        static void OwnerMenu()
        {
            Console.WriteLine("\n");
            Console.Clear();

            string[] menuOptions =
            {
                "Create Desk Staff Account",
                "View All Accounts",
                "Manage Bookings (Staff Menu)",
                "View Sales Reports",
                "Exit"
            };
            Console.Clear();

            while (true)
            {
                int choice = ConsoleUtils.OwnerMenuSelector(menuOptions);
                Console.Clear();
                switch (choice)
                {
                    case 0:
                        CreateAccount();
                        break;
                    case 1:
                        ManageAccounts();
                        break;
                    case 2:
                        StaffMenu();
                        break;
                    case 3:
                        viewSalesReports();
                        break;
                    case 4:
                        return;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void CreateAccount()
        {
            List<(string label, string value)> accountFields = new List<(string label, string value)>
            {
                ("New Staff Username", ""),
                ("New Staff Password", "")
            };

            List<string> inputs;
            if (!ConsoleUtils.InteractiveTextInput("CREATE DESK STAFF ACCOUNT", accountFields, out inputs))
            {
                ConsoleUtils.Coloring("Account creation canceled.", ConsoleColor.Yellow);
                return;
            }

            string user = inputs[0];
            string pass = inputs[1];

            // Validation after interactive input: Check if username already exists
            if (accounts.Any(a => a.Username.Equals(user, StringComparison.OrdinalIgnoreCase)))
            {
                ConsoleUtils.Coloring($"❌ Username '{user}' already exists! Account creation aborted.", ConsoleColor.Red);
                return;
            }

            // All validations passed
            accounts.Add(new Account(user, pass, "Staff"));
            SaveAccounts(accounts);
            ConsoleUtils.Coloring("✅ Desk Staff account created successfully!", ConsoleColor.Green);
        }

        static void ManageAccounts()
        {
            Console.Clear();
            ConsoleUtils.CenterTextLine("  █████╗  ██████╗ ██████╗ ██████╗ ██╗   ██╗███╗   ██╗████████╗███████╗");
            ConsoleUtils.CenterTextLine("██╔══██╗██╔════╝██╔════╝██╔═══██╗██║   ██║████╗  ██║╚══██╔══╝██╔════");
            ConsoleUtils.CenterTextLine(" ███████║██║     ██║     ██║   ██║██║   ██║██╔██╗ ██║   ██║   ███████╗");
            ConsoleUtils.CenterTextLine(" ██╔══██║██║     ██║     ██║   ██║██║   ██║██║╚██╗██║   ██║   ╚════██║");
            ConsoleUtils.CenterTextLine(" ██║  ██║╚██████╗╚██████╗╚██████╔╝╚██████╔╝██║ ╚████║   ██║   ███████║");
            ConsoleUtils.CenterTextLine(" ╚═╝  ╚═╝ ╚═════╝ ╚═════╝ ╚═════╝  ╚═════╝ ╚═╝  ╚═══╝   ╚═╝   ╚══════╝");


            if (accounts.Count == 0)
            {
                ConsoleUtils.Coloring("No accounts found.", ConsoleColor.Yellow);
                return;
            }

            string[] accountOptions = accounts.Select(a => $"{a.Username} - {a.Role}").ToArray();

            while (true)
            {
                int choiceIndex = ConsoleUtils.MenuSelector("Select Account to Manage", accountOptions.Concat(new[] { "Back" }).ToArray());
                if (choiceIndex == accountOptions.Length)
                    break;

                Account selectedAccount = accounts[choiceIndex];

                ManageSpecificAccount(selectedAccount);
            }
        }

        static void ManageSpecificAccount(Account account)
        {

            string status = account.IsActive ? "Deactivate" : "Activate";

            string[] options = { $"Reset Password for {account.Username}", $"{status} Account for {account.Username}", "Back" };
            int choice = ConsoleUtils.MenuSelector($"Manage {account.Username} ({account.Role})", options);

            switch (choice)
            {
                case 0:
                    ResetStaffPassword(account);
                    break;
                case 1:
                    ToggleAccountStatus(account);
                    break;
                case 2:
                    return;
            }
        }

        static void ResetStaffPassword(Account account)
        {
            Console.Clear();
            ConsoleUtils.CenterTextLine($"=== Reset Password for {account.Username} ===");

            List<(string label, string value)> fields = new List<(string label, string value)>
            {
                ("New Password", "")
            };

            List<string> inputs;
            if (!ConsoleUtils.InteractiveTextInput("RESET PASSWORD", fields, out inputs))
            {
                ConsoleUtils.Coloring("Password reset canceled.", ConsoleColor.Yellow);
                return;
            }

            string newPass = inputs[0];

            account.Password = newPass;
            SaveAccounts(accounts);
            ConsoleUtils.Coloring($"✅ Password for {account.Username} has been reset successfully!", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void ToggleAccountStatus(Account account)
        {
            string newStatus = account.IsActive ? "DEACTIVATED" : "ACTIVATED";
            Console.Clear();
            ConsoleUtils.CenterTextLine($"=== {newStatus} Account Confirmation ===");
            ConsoleUtils.CenterText($"Are you sure you want to change the status of {account.Username} to {newStatus}? (y/n): ");
            
            if (Console.ReadLine()!.Trim().ToLower() == "y")
            {
                account.IsActive = !account.IsActive; 
                SaveAccounts(accounts);
                ConsoleUtils.Coloring($"✅ Account {account.Username} is now {newStatus}!", ConsoleColor.Green);
            }
            else
            {
                ConsoleUtils.Coloring("Action canceled.", ConsoleColor.Yellow);
            }
            Console.ReadKey();
        }

        static void ResetOwnPassword()
        {
            Console.Clear();
            ConsoleUtils.CenterTextLine("=== Reset My Password ===");
        
            Account? currentUser = accounts.FirstOrDefault(a => a.Username == loggedInUser);

            if (currentUser == null)
            {
                ConsoleUtils.Coloring("❌ Error: Current user account not found.", ConsoleColor.Red);
                Console.ReadKey();
                return;
            }

            List<(string label, string value)> fields = new List<(string label, string value)>
            {
                ("Enter New Password", "")
            };
            
            List<string> inputs;
            if (!ConsoleUtils.InteractiveTextInput("RESET PASSWORD", fields, out inputs))
            {
                ConsoleUtils.Coloring("Password change canceled.", ConsoleColor.Yellow);
                return;
            }

            string newPass = inputs[0];

            currentUser.Password = newPass;
            SaveAccounts(accounts);
            
            ConsoleUtils.Coloring("✅ Your password has been reset successfully!", ConsoleColor.Green);
            Console.ReadKey();
        }

        static void StaffMenu()
        {

            Console.WriteLine("\n");
            string[] menuOptions =
            {
                "Book a Room",
                "Edit / Cancel Booking",
                "Check-In Guest",
                "Check-Out Guest",
                "View All Bookings",
                "Reset My Password",
                "Exit"
            };

            while (true)
            {
                int choice = ConsoleUtils.StaffMenuSelector(menuOptions);
                switch (choice)
                {
                    case 0:
                        BookRoom();
                        break;
                    case 1:
                        EditOrCancelBooking();
                        break;
                    case 2:
                        CheckIn();
                        break;
                    case 3:
                        CheckOut();
                        break;
                    case 4:
                        DisplayAllBookings();
                        break;
                    case 5:
                        ResetOwnPassword();
                        break;
                    case 6:
                        return;
                }
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
            }
        }

        static void viewSalesReports()
        {
            Console.Clear();
            ConsoleUtils.CenterTextLine("  ███████╗ █████╗ ██╗     ███████╗███████╗    ██████╗ ███████╗██████╗  ██████╗ ██████╗ ████████╗");
            ConsoleUtils.CenterTextLine("  ██╔════╝██╔══██╗██║     ██╔════╝██╔════╝    ██╔══██╗██╔════╝██╔══██╗██╔═══██╗██╔══██╗╚══██╔══╝");
            ConsoleUtils.CenterTextLine("███████╗███████║██║     █████╗  ███████╗    ██████╔╝█████╗  ██████╔╝██║   ██║██████╔╝   ██║ ");
            ConsoleUtils.CenterTextLine("╚════██║██╔══██║██║     ██╔══╝  ╚════██║    ██╔══██╗██╔══╝  ██╔═══╝ ██║   ██║██╔══██╗   ██║ ");
            ConsoleUtils.CenterTextLine("  ███████║██║  ██║███████╗███████╗███████║    ██║  ██║███████╗██║     ╚██████╔╝██║  ██║   ██║   ");
            ConsoleUtils.CenterTextLine("╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝    ╚═╝  ╚═╝╚══════╝╚═╝      ╚═════╝ ╚═╝  ╚═╝   ╚═╝ ");
            if (salesReports.Count == 0)
            {
                ConsoleUtils.Coloring("No sales reports found.", ConsoleColor.Yellow);
                return;
            }

            ConsoleUtils.CenterText("Search by Room Type or Room Number (leave blank to show all): ");
            string search = Console.ReadLine()!.Trim().ToLower();

            var filteredReports = string.IsNullOrEmpty(search)
                ? salesReports
                : salesReports.Where(r =>
                      r.RoomType.ToLower().Contains(search) ||
                      r.RoomNumber.ToString().Contains(search) || r.GuestName.ToLower().Contains(search) || r.CheckOutDate.ToString("yyyy-MM-dd").Contains(search))
                  .ToList();

            if (filteredReports.Count == 0)
            {
                ConsoleUtils.Coloring("No matching sales reports found.", ConsoleColor.Yellow);
                return;
            }


            Console.Clear();
            ConsoleUtils.CenterTextLine("  ███████╗ █████╗ ██╗     ███████╗███████╗    ██████╗ ███████╗██████╗  ██████╗ ██████╗ ████████╗");
            ConsoleUtils.CenterTextLine("  ██╔════╝██╔══██╗██║     ██╔════╝██╔════╝    ██╔══██╗██╔════╝██╔══██╗██╔═══██╗██╔══██╗╚══██╔══╝");
            ConsoleUtils.CenterTextLine("███████╗███████║██║     █████╗  ███████╗    ██████╔╝█████╗  ██████╔╝██║   ██║██████╔╝   ██║ ");
            ConsoleUtils.CenterTextLine("╚════██║██╔══██║██║     ██╔══╝  ╚════██║    ██╔══██╗██╔══╝  ██╔═══╝ ██║   ██║██╔══██╗   ██║ ");
            ConsoleUtils.CenterTextLine("  ███████║██║  ██║███████╗███████╗███████║    ██║  ██║███████╗██║     ╚██████╔╝██║  ██║   ██║   ");
            ConsoleUtils.CenterTextLine("╚══════╝╚═╝  ╚═╝╚══════╝╚══════╝╚══════╝    ╚═╝  ╚═╝╚══════╝╚═╝      ╚═════╝ ╚═╝  ╚═╝   ╚═╝ ");
            if (!string.IsNullOrEmpty(search))
                ConsoleUtils.CenterTextLine($"Filtered by: {search}\n");
            else
                ConsoleUtils.CenterTextLine("Filetered by: All Records\n");


            string header = $"  {"Guest Name".PadRight(20)} {"Room Info".PadRight(25)} {"Check-Out Date".PadRight(15)} {"Amount Paid".PadRight(15)}";
            ConsoleUtils.CenterTextLine(header);
            ConsoleUtils.CenterTextLine(new string('-', header.Length));

            foreach (var report in filteredReports)
            {
                ConsoleUtils.CenterTextLine(report.GetTableRow());
            }

            double total = filteredReports.Sum(r => r.AmountPaid);
            ConsoleUtils.CenterTextLine(new string('=', header.Length));
            string totalLine = $"Total Income: ₱{total:N2}";
            ConsoleUtils.CenterTextLine(totalLine.PadLeft(header.Length));


        }

        static void BookRoom()
        {
            Console.Clear();
            List<(string label, string value)> guestFields = new List<(string label, string value)>
            {
                ("Guest Name", ""),
                ("Phone Number", ""),
                ("Check-In Date (yyyy-mm-dd)", ""),
                ("Check-Out Date (yyyy-mm-dd)", "")
            };

            if (!ConsoleUtils.InteractiveTextInput("BOOK ROOM - GUEST DETAILS", guestFields, out List<string> inputs))
            {
                ConsoleUtils.Coloring("Booking canceled by user.", ConsoleColor.Yellow);
                return;
            }

            string name = inputs[0];
            string phoneNumber = inputs[1];

            if (!DateTime.TryParse(inputs[2], out DateTime checkInDate))
            {
                ConsoleUtils.Coloring("❌ Invalid Check-In Date format! Booking aborted.", ConsoleColor.Red);
                return;
            }

            if (!DateTime.TryParse(inputs[3], out DateTime checkOutDate) || checkOutDate <= checkInDate)
            {
                ConsoleUtils.Coloring("❌ Invalid Check-Out Date format or Check-Out Date is before Check-In Date! Booking aborted.", ConsoleColor.Red);
                return;
            }

            Room? ChosenRoom = null;
            while (ChosenRoom == null)
            {
                var roomTypes = allRooms.Select(r => r.RoomType).Distinct().ToList();
                int typeChoice = ConsoleUtils.MenuSelector("Select Room Type", roomTypes.Concat(new[] { "Go Back" }).ToArray());
                if (typeChoice == roomTypes.Count)
                    continue;
                string selectedRoomType = roomTypes[typeChoice];

                var availableRooms = allRooms
                    .Where(r => r.RoomType == selectedRoomType && !IsRoomOccupied(r.RoomNumber))
                    .ToList();

                if (availableRooms.Count == 0)
                {
                    ConsoleUtils.Coloring($"❌ No available {selectedRoomType} rooms for the selected dates.", ConsoleColor.Red);
                    Console.WriteLine("Press any key to return... ");
                    Console.ReadKey();
                    return;
                }

                string[] roomOptions = availableRooms
                    .Select(r => $"Room {r.RoomNumber} - ₱{r.RoomRate:N2}/night")
                    .ToArray();

                int roomIndex = ConsoleUtils.MenuSelector($"Available {selectedRoomType.ToUpper()} Rooms", roomOptions.Concat(new[] { "Cancel" }).ToArray());
                if (roomIndex == availableRooms.Count)
                    return;
                ChosenRoom = availableRooms[roomIndex];

                string bookedBy = loggedInUser;

                Booking booking = new Booking(name, phoneNumber, checkInDate, checkOutDate, ChosenRoom, bookedBy);
                bookings.Add(booking);
                SaveBookings(bookings);

                Console.Clear();
                ConsoleUtils.Coloring("✅ Room booked successfully!", ConsoleColor.Green);
                booking.DisplayInfo();
            }
        }

        static void EditOrCancelBooking()
        {
            //Add function that removes the checkedout bookings 
            Console.Clear();
            Console.WriteLine("\n=== Edit / Cancel Booking ===");

            if (bookings.Count == 0)
            {
                Console.WriteLine("No bookings to modify.");
                return;
            }

            string[] bookingOptions = bookings
                .Select(b => b.GetSummary())
                .ToArray();

            string[] headers = { "Name", "Room Info", "Status" };
            int choiceIndex = ConsoleUtils.MenuSelectorWithHeaders(
                "Select Booking to Modify",
                headers,
                bookingOptions.Concat(new[] { "Cancel" }).ToArray()
            );

            if (choiceIndex == bookings.Count)
                return;

            var booking = bookings[choiceIndex];
            Console.Clear();

            ConsoleUtils.CenterTextLine($"\nSelected: {booking.Name} - Room {booking.RoomNumber}");

            string[] menuOptions =
            {
                "Edit Information",
                "Change Room",
                "Delete Booking",
                "Cancel"
            };

            int choice = ConsoleUtils.MenuSelector("Modify Booking", menuOptions);

            switch (choice + 1)
            {
                case 1:
                    if (booking.IsCheckedOut)
                    {
                        ConsoleUtils.Coloring("❌ Cannot edit checked-out booking.", ConsoleColor.Red);
                        return;
                    }
                    else
                    {
                        EditBookingInfo(booking);
                    }
                    break;
                case 2:
                    if (booking.IsCheckedOut)
                    {
                        ConsoleUtils.Coloring("❌ Cannot change room of checked-out booking.", ConsoleColor.Red);
                        return;
                    }
                    else
                    {
                        ChangeBookingRoom(booking);
                    }
                    break;
                case 3:
                    Console.Write("Are you sure you want to delete this booking? (y/n): ");
                    if (Console.ReadLine()!.Trim().ToLower() == "y")
                    {
                        bookings.Remove(booking);
                        SaveBookings(bookings);
                        ConsoleUtils.Coloring("✅ Booking deleted successfully!", ConsoleColor.Green);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Action canceled.");
                        break;
                    }
                default:
                    Console.WriteLine("Action canceled.");
                    break;
            }
        }

        static void EditBookingInfo(Booking booking)
        {
            Console.Clear();
            Console.WriteLine("\nEditing booking info (press Enter to keep old value)");

            ConsoleUtils.CenterText($"Name ({booking.Name}): ");
            string newName = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newName)) booking.Name = newName;

            ConsoleUtils.CenterText($"Phone ({booking.PhoneNumber}): ");
            string newPhone = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newPhone)) booking.PhoneNumber = newPhone;

            ConsoleUtils.CenterText($"Check-In ({booking.CheckInDate:yyyy-MM-dd}): ");
            string newCheckIn = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newCheckIn) && DateTime.TryParse(newCheckIn, out DateTime newCheckInDate)) booking.CheckInDate = newCheckInDate;

            ConsoleUtils.CenterText($"Check-Out ({booking.CheckOutDate:yyyy-MM-dd}): ");
            string newCheckOut = Console.ReadLine()!;
            if (!string.IsNullOrWhiteSpace(newCheckOut) && DateTime.TryParse(newCheckOut, out DateTime newCheckOutDate))
            {
                if (newCheckOutDate <= booking.CheckInDate)
                {
                    ConsoleUtils.Coloring("❌ Check-Out Date must be after Check-In Date! Value not updated.", ConsoleColor.Red);
                }
                else
                {
                    booking.CheckOutDate = newCheckOutDate;
                }
            }

            SaveBookings(bookings);
            ConsoleUtils.Coloring("✅ Booking information updated successfully!", ConsoleColor.Green);
        }

        static void ChangeBookingRoom(Booking booking)
        {
            Console.WriteLine("\nAvailable rooms of same type:");
            var availableRooms = allRooms
                .Where(r => r.RoomType == booking.RoomType.RoomType && !IsRoomOccupied(r.RoomNumber))
                .ToList();

            if (availableRooms.Count == 0)
            {
                ConsoleUtils.Coloring("❌ No other available rooms of the same type.", ConsoleColor.Red);
                return;
            }

            foreach (var r in availableRooms)
                r.DisplayRoomInfo();

            Console.Write("\nEnter new room number: ");
            if (!int.TryParse(Console.ReadLine(), out int newRoom)) return;

            var selected = availableRooms.FirstOrDefault(r => r.RoomNumber == newRoom);
            if (selected == null)
            {
                ConsoleUtils.Coloring("❌ Selected room is not available.", ConsoleColor.Red);
                return;
            }

            booking.RoomNumber = selected.RoomNumber;
            booking.RoomType = selected;
            SaveBookings(bookings);
            ConsoleUtils.Coloring("✅ Booking room changed successfully!", ConsoleColor.Green);
        }

        static void CheckIn()
        {
            Console.Clear();
            ConsoleUtils.CenterTextLine("=== Check-In Menu === \n");
            var notCheckedIn = bookings.FindAll(b => !b.IsCheckedIn && !b.IsCheckedOut);

            if (notCheckedIn.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                ConsoleUtils.CenterTextLine("No bookings available for check-in.");
                Console.ResetColor();
                return;
            }

            string[] checkInOptions = notCheckedIn
                .Select(b => b.GetSummary())
                .ToArray();



            int choiceIndex = ConsoleUtils.InMenuSelector("", checkInOptions.Concat(new[] { "Cancel" }).ToArray());
            if (choiceIndex == notCheckedIn.Count) return;

            var guest = notCheckedIn[choiceIndex];

            guest.IsCheckedIn = true;
            guest.CheckedInBy = loggedInUser;
            SaveBookings(bookings);
            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            ConsoleUtils.CenterTextLine($"✅ {guest.Name} has been checked in successfully!");
            Console.ResetColor();
        }

        static void CheckOut()
        {
            Console.Clear();
            Console.WriteLine("\n=== Check-Out Menu ===");
            var checkedInGuests = bookings.FindAll(b => b.IsCheckedIn && !b.IsCheckedOut);

            if (checkedInGuests.Count == 0)
            {
                ConsoleUtils.Coloring("No guests available for check-out.", ConsoleColor.Yellow);
                return;
            }

            string[] checkOutOptions = checkedInGuests
                .Select(b => b.GetSummary())
                .ToArray();

            int choiceIndex = ConsoleUtils.OutMenuSelector("", checkOutOptions.Concat(new[] { "Cancel" }).ToArray());
            if (choiceIndex == checkedInGuests.Count) return;

            var guest = checkedInGuests[choiceIndex];

            Console.WriteLine($"Enter new check-out date (current: {guest.CheckOutDate:yyyy-MM-dd}, press Enter to keep): ");
            string newOut = Console.ReadLine()!;

            if (!string.IsNullOrWhiteSpace(newOut) && DateTime.TryParse(newOut, out DateTime newCheckOutDate))
            {
                if (newCheckOutDate <= guest.CheckInDate)
                {
                    ConsoleUtils.Coloring("❌ Check-Out Date must be after Check-In Date! Check-out cancelled.", ConsoleColor.Red);
                    return;
                }
                guest.CheckOutDate = newCheckOutDate;
            }

            guest.IsCheckedOut = true;
            double totalAmount = guest.CalculateTotalCost();
            guest.CheckedOutBy = loggedInUser;

            Console.Clear();
            ConsoleUtils.CenterTextLine($"=== Check-Out Summary ===\n");
            ConsoleUtils.CenterTextLine($"Guest: {guest.Name}");
            ConsoleUtils.CenterTextLine($"Room: {guest.RoomType.RoomType} ({guest.RoomNumber})");
            ConsoleUtils.CenterTextLine($"Stay Duration: {guest.GetStayDuration()} night(s)");
            ConsoleUtils.CenterTextLine($"Total Amount: ₱{totalAmount:N2}");
            ConsoleUtils.CenterTextLine("Payment received. Thank you!");

            salesReports.Add(new SalesReport(guest.Name, guest.RoomNumber, guest.RoomType.RoomType, guest.CheckOutDate, totalAmount));
            SaveBookings(bookings);
            SaveSalesReports(salesReports);
            ConsoleUtils.Coloring($"✅ {guest.Name} has been checked out successfully!", ConsoleColor.Green);
        }

        static void DisplayAllBookings()
        {
            //Add search function
            Console.Clear();
            Console.WriteLine("\n=== All Bookings ===");
            if (bookings.Count == 0)
            {
                ConsoleUtils.Coloring("No bookings found.", ConsoleColor.Yellow);
                return;
            }

            string[] bookingSummaries = bookings
                 .Select(b => b.GetSummary())
                 .Concat(new[] { "Back" }).ToArray();

            int index = ConsoleUtils.BookingMenuSelector("", bookingSummaries);

            if (index == bookings.Count) return;
            Console.Clear();
            bookings[index].DisplayInfo();
        }

        static bool IsRoomOccupied(int roomNumber)
        {
            return bookings.Any(b => b.RoomNumber == roomNumber && !b.IsCheckedOut);
        }


        static void SaveAccounts(List<Account> list)
        {
            File.WriteAllText(accountsFile, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
        }

        static List<Account> LoadAccounts()
        {
            if (!File.Exists(accountsFile)) return new List<Account>();
            string json = File.ReadAllText(accountsFile);
            return JsonSerializer.Deserialize<List<Account>>(json) ?? new List<Account>();
        }

        static void SaveBookings(List<Booking> list)
        {
            File.WriteAllText(bookingsFile, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
        }

        static List<Booking> LoadBookings()
        {
            if (!File.Exists(bookingsFile)) return new List<Booking>();
            string json = File.ReadAllText(bookingsFile);
            return JsonSerializer.Deserialize<List<Booking>>(json) ?? new List<Booking>();
        }

        static void SaveSalesReports(List<SalesReport> list)
        {
            File.WriteAllText(salesReportFile, JsonSerializer.Serialize(list, new JsonSerializerOptions { WriteIndented = true }));
        }

        static List<SalesReport> LoadSalesReports()
        {
            if (!File.Exists(salesReportFile)) return new List<SalesReport>();
            string json = File.ReadAllText(salesReportFile);
            return JsonSerializer.Deserialize<List<SalesReport>>(json) ?? new List<SalesReport>();
        }
    }
}