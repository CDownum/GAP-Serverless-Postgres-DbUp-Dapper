CREATE EXTENSION IF NOT EXISTS "uuid-ossp" WITH SCHEMA public;

CREATE TABLE IF NOT EXISTS gap.roles (
    id integer PRIMARY KEY NOT NULL,
    name character varying(128) NOT NULL,
    description character varying(256),   
    created_date timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL,
    last_modified timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL,
    modified_by uuid
);

CREATE TABLE IF NOT EXISTS gap.users (
	id UUID PRIMARY KEY DEFAULT uuid_generate_v4() NOT NULL,
    user_role_id integer NOT NULL,
	start_date timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL,
	end_date timestamp without time zone,
    first_name character varying(256) NOT NULL,
    middle_name character varying(256),
    last_name character varying(256) NOT NULL,
    gross_annual_income decimal(18,2) NOT NULL,
    email character varying(400),
    reporting_manager_id UUID,
    salaried boolean DEFAULT false NOT NULL,
    date_of_birth character varying(100) NOT NULL,
    salutation character varying(50),
    address1 character varying(256),
    address2 character varying(256),
    city character varying(128),
    state character varying(128),
    zip_code character varying(20),
    work_phone character varying(20),
    cell character varying(20),
    CONSTRAINT fk_user_role FOREIGN KEY (user_role_id) REFERENCES gap.roles(id)
);
