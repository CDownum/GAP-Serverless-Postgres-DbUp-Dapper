--Insert Roles
INSERT INTO gap.roles (id, name, description, created_date, last_modified)
VALUES (1,'Sales', 'Sales', Now(), Now()) ON CONFLICT DO NOTHING;

INSERT INTO gap.roles (id, name, description, created_date, last_modified)
VALUES (2,'Developer', 'Developer', Now(), Now()) ON CONFLICT DO NOTHING;

INSERT INTO gap.roles (id, name, description, created_date, last_modified)
VALUES (3,'CEO', 'Cheif Executive Officer', Now(), Now()) ON CONFLICT DO NOTHING;

--Insert Company
INSERT INTO gap.companies (id, name, created_date, last_modified, enabled)
VALUES (1,'New Wings Residential', Now(), Now(), true) ON CONFLICT DO NOTHING;

INSERT INTO gap.users
(id, user_role_id, start_date, first_name, middle_name, last_name, gross_annual_income, email, 
salaried, date_of_birth, salutation, address1, city, state, zip_code, cell, work_phone, company_id, is_admin)
VALUES (uuid_generate_v4(), 2, Now(), 'Charles', 'Michael', 'Downum', 100000, 'charles.downum@example.com',
true, '02271989', 'Mr.', '12345 Test Street', 'Austin', 'TX', '76543', '5436578908', '3456546789', 1, true) ON CONFLICT DO NOTHING;