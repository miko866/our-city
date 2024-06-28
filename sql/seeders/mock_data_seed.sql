-- --------------------------------------------------------------------------------------------------
-- our_city development base seeder. 
--
-- Only dummy data for LocalDevelopment, Development and Testing env.
-- 
-- --------------------------------------------------------------------------------------------------

--user
UPDATE asp_net_users SET
                         description  = 'You shall not pass!'
WHERE id = (SELECT id FROM asp_net_users WHERE user_name  = 'SysAdminUser' LIMIT 1);

--organisation
insert into organisation (id, name, zip, district_id, description, latitude, longitude, color, created_by, updated_by, 
                          created_at, updated_at)
select 'b39198d7-adab-4d4b-95e2-c865b707b1f0', 'Moravany', '07203', (SELECT id FROM district WHERE name  = 'Michalovce' LIMIT 1), null,
       48.7389, 21.7859, '#3F51B5', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select '9c6c68fb-ad9f-4595-8bcf-47b99e5672ec', 'Topoľčany', '95501', (SELECT id FROM district WHERE name  = 'Topoľčany' LIMIT 1), null,
       48.5635, 18.1749, '#FF9800', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select '9c6c68fb-ad9f-4595-8bcf-47b99e5672ef', 'Novoť', '02955', (SELECT id FROM district WHERE name  = 'Námestovo' LIMIT 1), null,
       49.4374, 19.2477, '#FF5722', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--user_organisation
insert into user_organisation (id, application_user_id, organisation_id, is_primary, created_by, updated_by, 
                               created_at, updated_at)
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'OrganisationAdminMoravanyUser' LIMIT 1), 
         (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity1', null, CURRENT_TIMESTAMP, 
         CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'OrganisationEditorMoravanyUser' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity2', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'TestUserAllAdmin' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), false, 'OurCity3', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'TestUserAllEditor' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity4', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'OrganisationAdminTOUser' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity5', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'OrganisationEditorTOUser' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), false, 'OurCity6', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'TestUserAllAdmin' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity7', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'TestUserAllEditor' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), false, 'OurCity8', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'OrganisationAdminNovotUser' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), false, 'OurCity9', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'TestUserAllAdmin' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), true, 'OurCity10', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM asp_net_users WHERE user_name  = 'TestUserAllEditor' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), false, 'OurCity11', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP;

--user_organisation_permission
insert into user_organisation_permission (id, user_organisation_id, permission_id, created_by, updated_by, created_at, updated_at)
--ADMIN Moravany
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--EDITOR Moravany
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--TestUserAllAdmin Moravany
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity3' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity3' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity3' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity3' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--TestAllEditor Moravany
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity4' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity4' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity4' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--ADMIN TO
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity5' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity5' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity5' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity5' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--EDITOR TO
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity6' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity6' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity6' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--TestAllAdmin TO
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity7' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity7' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity7' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity7' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity8' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity8' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity8' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--ADMIN Novot
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity9' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity9' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity9' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity9' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--TestAllAdmin Novot
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity10' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity10' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity10' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity10' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Delete' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
--TestAllEditor Novot
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity11' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.View' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity11' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Edit' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM user_organisation WHERE created_by  = 'OurCity11' LIMIT 1),
       (SELECT id FROM permission WHERE name  = 'Permissions.Organisation.Create' LIMIT 1), 'OurCity', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--file_item
insert into file_item 
    (id, file_name, file_origin_name, file_extension, href, alt_text, file_item_type_id, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'bed1f8846cab4b9aaad7902fb5276b266431b34cc2c046269bc3a4c7679817b3', 'moravany logo', 'png', 
       'storage/organisation/b39198d7-adab-4d4b-95e2-c865b707b1f0/bed1f8846cab4b9aaad7902fb5276b266431b34cc2c046269bc3a4c7679817b3.png', 'Moravany', 
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity1', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'b2b8fdc0bb404952aa0b955c95a4a713d8db2371d7e9483eab08911843d4fbea', 'topolcany logo', 'png',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ec/b2b8fdc0bb404952aa0b955c95a4a713d8db2371d7e9483eab08911843d4fbea.png', 'Topoľčany',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity2', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'bed1f8846cab4b9aaad7902fb5276b266431b34cc2c046269bc3a4c7679817c8', 'novot logo', 'png',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/bed1f8846cab4b9aaad7902fb5276b266431b34cc2c046269bc3a4c7679817c8.png', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity3', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'bed1f8846cab4b9aaad7902fb5276b266431b34cc2c046269bc3a4c7679817q7', 'novot logo mini', 'png',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/bed1f8846cab4b9aaad7902fb5276b266431b34cc2c046269bc3a4c7679817q7.png', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity4', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'dac9322b24c3450cae89118b82eeedc7722a34d73113455aa64d3c388a9d1849', 'star wars high republic', 'jpeg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/dac9322b24c3450cae89118b82eeedc7722a34d73113455aa64d3c388a9d1849.jpeg', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity5', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91848', 'novoť kostol', 'jpg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91848.jpg', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity6', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91811', 'novoť kostol staré', 
       'jpg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91811.jpg', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity7', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91822', 'novoť pole staré',
       'jpg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91822.jpg', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity8', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91833', 'novoť hora staré',
       'jpg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91833.jpg', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity9', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91844', 'novoť z hora',
       'jpeg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91844.jpeg', 'Novoť',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity10', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91855', 'star wars',
       'jpg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91855.jpg', 'StarWars',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity11', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91866', 'star wars',
       'jpeg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91866.jpeg', 'StarWars',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity12', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91877', 'star wars',
       'jpeg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91877.jpeg', 'StarWars',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity13', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), '49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91888', 'star wars',
       'jpeg',
       'storage/organisation/9c6c68fb-ad9f-4595-8bcf-47b99e5672ef/49b9597fa38a46778fdaeace213401885c0775ead7d744f09fe80e39c3f91888.jpeg', 'StarWars',
       (SELECT id FROM file_item_type WHERE name  = 'OrganisationImage' LIMIT 1), 'OurCity14', null, CURRENT_TIMESTAMP,
       CURRENT_TIMESTAMP;
           
--organisation_file_item
insert into organisation_file_item (id, file_item_id, organisation_id, is_logo, is_logo_mini, order_nr, created_by, 
                                    updated_by, created_at, updated_at)
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM organisation WHERE id  = 'b39198d7-adab-4d4b-95e2-c865b707b1f0' LIMIT 1), 
       false, true, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM organisation WHERE id  = 'b39198d7-adab-4d4b-95e2-c865b707b1f0' LIMIT 1),
       true, false, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM organisation WHERE id  = '9c6c68fb-ad9f-4595-8bcf-47b99e5672ec' LIMIT 1), 
       false, true, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM organisation WHERE id  = '9c6c68fb-ad9f-4595-8bcf-47b99e5672ec' LIMIT 1),
       true, false, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity3' LIMIT 1),
       (SELECT id FROM organisation WHERE id  = '9c6c68fb-ad9f-4595-8bcf-47b99e5672ef' LIMIT 1), 
       true, false, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity4' LIMIT 1),
       (SELECT id FROM organisation WHERE id  = '9c6c68fb-ad9f-4595-8bcf-47b99e5672ef' LIMIT 1),
       false, true, 2, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;
           

--organisation_module_service
insert into organisation_module_service (id, module_service_id, organisation_id, is_active, created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Aktuality' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity1', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Udalosti' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity2', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Obecný rozhlas' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity3', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Mimoriadne oznamy' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity4', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Jednoduché stránky' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Moravany' LIMIT 1), true, 'OurCity5', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Aktuality' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity6', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Udalosti' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity7', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Obecný rozhlas' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity8', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Mimoriadne oznamy' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity9', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Jednoduché stránky' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Topoľčany' LIMIT 1), true, 'OurCity10', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Aktuality' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), true, 'OurCity11', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Udalosti' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), true, 'OurCity12', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Obecný rozhlas' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), true, 'OurCity13', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Mimoriadne oznamy' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), true, 'OurCity14', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_service WHERE name  = 'Jednoduché stránky' LIMIT 1),
       (SELECT id FROM organisation WHERE name  = 'Novoť' LIMIT 1), true, 'OurCity15', null, CURRENT_TIMESTAMP, 
       CURRENT_TIMESTAMP;

--module_news
insert into module_news (id, title, shor_text, context, url_link, video_link, organisation_module_service_id, created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), 'Novinky z Novoťe STAR WARS - the force may be with you!', 'Captain. Yes, sir? Tell them we' ||
                                                                                     ' wish to board at once. ' ||
                                                          'With all due' ||
                                                 ' respect, ' ||
                                              'the Ambassadors for the Supreme Chancellor wish to board immediately. ' ||
                                              'Yes, yes, of course, as you know, our blockade is perfectly legal, and we''d be ' ||
                                              'happy to receive the Ambassador. I''m TC-14 at your service. This way, please. ' ||
                                              'We are greatly honored by your visit Ambassadors.', 
    'Senator, we''re making our final approach into Coruscant. Very good, Lieutenant. We made it. I guess I was wrong.
The Trade Federation is to take delivery of a droid army here, and it is clear that Viceroy Gunray, is behind the assassination attempts on Senator Amidala. The Commerce Guilds and the Corporate Alliance, have both pledged their armies to Count Dooku and are forming a, Wait. Wait. More happening on Geonosis, I feel, than has been revealed. I agree.
You''ll be safe. Anakin, I won''t be long. We must persuade the Commerce Guild, and the Corporate Alliance to sign the treaty. What about the senator from Naboo? Is she dead yet? I am not signing your treaty until I have her head on my desk. I am a man of my word, Viceroy.
I don''t like just waiting here for something to happen to her. What''s going on? Ah, she covered the cameras. I don''t think she liked me watching her. What is she thinking? She programmed R2 to warn us if there is an intruder.
I''m-I''m a senator. If you follow your thoughts through to conclusion, it''ll take us to a place we cannot go, regardless of the way we feel about each other. Then you do feel something. I will not let you give up your future for me. You are asking me to be rational. That is something I know I cannot do. Believe me, I wish that I could just wish away my feelings, but I can''t.', 
    'https://www.starwarsnewsnet.com/category/films/the-mandalorian-grogu', 'https://www.youtube.com/watch?v=c9oeInUeLBY', 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity11' LIMIT 11), 'OurCity1', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Novinky z Novoťe', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua.',
       'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet. Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumy eirmod tempor invidunt ut labore et dolore magna aliquyam erat, sed diam voluptua. At vero eos et accusam et justo duo dolores et ea rebum. Stet clita kasd gubergren, no sea takimata sanctus est Lorem ipsum dolor sit amet.',
       'https://www.novot.sk', 'https://www.youtube.com/watch?v=vwbZ53zpF3s',
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity11' LIMIT 11), 'OurCity2', null,
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_news_file_item
insert into module_news_file_item (id, file_item_id, module_news_id, is_featured_image, order_nr, created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity5' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1), true, 1, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity6' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1), true, 1, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity7' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1), false, 1, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity8' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1), false, 2, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity9' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1), false, 3, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity10' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1), false, 4, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity11' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1), false, 5, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity12' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1), false, 6, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity13' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1), false, 7, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity14' LIMIT 1),
       (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1), false, 8, 'OurCity', null, 
       CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_news_tag
insert into module_news_tag (id, module_news_id, tag_id, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM tag WHERE name  = 'Obecný úrad' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_news WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM tag WHERE name  = 'Kultúra' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM tag WHERE name  = 'Šport' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM module_news WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM tag WHERE name  = 'Obecný úrad' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_event
insert into module_event (id, title, shor_text, context, url_link, video_link, date_from, date_to, organisation_module_service_id, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Niečo sa stalo', 'Lorem ipsum dolor sit amet, consetetur sadipscing elitr, sed diam nonumyeirmod tempor invidunt', 
       null, null, null, now()::DATE + 1, now()::DATE + 2, (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity12' LIMIT 11),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Treba mi niečo', 'BVeľmy dôležité!!' , null, null, null, now()::DATE + 2, now()::DATE + 2, 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity12' LIMIT 1),
       'OurCity2', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'TEST', 'TESTUJEME LEN tak kľud!' , null, null, null, now()::DATE + 3, now()::DATE + 4, 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity12' LIMIT 1), 
       'OurCity3', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'TEST 2', 'TEST 2!' , null, null, null, now()::DATE + 4, now()::DATE + 5, 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity12' LIMIT 1), 
       'OurCity4', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'TEST 3', 'TEST 3!' , null, null, null, now()::DATE + 5, now()::DATE + 6, 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity12' LIMIT 1), 
       'OurCity5', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'TEST 4', 'TEST 4!' , null, null, null, now()::DATE + 7, now()::DATE + 7,
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity12' LIMIT 1),
       'OurCity6', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;


--module_event_tag
insert into module_event_tag (id, tag_id, module_event_id, created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Šport' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity1' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Kultúra' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity1' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Zdravie' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity2' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Zvoz odpadu' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity2' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Fara' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity3' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Obecný úrad' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity3' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Kultúra' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity4' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Zdravie' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity4' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Zvoz odpadu' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity5' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Fara' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity5' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Obecný úrad' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity6' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM tag WHERE name  = 'Kultúra' LIMIT 1),
       (SELECT id FROM module_event WHERE created_by  = 'OurCity6' LIMIT 1), 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_municipality_radio
insert into module_municipal_radio (id, shor_text, organisation_module_service_id, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'RADIO Test text 1',   (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity13' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'RADIO Test text 2',   (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity13' LIMIT 1),
       'OurCity2', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'RADIO Test text 3',   (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity13' LIMIT 1),
       'OurCity3', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--message
insert into message (id, text_message, category, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Message 01 TEST', 'category1', 'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP 
UNION ALL
select gen_random_uuid(), 'Message 02 TEST', 'category1', 'OurCity2', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 03 TEST', 'category1', 'OurCity3', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 04 TEST', 'category1', 'OurCity4', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 05 TEST', 'category1', 'OurCity5', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 06 TEST', 'category1', 'OurCity6', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 07 TEST', 'category1', 'OurCity7', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 08 TEST', 'category1', 'OurCity8', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION
    ALL
select gen_random_uuid(), 'Message 09 TEST', 'category1', 'OurCity9', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_municipal_radio_message
insert into module_municipal_radio_message (id, message_id, module_municipal_radio_id, created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity1' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity1' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP 
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity2' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity1' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP 
UNION ALL 
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity3' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity1' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity4' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity2' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity5' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity2' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity6' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity3' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity7' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity3' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity8' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity3' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM message WHERE created_by  = 'OurCity9' LIMIT 1), (SELECT id FROM module_municipal_radio WHERE created_by  = 'OurCity3' LIMIT 1),
       'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_special_announcement
insert into module_special_announcement (id, text_message, severity, url_link, organisation_module_service_id, created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), 'Special announcement 01 TEST', 'INFO', 'https://www.novot.sk', 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity14' LIMIT 1), 'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Special announcement 02 TEST', 'WARNING', 'https://www.novot.sk', 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity14' LIMIT 1), 'OurCity2', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Special announcement 03 TEST', 'DANGER', 'https://www.novot.sk', 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity14' LIMIT 1), 'OurCity3', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_simple_page
insert into module_simple_page (id, name, title, context, icon, url_link, video_link, organisation_module_service_id, 
                                created_by, updated_by, created_at, updated_at) 
select gen_random_uuid(), 'Page NAME', 'Simple page 01 TEST', 'Simple page 01 TEST', 'icon', 'https://www.novot.sk', 
       'https://www.youtube.com/watch?v=c9oeInUeLBY', 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity15' LIMIT 1), 'OurCity1', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'PAGE name 2', 'Simple page 02 TEST', 'Simple page 02 TEST', 'icon', 'https://www.novot.sk', 
       'https://www.youtube.com/watch?v=c9oeInUeLBY', 
       (SELECT id FROM organisation_module_service WHERE created_by  = 'OurCity15' LIMIT 1), 'OurCity2', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module_simple_page_file_item
insert into module_simple_page_file_item (id, file_item_id, module_simple_page_id, is_featured_image, order_nr, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity1' LIMIT 1),
       (SELECT id FROM module_simple_page WHERE created_by  = 'OurCity1' LIMIT 1), true, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), (SELECT id FROM file_item WHERE created_by  = 'OurCity2' LIMIT 1),
       (SELECT id FROM module_simple_page WHERE created_by  = 'OurCity2' LIMIT 1), true, 1, 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;