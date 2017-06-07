CREATE TABLE [dbo].[Customers] (
    [Id_Customer]    INT            IDENTITY (1, 1) NOT NULL,
    [CustomerName]   NVARCHAR (200) NULL,
    [ContactPerson]  NVARCHAR (200) NULL,
    [PhoneNumber]    NVARCHAR (100) NULL,
    [EmailAddress]   NVARCHAR (100) NULL,
    [CreatedAt]      DATETIME       NULL,
    [LastModifiedAt] DATETIME       NULL,
    [DeletedAt]      DATETIME       NULL,
    [Active]         BIT            NOT NULL,
    CONSTRAINT [PK_Customers] PRIMARY KEY CLUSTERED ([Id_Customer] ASC)
);

