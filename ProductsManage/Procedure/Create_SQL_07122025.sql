USE ProductDb;
GO

CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL UNIQUE,
    Description NVARCHAR(MAX),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX),
    Price DECIMAL(10,2) NOT NULL CHECK (Price >= 0),
    StockQuantity INT NOT NULL DEFAULT 0 CHECK (StockQuantity >= 0),
    CategoryId INT NULL FOREIGN KEY REFERENCES Categories(Id) ON DELETE CASCADE,
    Status NVARCHAR(20) DEFAULT 'active' CHECK (Status IN ('active', 'inactive', 'discontinued')),
    CreatedAt DATETIME2 DEFAULT GETUTCDATE(),
    UpdatedAt DATETIME2 DEFAULT GETUTCDATE()
);

CREATE INDEX Index_Products_CategoryId ON Products(CategoryId);
CREATE INDEX Index_Products_Price ON Products(Price);
CREATE INDEX Index_Products_Status ON Products(Status);
GO