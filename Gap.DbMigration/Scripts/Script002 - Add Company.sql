CREATE TABLE IF NOT EXISTS gap.companies (
    id integer PRIMARY KEY NOT NULL,
    name character varying(128) NOT NULL,
    enabled boolean default false NOT NULL,
    created_date timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL,
    last_modified timestamp without time zone DEFAULT '1970-01-01 08:00:00'::timestamp without time zone NOT NULL
);

ALTER TABLE IF EXISTS gap.users ADD COLUMN IF NOT EXISTS company_id Integer NOT NULL REFERENCES gap.companies(id) ON DELETE RESTRICT