
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/27/2021 02:00:24
-- Generated from EDMX file: H:\Work\R6Tracker\R6T.Model\R6TrackerModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [R6Tracker];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GameStats_MatchType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameStats] DROP CONSTRAINT [FK_GameStats_MatchType];
GO
IF OBJECT_ID(N'[dbo].[FK_GameStats_Player]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[GameStats] DROP CONSTRAINT [FK_GameStats_Player];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[GameStats]', 'U') IS NOT NULL
    DROP TABLE [dbo].[GameStats];
GO
IF OBJECT_ID(N'[dbo].[MatchType]', 'U') IS NOT NULL
    DROP TABLE [dbo].[MatchType];
GO
IF OBJECT_ID(N'[dbo].[Player]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Player];
GO
IF OBJECT_ID(N'[dbo].[sysdiagrams]', 'U') IS NOT NULL
    DROP TABLE [dbo].[sysdiagrams];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'GameStats'
CREATE TABLE [dbo].[GameStats] (
    [GameStatId] uniqueidentifier  NOT NULL,
    [PlayerId] uniqueidentifier  NULL,
    [MatchTypeId] int  NULL,
    [PlayerLevel] int  NULL,
    [MatchesPlayed] int  NULL,
    [Wins] int  NULL,
    [Losses] int  NULL,
    [Kills] int  NULL,
    [Deaths] int  NULL,
    [Headshots] int  NULL,
    [MeleeKills] int  NULL,
    [BlindKills] int  NULL,
    [KD] decimal(4,2)  NULL,
    [TimePlayed] varchar(20)  NULL,
    [TotalXp] varchar(50)  NULL,
    [CreatedDate] datetime  NULL
);
GO

-- Creating table 'MatchTypes'
CREATE TABLE [dbo].[MatchTypes] (
    [MatchTypeId] int  NOT NULL,
    [MatchTypeName] varchar(20)  NULL
);
GO

-- Creating table 'Players'
CREATE TABLE [dbo].[Players] (
    [PlayerId] uniqueidentifier  NOT NULL,
    [PlayerName] nvarchar(100)  NULL,
    [Alias] nvarchar(100)  NULL,
    [IsActive] bit  NULL
);
GO

-- Creating table 'sysdiagrams'
CREATE TABLE [dbo].[sysdiagrams] (
    [name] nvarchar(128)  NOT NULL,
    [principal_id] int  NOT NULL,
    [diagram_id] int IDENTITY(1,1) NOT NULL,
    [version] int  NULL,
    [definition] varbinary(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [GameStatId] in table 'GameStats'
ALTER TABLE [dbo].[GameStats]
ADD CONSTRAINT [PK_GameStats]
    PRIMARY KEY CLUSTERED ([GameStatId] ASC);
GO

-- Creating primary key on [MatchTypeId] in table 'MatchTypes'
ALTER TABLE [dbo].[MatchTypes]
ADD CONSTRAINT [PK_MatchTypes]
    PRIMARY KEY CLUSTERED ([MatchTypeId] ASC);
GO

-- Creating primary key on [PlayerId] in table 'Players'
ALTER TABLE [dbo].[Players]
ADD CONSTRAINT [PK_Players]
    PRIMARY KEY CLUSTERED ([PlayerId] ASC);
GO

-- Creating primary key on [diagram_id] in table 'sysdiagrams'
ALTER TABLE [dbo].[sysdiagrams]
ADD CONSTRAINT [PK_sysdiagrams]
    PRIMARY KEY CLUSTERED ([diagram_id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [MatchTypeId] in table 'GameStats'
ALTER TABLE [dbo].[GameStats]
ADD CONSTRAINT [FK_GameStats_MatchType]
    FOREIGN KEY ([MatchTypeId])
    REFERENCES [dbo].[MatchTypes]
        ([MatchTypeId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameStats_MatchType'
CREATE INDEX [IX_FK_GameStats_MatchType]
ON [dbo].[GameStats]
    ([MatchTypeId]);
GO

-- Creating foreign key on [PlayerId] in table 'GameStats'
ALTER TABLE [dbo].[GameStats]
ADD CONSTRAINT [FK_GameStats_Player]
    FOREIGN KEY ([PlayerId])
    REFERENCES [dbo].[Players]
        ([PlayerId])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GameStats_Player'
CREATE INDEX [IX_FK_GameStats_Player]
ON [dbo].[GameStats]
    ([PlayerId]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------