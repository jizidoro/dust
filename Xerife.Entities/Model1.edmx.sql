
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 02/15/2019 22:02:38
-- Generated from EDMX file: D:\Watcher\Xerife.Entities\Model1.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [ProjetoXerife];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_ProjetoTarefaChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TarefaChannelSet] DROP CONSTRAINT [FK_ProjetoTarefaChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_UsuarioPerfil]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UsuarioSet] DROP CONSTRAINT [FK_UsuarioPerfil];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ProjetoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProjetoSet];
GO
IF OBJECT_ID(N'[dbo].[UsuarioVpnSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioVpnSet];
GO
IF OBJECT_ID(N'[dbo].[VpnHistoricoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[VpnHistoricoSet];
GO
IF OBJECT_ID(N'[dbo].[TarefaChannelSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TarefaChannelSet];
GO
IF OBJECT_ID(N'[dbo].[UsuarioSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UsuarioSet];
GO
IF OBJECT_ID(N'[dbo].[PerfilSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PerfilSet];
GO
IF OBJECT_ID(N'[dbo].[LogIntegracaoSet]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogIntegracaoSet];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ProjetoSet'
CREATE TABLE [dbo].[ProjetoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [TfsUrl] nvarchar(max)  NOT NULL,
    [TfsCollection] nvarchar(max)  NOT NULL,
    [TfsProject] nvarchar(max)  NOT NULL,
    [TfsTeam] nvarchar(max)  NOT NULL,
    [IdChannel] int  NOT NULL,
    [Status] bit  NOT NULL,
    [TfsAreaPath] nvarchar(max)  NOT NULL,
    [UsuarioGerente] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'UsuarioVpnSet'
CREATE TABLE [dbo].[UsuarioVpnSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Inicio] datetime  NOT NULL,
    [Fim] datetime  NOT NULL,
    [Nome] nvarchar(max)  NULL
);
GO

-- Creating table 'VpnHistoricoSet'
CREATE TABLE [dbo].[VpnHistoricoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Inicio] datetime  NULL,
    [Fim] datetime  NULL,
    [DataAcao] datetime  NOT NULL,
    [Acao] int  NOT NULL,
    [Responsavel] nvarchar(max)  NOT NULL,
    [Usuario] nvarchar(max)  NOT NULL,
    [Justificativa] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'TarefaChannelSet'
CREATE TABLE [dbo].[TarefaChannelSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [IdTarefaChannel] int  NOT NULL,
    [IdIterationTfs] nvarchar(max)  NULL,
    [Projeto_Id] int  NOT NULL
);
GO

-- Creating table 'UsuarioSet'
CREATE TABLE [dbo].[UsuarioSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Login] nvarchar(max)  NOT NULL,
    [Perfil_Id] int  NOT NULL
);
GO

-- Creating table 'PerfilSet'
CREATE TABLE [dbo].[PerfilSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'LogIntegracaoSet'
CREATE TABLE [dbo].[LogIntegracaoSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Projeto] nvarchar(max)  NOT NULL,
    [Data] datetime  NOT NULL,
    [Registro] nvarchar(max)  NOT NULL,
    [Status] bit  NOT NULL
);
GO

-- Creating table 'ParametroSistemaSet'
CREATE TABLE [dbo].[ParametroSistemaSet] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Nome] nvarchar(max)  NOT NULL,
    [Descricao] nvarchar(max)  NULL,
    [Valor] nvarchar(max)  NOT NULL,
    [Sigla] nvarchar(max)  NOT NULL,
    [Perfil_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ProjetoSet'
ALTER TABLE [dbo].[ProjetoSet]
ADD CONSTRAINT [PK_ProjetoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsuarioVpnSet'
ALTER TABLE [dbo].[UsuarioVpnSet]
ADD CONSTRAINT [PK_UsuarioVpnSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'VpnHistoricoSet'
ALTER TABLE [dbo].[VpnHistoricoSet]
ADD CONSTRAINT [PK_VpnHistoricoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'TarefaChannelSet'
ALTER TABLE [dbo].[TarefaChannelSet]
ADD CONSTRAINT [PK_TarefaChannelSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [PK_UsuarioSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PerfilSet'
ALTER TABLE [dbo].[PerfilSet]
ADD CONSTRAINT [PK_PerfilSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LogIntegracaoSet'
ALTER TABLE [dbo].[LogIntegracaoSet]
ADD CONSTRAINT [PK_LogIntegracaoSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ParametroSistemaSet'
ALTER TABLE [dbo].[ParametroSistemaSet]
ADD CONSTRAINT [PK_ParametroSistemaSet]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Projeto_Id] in table 'TarefaChannelSet'
ALTER TABLE [dbo].[TarefaChannelSet]
ADD CONSTRAINT [FK_ProjetoTarefaChannel]
    FOREIGN KEY ([Projeto_Id])
    REFERENCES [dbo].[ProjetoSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ProjetoTarefaChannel'
CREATE INDEX [IX_FK_ProjetoTarefaChannel]
ON [dbo].[TarefaChannelSet]
    ([Projeto_Id]);
GO

-- Creating foreign key on [Perfil_Id] in table 'UsuarioSet'
ALTER TABLE [dbo].[UsuarioSet]
ADD CONSTRAINT [FK_UsuarioPerfil]
    FOREIGN KEY ([Perfil_Id])
    REFERENCES [dbo].[PerfilSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UsuarioPerfil'
CREATE INDEX [IX_FK_UsuarioPerfil]
ON [dbo].[UsuarioSet]
    ([Perfil_Id]);
GO

-- Creating foreign key on [Perfil_Id] in table 'ParametroSistemaSet'
ALTER TABLE [dbo].[ParametroSistemaSet]
ADD CONSTRAINT [FK_PerfilParametroSistema]
    FOREIGN KEY ([Perfil_Id])
    REFERENCES [dbo].[PerfilSet]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PerfilParametroSistema'
CREATE INDEX [IX_FK_PerfilParametroSistema]
ON [dbo].[ParametroSistemaSet]
    ([Perfil_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------