-- mylibrary_schema.sql
-- SQLite-compatible SQL script to create and seed the database

-- Create Users table
CREATE TABLE IF NOT EXISTS Users (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Username TEXT NOT NULL,
    Password TEXT NOT NULL
);

-- Insert default admin user if not exists
INSERT INTO Users (Username, Password)
SELECT 'admin', '123'
WHERE NOT EXISTS (SELECT 1 FROM Users WHERE Username = 'admin');

-- Create Books table
CREATE TABLE IF NOT EXISTS Books (
    BookID INTEGER PRIMARY KEY AUTOINCREMENT,
    Title TEXT NOT NULL,
    Author TEXT NOT NULL,
    Year INTEGER NOT NULL,
    AvailableCopies INTEGER NOT NULL
);

-- Create Borrowers table
CREATE TABLE IF NOT EXISTS Borrowers (
    BorrowerID INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Email TEXT NOT NULL,
    Phone TEXT NOT NULL
);

-- Create IssuedBooks table
CREATE TABLE IF NOT EXISTS IssuedBooks (
    IssueID INTEGER PRIMARY KEY AUTOINCREMENT,
    BookID INTEGER NOT NULL,
    BorrowerID INTEGER NOT NULL,
    IssueDate TEXT NOT NULL,
    DueDate TEXT NOT NULL,
    FOREIGN KEY (BookID) REFERENCES Books(BookID),
    FOREIGN KEY (BorrowerID) REFERENCES Borrowers(BorrowerID)
);
