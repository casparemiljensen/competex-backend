CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;
CREATE TABLE clubs (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "AssociatedSport" text NOT NULL,
    CONSTRAINT "PK_clubs" PRIMARY KEY ("Id")
);

CREATE TABLE competition_types (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "ScoreType" integer NOT NULL,
    "ScoreMethod" integer NOT NULL,
    CONSTRAINT "PK_competition_types" PRIMARY KEY ("Id")
);

CREATE TABLE fields (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "Location" text NOT NULL,
    "Capacity" integer NOT NULL,
    "SurfaceType" integer,
    CONSTRAINT "PK_fields" PRIMARY KEY ("Id")
);

CREATE TABLE locations (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "Address" text NOT NULL,
    "Zip" text NOT NULL,
    "City" text NOT NULL,
    "Country" text NOT NULL,
    CONSTRAINT "PK_locations" PRIMARY KEY ("Id")
);

CREATE TABLE members (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "FirstName" text NOT NULL,
    "LastName" text NOT NULL,
    "Birthday" timestamp with time zone NOT NULL,
    "Email" text NOT NULL,
    "Phone" text NOT NULL,
    "Permissions" smallint NOT NULL,
    CONSTRAINT "PK_members" PRIMARY KEY ("Id")
);

CREATE TABLE sport_types (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "EntityType" integer NOT NULL,
    CONSTRAINT "PK_sport_types" PRIMARY KEY ("Id")
);

CREATE TABLE club_memberships (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "ClubId" uuid NOT NULL,
    "MemberId" uuid NOT NULL,
    "JoinDate" timestamp with time zone NOT NULL,
    "Role" integer,
    CONSTRAINT "PK_club_memberships" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_club_memberships_clubs_ClubId" FOREIGN KEY ("ClubId") REFERENCES clubs ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_club_memberships_members_MemberId" FOREIGN KEY ("MemberId") REFERENCES members ("Id") ON DELETE CASCADE
);

CREATE TABLE entities (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Type" integer NOT NULL,
    "Name" text NOT NULL,
    "Birthdate" timestamp with time zone NOT NULL,
    "Level" integer NOT NULL,
    "OwnerId" uuid NOT NULL,
    CONSTRAINT "PK_entities" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_entities_members_OwnerId" FOREIGN KEY ("OwnerId") REFERENCES members ("Id") ON DELETE CASCADE
);

CREATE TABLE judges (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "JudgeType" integer NOT NULL,
    "MemberId" uuid NOT NULL,
    "Description" text NOT NULL,
    CONSTRAINT "PK_judges" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_judges_members_MemberId" FOREIGN KEY ("MemberId") REFERENCES members ("Id") ON DELETE CASCADE
);

CREATE TABLE events (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Title" text NOT NULL,
    "Description" text NOT NULL,
    "StartDate" timestamp with time zone NOT NULL,
    "EndDate" timestamp with time zone NOT NULL,
    "LocationId" uuid,
    "RegistrationStartDate" timestamp with time zone NOT NULL,
    "RegistrationEndDate" timestamp with time zone NOT NULL,
    "Status" integer NOT NULL,
    "OrganizerId" uuid NOT NULL,
    "SportTypeId" uuid NOT NULL,
    "EntryFee" integer NOT NULL,
    CONSTRAINT "PK_events" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_events_clubs_OrganizerId" FOREIGN KEY ("OrganizerId") REFERENCES clubs ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_events_locations_LocationId" FOREIGN KEY ("LocationId") REFERENCES locations ("Id") ON DELETE SET NULL,
    CONSTRAINT "FK_events_sport_types_SportTypeId" FOREIGN KEY ("SportTypeId") REFERENCES sport_types ("Id") ON DELETE SET NULL
);

CREATE TABLE participants (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "MemberId" uuid,
    "EntityId" uuid,
    CONSTRAINT "PK_participants" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_participants_entities_EntityId" FOREIGN KEY ("EntityId") REFERENCES entities ("Id"),
    CONSTRAINT "FK_participants_members_MemberId" FOREIGN KEY ("MemberId") REFERENCES members ("Id")
);

CREATE TABLE competitions (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "CompetitionTypeId" uuid NOT NULL,
    "EventId" uuid NOT NULL,
    "Name" text NOT NULL,
    "StartDate" timestamp with time zone NOT NULL,
    "EndDate" timestamp with time zone NOT NULL,
    "Level" integer NOT NULL,
    "Status" integer NOT NULL,
    "MinParticipants" integer NOT NULL,
    "MaxParticipants" integer NOT NULL,
    "RegistrationPrice" integer NOT NULL,
    CONSTRAINT "PK_competitions" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_competitions_competition_types_CompetitionTypeId" FOREIGN KEY ("CompetitionTypeId") REFERENCES competition_types ("Id") ON DELETE SET NULL,
    CONSTRAINT "FK_competitions_events_EventId" FOREIGN KEY ("EventId") REFERENCES events ("Id") ON DELETE CASCADE
);

CREATE TABLE event_competitions (
    "CompetitionId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "EventId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    CONSTRAINT "PK_event_competitions" PRIMARY KEY ("CompetitionId", "EventId"),
    CONSTRAINT "FK_event_competitions_competitions_CompetitionId" FOREIGN KEY ("CompetitionId") REFERENCES competitions ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_event_competitions_events_EventId" FOREIGN KEY ("EventId") REFERENCES events ("Id") ON DELETE CASCADE
);

CREATE TABLE registrations (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "ParticipantId" uuid NOT NULL,
    "CompetitionId" uuid NOT NULL,
    "RegistrationDate" timestamp with time zone NOT NULL,
    "Status" integer NOT NULL,
    CONSTRAINT "PK_registrations" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_registrations_competitions_CompetitionId" FOREIGN KEY ("CompetitionId") REFERENCES competitions ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_registrations_participants_ParticipantId" FOREIGN KEY ("ParticipantId") REFERENCES participants ("Id") ON DELETE CASCADE
);

CREATE TABLE rounds (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Name" text NOT NULL,
    "SequenceNumber" bigint NOT NULL,
    "RoundType" integer NOT NULL,
    "CompetitionId" uuid NOT NULL,
    "Status" integer NOT NULL,
    "StartTime" timestamp with time zone NOT NULL,
    "EndTime" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_rounds" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_rounds_competitions_CompetitionId" FOREIGN KEY ("CompetitionId") REFERENCES competitions ("Id") ON DELETE RESTRICT
);

CREATE TABLE score_results (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "CompetitionId" uuid NOT NULL,
    "ParticipantId" uuid NOT NULL,
    "Faults" integer NOT NULL,
    "Time" interval NOT NULL,
    CONSTRAINT "PK_score_results" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_score_results_competitions_CompetitionId" FOREIGN KEY ("CompetitionId") REFERENCES competitions ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_score_results_participants_ParticipantId" FOREIGN KEY ("ParticipantId") REFERENCES participants ("Id") ON DELETE CASCADE
);

CREATE TABLE matches (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "RoundId" uuid NOT NULL,
    "Status" integer NOT NULL,
    "StartTime" timestamp with time zone NOT NULL,
    "EndTime" timestamp with time zone NOT NULL,
    "FieldId" uuid,
    "JudgeId" uuid,
    CONSTRAINT "PK_matches" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_matches_fields_FieldId" FOREIGN KEY ("FieldId") REFERENCES fields ("Id"),
    CONSTRAINT "FK_matches_judges_JudgeId" FOREIGN KEY ("JudgeId") REFERENCES judges ("Id"),
    CONSTRAINT "FK_matches_rounds_RoundId" FOREIGN KEY ("RoundId") REFERENCES rounds ("Id") ON DELETE CASCADE
);

CREATE TABLE match_participants (
    "MatchId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "ParticipantId" uuid NOT NULL DEFAULT (gen_random_uuid()),
    CONSTRAINT "PK_match_participants" PRIMARY KEY ("MatchId", "ParticipantId"),
    CONSTRAINT "FK_match_participants_matches_MatchId" FOREIGN KEY ("MatchId") REFERENCES matches ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_match_participants_participants_ParticipantId" FOREIGN KEY ("ParticipantId") REFERENCES participants ("Id") ON DELETE CASCADE
);

CREATE TABLE scores (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "MatchId" uuid NOT NULL,
    "ParticipantId" uuid NOT NULL,
    "ScoreType" smallint NOT NULL,
    "Points" integer,
    "Faults" integer,
    "Time" interval,
    CONSTRAINT "PK_scores" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_scores_matches_MatchId" FOREIGN KEY ("MatchId") REFERENCES matches ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_scores_participants_ParticipantId" FOREIGN KEY ("ParticipantId") REFERENCES participants ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_club_memberships_ClubId" ON club_memberships ("ClubId");

CREATE INDEX "IX_club_memberships_MemberId" ON club_memberships ("MemberId");

CREATE INDEX "IX_competitions_CompetitionTypeId" ON competitions ("CompetitionTypeId");

CREATE INDEX "IX_competitions_EventId" ON competitions ("EventId");

CREATE INDEX "IX_entities_OwnerId" ON entities ("OwnerId");

CREATE INDEX "IX_event_competitions_EventId" ON event_competitions ("EventId");

CREATE INDEX "IX_events_LocationId" ON events ("LocationId");

CREATE INDEX "IX_events_OrganizerId" ON events ("OrganizerId");

CREATE INDEX "IX_events_SportTypeId" ON events ("SportTypeId");

CREATE INDEX "IX_judges_MemberId" ON judges ("MemberId");

CREATE INDEX "IX_match_participants_ParticipantId" ON match_participants ("ParticipantId");

CREATE INDEX "IX_matches_FieldId" ON matches ("FieldId");

CREATE INDEX "IX_matches_JudgeId" ON matches ("JudgeId");

CREATE INDEX "IX_matches_RoundId" ON matches ("RoundId");

CREATE INDEX "IX_participants_EntityId" ON participants ("EntityId");

CREATE INDEX "IX_participants_MemberId" ON participants ("MemberId");

CREATE INDEX "IX_registrations_CompetitionId" ON registrations ("CompetitionId");

CREATE INDEX "IX_registrations_ParticipantId" ON registrations ("ParticipantId");

CREATE INDEX "IX_rounds_CompetitionId" ON rounds ("CompetitionId");

CREATE INDEX "IX_score_results_CompetitionId" ON score_results ("CompetitionId");

CREATE INDEX "IX_score_results_ParticipantId" ON score_results ("ParticipantId");

CREATE INDEX "IX_scores_MatchId" ON scores ("MatchId");

CREATE INDEX "IX_scores_ParticipantId" ON scores ("ParticipantId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241219230216_InitialCreate', '9.0.0');

COMMIT;

