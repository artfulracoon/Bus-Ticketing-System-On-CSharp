CREATE TABLE [dbo].[Yolcu] (
    [Isim]      NCHAR (50) NOT NULL,
    [Soyisim]   NCHAR (50) NOT NULL,
    [TC_Kimlik] NCHAR (50) NOT NULL,
    [Eposta]    NCHAR (50) NOT NULL,
    [Koltuk_No] NCHAR (10) NOT NULL,
    [Sefer_No]  NCHAR (50) NOT NULL,
    [Bilet_No]  NCHAR (50) NOT NULL,
    CONSTRAINT [PK_Table] PRIMARY KEY CLUSTERED ([Bilet_No] ASC),
    CONSTRAINT [UC_Yolcu] UNIQUE NONCLUSTERED ([Sefer_No] ASC, [Koltuk_No] ASC),
    CONSTRAINT [FK_SeferNo] FOREIGN KEY ([Sefer_No]) REFERENCES [dbo].[Sefer] ([Sefer_No])
);

