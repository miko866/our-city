-- --------------------------------------------------------------------------------------------------
-- our_city development base truncate 
-- 
-- this script will be called before the data are seeded, as it delete all tables with data
-- 
-- --------------------------------------------------------------------------------------------------

-- Truncate all tables in the current schema
-- DO $$ DECLARE
-- r RECORD;
-- BEGIN
-- FOR r IN (SELECT tablename FROM pg_tables WHERE schemaname = current_schema()) LOOP
--         EXECUTE 'TRUNCATE TABLE ' || quote_ident(r.tablename) || ' RESTART identity CASCADE';
-- END LOOP;
-- END $$;

-- Truncate without '__EFMigrationsHistory' Table 
-- important for EF migrations 
DO $$
    DECLARE row RECORD;
    BEGIN
        FOR row IN SELECT table_name
                   FROM information_schema.tables
                   WHERE table_type='BASE TABLE'
                     AND table_schema='public'
                     AND table_name NOT IN ('__EFMigrationsHistory')
            LOOP
                EXECUTE format('TRUNCATE TABLE %I RESTART identity CASCADE;',row.table_name);
            END LOOP;
    END;
$$;