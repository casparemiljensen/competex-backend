-- Adminer 4.8.4 PostgreSQL 16.1 (Debian 16.1-1.pgdg120+1) dump

\connect "competexdb";

DROP TABLE IF EXISTS "Club";
CREATE TABLE "public"."Club" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "AssociatedSport" uuid NOT NULL,
    CONSTRAINT "Club_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

COMMENT ON COLUMN "public"."Club"."AssociatedSport" IS 'Maybe a list of UUID in the future';


DROP TABLE IF EXISTS "ClubMembership";
CREATE TABLE "public"."ClubMembership" (
    "Id" uuid NOT NULL,
    "ClubId" uuid NOT NULL,
    "MemberId" uuid NOT NULL,
    "JoinDate" timestamptz NOT NULL,
    "Role" smallint,
    CONSTRAINT "ClubMembership_ClubId" UNIQUE ("ClubId")
) WITH (oids = false);


DROP TABLE IF EXISTS "Competition";
CREATE TABLE "public"."Competition" (
    "Id" uuid NOT NULL,
    "CompetitionType" uuid NOT NULL,
    "StartDate" timestamptz NOT NULL,
    "EndDate" timestamptz NOT NULL,
    "Level" smallint NOT NULL,
    "Status" smallint NOT NULL,
    "MinParticipants" integer NOT NULL,
    "MaxParticipants" integer NOT NULL,
    "RegistrationPrice" integer NOT NULL,
    CONSTRAINT "Competition_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Competition" ("Id", "CompetitionType", "StartDate", "EndDate", "Level", "Status", "MinParticipants", "MaxParticipants", "RegistrationPrice") VALUES
('a4bd46ad-aabe-4ec6-9b3f-55836b3f241e',	'2418a441-4fcf-4a7e-a256-ed97ad1403ce',	'2023-04-15 14:30:00+00',	'2023-04-16 14:30:00+00',	0,	0,	10,	20,	50);

DROP TABLE IF EXISTS "CompetitionType";
CREATE TABLE "public"."CompetitionType" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "CompetitionAttributes" uuid NOT NULL,
    "ScoreType" smallint NOT NULL,
    "ScoreMethod" smallint NOT NULL,
    CONSTRAINT "CompetitionType_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "CompetitionType" ("Id", "Name", "CompetitionAttributes", "ScoreType", "ScoreMethod") VALUES
('2418a441-4fcf-4a7e-a256-ed97ad1403ce',	'HandBall',	'e51c6848-e3b6-4a87-9bef-daa41be2d805',	0,	0);

DROP TABLE IF EXISTS "Ekvipage";
CREATE TABLE "public"."Ekvipage" (
    "Id" uuid NOT NULL,
    "Member" uuid NOT NULL,
    "Entity" uuid NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "Ekvipage_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Entity";
CREATE TABLE "public"."Entity" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Birthday" date NOT NULL,
    "Level" smallint NOT NULL,
    "Owner" uuid NOT NULL,
    "Type" smallint NOT NULL,
    CONSTRAINT "Entity_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Event";
CREATE TABLE "public"."Event" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "StartDate" timestamptz NOT NULL,
    "EndDate" timestamptz NOT NULL,
    "Location" uuid,
    "RegistrationStartDate" timestamptz NOT NULL,
    "RegistrationEndDate" timestamptz NOT NULL,
    "Status" smallint NOT NULL,
    "Organizer" uuid NOT NULL,
    "SportType" uuid NOT NULL,
    "Competitions" uuid NOT NULL,
    "EntryFee" integer NOT NULL,
    CONSTRAINT "Event_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Field";
CREATE TABLE "public"."Field" (
    "Id" uuid NOT NULL,
    "Location" text NOT NULL,
    "Capacity" integer NOT NULL,
    "SurfaceType" smallint DEFAULT '0' NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "Field_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Field" ("Id", "Location", "Capacity", "SurfaceType", "Name") VALUES
('9a16dcb4-8efc-4185-9171-dffe2b7f4027',	'Field 3',	30,	1,	'Field 3');

DROP TABLE IF EXISTS "Judge";
CREATE TABLE "public"."Judge" (
    "Member" uuid NOT NULL,
    "Description" text NOT NULL,
    "JudgeType" smallint NOT NULL,
    "Id" uuid NOT NULL,
    CONSTRAINT "Judge_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Judge" ("Member", "Description", "JudgeType", "Id") VALUES
('9052476b-b5a7-4aeb-b8cb-b579982452ef',	'He a judge',	1,	'ea592a7e-caf5-4708-863d-ab5800488190');

DROP TABLE IF EXISTS "Location";
CREATE TABLE "public"."Location" (
    "Id" uuid NOT NULL,
    "Address" text NOT NULL,
    "Zip" text NOT NULL,
    "City" text NOT NULL,
    "Country" text NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "Location_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Match";
CREATE TABLE "public"."Match" (
    "Id" uuid NOT NULL,
    "RoundId" uuid NOT NULL,
    "Participants" uuid NOT NULL,
    "Status" smallint DEFAULT '0' NOT NULL,
    "StartTime" timestamptz NOT NULL,
    "EndTime" timestamptz NOT NULL,
    "Field" uuid,
    "Judge" uuid,
    "Scores" uuid NOT NULL,
    CONSTRAINT "Match_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Match" ("Id", "RoundId", "Participants", "Status", "StartTime", "EndTime", "Field", "Judge", "Scores") VALUES
('4ddb07a9-36e4-411f-8ce4-27d352c25765',	'b5f764d8-85c4-4a83-b323-5ede8db6c1c9',	'0745a843-22c7-4405-91e9-2c8fc7d99794',	0,	'2023-04-15 14:30:00+00',	'2023-04-15 15:00:00+00',	'9a16dcb4-8efc-4185-9171-dffe2b7f4027',	'ea592a7e-caf5-4708-863d-ab5800488190',	'5abdd338-5e2e-401e-83f9-a1dec65d2ea9');

DROP TABLE IF EXISTS "Member";
CREATE TABLE "public"."Member" (
    "Id" uuid NOT NULL,
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "Birthday" date NOT NULL,
    "Email" text NOT NULL,
    "Phone" text NOT NULL,
    "Permissions" smallint NOT NULL,
    CONSTRAINT "Member_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Member" ("Id", "FirstName", "LastName", "Birthday", "Email", "Phone", "Permissions") VALUES
('58a01cc0-1a49-455b-998c-1500b3db0dca',	'Thomas',	'Nykjær',	'2000-07-19',	'thomas.nykjaer@live.dk',	'60602698',	2),
('873c08d4-a57e-42c7-95d1-62b16089185d',	'Illum',	'Thomas',	'1923-04-15',	'email@email.email',	'+4563975839',	2),
('9052476b-b5a7-4aeb-b8cb-b579982452ef',	'Pætur',	'Pablo',	'2002-01-01',	'pætur@email.email',	'1-800-hot-n-fun',	5);

DROP TABLE IF EXISTS "Penalty";
CREATE TABLE "public"."Penalty" (
    "Id" uuid NOT NULL,
    "PenaltyValue" text NOT NULL,
    "PenaltyType" smallint NOT NULL,
    CONSTRAINT "Penalty_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "PointScore";
CREATE TABLE "public"."PointScore" (
    "Id" uuid NOT NULL,
    "Match" uuid NOT NULL,
    "Participant" uuid NOT NULL,
    "ScoreValue" integer NOT NULL,
    "Penalties" uuid NOT NULL,
    "Points" integer NOT NULL,
    CONSTRAINT "PointScore_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Registration";
CREATE TABLE "public"."Registration" (
    "Id" uuid NOT NULL,
    "MemberId" uuid NOT NULL,
    "CometitionId" uuid NOT NULL,
    "RegistrationDate" timestamptz NOT NULL,
    "Status" smallint DEFAULT '0' NOT NULL,
    CONSTRAINT "Registration_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Round";
CREATE TABLE "public"."Round" (
    "Id" uuid NOT NULL,
    "SequenceNumber" integer NOT NULL,
    "RoundType" smallint NOT NULL,
    "CompetitionId" uuid NOT NULL,
    "Status" smallint NOT NULL,
    "StartTime" timestamptz NOT NULL,
    "EndTime" timestamptz NOT NULL,
    "Matches" uuid NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "Round_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Round" ("Id", "SequenceNumber", "RoundType", "CompetitionId", "Status", "StartTime", "EndTime", "Matches", "Name") VALUES
('b5f764d8-85c4-4a83-b323-5ede8db6c1c9',	0,	0,	'a4bd46ad-aabe-4ec6-9b3f-55836b3f241e',	0,	'2023-04-15 14:30:00+00',	'2023-04-17 14:30:00+00',	'308c8228-4808-4f63-a51c-53bfbba491d7',	'Handball Lund Cup 2023');

DROP TABLE IF EXISTS "ScoringSystem";
CREATE TABLE "public"."ScoringSystem" (
    "Id" uuid NOT NULL,
    "Description" integer NOT NULL,
    "ScoreType" integer NOT NULL,
    "ScoringRules" integer NOT NULL,
    "Penalties" integer NOT NULL,
    "EvaluationMethod" text NOT NULL,
    CONSTRAINT "ScoringSystem_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "SetScore";
CREATE TABLE "public"."SetScore" (
    "Id" uuid NOT NULL,
    "Match" uuid NOT NULL,
    "Participant" uuid NOT NULL,
    "ScoreValue" integer NOT NULL,
    "Penalties" uuid NOT NULL,
    "SetsWon" integer NOT NULL,
    CONSTRAINT "SetScore_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Single";
CREATE TABLE "public"."Single" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Member" uuid NOT NULL,
    CONSTRAINT "Single_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Single" ("Id", "Name", "Member") VALUES
('7451346a-6501-4137-b483-4c9c19f8a064',	'Mr. Nykjær',	'58a01cc0-1a49-455b-998c-1500b3db0dca');

DROP TABLE IF EXISTS "SportType";
CREATE TABLE "public"."SportType" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "EventAttributes" uuid NOT NULL,
    "EntityType" smallint NOT NULL,
    CONSTRAINT "SportType_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "Team";
CREATE TABLE "public"."Team" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Members" uuid NOT NULL,
    CONSTRAINT "Team_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "TimeScore";
CREATE TABLE "public"."TimeScore" (
    "Id" uuid NOT NULL,
    "Match" uuid NOT NULL,
    "Participant" uuid NOT NULL,
    "ScoreType" smallint NOT NULL,
    "ScoreValue" interval NOT NULL,
    "Penalties" uuid NOT NULL,
    "Time" interval NOT NULL,
    CONSTRAINT "TimeScore_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


DROP TABLE IF EXISTS "data_CompetitionType_CompetitionAttributes";
CREATE TABLE "public"."data_CompetitionType_CompetitionAttributes" (
    "CompetitionType" uuid NOT NULL,
    "CompetitionAttribute" text NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_CompetitionType_CompetitionAttributes_CompetitionType" ON "public"."data_CompetitionType_CompetitionAttributes" USING btree ("CompetitionType");


DROP TABLE IF EXISTS "data_SportType_EventAttributes";
CREATE TABLE "public"."data_SportType_EventAttributes" (
    "SportType" uuid NOT NULL,
    "EventAttribute" text NOT NULL
) WITH (oids = false);

CREATE INDEX "data_SportType_EventAttributes_SportType" ON "public"."data_SportType_EventAttributes" USING btree ("SportType");


DROP TABLE IF EXISTS "fk_Event_Competitions";
CREATE TABLE "public"."fk_Event_Competitions" (
    "Event" uuid NOT NULL,
    "Competition" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Event_Competitions_Event" ON "public"."fk_Event_Competitions" USING btree ("Event");


DROP TABLE IF EXISTS "fk_Match_Participants";
CREATE TABLE "public"."fk_Match_Participants" (
    "Match" uuid NOT NULL,
    "Participant" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Match_Participants_Match" ON "public"."fk_Match_Participants" USING btree ("Match");

INSERT INTO "fk_Match_Participants" ("Match", "Participant") VALUES
('550dbeb0-403c-41d9-ac24-77f2001fc670',	'873c08d4-a57e-42c7-95d1-62b16089185d'),
('0745a843-22c7-4405-91e9-2c8fc7d99794',	'58a01cc0-1a49-455b-998c-1500b3db0dca');

DROP TABLE IF EXISTS "fk_Match_PointScores";
CREATE TABLE "public"."fk_Match_PointScores" (
    "Match" uuid NOT NULL,
    "PointScore" uuid NOT NULL
) WITH (oids = false);


DROP TABLE IF EXISTS "fk_Match_SetScores";
CREATE TABLE "public"."fk_Match_SetScores" (
    "Match" uuid NOT NULL,
    "SetScore" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Match_Scores_Match" ON "public"."fk_Match_SetScores" USING btree ("Match");


DROP TABLE IF EXISTS "fk_Match_TimeScores";
CREATE TABLE "public"."fk_Match_TimeScores" (
    "Match" uuid NOT NULL,
    "TimeScore" uuid NOT NULL
) WITH (oids = false);


DROP TABLE IF EXISTS "fk_PointScore_Penalties";
CREATE TABLE "public"."fk_PointScore_Penalties" (
    "PointScore" uuid NOT NULL,
    "Penalty" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_PointScore_Penalties_PointScore" ON "public"."fk_PointScore_Penalties" USING btree ("PointScore");


DROP TABLE IF EXISTS "fk_Round_Matches";
CREATE TABLE "public"."fk_Round_Matches" (
    "Round" uuid NOT NULL,
    "Match" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Round_Matches_Round" ON "public"."fk_Round_Matches" USING btree ("Round");


DROP TABLE IF EXISTS "fk_Round_Participants";
CREATE TABLE "public"."fk_Round_Participants" (
    "Round" uuid NOT NULL,
    "Participant" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Round_Participants_Round" ON "public"."fk_Round_Participants" USING btree ("Round");


DROP TABLE IF EXISTS "fk_SetScore_Penalties";
CREATE TABLE "public"."fk_SetScore_Penalties" (
    "Score" uuid NOT NULL,
    "Penalty" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Score_Penalties_Score" ON "public"."fk_SetScore_Penalties" USING btree ("Score");


DROP TABLE IF EXISTS "fk_TimeScore_Penalties";
CREATE TABLE "public"."fk_TimeScore_Penalties" (
    "TimeScore" uuid NOT NULL,
    "Penalty" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_TimeScore_Penalties_TimeScore" ON "public"."fk_TimeScore_Penalties" USING btree ("TimeScore");


ALTER TABLE ONLY "public"."Club" ADD CONSTRAINT "Club_AssociatedSport_fkey" FOREIGN KEY ("AssociatedSport") REFERENCES "SportType"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."ClubMembership" ADD CONSTRAINT "ClubMembership_ClubId_fkey" FOREIGN KEY ("ClubId") REFERENCES "Club"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."ClubMembership" ADD CONSTRAINT "ClubMembership_ClubId_fkey1" FOREIGN KEY ("ClubId") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Competition" ADD CONSTRAINT "Competition_CompetitionType_fkey" FOREIGN KEY ("CompetitionType") REFERENCES "CompetitionType"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Ekvipage" ADD CONSTRAINT "Ekvipage_Entity_fkey" FOREIGN KEY ("Entity") REFERENCES "Entity"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Ekvipage" ADD CONSTRAINT "Ekvipage_Member_fkey" FOREIGN KEY ("Member") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Entity" ADD CONSTRAINT "Entity_Owner_fkey" FOREIGN KEY ("Owner") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Event" ADD CONSTRAINT "Event_Location_fkey" FOREIGN KEY ("Location") REFERENCES "Location"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Event" ADD CONSTRAINT "Event_Organizer_fkey" FOREIGN KEY ("Organizer") REFERENCES "Member"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Event" ADD CONSTRAINT "Event_SportType_fkey" FOREIGN KEY ("SportType") REFERENCES "SportType"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Judge" ADD CONSTRAINT "Judge_Member_fkey" FOREIGN KEY ("Member") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Match" ADD CONSTRAINT "Match_Field_fkey" FOREIGN KEY ("Field") REFERENCES "Field"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Match" ADD CONSTRAINT "Match_Judge_fkey" FOREIGN KEY ("Judge") REFERENCES "Judge"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Match" ADD CONSTRAINT "Match_RoundId_fkey" FOREIGN KEY ("RoundId") REFERENCES "Round"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."PointScore" ADD CONSTRAINT "PointScore_Match_fkey" FOREIGN KEY ("Match") REFERENCES "Match"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."PointScore" ADD CONSTRAINT "PointScore_Participant_fkey" FOREIGN KEY ("Participant") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Registration" ADD CONSTRAINT "Registration_CometitionId_fkey" FOREIGN KEY ("CometitionId") REFERENCES "Competition"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Registration" ADD CONSTRAINT "Registration_MemberId_fkey" FOREIGN KEY ("MemberId") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Round" ADD CONSTRAINT "Round_CompetitionId_fkey" FOREIGN KEY ("CompetitionId") REFERENCES "Competition"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."SetScore" ADD CONSTRAINT "SetScore_Match_fkey" FOREIGN KEY ("Match") REFERENCES "Match"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."SetScore" ADD CONSTRAINT "SetScore_Participant_fkey" FOREIGN KEY ("Participant") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Single" ADD CONSTRAINT "Single_Member_fkey" FOREIGN KEY ("Member") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."TimeScore" ADD CONSTRAINT "TimeScore_Match_fkey" FOREIGN KEY ("Match") REFERENCES "Match"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."fk_Event_Competitions" ADD CONSTRAINT "fk_Event_Competitions_Competition_fkey" FOREIGN KEY ("Competition") REFERENCES "Competition"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."fk_Match_Participants" ADD CONSTRAINT "fk_Match_Participants_Participant_fkey" FOREIGN KEY ("Participant") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."fk_Round_Matches" ADD CONSTRAINT "fk_Round_Matches_Match_fkey" FOREIGN KEY ("Match") REFERENCES "Match"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."fk_SetScore_Penalties" ADD CONSTRAINT "fk_Score_Penalties_Penalty_fkey" FOREIGN KEY ("Penalty") REFERENCES "Penalty"("Id") NOT DEFERRABLE;

-- 2024-11-26 13:46:08.839198+00
