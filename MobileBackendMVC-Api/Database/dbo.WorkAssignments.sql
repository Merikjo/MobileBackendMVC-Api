CREATE TABLE [dbo].[WorkAssignments] (
    [Id_WorkAssignment] INT             IDENTITY (1, 1) NOT NULL,
    [Id_Customer]       INT             NULL,
    [Title]             NVARCHAR (200)  NULL,
    [Description]       NVARCHAR (2000) NULL,
    [Deadline]          DATETIME        NULL,
    [InProgress]        BIT             NOT NULL,
    [InProgressAt]      DATETIME        NULL,
    [Completed]         BIT             NOT NULL,
    [CompletedAt]       DATETIME        NULL,
    [CreatedAt]         DATETIME        NULL,
    [LastModifiedAt]    DATETIME        NULL,
    [DeletedAt]         DATETIME        NULL,
    [Active]            BIT             NOT NULL,
    CONSTRAINT [PK_WorkAssignments] PRIMARY KEY CLUSTERED ([Id_WorkAssignment] ASC),
    CONSTRAINT [FK_WorkAssignments_Customers] FOREIGN KEY ([Id_Customer]) REFERENCES [dbo].[Customers] ([Id_Customer])
);

