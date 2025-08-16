IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
CREATE TABLE [Clients] (
    [ClientID] uniqueidentifier NOT NULL,
    [ClientName] nvarchar(25) NOT NULL,
    [Country] nvarchar(20) NOT NULL,
    [Email] nvarchar(50) NOT NULL,
    [Phone] nvarchar(15) NOT NULL,
    [Age] int NOT NULL,
    [Gender] nvarchar(max) NOT NULL,
    [Notes] nvarchar(max) NULL,
    CONSTRAINT [PK_Clients] PRIMARY KEY ([ClientID])
);

CREATE TABLE [Payments] (
    [PaymentId] uniqueidentifier NOT NULL,
    [PaymentName] nvarchar(50) NOT NULL,
    [PaymentDetails] nvarchar(300) NOT NULL,
    [Location] nvarchar(max) NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY ([PaymentId])
);

CREATE TABLE [Services] (
    [ServiceId] uniqueidentifier NOT NULL,
    [ServiceName] nvarchar(100) NOT NULL,
    [ServiceDescribtion] nvarchar(max) NOT NULL,
    [ServicePriceLocal] float NOT NULL,
    [ServicePriceInternational] float NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([ServiceId])
);

CREATE TABLE [Bookings] (
    [BookingId] uniqueidentifier NOT NULL,
    [ClientID] uniqueidentifier NOT NULL,
    [PaymentID] uniqueidentifier NOT NULL,
    [ServiceID] uniqueidentifier NOT NULL,
    [Status] nvarchar(max) NOT NULL,
    [Amount] float NOT NULL,
    CONSTRAINT [PK_Bookings] PRIMARY KEY ([BookingId]),
    CONSTRAINT [FK_Bookings_Clients_ClientID] FOREIGN KEY ([ClientID]) REFERENCES [Clients] ([ClientID]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Payments_PaymentID] FOREIGN KEY ([PaymentID]) REFERENCES [Payments] ([PaymentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Bookings_Services_ServiceID] FOREIGN KEY ([ServiceID]) REFERENCES [Services] ([ServiceId]) ON DELETE CASCADE
);

CREATE TABLE [Reviews] (
    [ReviewId] uniqueidentifier NOT NULL,
    [BookingID] uniqueidentifier NOT NULL,
    [ReviewMessage] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([ReviewId]),
    CONSTRAINT [FK_Reviews_Bookings_BookingID] FOREIGN KEY ([BookingID]) REFERENCES [Bookings] ([BookingId]) ON DELETE CASCADE
);

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ClientID', N'Age', N'ClientName', N'Country', N'Email', N'Gender', N'Notes', N'Phone') AND [object_id] = OBJECT_ID(N'[Clients]'))
    SET IDENTITY_INSERT [Clients] ON;
INSERT INTO [Clients] ([ClientID], [Age], [ClientName], [Country], [Email], [Gender], [Notes], [Phone])
VALUES ('485621b3-2a44-4d22-a870-ee9cc3f22326', 36, N'Maha', N'USA', N'Test@gmail.com', N'female', N'', N'+12029882614'),
('5ee9b5ae-1d0a-41ea-8943-eb52a4c9f20a', 28, N'Kholoud', N'Egypt', N'kholouddesouky@gmail.com', N'female', N'', N'0100000000'),
('dd651db5-c90d-4bda-bab9-a8e8e83b7df2', 40, N'Rania', N'Germany', N'Rania@gmail.com', N'female', N'', N'+4932211077146');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ClientID', N'Age', N'ClientName', N'Country', N'Email', N'Gender', N'Notes', N'Phone') AND [object_id] = OBJECT_ID(N'[Clients]'))
    SET IDENTITY_INSERT [Clients] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PaymentId', N'Location', N'PaymentDetails', N'PaymentName') AND [object_id] = OBJECT_ID(N'[Payments]'))
    SET IDENTITY_INSERT [Payments] ON;
INSERT INTO [Payments] ([PaymentId], [Location], [PaymentDetails], [PaymentName])
VALUES ('2ebdc7a1-1398-48f1-940d-c4022acce082', N'Egypt', N'100044321747', N'CIB Bank Transfer'),
('8762980a-28f1-47b4-84cd-47d65ae7ca71', N'Egypt', N'Asmaa.mostafa1987@instapay', N'Instapay'),
('945f0aa6-2210-46c5-bcb5-67b77d221f29', N'International', N'+1 (214) 912-7068', N'Zelle (US)'),
('bd622138-dc61-456e-9187-386bd55c7656', N'International', N'a_abdelgawad@hotmail.com', N'Paypal'),
('dfc9ec27-9b64-47b8-ab41-799dc2f61f03', N'International', N'IBAN:SA16 8000 0858 6080 1286 3374 - Accountnumber:  077030010006082863374 - Name: Hassan Mohamed Shawki ElHayawan', N'بنك الراجحي السعودي'),
('fc64db83-e94b-4e07-a438-1b71cc4e2fb8', N'Egypt', N'01006970792', N'Vodafone Cash'),
('fffe67f8-db58-4f1d-907e-a3753ce3c939', N'International', N'IBAN: AE85 0260 0010 1579 8179 101 - Accountnumber: 1015798179101 - Name: Mayar Ahmed', N'Emirates NBD');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'PaymentId', N'Location', N'PaymentDetails', N'PaymentName') AND [object_id] = OBJECT_ID(N'[Payments]'))
    SET IDENTITY_INSERT [Payments] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ServiceId', N'ServiceDescribtion', N'ServiceName', N'ServicePriceInternational', N'ServicePriceLocal') AND [object_id] = OBJECT_ID(N'[Services]'))
    SET IDENTITY_INSERT [Services] ON;
INSERT INTO [Services] ([ServiceId], [ServiceDescribtion], [ServiceName], [ServicePriceInternational], [ServicePriceLocal])
VALUES ('0e0dd7bd-4941-4d50-956e-d3553580fadc', N'لمشاركة كل تحديات الحياه بشكل عام ، وانطلاقه امنه واكثر صحية', N'دايرة سنجل مامي', 60.0E0, 600.0E0),
('10f2cd9c-95c3-4d80-afc6-b3b36836f588', N' لتشافي علاقتك مع المال والوصول للثراء والحريه الماليه بالتناغم مع الانوثه', N'دايرة الوفرة المالية', 350.0E0, 3500.0E0),
('3fe0d485-b0e4-4697-8cbe-651ef310b751', N' لتعلم قبول الرفض وازاي تقولي لاءه وتصنعي حدود امنه بكل لطف . دايره تعايش وتدريب', N'دايرة الرفض', 300.0E0, 3000.0E0),
('64ed73c8-94c4-44b8-b8ef-af46be9f2d73', N'لاسكتشاف شغفك وبصمتك الفريده وتحويلها لمشروع وادارته والربح منه بالتناغم مع الانوثه', N'دايرة سحر التمكين', 350.0E0, 3500.0E0),
('756d3b5a-a2a2-426c-b9b6-7f8552c64bb4', N'لممارسة الامتنان', N'دايرة الشكر', 60.0E0, 600.0E0),
('8ec38d0b-1e87-43a2-92fc-f8511a9de30d', N'لتواصل اعمق و مريح مع جسدك  .. هي فرصة لعمل علاقه حقيقيه مع جسمك كأنثى واستكشافه ، وقبوله زي ماهو بدون تعديلات او بدون شروط', N'دايرة مساحة جسد', 500.0E0, 5000.0E0),
('a0561a1e-f2ee-4256-bf77-7b4a83d20d35', N'هنتقابل مرة في الشهر نحكي ونتشارك ونسمع بعض بقلوب رحيمة بدون احكام ولا نصايح ندعم , نطبطب , نحضن , نقبل بعض بكل اللي فينا باللي عاجبنا و اللي مش عاجبنا', N'دايرة الأنس', 60.0E0, 600.0E0),
('e412d77e-77d3-4c06-8f7b-4df9490e04d6', N'', N'(استشارات فردية (اونلاين/اوفلاين', 170.0E0, 1700.0E0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ServiceId', N'ServiceDescribtion', N'ServiceName', N'ServicePriceInternational', N'ServicePriceLocal') AND [object_id] = OBJECT_ID(N'[Services]'))
    SET IDENTITY_INSERT [Services] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BookingId', N'Amount', N'ClientID', N'PaymentID', N'ServiceID', N'Status') AND [object_id] = OBJECT_ID(N'[Bookings]'))
    SET IDENTITY_INSERT [Bookings] ON;
INSERT INTO [Bookings] ([BookingId], [Amount], [ClientID], [PaymentID], [ServiceID], [Status])
VALUES ('1a3e69e4-7ec9-48ff-8713-3a0c5d48702c', 200.0E0, '485621b3-2a44-4d22-a870-ee9cc3f22326', '2ebdc7a1-1398-48f1-940d-c4022acce082', '756d3b5a-a2a2-426c-b9b6-7f8552c64bb4', N'Waiting'),
('627bb73e-8232-466f-b348-799b7bcd9b01', 50.0E0, '485621b3-2a44-4d22-a870-ee9cc3f22326', '2ebdc7a1-1398-48f1-940d-c4022acce082', '756d3b5a-a2a2-426c-b9b6-7f8552c64bb4', N'Cancelled'),
('d2e84756-7743-4265-a40f-bac354fe0f31', 2000.0E0, '485621b3-2a44-4d22-a870-ee9cc3f22326', '2ebdc7a1-1398-48f1-940d-c4022acce082', '756d3b5a-a2a2-426c-b9b6-7f8552c64bb4', N'Completed');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BookingId', N'Amount', N'ClientID', N'PaymentID', N'ServiceID', N'Status') AND [object_id] = OBJECT_ID(N'[Bookings]'))
    SET IDENTITY_INSERT [Bookings] OFF;

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ReviewId', N'BookingID', N'ReviewMessage') AND [object_id] = OBJECT_ID(N'[Reviews]'))
    SET IDENTITY_INSERT [Reviews] ON;
INSERT INTO [Reviews] ([ReviewId], [BookingID], [ReviewMessage])
VALUES ('627bb73e-8232-466f-b348-799b7bcd9b01', 'd2e84756-7743-4265-a40f-bac354fe0f31', N'Very friendly staff and clean environment.'),
('d84c39b2-0f8e-4e6d-8918-31c264ce3fd2', '1a3e69e4-7ec9-48ff-8713-3a0c5d48702c', N'Satisfactory experience overall.'),
('e3cfe13f-03bb-4b12-85d8-f4e4a7cd4aa1', 'd2e84756-7743-4265-a40f-bac354fe0f31', N'Excellent Payment, highly recommended!');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'ReviewId', N'BookingID', N'ReviewMessage') AND [object_id] = OBJECT_ID(N'[Reviews]'))
    SET IDENTITY_INSERT [Reviews] OFF;

CREATE INDEX [IX_Bookings_ClientID] ON [Bookings] ([ClientID]);

CREATE INDEX [IX_Bookings_PaymentID] ON [Bookings] ([PaymentID]);

CREATE INDEX [IX_Bookings_ServiceID] ON [Bookings] ([ServiceID]);

CREATE INDEX [IX_Reviews_BookingID] ON [Reviews] ([BookingID]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250716055714_initial', N'9.0.7');

CREATE INDEX [IX_Appointments_BookingID] ON [Appointments] ([BookingID]);

ALTER TABLE [Appointments] ADD CONSTRAINT [FK_Appointments_Bookings_BookingID] FOREIGN KEY ([BookingID]) REFERENCES [Bookings] ([BookingId]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250810040311_AddAppointments', N'9.0.7');

COMMIT;
GO

