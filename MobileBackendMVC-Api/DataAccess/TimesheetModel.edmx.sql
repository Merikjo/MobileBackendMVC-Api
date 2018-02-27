
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/06/2017 13:34:40
-- Generated from EDMX file: E:\Visual Studio 2017\Projects\MobileBackendMVC-Api2\MobileBackendMVC-Api\DataAccess\TimesheetModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [JohaMeriSQL5];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Employees_Contractors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Contractors];
GO
IF OBJECT_ID(N'[dbo].[FK_Employees_Departments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Employees] DROP CONSTRAINT [FK_Employees_Departments];
GO
IF OBJECT_ID(N'[dbo].[FK_PinCodes_Contractors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PinCodes] DROP CONSTRAINT [FK_PinCodes_Contractors];
GO
IF OBJECT_ID(N'[dbo].[FK_PinCodes_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PinCodes] DROP CONSTRAINT [FK_PinCodes_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_PinCodes_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[PinCodes] DROP CONSTRAINT [FK_PinCodes_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheets_Contractors]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheets] DROP CONSTRAINT [FK_Timesheets_Contractors];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheets_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheets] DROP CONSTRAINT [FK_Timesheets_Customers];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheets_Employees]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheets] DROP CONSTRAINT [FK_Timesheets_Employees];
GO
IF OBJECT_ID(N'[dbo].[FK_Timesheets_WorkAssignments]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Timesheets] DROP CONSTRAINT [FK_Timesheets_WorkAssignments];
GO
IF OBJECT_ID(N'[dbo].[FK_WorkAssignments_Customers]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WorkAssignments] DROP CONSTRAINT [FK_WorkAssignments_Customers];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Contractors]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Contractors];
GO
IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Departments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Departments];
GO
IF OBJECT_ID(N'[dbo].[Employees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employees];
GO
IF OBJECT_ID(N'[dbo].[PinCodes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PinCodes];
GO
IF OBJECT_ID(N'[dbo].[Timesheets]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Timesheets];
GO
IF OBJECT_ID(N'[dbo].[WorkAssignments]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WorkAssignments];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Contractors'
CREATE TABLE [dbo].[Contractors] (
    [Id_Contractor] int IDENTITY(1,1) NOT NULL,
    [CompanyName] nvarchar(200)  NULL,
    [ContactPerson] nvarchar(200)  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(100)  NULL,
    [VatId] nvarchar(50)  NULL,
    [HourlyRate] float  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id_Customer] int IDENTITY(1,1) NOT NULL,
    [CustomerName] nvarchar(200)  NULL,
    [ContactPerson] nvarchar(200)  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(100)  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Departments'
CREATE TABLE [dbo].[Departments] (
    [Id_Department] int IDENTITY(1,1) NOT NULL,
    [DepartmentName] nvarchar(100)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id_Employee] int IDENTITY(1,1) NOT NULL,
    [Id_Contractor] int  NULL,
    [Id_Department] int  NULL,
    [FirstName] nvarchar(100)  NULL,
    [LastName] nvarchar(100)  NULL,
    [PhoneNumber] nvarchar(100)  NULL,
    [EmailAddress] nvarchar(100)  NULL,
    [EmployeeReference] nvarchar(2000)  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL,
    [EmployeePicture] varbinary(max)  NULL
);
GO

-- Creating table 'PinCodes'
CREATE TABLE [dbo].[PinCodes] (
    [Id_PinCode] int IDENTITY(1,1) NOT NULL,
    [PinCode] nvarchar(100)  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL,
    [Id_Employee] int  NULL,
    [Id_Customer] int  NULL,
    [Id_Contractor] int  NULL
);
GO

-- Creating table 'Timesheets'
CREATE TABLE [dbo].[Timesheets] (
    [Id_Timesheet] int IDENTITY(1,1) NOT NULL,
    [Id_Customer] int  NULL,
    [Id_Contractor] int  NULL,
    [Id_Employee] int  NULL,
    [Id_WorkAssignment] int  NULL,
    [StartTime] datetime  NULL,
    [StopTime] datetime  NULL,
    [Comments] nvarchar(2000)  NULL,
    [WorkComplete] bit  NOT NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'WorkAssignments'
CREATE TABLE [dbo].[WorkAssignments] (
    [Id_WorkAssignment] int IDENTITY(1,1) NOT NULL,
    [Id_Customer] int  NULL,
    [Title] nvarchar(200)  NULL,
    [Description] nvarchar(2000)  NULL,
    [Deadline] datetime  NULL,
    [InProgress] bit  NOT NULL,
    [InProgressAt] datetime  NULL,
    [Completed] bit  NOT NULL,
    [CompletedAt] datetime  NULL,
    [CreatedAt] datetime  NULL,
    [LastModifiedAt] datetime  NULL,
    [DeletedAt] datetime  NULL,
    [Active] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id_Contractor] in table 'Contractors'
ALTER TABLE [dbo].[Contractors]
ADD CONSTRAINT [PK_Contractors]
    PRIMARY KEY CLUSTERED ([Id_Contractor] ASC);
GO

-- Creating primary key on [Id_Customer] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id_Customer] ASC);
GO

-- Creating primary key on [Id_Department] in table 'Departments'
ALTER TABLE [dbo].[Departments]
ADD CONSTRAINT [PK_Departments]
    PRIMARY KEY CLUSTERED ([Id_Department] ASC);
GO

-- Creating primary key on [Id_Employee] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id_Employee] ASC);
GO

-- Creating primary key on [Id_PinCode] in table 'PinCodes'
ALTER TABLE [dbo].[PinCodes]
ADD CONSTRAINT [PK_PinCodes]
    PRIMARY KEY CLUSTERED ([Id_PinCode] ASC);
GO

-- Creating primary key on [Id_Timesheet] in table 'Timesheets'
ALTER TABLE [dbo].[Timesheets]
ADD CONSTRAINT [PK_Timesheets]
    PRIMARY KEY CLUSTERED ([Id_Timesheet] ASC);
GO

-- Creating primary key on [Id_WorkAssignment] in table 'WorkAssignments'
ALTER TABLE [dbo].[WorkAssignments]
ADD CONSTRAINT [PK_WorkAssignments]
    PRIMARY KEY CLUSTERED ([Id_WorkAssignment] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Id_Contractor] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_Contractors]
    FOREIGN KEY ([Id_Contractor])
    REFERENCES [dbo].[Contractors]
        ([Id_Contractor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Employees_Contractors'
CREATE INDEX [IX_FK_Employees_Contractors]
ON [dbo].[Employees]
    ([Id_Contractor]);
GO

-- Creating foreign key on [Id_Contractor] in table 'PinCodes'
ALTER TABLE [dbo].[PinCodes]
ADD CONSTRAINT [FK_PinCodes_Contractors]
    FOREIGN KEY ([Id_Contractor])
    REFERENCES [dbo].[Contractors]
        ([Id_Contractor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PinCodes_Contractors'
CREATE INDEX [IX_FK_PinCodes_Contractors]
ON [dbo].[PinCodes]
    ([Id_Contractor]);
GO

-- Creating foreign key on [Id_Contractor] in table 'Timesheets'
ALTER TABLE [dbo].[Timesheets]
ADD CONSTRAINT [FK_Timesheets_Contractors]
    FOREIGN KEY ([Id_Contractor])
    REFERENCES [dbo].[Contractors]
        ([Id_Contractor])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheets_Contractors'
CREATE INDEX [IX_FK_Timesheets_Contractors]
ON [dbo].[Timesheets]
    ([Id_Contractor]);
GO

-- Creating foreign key on [Id_Customer] in table 'PinCodes'
ALTER TABLE [dbo].[PinCodes]
ADD CONSTRAINT [FK_PinCodes_Customers]
    FOREIGN KEY ([Id_Customer])
    REFERENCES [dbo].[Customers]
        ([Id_Customer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PinCodes_Customers'
CREATE INDEX [IX_FK_PinCodes_Customers]
ON [dbo].[PinCodes]
    ([Id_Customer]);
GO

-- Creating foreign key on [Id_Customer] in table 'Timesheets'
ALTER TABLE [dbo].[Timesheets]
ADD CONSTRAINT [FK_Timesheets_Customers]
    FOREIGN KEY ([Id_Customer])
    REFERENCES [dbo].[Customers]
        ([Id_Customer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheets_Customers'
CREATE INDEX [IX_FK_Timesheets_Customers]
ON [dbo].[Timesheets]
    ([Id_Customer]);
GO

-- Creating foreign key on [Id_Customer] in table 'WorkAssignments'
ALTER TABLE [dbo].[WorkAssignments]
ADD CONSTRAINT [FK_WorkAssignments_Customers]
    FOREIGN KEY ([Id_Customer])
    REFERENCES [dbo].[Customers]
        ([Id_Customer])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WorkAssignments_Customers'
CREATE INDEX [IX_FK_WorkAssignments_Customers]
ON [dbo].[WorkAssignments]
    ([Id_Customer]);
GO

-- Creating foreign key on [Id_Department] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [FK_Employees_Departments]
    FOREIGN KEY ([Id_Department])
    REFERENCES [dbo].[Departments]
        ([Id_Department])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Employees_Departments'
CREATE INDEX [IX_FK_Employees_Departments]
ON [dbo].[Employees]
    ([Id_Department]);
GO

-- Creating foreign key on [Id_Employee] in table 'PinCodes'
ALTER TABLE [dbo].[PinCodes]
ADD CONSTRAINT [FK_PinCodes_Employees]
    FOREIGN KEY ([Id_Employee])
    REFERENCES [dbo].[Employees]
        ([Id_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PinCodes_Employees'
CREATE INDEX [IX_FK_PinCodes_Employees]
ON [dbo].[PinCodes]
    ([Id_Employee]);
GO

-- Creating foreign key on [Id_Employee] in table 'Timesheets'
ALTER TABLE [dbo].[Timesheets]
ADD CONSTRAINT [FK_Timesheets_Employees]
    FOREIGN KEY ([Id_Employee])
    REFERENCES [dbo].[Employees]
        ([Id_Employee])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheets_Employees'
CREATE INDEX [IX_FK_Timesheets_Employees]
ON [dbo].[Timesheets]
    ([Id_Employee]);
GO

-- Creating foreign key on [Id_WorkAssignment] in table 'Timesheets'
ALTER TABLE [dbo].[Timesheets]
ADD CONSTRAINT [FK_Timesheets_WorkAssignments]
    FOREIGN KEY ([Id_WorkAssignment])
    REFERENCES [dbo].[WorkAssignments]
        ([Id_WorkAssignment])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Timesheets_WorkAssignments'
CREATE INDEX [IX_FK_Timesheets_WorkAssignments]
ON [dbo].[Timesheets]
    ([Id_WorkAssignment]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------