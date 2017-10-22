CREATE TABLE [dbo].[ProductOption] (
    [Id]          UNIQUEIDENTIFIER NOT NULL,
    [ProductId]   UNIQUEIDENTIFIER NOT NULL,
    [Name]        NVARCHAR (100)   NOT NULL,
    [Description] NVARCHAR (500)   NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC), 
    CONSTRAINT [FK_ProductOption_ToProduct] FOREIGN KEY ([ProductId]) REFERENCES [Product]([Id])
);

