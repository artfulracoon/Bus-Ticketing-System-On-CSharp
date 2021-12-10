CREATE TABLE [dbo].[Sefer] (
    [Nereden]  NCHAR (50) NOT NULL,
    [Nereye]   NCHAR (50) NOT NULL,
    [Tarih]    NCHAR (50) NOT NULL,
    [Saat]     NCHAR (50) NOT NULL,
    [Ucret]    NCHAR (50) NOT NULL,
    [Sefer_No] NCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Sefer_No] ASC),
    UNIQUE NONCLUSTERED ([Sefer_No] ASC)
);

