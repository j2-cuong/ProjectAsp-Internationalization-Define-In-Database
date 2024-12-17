CREATE TABLE [dbo].[LanguageConfig](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[LanguageCode] [nvarchar](10) NOT NULL,
	[LanguageName] [nvarchar](50) NOT NULL,
	[LanguageIcon] [nvarchar](50) NOT NULL,
	[IsActive] [bit] DEFAULT 1,
	[DisplayOrder] [int] NULL,
	[IsChangeData] [bit] DEFAULT 0
)

CREATE TABLE [dbo].[TranslationKey](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[KeyName] [nvarchar](100) NOT NULL,
	[KeyGroup] [nvarchar](50) NULL,
	[Description] [nvarchar](255) NULL,
)


CREATE TABLE [dbo].[Translation](
	[Id] [uniqueidentifier] NOT NULL PRIMARY KEY,
	[TranslationKeyId] [uniqueidentifier] NOT NULL,
	[LanguageId] [uniqueidentifier] NOT NULL,
	[TranslatedText] [nvarchar](max) NOT NULL,
)

GO
CREATE PROCEDURE [dbo].[GetTranslations]
    @LanguageCode NVARCHAR(10),
    @Keys NVARCHAR(255)
AS
BEGIN
    SELECT 
        tk.KeyName,
        t.TranslatedText
    FROM TranslationKey tk
    LEFT JOIN Translation t ON t.TranslationKeyId = tk.Id
    LEFT JOIN LanguageConfig l ON l.Id = t.LanguageId
    WHERE l.LanguageCode = @LanguageCode AND tk.KeyName = @Keys
END

GO
-- Thêm ngôn ngữ vào bảng 
INSERT [dbo].[LanguageConfig] 
([Id], [LanguageCode], [LanguageName], [LanguageIcon], [IsActive], [DisplayOrder], [IsChangeData]) 
VALUES (N'6672c830-d8fa-449e-9c78-0b7b280e195d', N'la_LA', N'ພາສາລາວ', N'flag-icon flag-icon-la', 1, 3, 0)

INSERT [dbo].[LanguageConfig] 
([Id], [LanguageCode], [LanguageName], [LanguageIcon], [IsActive], [DisplayOrder], [IsChangeData]) 
VALUES (N'4b51200e-9ae8-4505-8ac5-0f535d4e77a2', N'en_US', N'English', N'flag-icon flag-icon-us', 1, 1, 0)

INSERT [dbo].[LanguageConfig] 
([Id], [LanguageCode], [LanguageName], [LanguageIcon], [IsActive], [DisplayOrder], [IsChangeData]) 
VALUES (N'2d3a2bb3-d643-4ec5-8b4b-18a1bc6507ef', N'vi_VN', N'Tiếng Việt', N'flag-icon flag-icon-vn', 1, 2, 0)

INSERT [dbo].[LanguageConfig] 
([Id], [LanguageCode], [LanguageName], [LanguageIcon], [IsActive], [DisplayOrder], [IsChangeData]) 
VALUES (N'7451d210-804f-4e32-81e8-268caca024ce', N'hi_IN', N'हिंदी भाषा', N'flag-icon flag-icon-in', 1, 6, 0)

INSERT [dbo].[LanguageConfig] 
([Id], [LanguageCode], [LanguageName], [LanguageIcon], [IsActive], [DisplayOrder], [IsChangeData]) 
VALUES (N'c40352b6-4663-4a36-ab5c-287b9da55bf6', N'zh_CN', N'中文', N'flag-icon flag-icon-cn', 1, 4, 0)

INSERT [dbo].[LanguageConfig] 
([Id], [LanguageCode], [LanguageName], [LanguageIcon], [IsActive], [DisplayOrder], [IsChangeData]) 
VALUES (N'64552140-2571-43a5-a7b2-dbca341ad9ac', N'ja_JP', N'日本語', N'flag-icon flag-icon-jp', 1, 5, 0)

--Thêm keys để tìm bản dịch

INSERT [dbo].[TranslationKey] 
([Id], [KeyName], [KeyGroup], [Description]) 
VALUES (N'77b49017-1196-4033-921d-8be10bdd53c6', N'label.email', N'ContactInfo', N'Email Label')

INSERT [dbo].[TranslationKey] 
([Id], [KeyName], [KeyGroup], [Description]) 
VALUES (N'6748d35f-ebcc-4d19-884e-ab9e9bb88461', N'label.lastName', N'PersonalInfo', N'Last Name Label')

INSERT [dbo].[TranslationKey] 
([Id], [KeyName], [KeyGroup], [Description]) 
VALUES (N'2ddf7083-323a-481d-99d1-ad07ddff8991', N'label.firstName', N'PersonalInfo', N'First Name Label')

INSERT [dbo].[TranslationKey] 
([Id], [KeyName], [KeyGroup], [Description]) 
VALUES (N'360a2753-6209-4bf7-a6db-adf3505ae686', N'label.userInfo', N'FormTitle', N'User Information Form Title')

INSERT [dbo].[TranslationKey] 
([Id], [KeyName], [KeyGroup], [Description]) 
VALUES (N'df4090ab-a76c-4f8f-93cf-e1f842c5970f', N'button.submit', N'Common', N'Submit Button Text')

INSERT [dbo].[TranslationKey] 
([Id], [KeyName], [KeyGroup], [Description]) 
VALUES (N'fb4c0de4-a4c7-46d0-b491-f41dd84b12a8', N'placeholder.firstName', N'PersonalInfo', N'First Name Placeholder')

-- Thêm bản dịch ( tìm kiếm bằng Id ngôn ngữ và Id Key ) 

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'900f2e8a-bbdb-4038-a375-1245b52f7e6d', N'360a2753-6209-4bf7-a6db-adf3505ae686', N'6672c830-d8fa-449e-9c78-0b7b280e195d', N'Tiếng Lào')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'930b59ee-2764-4e0a-895e-1a02c38eb273', N'2ddf7083-323a-481d-99d1-ad07ddff8991', N'4b51200e-9ae8-4505-8ac5-0f535d4e77a2', N'First Name')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'bda756b6-5765-40f5-81e7-4564fb7dcdeb', N'360a2753-6209-4bf7-a6db-adf3505ae686', N'2d3a2bb3-d643-4ec5-8b4b-18a1bc6507ef', N'Thông tin người dùng')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'5a3c16d9-7821-4cef-9cfd-76307d89b862', N'360a2753-6209-4bf7-a6db-adf3505ae686', N'c40352b6-4663-4a36-ab5c-287b9da55bf6', N'用户信息')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'fd031bae-66fa-47b8-88e0-8a6c12f28156', N'360a2753-6209-4bf7-a6db-adf3505ae686', N'4b51200e-9ae8-4505-8ac5-0f535d4e77a2', N'User Information')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'03b28f93-3090-4c72-b2a6-a14ad81aead4', N'360a2753-6209-4bf7-a6db-adf3505ae686', N'7451d210-804f-4e32-81e8-268caca024ce', N'उपयोगकर्ता')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'061d69d9-f143-44cc-89c6-a21846abe2a0', N'2ddf7083-323a-481d-99d1-ad07ddff8991', N'2d3a2bb3-d643-4ec5-8b4b-18a1bc6507ef', N'Hehe')

INSERT [dbo].[Translation] 
([Id], [TranslationKeyId], [LanguageId], [TranslatedText]) 
VALUES (N'922a6376-2de9-4c9e-aca7-ca731bdd9922', N'360a2753-6209-4bf7-a6db-adf3505ae686', N'64552140-2571-43a5-a7b2-dbca341ad9ac', N'Haha')

