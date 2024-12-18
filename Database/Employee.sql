BEGIN TRAN

USE Employee;

IF OBJECT_ID('Employee') IS NOT NULL BEGIN DROP TABLE Employee END;

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


COMMIT TRAN