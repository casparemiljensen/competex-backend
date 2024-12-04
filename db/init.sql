-- Adminer 4.8.4 PostgreSQL 17.1 (Debian 17.1-1.pgdg120+1) dump
CREATE DATABASE "competexdb";
\connect "competexdb";

CREATE TABLE "public"."AdminSportType" (
    "AdminId" uuid NOT NULL,
    "SportTypeId" uuid NOT NULL
) WITH (oids = false);


CREATE TABLE "public"."Club" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "AssociatedSport" text NOT NULL,
    CONSTRAINT "Club_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Club" ("Id", "Name", "AssociatedSport") VALUES
('0cc431c3-a954-4648-b75f-ddaf286343ae',	'KLG',	'Tennis');

CREATE TABLE "public"."ClubMembership" (
    "Id" uuid NOT NULL,
    "ClubId" uuid NOT NULL,
    "MemberId" uuid NOT NULL,
    "JoinDate" timestamp NOT NULL,
    "Role" smallint,
    CONSTRAINT "ClubMembership_ClubId" UNIQUE ("ClubId")
) WITH (oids = false);

INSERT INTO "ClubMembership" ("Id", "ClubId", "MemberId", "JoinDate", "Role") VALUES
('d1a15a87-8402-495f-8c2a-c34d653ce2dc',	'0cc431c3-a954-4648-b75f-ddaf286343ae',	'58a01cc0-1a49-455b-998c-1500b3db0dca',	'1999-01-08 04:05:06',	NULL);

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
    "EventId" uuid NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "Competition_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Competition" ("Id", "CompetitionType", "StartDate", "EndDate", "Level", "Status", "MinParticipants", "MaxParticipants", "RegistrationPrice", "EventId", "Name") VALUES
('a4bd46ad-aabe-4ec6-9b3f-55836b3f241e',	'2418a441-4fcf-4a7e-a256-ed97ad1403ce',	'2023-04-15 14:30:00+00',	'2023-04-16 14:30:00+00',	0,	0,	10,	20,	50,	'd776254f-0084-41b3-a17c-1a1ac350084e',	'2020 HAndball stuffs');

CREATE TABLE "public"."CompetitionType" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "ScoreType" smallint NOT NULL,
    "ScoreMethod" smallint NOT NULL,
    CONSTRAINT "CompetitionType_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "CompetitionType" ("Id", "Name", "ScoreType", "ScoreMethod") VALUES
('2418a441-4fcf-4a7e-a256-ed97ad1403ce',	'HandBall',	0,	0);

CREATE TABLE "public"."Entity" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Birthday" date NOT NULL,
    "Level" smallint NOT NULL,
    "Owner" uuid NOT NULL,
    "Type" smallint NOT NULL,
    CONSTRAINT "Entity_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."Event" (
    "Id" uuid NOT NULL,
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "StartDate" timestamp NOT NULL,
    "EndDate" timestamp NOT NULL,
    "Location" uuid,
    "RegistrationStartDate" timestamp NOT NULL,
    "RegistrationEndDate" timestamp NOT NULL,
    "Status" smallint NOT NULL,
    "Organizer" uuid NOT NULL,
    "SportType" uuid NOT NULL,
    "EntryFee" integer NOT NULL,
    CONSTRAINT "Event_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Event" ("Id", "Title", "Description", "StartDate", "EndDate", "Location", "RegistrationStartDate", "RegistrationEndDate", "Status", "Organizer", "SportType", "EntryFee") VALUES
('d776254f-0084-41b3-a17c-1a1ac350084e',	'TestEvent',	'This is a test event description',	'2024-11-27 10:35:14.506871',	'2024-11-27 10:35:14.506871',	NULL,	'2024-11-27 10:35:14.506871',	'2024-11-27 10:35:14.506871',	0,	'290a2cfb-1af3-4b41-bf45-94935ecad69d',	'72aa9288-be28-4e23-9f49-82f37f3948f4',	23);

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

CREATE TABLE "public"."Judge" (
    "MemberId" uuid NOT NULL,
    "Description" text NOT NULL,
    "JudgeType" smallint NOT NULL,
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
    CONSTRAINT "Judge_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Judge" ("MemberId", "Description", "JudgeType", "Id") VALUES
('58a01cc0-1a49-455b-998c-1500b3db0dca',	'asd',	1,	'aa76039e-5f08-4bec-9eb3-2ced90c39575');

CREATE TABLE "public"."Location" (
    "Id" uuid NOT NULL,
    "Address" text NOT NULL,
    "Zip" text NOT NULL,
    "City" text NOT NULL,
    "Country" text NOT NULL,
    "Name" text NOT NULL,
    CONSTRAINT "Location_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."Match" (
    "Id" uuid NOT NULL,
    "RoundId" uuid NOT NULL,
    "Status" smallint DEFAULT '0' NOT NULL,
    "StartTime" timestamptz NOT NULL,
    "EndTime" timestamptz NOT NULL,
    "Field" uuid,
    "Judge" uuid,
    CONSTRAINT "Match_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Match" ("Id", "RoundId", "Status", "StartTime", "EndTime", "Field", "Judge") VALUES
('eab24cde-fa47-4ccc-8a0d-7c0d40bd8067',	'b5f764d8-85c4-4a83-b323-5ede8db6c1c9',	1,	'1999-01-08 04:05:06+00',	'1999-01-08 04:05:07+00',	NULL,	NULL);

CREATE TABLE "public"."MatchParticipants" (
    "MatchId" uuid NOT NULL,
    "ParticipantId" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Match_Participants_Match" ON "public"."MatchParticipants" USING btree ("MatchId");


CREATE TABLE "public"."MatchScores" (
    "MatchId" uuid NOT NULL,
    "ScoreId" uuid NOT NULL
) WITH (oids = false);


CREATE TABLE "public"."Member" (
    "Id" uuid DEFAULT gen_random_uuid() NOT NULL,
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
('290a2cfb-1af3-4b41-bf45-94935ecad69d',	'Lucas',	'Dexter',	'2024-11-27',	'lucas@gmail.om',	'1',	3),
('4ad019ae-db24-4b6d-a5d0-720018b41ce5',	'string',	'string',	'2024-11-29',	'string',	'string',	3),
('30b45915-29c4-4f76-9908-3aa4888793c0',	'string',	'string',	'2024-11-29',	'string',	'string',	3),
('8b1f62e0-e89b-4891-9f96-33c8d9640d29',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('8e8f69fc-ba17-4aed-bda8-a877d51a7d43',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('dbcbbace-92a9-423f-82bf-5d4de11bd01a',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('67ed9d61-86e2-41da-9879-eadff2f2de35',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('73f8c41c-84fe-420a-8412-c14c4cdc1b0b',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('6bd3fdfe-575d-4279-9151-2c0c570aea43',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('277531d9-ade8-4611-a59d-f4e0985301b4',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('fb4b7290-fd59-4f0a-bbf3-8cca407711dd',	'string',	'string',	'2024-11-29',	'string',	'string',	1),
('02f10df6-34ae-4adf-b64d-029e0929e53b',	'string',	'string',	'2024-11-29',	'asd',	'string',	3),
('d8780171-f4c0-4964-87ee-47e51fa9339c',	'Pætur',	'help',	'2024-11-29',	'string',	'stdgdgdgdgring',	0);

CREATE TABLE "public"."Participant" (
    "Id" uuid NOT NULL,
    "Entity" uuid,
    "Name" text NOT NULL,
    CONSTRAINT "Ekvipage_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."ParticipantMembers" (
    "ParticipantId" uuid NOT NULL,
    "MemberId" uuid NOT NULL
) WITH (oids = false);


CREATE TABLE "public"."Penalty" (
    "Id" uuid NOT NULL,
    "PenaltyValue" text NOT NULL,
    "PenaltyType" smallint NOT NULL,
    CONSTRAINT "Penalty_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."Registration" (
    "Id" uuid NOT NULL,
    "MemberId" uuid NOT NULL,
    "CometitionId" uuid NOT NULL,
    "RegistrationDate" timestamptz NOT NULL,
    "Status" smallint DEFAULT '0' NOT NULL,
    CONSTRAINT "Registration_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


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

CREATE TABLE "public"."RoundMatches" (
    "RoundId" uuid NOT NULL,
    "MatchId" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Round_Matches_Round" ON "public"."RoundMatches" USING btree ("RoundId");


CREATE TABLE "public"."RoundParticipants" (
    "RoundId" uuid NOT NULL,
    "ParticipantId" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_Round_Participants_Round" ON "public"."RoundParticipants" USING btree ("RoundId");


CREATE TABLE "public"."ScorePenalties" (
    "ScoreId" uuid NOT NULL,
    "PenaltyId" uuid NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_PointScore_Penalties_PointScore" ON "public"."ScorePenalties" USING btree ("ScoreId");


CREATE TABLE "public"."ScoringSystem" (
    "Id" uuid NOT NULL,
    "Description" integer NOT NULL,
    "ScoreType" integer NOT NULL,
    "ScoringRules" integer NOT NULL,
    "Penalties" integer NOT NULL,
    "EvaluationMethod" text NOT NULL,
    CONSTRAINT "ScoringSystem_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."ScoringSystemPenalties" (
    "ScoringSystemId" uuid NOT NULL,
    "PanaltyId" uuid NOT NULL
) WITH (oids = false);


CREATE TABLE "public"."SetScore" (
    "Id" uuid NOT NULL,
    "Match" uuid NOT NULL,
    "Participant" uuid NOT NULL,
    "SetsWon" integer,
    "ScoreType" smallint NOT NULL,
    "Time" interval,
    "Points" integer,
    CONSTRAINT "SetScore_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."Single" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Member" uuid NOT NULL,
    CONSTRAINT "Single_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "Single" ("Id", "Name", "Member") VALUES
('7451346a-6501-4137-b483-4c9c19f8a064',	'Mr. Nykjær',	'58a01cc0-1a49-455b-998c-1500b3db0dca');

CREATE TABLE "public"."SportType" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "EventAttributes" uuid NOT NULL,
    "EntityType" smallint NOT NULL,
    CONSTRAINT "SportType_Id" PRIMARY KEY ("Id")
) WITH (oids = false);

INSERT INTO "SportType" ("Id", "Name", "EventAttributes", "EntityType") VALUES
('72aa9288-be28-4e23-9f49-82f37f3948f4',	'Handball',	'8028989c-072d-40ab-b8b6-335dc39c7ed1',	2);

CREATE TABLE "public"."Team" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "Members" uuid NOT NULL,
    CONSTRAINT "Team_Id" PRIMARY KEY ("Id")
) WITH (oids = false);


CREATE TABLE "public"."data_CompetitionType_CompetitionAttributes" (
    "CompetitionTypeId" uuid NOT NULL,
    "CompetitionAttribute" text NOT NULL
) WITH (oids = false);

CREATE INDEX "fk_CompetitionType_CompetitionAttributes_CompetitionType" ON "public"."data_CompetitionType_CompetitionAttributes" USING btree ("CompetitionTypeId");


CREATE TABLE "public"."data_Event_EventAttributes" (
    "EventId" uuid NOT NULL,
    "EventAttribute" text NOT NULL
) WITH (oids = false);

INSERT INTO "data_Event_EventAttributes" ("EventId", "EventAttribute") VALUES
('d776254f-0084-41b3-a17c-1a1ac350084e',	'This is an attribute');

CREATE TABLE "public"."data_SportType_EventAttributes" (
    "SportTypeId" uuid NOT NULL,
    "EventAttribute" text NOT NULL
) WITH (oids = false);

CREATE INDEX "data_SportType_EventAttributes_SportType" ON "public"."data_SportType_EventAttributes" USING btree ("SportTypeId");


ALTER TABLE ONLY "public"."ClubMembership" ADD CONSTRAINT "ClubMembership_ClubId_fkey" FOREIGN KEY ("ClubId") REFERENCES "Club"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."ClubMembership" ADD CONSTRAINT "ClubMembership_MemberId_fkey" FOREIGN KEY ("MemberId") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Competition" ADD CONSTRAINT "Competition_CompetitionType_fkey" FOREIGN KEY ("CompetitionType") REFERENCES "CompetitionType"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Competition" ADD CONSTRAINT "Competition_EventId_fkey" FOREIGN KEY ("EventId") REFERENCES "Event"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Entity" ADD CONSTRAINT "Entity_Owner_fkey" FOREIGN KEY ("Owner") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Event" ADD CONSTRAINT "Event_Location_fkey" FOREIGN KEY ("Location") REFERENCES "Location"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Event" ADD CONSTRAINT "Event_Organizer_fkey" FOREIGN KEY ("Organizer") REFERENCES "Member"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Event" ADD CONSTRAINT "Event_SportType_fkey" FOREIGN KEY ("SportType") REFERENCES "SportType"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Judge" ADD CONSTRAINT "Judge_Member_fkey" FOREIGN KEY ("MemberId") REFERENCES "Member"("Id") ON UPDATE CASCADE ON DELETE CASCADE NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Match" ADD CONSTRAINT "Match_Field_fkey" FOREIGN KEY ("Field") REFERENCES "Field"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Match" ADD CONSTRAINT "Match_Judge_fkey" FOREIGN KEY ("Judge") REFERENCES "Judge"("Id") ON DELETE CASCADE NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Match" ADD CONSTRAINT "Match_RoundId_fkey" FOREIGN KEY ("RoundId") REFERENCES "Round"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."MatchParticipants" ADD CONSTRAINT "MatchParticipants_MatchId_fkey" FOREIGN KEY ("MatchId") REFERENCES "Match"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."MatchParticipants" ADD CONSTRAINT "MatchParticipants_ParticipantId_fkey" FOREIGN KEY ("ParticipantId") REFERENCES "Participant"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Participant" ADD CONSTRAINT "Ekvipage_Entity_fkey" FOREIGN KEY ("Entity") REFERENCES "Entity"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."ParticipantMembers" ADD CONSTRAINT "ParticipantMembers_MemberId_fkey" FOREIGN KEY ("MemberId") REFERENCES "Member"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."ParticipantMembers" ADD CONSTRAINT "ParticipantMembers_ParticipantId_fkey" FOREIGN KEY ("ParticipantId") REFERENCES "Participant"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Registration" ADD CONSTRAINT "Registration_CometitionId_fkey" FOREIGN KEY ("CometitionId") REFERENCES "Competition"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."Registration" ADD CONSTRAINT "Registration_MemberId_fkey" FOREIGN KEY ("MemberId") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Round" ADD CONSTRAINT "Round_CompetitionId_fkey" FOREIGN KEY ("CompetitionId") REFERENCES "Competition"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."RoundMatches" ADD CONSTRAINT "RoundMatches_RoundId_fkey" FOREIGN KEY ("RoundId") REFERENCES "Round"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."RoundMatches" ADD CONSTRAINT "fk_Round_Matches_Match_fkey" FOREIGN KEY ("MatchId") REFERENCES "Match"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."ScoringSystemPenalties" ADD CONSTRAINT "ScoringSystemPenalties_PanaltyId_fkey" FOREIGN KEY ("PanaltyId") REFERENCES "Penalty"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."ScoringSystemPenalties" ADD CONSTRAINT "ScoringSystemPenalties_ScoringSystemId_fkey" FOREIGN KEY ("ScoringSystemId") REFERENCES "ScoringSystem"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."SetScore" ADD CONSTRAINT "SetScore_Match_fkey" FOREIGN KEY ("Match") REFERENCES "Match"("Id") NOT DEFERRABLE;
ALTER TABLE ONLY "public"."SetScore" ADD CONSTRAINT "SetScore_Participant_fkey" FOREIGN KEY ("Participant") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."Single" ADD CONSTRAINT "Single_Member_fkey" FOREIGN KEY ("Member") REFERENCES "Member"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."data_CompetitionType_CompetitionAttributes" ADD CONSTRAINT "data_CompetitionType_CompetitionAttribut_CompetitionTypeId_fkey" FOREIGN KEY ("CompetitionTypeId") REFERENCES "CompetitionType"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."data_Event_EventAttributes" ADD CONSTRAINT "data_Event_EventAttributes_EventId_fkey" FOREIGN KEY ("EventId") REFERENCES "Event"("Id") NOT DEFERRABLE;

ALTER TABLE ONLY "public"."data_SportType_EventAttributes" ADD CONSTRAINT "data_SportType_EventAttributes_SportType_fkey" FOREIGN KEY ("SportTypeId") REFERENCES "SportType"("Id") NOT DEFERRABLE;

-- 2024-12-04 18:26:18.978409+00
