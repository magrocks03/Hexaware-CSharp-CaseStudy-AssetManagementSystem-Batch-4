CREATE DATABASE AssetDB;

Use AssetDB;

CREATE TABLE employees (
    employee_id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    department VARCHAR(100),
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(100) NOT NULL
);

CREATE TABLE assets (
    asset_id INT PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    type VARCHAR(50),
    serial_number VARCHAR(100) UNIQUE,
    purchase_date DATE,
    location VARCHAR(100),
    status VARCHAR(50), -- e.g., Available, Allocated, UnderMaintenance
    owner_id INT,
    FOREIGN KEY (owner_id) REFERENCES employees(employee_id)
);

CREATE TABLE maintenance_records (
    maintenance_id INT IDENTITY(1,1) PRIMARY KEY,
    asset_id INT NOT NULL,
    maintenance_date DATE NOT NULL,
    description VARCHAR(255),
    cost DECIMAL(10, 2),
    FOREIGN KEY (asset_id) REFERENCES assets(asset_id)
);

CREATE TABLE asset_allocations (
    allocation_id INT IDENTITY(1,1) PRIMARY KEY,
    asset_id INT NOT NULL,
    employee_id INT NOT NULL,
    allocation_date DATE,
    return_date DATE,
    FOREIGN KEY (asset_id) REFERENCES assets(asset_id),
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id)
);

CREATE TABLE reservations (
    reservation_id INT IDENTITY(1,1) PRIMARY KEY,
    asset_id INT NOT NULL,
    employee_id INT NOT NULL,
    reservation_date DATE,
    start_date DATE,
    end_date DATE,
    status VARCHAR(50), -- e.g., Active, Cancelled, Completed
    FOREIGN KEY (asset_id) REFERENCES assets(asset_id),
    FOREIGN KEY (employee_id) REFERENCES employees(employee_id)
);

SELECT * FROM employees;
SELECT * FROM assets;
SELECT * FROM maintenance_records;
SELECT * FROM asset_allocations;
SELECT * FROM reservations;

INSERT INTO employees (employee_id, name, department, email, password) VALUES
(101, 'Alice Johnson', 'IT', 'alice.j@company.com', 'alice123'),
(102, 'Bob Smith', 'Finance', 'bob.s@company.com', 'bob456'),
(103, 'Charlie Brown', 'HR', 'charlie.b@company.com', 'charlie789');

INSERT INTO assets (asset_id, name, type, serial_number, purchase_date, location, status, owner_id) VALUES
(201, 'Dell Laptop', 'Electronics', 'DL2023A01', '2022-05-15', 'Room 101', 'Available', 101),
(202, 'Epson Projector', 'Electronics', 'EP2021X22', '2021-08-20', 'Conference Room', 'Allocated', 102),
(203, 'Office Chair', 'Furniture', 'OC2023Z45', '2023-01-10', 'Room 202', 'Available', 103);

INSERT INTO maintenance_records (asset_id, maintenance_date, description, cost) VALUES
(201, '2024-01-10', 'Battery replaced', 3500.00),
(202, '2024-03-20', 'Lamp replacement', 2500.00);

INSERT INTO asset_allocations (asset_id, employee_id, allocation_date, return_date) VALUES
(201, 101, '2024-06-01', NULL),
(202, 102, '2024-06-05', '2024-06-15');

INSERT INTO reservations (asset_id, employee_id, reservation_date, start_date, end_date, status) VALUES
(203, 103, '2024-06-20', '2024-07-01', '2024-07-05', 'Active'),
(201, 102, '2024-06-22', '2024-07-03', '2024-07-10', 'Cancelled');

INSERT INTO employees (employee_id, name, department, email, password)
VALUES (999, 'Test User', 'QA', 'test@company.com', 'test123');

DELETE FROM employees;
DELETE FROM reservations;
DELETE FROM asset_allocations;
DELETE FROM maintenance_records;
DELETE FROM assets;

DBCC CHECKIDENT ('reservations', RESEED, 0);
DBCC CHECKIDENT ('asset_allocations', RESEED, 0);
DBCC CHECKIDENT ('maintenance_records', RESEED, 0);

