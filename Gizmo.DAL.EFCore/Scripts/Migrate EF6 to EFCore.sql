/*
Script to migrate EF6 to EF-Core 
*/


GO
PRINT N'Dropping Index [dbo].[DeviceHdmi].[IX_DeviceId]...';


GO
DROP INDEX [IX_DeviceId]
    ON [dbo].[DeviceHdmi];


GO
PRINT N'Dropping Index [dbo].[HostEndpoint].[IX_HostId]...';


GO
DROP INDEX [IX_HostId]
    ON [dbo].[HostEndpoint];


GO
PRINT N'Dropping Index [dbo].[HostGroupWaitingLine].[IX_HosGroupId]...';


GO
DROP INDEX [IX_HosGroupId]
    ON [dbo].[HostGroupWaitingLine];


GO
PRINT N'Dropping Index [dbo].[InvoiceLine].[UQ_PointsTransaction]...';


GO
DROP INDEX [UQ_PointsTransaction]
    ON [dbo].[InvoiceLine];


GO
PRINT N'Dropping Index [dbo].[InvoiceLineExtended].[IX_InvoiceLineId]...';


GO
DROP INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineExtended];


GO
PRINT N'Dropping Index [dbo].[InvoiceLineProduct].[IX_InvoiceLineId]...';


GO
DROP INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineProduct];


GO
PRINT N'Dropping Index [dbo].[InvoiceLineProduct].[UQ_OrderLine]...';


GO
DROP INDEX [UQ_OrderLine]
    ON [dbo].[InvoiceLineProduct];


GO
PRINT N'Dropping Index [dbo].[InvoiceLineSession].[IX_InvoiceLineId]...';


GO
DROP INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineSession];


GO
PRINT N'Dropping Index [dbo].[InvoiceLineTime].[IX_InvoiceLineId]...';


GO
DROP INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineTime];


GO
PRINT N'Dropping Index [dbo].[InvoiceLineTimeFixed].[IX_InvoiceLineId]...';


GO
DROP INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineTimeFixed];


GO
PRINT N'Dropping Index [dbo].[LogException].[IX_LogId]...';


GO
DROP INDEX [IX_LogId]
    ON [dbo].[LogException];


GO
PRINT N'Dropping Index [dbo].[PaymentIntentDeposit].[IX_PaymentIntentId]...';


GO
DROP INDEX [IX_PaymentIntentId]
    ON [dbo].[PaymentIntentDeposit];


GO
PRINT N'Dropping Index [dbo].[PaymentIntentOrder].[IX_PaymentIntentId]...';


GO
DROP INDEX [IX_PaymentIntentId]
    ON [dbo].[PaymentIntentOrder];


GO
PRINT N'Dropping Index [dbo].[Product].[IX_ProductId]...';


GO
DROP INDEX [IX_ProductId]
    ON [dbo].[Product];


GO
PRINT N'Dropping Index [dbo].[ProductBaseExtended].[IX_ProductId]...';


GO
DROP INDEX [IX_ProductId]
    ON [dbo].[ProductBaseExtended];


GO
PRINT N'Dropping Index [dbo].[ProductBundle].[IX_ProductId]...';


GO
DROP INDEX [IX_ProductId]
    ON [dbo].[ProductBundle];


GO
PRINT N'Dropping Index [dbo].[ProductOLExtended].[IX_ProductOLId]...';


GO
DROP INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLExtended];


GO
PRINT N'Dropping Index [dbo].[ProductOLProduct].[IX_ProductOLId]...';


GO
DROP INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLProduct];


GO
PRINT N'Dropping Index [dbo].[ProductOLSession].[IX_ProductOLId]...';


GO
DROP INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLSession];


GO
PRINT N'Dropping Index [dbo].[ProductOLTime].[IX_ProductOLId]...';


GO
DROP INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLTime];


GO
PRINT N'Dropping Index [dbo].[ProductOLTimeFixed].[IX_ProductOLId]...';


GO
DROP INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLTimeFixed];


GO
PRINT N'Dropping Index [dbo].[ProductTime].[IX_ProductId]...';


GO
DROP INDEX [IX_ProductId]
    ON [dbo].[ProductTime];


GO
PRINT N'Dropping Index [dbo].[ProductTimePeriod].[IX_ProductId]...';


GO
DROP INDEX [IX_ProductId]
    ON [dbo].[ProductTimePeriod];


GO
PRINT N'Dropping Index [dbo].[RefundDepositPayment].[IX_RefundId]...';


GO
DROP INDEX [IX_RefundId]
    ON [dbo].[RefundDepositPayment];


GO
PRINT N'Dropping Index [dbo].[RefundInvoicePayment].[IX_RefundId]...';


GO
DROP INDEX [IX_RefundId]
    ON [dbo].[RefundInvoicePayment];


GO
PRINT N'Dropping Index [dbo].[TaskJunction].[IX_TaskId]...';


GO
DROP INDEX [IX_TaskId]
    ON [dbo].[TaskJunction];


GO
PRINT N'Dropping Index [dbo].[TaskNotification].[IX_TaskId]...';


GO
DROP INDEX [IX_TaskId]
    ON [dbo].[TaskNotification];


GO
PRINT N'Dropping Index [dbo].[TaskProcess].[IX_TaskId]...';


GO
DROP INDEX [IX_TaskId]
    ON [dbo].[TaskProcess];


GO
PRINT N'Dropping Index [dbo].[TaskScript].[IX_TaskId]...';


GO
DROP INDEX [IX_TaskId]
    ON [dbo].[TaskScript];


GO
PRINT N'Dropping Index [dbo].[UsageRate].[IX_UsageId]...';


GO
DROP INDEX [IX_UsageId]
    ON [dbo].[UsageRate];


GO
PRINT N'Dropping Index [dbo].[UsageTime].[IX_UsageId]...';


GO
DROP INDEX [IX_UsageId]
    ON [dbo].[UsageTime];


GO
PRINT N'Dropping Index [dbo].[UsageTimeFixed].[IX_UsageId]...';


GO
DROP INDEX [IX_UsageId]
    ON [dbo].[UsageTimeFixed];


GO
PRINT N'Dropping Index [dbo].[UsageUserSession].[IX_UsageId]...';


GO
DROP INDEX [IX_UsageId]
    ON [dbo].[UsageUserSession];


GO
PRINT N'Dropping Index [dbo].[UserCreditLimit].[IX_UserId]...';


GO
DROP INDEX [IX_UserId]
    ON [dbo].[UserCreditLimit];


GO
PRINT N'Dropping Index [dbo].[UserGuest].[IX_UserId]...';


GO
DROP INDEX [IX_UserId]
    ON [dbo].[UserGuest];


GO
PRINT N'Dropping Index [dbo].[UserMember].[IX_UserId]...';


GO
DROP INDEX [IX_UserId]
    ON [dbo].[UserMember];


GO
PRINT N'Dropping Index [dbo].[UserNote].[IX_NoteId]...';


GO
DROP INDEX [IX_NoteId]
    ON [dbo].[UserNote];


GO
PRINT N'Dropping Index [dbo].[UserOperator].[IX_UserId]...';


GO
DROP INDEX [IX_UserId]
    ON [dbo].[UserOperator];


GO
PRINT N'Dropping Index [dbo].[VerificationEmail].[IX_VerificationId]...';


GO
DROP INDEX [IX_VerificationId]
    ON [dbo].[VerificationEmail];


GO
PRINT N'Dropping Index [dbo].[VerificationMobilePhone].[IX_VerificationId]...';


GO
DROP INDEX [IX_VerificationId]
    ON [dbo].[VerificationMobilePhone];


GO
PRINT N'Dropping Index [dbo].[VoidDepositPayment].[IX_VoidId]...';


GO
DROP INDEX [IX_VoidId]
    ON [dbo].[VoidDepositPayment];


GO
PRINT N'Dropping Index [dbo].[VoidInvoice].[IX_VoidId]...';


GO
DROP INDEX [IX_VoidId]
    ON [dbo].[VoidInvoice];


GO
PRINT N'Creating Table [dbo].[__EFMigrationsHistory]...';


GO
CREATE TABLE [dbo].[__EFMigrationsHistory] (
    [MigrationId]    NVARCHAR (150) NOT NULL,
    [ProductVersion] NVARCHAR (32)  NOT NULL,
    CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED ([MigrationId] ASC)
);


GO
PRINT N'Creating Index [dbo].[DeviceHdmi].[IX_DeviceId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_DeviceId]
    ON [dbo].[DeviceHdmi]([DeviceId] ASC);


GO
PRINT N'Creating Index [dbo].[HostEndpoint].[IX_HostId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HostId]
    ON [dbo].[HostEndpoint]([HostId] ASC);


GO
PRINT N'Creating Index [dbo].[HostGroupWaitingLine].[IX_HosGroupId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_HosGroupId]
    ON [dbo].[HostGroupWaitingLine]([HosGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[InvoiceLine].[UQ_PointsTransaction]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_PointsTransaction]
    ON [dbo].[InvoiceLine]([PointsTransactionId] ASC);


GO
PRINT N'Creating Index [dbo].[InvoiceLineExtended].[IX_InvoiceLineId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineExtended]([InvoiceLineId] ASC);


GO
PRINT N'Creating Index [dbo].[InvoiceLineProduct].[IX_InvoiceLineId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineProduct]([InvoiceLineId] ASC);


GO
PRINT N'Creating Index [dbo].[InvoiceLineProduct].[UQ_OrderLine]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [UQ_OrderLine]
    ON [dbo].[InvoiceLineProduct]([OrderLineId] ASC) WHERE ([OrderLineId] IS NOT NULL);


GO
PRINT N'Creating Index [dbo].[InvoiceLineSession].[IX_InvoiceLineId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineSession]([InvoiceLineId] ASC);


GO
PRINT N'Creating Index [dbo].[InvoiceLineTime].[IX_InvoiceLineId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineTime]([InvoiceLineId] ASC);


GO
PRINT N'Creating Index [dbo].[InvoiceLineTimeFixed].[IX_InvoiceLineId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_InvoiceLineId]
    ON [dbo].[InvoiceLineTimeFixed]([InvoiceLineId] ASC);


GO
PRINT N'Creating Index [dbo].[LogException].[IX_LogId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_LogId]
    ON [dbo].[LogException]([LogId] ASC);


GO
PRINT N'Creating Index [dbo].[PaymentIntentDeposit].[IX_PaymentIntentId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PaymentIntentId]
    ON [dbo].[PaymentIntentDeposit]([PaymentIntentId] ASC);


GO
PRINT N'Creating Index [dbo].[PaymentIntentOrder].[IX_PaymentIntentId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_PaymentIntentId]
    ON [dbo].[PaymentIntentOrder]([PaymentIntentId] ASC);


GO
PRINT N'Creating Index [dbo].[Product].[IX_ProductId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductId]
    ON [dbo].[Product]([ProductId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductBaseExtended].[IX_ProductId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductId]
    ON [dbo].[ProductBaseExtended]([ProductId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductBundle].[IX_ProductId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductId]
    ON [dbo].[ProductBundle]([ProductId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductOLExtended].[IX_ProductOLId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLExtended]([ProductOLId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductOLProduct].[IX_ProductOLId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLProduct]([ProductOLId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductOLSession].[IX_ProductOLId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLSession]([ProductOLId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductOLTime].[IX_ProductOLId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLTime]([ProductOLId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductOLTimeFixed].[IX_ProductOLId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductOLId]
    ON [dbo].[ProductOLTimeFixed]([ProductOLId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductTime].[IX_ProductId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductId]
    ON [dbo].[ProductTime]([ProductId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductTimePeriod].[IX_ProductId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductId]
    ON [dbo].[ProductTimePeriod]([ProductId] ASC);


GO
PRINT N'Creating Index [dbo].[RefundDepositPayment].[IX_RefundId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RefundId]
    ON [dbo].[RefundDepositPayment]([RefundId] ASC);


GO
PRINT N'Creating Index [dbo].[RefundInvoicePayment].[IX_RefundId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_RefundId]
    ON [dbo].[RefundInvoicePayment]([RefundId] ASC);


GO
PRINT N'Creating Index [dbo].[TaskJunction].[IX_TaskId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[TaskJunction]([TaskId] ASC);


GO
PRINT N'Creating Index [dbo].[TaskNotification].[IX_TaskId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[TaskNotification]([TaskId] ASC);


GO
PRINT N'Creating Index [dbo].[TaskProcess].[IX_TaskId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[TaskProcess]([TaskId] ASC);


GO
PRINT N'Creating Index [dbo].[TaskScript].[IX_TaskId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[TaskScript]([TaskId] ASC);


GO
PRINT N'Creating Index [dbo].[UsageRate].[IX_UsageId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UsageId]
    ON [dbo].[UsageRate]([UsageId] ASC);


GO
PRINT N'Creating Index [dbo].[UsageTime].[IX_UsageId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UsageId]
    ON [dbo].[UsageTime]([UsageId] ASC);


GO
PRINT N'Creating Index [dbo].[UsageTimeFixed].[IX_UsageId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UsageId]
    ON [dbo].[UsageTimeFixed]([UsageId] ASC);


GO
PRINT N'Creating Index [dbo].[UsageUserSession].[IX_UsageId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UsageId]
    ON [dbo].[UsageUserSession]([UsageId] ASC);


GO
PRINT N'Creating Index [dbo].[UserCreditLimit].[IX_UserId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserCreditLimit]([UserId] ASC);


GO
PRINT N'Creating Index [dbo].[UserGuest].[IX_UserId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserGuest]([UserId] ASC);


GO
PRINT N'Creating Index [dbo].[UserMember].[IX_UserId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserMember]([UserId] ASC);


GO
PRINT N'Creating Index [dbo].[UserNote].[IX_NoteId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_NoteId]
    ON [dbo].[UserNote]([NoteId] ASC);


GO
PRINT N'Creating Index [dbo].[UserOperator].[IX_UserId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserOperator]([UserId] ASC);


GO
PRINT N'Creating Index [dbo].[VerificationEmail].[IX_VerificationId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_VerificationId]
    ON [dbo].[VerificationEmail]([VerificationId] ASC);


GO
PRINT N'Creating Index [dbo].[VerificationMobilePhone].[IX_VerificationId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_VerificationId]
    ON [dbo].[VerificationMobilePhone]([VerificationId] ASC);


GO
PRINT N'Creating Index [dbo].[VoidDepositPayment].[IX_VoidId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_VoidId]
    ON [dbo].[VoidDepositPayment]([VoidId] ASC);


GO
PRINT N'Creating Index [dbo].[VoidInvoice].[IX_VoidId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_VoidId]
    ON [dbo].[VoidInvoice]([VoidId] ASC);


GO
PRINT N'Creating Index [dbo].[BundleProductUserPrice].[IX_UserGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserGroupId]
    ON [dbo].[BundleProductUserPrice]([UserGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[DeviceHost].[IX_HostId]...';


GO
CREATE NONCLUSTERED INDEX [IX_HostId]
    ON [dbo].[DeviceHost]([HostId] ASC);


GO
PRINT N'Creating Index [dbo].[HostGroupUserBillProfile].[IX_UserGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserGroupId]
    ON [dbo].[HostGroupUserBillProfile]([UserGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[HostLayoutGroupLayout].[IX_HostId]...';


GO
CREATE NONCLUSTERED INDEX [IX_HostId]
    ON [dbo].[HostLayoutGroupLayout]([HostId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductBundleUserPrice].[IX_UserGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserGroupId]
    ON [dbo].[ProductBundleUserPrice]([UserGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductHostHidden].[IX_HostGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_HostGroupId]
    ON [dbo].[ProductHostHidden]([HostGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductOL].[IX_ProductOLId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_ProductOLId]
    ON [dbo].[ProductOL]([ProductOLId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductTax].[IX_TaxId]...';


GO
CREATE NONCLUSTERED INDEX [IX_TaxId]
    ON [dbo].[ProductTax]([TaxId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductTimeHostDisallowed].[IX_HostGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_HostGroupId]
    ON [dbo].[ProductTimeHostDisallowed]([HostGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductTimePeriodDay].[IX_ProductTimePeriodDayId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ProductTimePeriodDayId]
    ON [dbo].[ProductTimePeriodDay]([ProductTimePeriodDayId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductUserDisallowed].[IX_UserGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserGroupId]
    ON [dbo].[ProductUserDisallowed]([UserGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[ProductUserPrice].[IX_UserGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserGroupId]
    ON [dbo].[ProductUserPrice]([UserGroupId] ASC);


GO
PRINT N'Creating Index [dbo].[ReservationHost].[IX_HostId]...';


GO
CREATE NONCLUSTERED INDEX [IX_HostId]
    ON [dbo].[ReservationHost]([HostId] ASC);


GO
PRINT N'Creating Index [dbo].[ReservationUser].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[ReservationUser]([UserId] ASC);


GO
PRINT N'Creating Index [dbo].[Shift].[IX_ShiftId]...';


GO
CREATE NONCLUSTERED INDEX [IX_ShiftId]
    ON [dbo].[Shift]([ShiftId] ASC);


GO
PRINT N'Creating Index [dbo].[ShiftCount].[IX_PaymentMethodId]...';


GO
CREATE NONCLUSTERED INDEX [IX_PaymentMethodId]
    ON [dbo].[ShiftCount]([PaymentMethodId] ASC);


GO
PRINT N'Creating Index [dbo].[TaskBase].[IX_TaskId]...';


GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_TaskId]
    ON [dbo].[TaskBase]([TaskId] ASC);


GO
PRINT N'Creating Index [dbo].[UsageSession].[IX_UsageSessionId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UsageSessionId]
    ON [dbo].[UsageSession]([UsageSessionId] ASC);


GO
PRINT N'Creating Index [dbo].[UserAgreementState].[IX_UserId]...';


GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[UserAgreementState]([UserId] ASC);


GO
PRINT N'Creating Index [dbo].[UserAttribute].[IX_AttributeId]...';


GO
CREATE NONCLUSTERED INDEX [IX_AttributeId]
    ON [dbo].[UserAttribute]([AttributeId] ASC);


GO
PRINT N'Creating Index [dbo].[UserGroupHostDisallowed].[IX_HostGroupId]...';


GO
CREATE NONCLUSTERED INDEX [IX_HostGroupId]
    ON [dbo].[UserGroupHostDisallowed]([HostGroupId] ASC);

GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
GO
INSERT INTO [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20221220193351_Initial', N'6.0.12')
COMMIT TRANSACTION

GO
PRINT N'Update complete.';


GO
