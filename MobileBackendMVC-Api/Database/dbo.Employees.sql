CREATE TABLE [dbo].[Employees] (
    [Id_Employee]       INT             IDENTITY (1, 1) NOT NULL,
    [Id_Contractor]     INT             NULL,
    [Id_Department]     INT             NULL,
    [FirstName]         NVARCHAR (100)  NULL,
    [LastName]          NVARCHAR (100)  NULL,
    [PhoneNumber]       NVARCHAR (100)  NULL,
    [EmailAddress]      NVARCHAR (100)  NULL,
    [EmployeeReference] NVARCHAR (2000) NULL,
    [DeletedAt]         DATETIME        NULL,
    [Active]            BIT             NOT NULL,
    [EmployeePicture]   VARBINARY (MAX) NULL,
    PRIMARY KEY CLUSTERED ([Id_Employee] ASC),
    CONSTRAINT [FK_Employees_Contractors] FOREIGN KEY ([Id_Contractor]) REFERENCES [dbo].[Contractors] ([Id_Contractor]),
    CONSTRAINT [FK_Employees_Departments] FOREIGN KEY ([Id_Department]) REFERENCES [dbo].[Departments] ([Id_Department])
);

