CREATE TABLE [dbo].[GearStat]
(
	[Id_Gear] INT NOT NULL, 
    [Stat_Name] VARCHAR(50) NOT NULL, 
    [Amount] INT NOT NULL, 
    PRIMARY KEY ([Id_Gear], [Stat_Name]), 
    CONSTRAINT [FK_GearStat_Gear] FOREIGN KEY ([Id_Gear]) REFERENCES [Gear]([Id]), 
    CONSTRAINT [FK_GearStat_Stat] FOREIGN KEY ([Stat_Name]) REFERENCES [Stat]([Name])
)
