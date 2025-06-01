#  MyLibrary - C# WinForms Desktop Application

**MyLibrary** is a desktop application built with **C# and Windows Forms** to help manage a small library’s book inventory and borrower records using a **local SQLite database**. It demonstrates key concepts in event-driven programming, database integration, and user interface design.

---

##  Features

-  **User Login System**
  - Authenticate users using SQLite `Users` table.
  - Includes registration form with duplicate username check.

-  **Books Management**
  - Add, edit, delete books
  - Data validation (title, author, numeric checks for year and copies)

-  **Borrowers Management**
  - Add, edit, delete borrower records
  - Display borrower list in a DataGridView

-  **Book Issue & Return**
  - Issue books to borrowers
  - Return books and update available copies

-  **Validation & Exception Handling**
  - All input fields are validated
  - Database operations wrapped in try-catch with user-friendly error messages

>  Bonus features like filtering and overdue reports are not yet implemented.

---

##  Setup Instructions

1. Clone or download this repository.
2. Open the solution file (`MyLibraryApp1.sln`) in **Visual Studio**.
3. Build and run the project.

On first launch, the application will automatically:
- Create the `MyLibrary.db` SQLite database in the application folder
- Create all required tables (`Users`, `Books`, `Borrowers`, `IssuedBooks`)
- Insert a default admin user

 **No additional software or SQLite installation is required.**  
All required SQLite libraries are bundled using the `System.Data.SQLite` NuGet package.

---

##  Default Login

- **Username:** `admin`  
- **Password:** `123`

You may also register new users using the **Register** button on the login form.

---

##  SQLite Dependency

SQLite is embedded and configured through `System.Data.SQLite`.

The application works automatically if:
- You installed `System.Data.SQLite` via NuGet
- `System.Data.SQLite.dll` and `SQLite.Interop.dll` (x86 or x64) are present in your `bin` folder
- Nothing was removed from your build or publish output

 **If you encounter** the error  
`Unable to load DLL 'SQLite.Interop.dll'`  
this usually means the DLLs are missing. To fix it:

```bash
dotnet publish -c Release -r win-x64 --self-contained true
```

Or ensure your build output contains the correct DLLs.

---

##  SQL Reference (for Submission)

Although the app creates the database and tables automatically in code, a `mylibrary_schema.sql` file is included for submission as required.

It defines:
- Users table with default admin
- Books, Borrowers, IssuedBooks tables
- Foreign key relationships

---

## 📸 Screenshots

Screenshots of the key application screens are placed in:

```
/Data/screenshots/
```

##  References

-  **AI Assistance:**  
  ChatGPT (OpenAI) was used to support code structure, and generation of documentation for this project.

-  **YouTube Tutorial:**  
  [C# WinForms Library Management System Tutorial](https://www.youtube.com/watch?v=7WXtWfa4oR8&t=1810s)  
  Referenced for guidance on UI layout and form-based design structure.
