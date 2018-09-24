-- Script Date: 9/22/2018 12:24 PM  - ErikEJ.SqlCeScripting version 3.5.2.75
CREATE TABLE [EnabledFeatures] (
  [Id] INTEGER NOT NULL
, [FeatureName] TEXT NOT NULL
, [Enabled] INTEGER DEFAULT 0 NOT NULL
, CONSTRAINT [PK_EnabledFeatures] PRIMARY KEY ([Id])
);
