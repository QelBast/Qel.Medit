CREATE TABLE "Users"
(
    "Id" uuid PRIMARY KEY not null,
    "UserName" text UNIQUE not null,
    "PasswordHash" text UNIQUE not null,
    "CreationDateTime" timestamp with time zone,
    "ModifyDateTime" timestamp with time zone,
    "IsDeleted" BOOLEAN NOT NULL DEFAULT false
)