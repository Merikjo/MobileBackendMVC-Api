CREATE TABLE [dbo].[PinCodes] (
    [Id_PinCode]     INT            IDENTITY (1, 1) NOT NULL,
    [PinCode]        NVARCHAR (100) NULL,
    [CreatedAt]      DATETIME       NULL,
    [LastModifiedAt] DATETIME       NULL,
    [DeletedAt]      DATETIME       NULL,
    [Active]         BIT            NOT NULL,
    [Id_Employee]    INT            NULL,
    [Id_Customer]    INT            NULL,
    [Id_Contractor]  INT            NULL,
    PRIMARY KEY CLUSTERED ([Id_PinCode] ASC),
    CONSTRAINT [FK_PinCodes_Customers] FOREIGN KEY ([Id_Customer]) REFERENCES [dbo].[Customers] ([Id_Customer]),
    CONSTRAINT [FK_PinCodes_Contractors] FOREIGN KEY ([Id_Contractor]) REFERENCES [dbo].[Contractors] ([Id_Contractor]),
    CONSTRAINT [FK_PinCodes_Employees] FOREIGN KEY ([Id_Employee]) REFERENCES [dbo].[Employees] ([Id_Employee])
);

