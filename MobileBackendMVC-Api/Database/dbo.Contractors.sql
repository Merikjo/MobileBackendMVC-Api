CREATE TABLE [dbo].[Contractors] (
    [Id_Contractor]  INT            IDENTITY (1, 1) NOT NULL,
    [CompanyName]    NVARCHAR (200) NULL,
    [ContactPerson]  NVARCHAR (200) NULL,
    [PhoneNumber]    NVARCHAR (100) NULL,
    [EmailAddress]   NVARCHAR (100) NULL,
    [VatId]          NVARCHAR (50)  NULL,
    [HourlyRate]     FLOAT (53)     NULL,
    [CreatedAt]      DATETIME       NULL,
    [LastModifiedAt] DATETIME       NULL,
    [DeletedAt]      DATETIME       NULL,
    [Active]         BIT            NOT NULL,
    CONSTRAINT [PK_Contractor] PRIMARY KEY CLUSTERED ([Id_Contractor] ASC)
);

