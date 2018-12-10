
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 12/05/2018 20:04:23
-- Generated from EDMX file: C:\Users\Lukas\source\repos\VirtualLibrarityV2.0\VirtualLibAPI\LibraryModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LibraryDB];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'UserSet'
CREATE TABLE [dbo].[UserSet] (
    [Id] bigint  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Password] nvarchar(max)  NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Surname] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'CopySet'
CREATE TABLE [dbo].[CopySet] (
    [Id] bigint IDENTITY(1,1) NOT NULL,
    [ReturnDate] datetime  NULL,
    [BookQRCode] nvarchar(max)  NOT NULL,
    [UserId] bigint  NULL
);
GO

-- Creating table 'BookSet'
CREATE TABLE [dbo].[BookSet] (
    [QRCode] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Title] nvarchar(max)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'UserSet'
ALTER TABLE [dbo].[UserSet]
ADD CONSTRAINT [PK_UserSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CopySet'
ALTER TABLE [dbo].[CopySet]
ADD CONSTRAINT [PK_CopySet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [QRCode] in table 'BookSet'
ALTER TABLE [dbo].[BookSet]
ADD CONSTRAINT [PK_BookSet]
    PRIMARY KEY CLUSTERED ([QRCode] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [BookQRCode] in table 'CopySet'
ALTER TABLE [dbo].[CopySet]
ADD CONSTRAINT [FK_BookCopy]
    FOREIGN KEY ([BookQRCode])
    REFERENCES [dbo].[BookSet]
        ([QRCode])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookCopy'
CREATE INDEX [IX_FK_BookCopy]
ON [dbo].[CopySet]
    ([BookQRCode]);
GO

-- Creating foreign key on [UserId] in table 'CopySet'
ALTER TABLE [dbo].[CopySet]
ADD CONSTRAINT [FK_UserCopy]
    FOREIGN KEY ([UserId])
    REFERENCES [dbo].[UserSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserCopy'
CREATE INDEX [IX_FK_UserCopy]
ON [dbo].[CopySet]
    ([UserId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------