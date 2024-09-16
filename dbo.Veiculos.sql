CREATE TABLE [dbo].[Veiculos] (
    [Id]            INT            IDENTITY (1, 1) NOT NULL,
    [Nome]          CHAR(10)            NOT NULL,
    [Placa]         NVARCHAR (MAX) NOT NULL,
    [AnoFabricacao] INT            NOT NULL,
    [AnoModelo]     INT            NOT NULL,
    CONSTRAINT [PK_Veiculos] PRIMARY KEY CLUSTERED ([Id] ASC)
);

