-- --------------------------------------------------------------------------------------------------
-- our_city development base seeder. 
-- 
-- Lookup Tables
--
-- this script will only work when the dev users. use api endpoint to execute this, 
-- as it seeds the users there
-- 
-- --------------------------------------------------------------------------------------------------

--file_item_type
insert into file_item_type (id, name, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'UserImage', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'UserFile', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'UserVideo', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'OrganisationImage', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'OrganisationFile', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'OrganisationVideo', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'ApplicationImage', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'ApplicationFile', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'ApplicationVideo', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Other', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--permission
insert into permission (id, name, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Permissions.Organisation.Create', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP 
UNION ALL
select gen_random_uuid(), 'Permissions.Organisation.View', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP 
UNION ALL
select gen_random_uuid(), 'Permissions.Organisation.Edit', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP 
UNION ALL
select gen_random_uuid(), 'Permissions.Organisation.Delete', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--module-service
insert into module_service (id, name, icon, description, module_type, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Aktuality', 'home', 'Aktuality modul', 'aktuality', 'OurCity', null, CURRENT_TIMESTAMP,CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Udalosti', 'calendar', 'Udalosť modul', 'calendar', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Obecný rozhlas', 'notification', 'Obecný rozhlas modul', 'obecny-rozhlas', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Mimoriadne oznamy', 'book', 'Mimoriadne oznamy modul', 'notifications', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP
UNION ALL
select gen_random_uuid(), 'Jednoduché stránky', 'info', 'Jednoduché stránky modul', 'simple-page', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--tag
insert into tag (id, name, color, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Šport', '#f50', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Kultúra', '#2db7f5', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Zdravie', '#87d068', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Fara', '#108ee9', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Škola', '#7265e6', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Obecný úrad', '#f50505', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Zvoz odpadu', '#3B1F2B', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;
    

--country
insert into country (id, name, abbreviation, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Slovensko', 'SK', 'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--state
insert into state (id, name, abbreviation, country_id, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Banskobystrický kraj', 'SK-BC', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bratislavský kraj', 'SK-BL', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Košický kraj', 'SK-KI', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Nitriansky kraj', 'SK-NI', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Prešovský kraj', 'SK-PV', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Trenčiansky kraj', 'SK-TC', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Trnavský kraj', 'SK-TA', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Žilinský kraj', 'SK-ZI', (SELECT id FROM country WHERE name = 'Slovensko' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;

--district
insert into district (id, name, abbreviation, state_id, created_by, updated_by, created_at, updated_at)
select gen_random_uuid(), 'Bánovce nad Bebravou', 'BN', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Banská Bystrica', 'BB', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Banská Štiavnica', 'BS', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bardejov', 'BJ', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bratislava I', 'BA, BL', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bratislava II', 'BA, BL', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bratislava III', 'BA, BL', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bratislava IV', 'BA, BL', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bratislava V', 'BA, BL', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Brezno', 'BR', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Bytča', 'BY', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Čadca', 'CA', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Detva', 'DT', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Dolný Kubín', 'DK', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Dunajská Streda', 'DS', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Galanta', 'GA', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Gelnica', 'GL', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Hlohovec', 'HC', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Humenné', 'HE', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Ilava', 'IL', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Kežmarok', 'KK', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Komárno', 'KN', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Košice I', 'KE', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Košice II', 'KE', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Košice III', 'KE', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Košice IV', 'KE', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Košice-okolie', 'KS', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Krupina', 'KA', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Kysucké Nové Mesto', 'KM', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Levice', 'LV', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Levoča', 'LE', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Liptovský Mikuláš', 'LM', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Lučenec', 'LC', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Malacky', 'MA', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Martin', 'MT', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Medzilaborce', 'ML', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Michalovce', 'MI', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Myjava', 'MY', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Námestovo', 'NO', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Nitra', 'NR', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Nové Mesto nad Váhom', 'NM', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Nové Zámky', 'NZ', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Partizánske', 'PE', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Pezinok', 'PK', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Piešťany', 'PN', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Poltár', 'PT', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Poprad', 'PP', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Považská Bystrica', 'PB', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Prešov', 'PO', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Prievidza', 'PD', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Púchov', 'PU', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Revúca', 'RA', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Rimavská Sobota', 'RS', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Rožňava', 'RV', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Ružomberok', 'RK', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Sabinov', 'SB', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Senec', 'SC', (SELECT id FROM state WHERE name = 'Bratislavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Senica', 'SE', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Skalica', 'SI', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Snina', 'SV', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Sobrance', 'SO', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Spišská Nová Ves', 'SN', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Stará Ľubovňa', 'SL', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Stropkov', 'SP', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Svidník', 'SK', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Šaľa', 'SA', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Topoľčany', 'TO', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Trebišov', 'TV', (SELECT id FROM state WHERE name = 'Košický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Trenčín', 'TN', (SELECT id FROM state WHERE name = 'Trenčiansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Trnava', 'TT', (SELECT id FROM state WHERE name = 'Trnavský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Turčianske Teplice', 'TR', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Tvrdošín', 'TS', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Veľký Krtíš', 'VK', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Vranov nad Topľou', 'VT', (SELECT id FROM state WHERE name = 'Prešovský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Zlaté Moravce', 'ZM', (SELECT id FROM state WHERE name = 'Nitriansky kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Zvolen', 'ZV', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Žarnovica', 'ZC', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Žiar nad Hronom', 'ZH', (SELECT id FROM state WHERE name = 'Banskobystrický kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP UNION ALL
select gen_random_uuid(), 'Žilina', 'ZA', (SELECT id FROM state WHERE name = 'Žilinský kraj' LIMIT 1), 
'OurCity', null, CURRENT_TIMESTAMP, CURRENT_TIMESTAMP;
