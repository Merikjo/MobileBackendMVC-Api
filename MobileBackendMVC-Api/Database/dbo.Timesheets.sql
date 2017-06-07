CREATE TABLE [dbo].[Timesheets] (
    [Id_Timesheet]      INT             IDENTITY (1, 1) NOT NULL,
    [Id_Customer]       INT             NULL,
    [Id_Contractor]     INT             NULL,
    [Id_Employee]       INT             NULL,
    [Id_WorkAssignment] INT             NULL,
    [StartTime]         DATETIME        NULL,
    [StopTime]          DATETIME        NULL,
    [Comments]          NVARCHAR (2000) NULL,
    [WorkComplete]      BIT             NOT NULL,
    [CreatedAt]         DATETIME        NULL,
    [LastModifiedAt]    DATETIME        NULL,
    [DeletedAt]         DATETIME        NULL,
    [Active]            BIT             NOT NULL,
    CONSTRAINT [PK_Timesheets] PRIMARY KEY CLUSTERED ([Id_Timesheet] ASC),
    CONSTRAINT [FK_Timesheets_Contractors] FOREIGN KEY ([Id_Contractor]) REFERENCES [dbo].[Contractors] ([Id_Contractor]),
    CONSTRAINT [FK_Timesheets_Customers] FOREIGN KEY ([Id_Customer]) REFERENCES [dbo].[Customers] ([Id_Customer]),
    CONSTRAINT [FK_Timesheets_WorkAssignments] FOREIGN KEY ([Id_WorkAssignment]) REFERENCES [dbo].[WorkAssignments] ([Id_WorkAssignment]),
    CONSTRAINT [FK_Timesheets_Employees] FOREIGN KEY ([Id_Employee]) REFERENCES [dbo].[Employees] ([Id_Employee])
);

