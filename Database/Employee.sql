BEGIN TRAN

USE Employee;

IF OBJECT_ID('Employee') IS NOT NULL BEGIN DROP TABLE Employee END;
IF OBJECT_ID('Users') IS NOT NULL BEGIN DROP TABLE Users END;

CREATE TABLE Users
(
	id BIGINT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	user_name VARCHAR(200) NOT NULL,
	password_hash VARCHAR(255) NOT NULL,
	password_salt VARCHAR(255) NOT NULL,
	name VARCHAR(200) NULL,
	created_by BIGINT NULL,
	created_at DATETIME NULL,
	updated_by BIGINT NULL,
	updated_at DATETIME NULL,
	deleted_by BIGINT NULL,
	deleted_at BIGINT NULL,
);


CREATE TABLE Employee
(
	id BIGINT PRIMARY KEY IDENTITY(1,1) not null,
	name VARCHAR(200) NULL,
	epf VARCHAR(200) NULL,
	mobile int NULL,
	address VARCHAR(255) NULL,
	email VARCHAR(255) NULL,
	created_by BIGINT NULL,
	created_at DATETIME NULL,
	updated_by BIGINT NULL,
	updated_at DATETIME NULL,
	deleted_by BIGINT NULL,
	deleted_at BIGINT NULL,
);

INSERT [dbo].[Users] ([user_name],[password_hash], [password_salt], [name], [created_at], [created_by], [deleted_at], [deleted_by], [updated_at], [updated_by]) VALUES (N'admin','39d0b09aa11dd4c072bcd449ad5757e05d16b2b206e7665532be9f114fcb7fc4592b9d1b3e61888f50b404ba8e885d42c7006e846c2de2f165e157c7fb6aa1d8', '25625732412409945593', N'Admin User', GETDATE(), NULL, NULL,NULL,NULL, NULL);

COMMIT TRAN